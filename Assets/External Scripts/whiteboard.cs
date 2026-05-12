using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
[RequireComponent(typeof(XRSimpleInteractable))]


[RequireComponent(typeof(Renderer))]
public class whiteboard : MonoBehaviour
{
    public int textureWidth;
    public int textureHeight;
     public LayerMask mask;
    public InputActionReference rTrigger;
    public InputActionReference lTrigger;
    private Texture2D drawTexture;
    private NearFarInteractor ray = null;

    private Vector2 smoothUV;

   

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BoardManager.Instance.RegisterDrawableSurface(this);
        drawTexture = new Texture2D(textureWidth, textureHeight);
        Color[] pixels = new Color[textureWidth * textureHeight];
        for (int i = 0; i < pixels.Length; i++){
            pixels[i] = BoardManager.Instance.bgColor;
        }
        drawTexture.SetPixels(pixels);
        drawTexture.Apply();
        GetComponent<Renderer>().material.mainTexture = drawTexture;
        GetComponent<XRSimpleInteractable>().hoverEntered.AddListener(OnHoverEntered);
        GetComponent<XRSimpleInteractable>().hoverExited.AddListener(OnHoverExited);
    }

    public void draw(Vector2 uv)
    {
        int xcoord = (int)(uv.x * textureWidth);
        int ycoord = (int)(uv.y * textureHeight);
        int brushRadius = BoardManager.Instance.brushRadius;
        Color brushColor = BoardManager.Instance.brushColor;
        if(BoardManager.Instance.eraserMode == true)
        {
            brushColor = BoardManager.Instance.bgColor;
        }
        for (int x = -brushRadius; x <= brushRadius; x++)
        {
            for (int y = -brushRadius; y <= brushRadius; y++)
            {
                if (x * x + y * y <= brushRadius * brushRadius) 
                {
                    int px = Mathf.Clamp(xcoord + x, 0, textureWidth - 1);
                    int py = Mathf.Clamp(ycoord + y, 0, textureHeight - 1);
                    drawTexture.SetPixel(px, py, brushColor);
                    
                }
            }
        }
        drawTexture.Apply();
    }

    void OnHoverEntered(HoverEnterEventArgs args)
{
    ray = args.interactorObject as NearFarInteractor;
}

    void OnHoverExited(HoverExitEventArgs args)
{
    ray = null;

}

    // Update is called once per frame
void Update()
{
    if(ray != null && Physics.Raycast(ray.attachTransform.position, ray.attachTransform.forward, out RaycastHit hit, Mathf.Infinity, mask) == true)
        {
            if (rTrigger.action.WasPressedThisFrame() || lTrigger.action.WasPressedThisFrame())
            {
                smoothUV = hit.textureCoord;
            }

            if (rTrigger.action.IsPressed() || lTrigger.action.IsPressed())
            {
                smoothUV = Vector2.Lerp(smoothUV, hit.textureCoord, BoardManager.Instance.smoothingFactor * Time.deltaTime);
                draw(smoothUV);
            }
        }
}
}