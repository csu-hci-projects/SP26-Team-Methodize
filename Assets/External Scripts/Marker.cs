using UnityEngine;

public class Marker : MonoBehaviour
{
    private Vector2 uv;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerStay(Collider col)
    {
            Vector3 contactPoint = col.transform.InverseTransformPoint(transform.position);
            uv = new Vector2(contactPoint.x + 0.5f, contactPoint.y + 0.5f);
            BoardManager.Instance.DrawOn(col.gameObject, uv);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
