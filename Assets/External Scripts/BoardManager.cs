using UnityEngine;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
public static BoardManager Instance;
public int brushRadius = 5;
public float smoothingFactor = 6;
public Color brushColor = Color.black;
public Color bgColor = Color.white;
public List<whiteboard> whiteboards = new List<whiteboard>();
public bool eraserMode = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
    if(Instance == null)
    {
        Instance = this;
    }
    else
    {
         Destroy(gameObject);
    }
    }
    void Start()
    {
        
    }

    public void RegisterDrawableSurface(whiteboard draw)
    {
        whiteboards.Add(draw);
    }

    public void DrawOn(GameObject surface, Vector2 uv)
    {
        whiteboard wb = whiteboards.Find(w => w.gameObject == surface);
        if(wb != null) wb.draw(uv);
    }

    public void setBrushRadius()
    {
        
    }

    public void setSmoothingFactor()
    {
        
    }



    public void isEraser()
    {
        eraserMode = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
