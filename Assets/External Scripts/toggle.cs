using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class toggle : MonoBehaviour
{
public InputActionReference toggleAction;

public XRGrabInteractable grab;
public BoxCollider box;
//public XRSimpleInteractable sim;
public Rigidbody rig;
public MeshCollider mesh;


private bool isDrawMode = true;

void Update()
{
    if(toggleAction.action.WasPressedThisFrame())
    {
        isDrawMode = !isDrawMode;
        mesh.enabled = !isDrawMode;
        //sim.enabled = isDrawMode;
        grab.enabled = isDrawMode;
        box.enabled = isDrawMode;
    }
}
}
