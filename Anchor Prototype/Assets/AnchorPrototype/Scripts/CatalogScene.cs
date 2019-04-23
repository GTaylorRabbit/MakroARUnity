using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatalogScene : MonoBehaviour
{
    public float radius = 0.05f;
    public int productCount = 8;
    
    void Start()
    {
        for (int i = 0; i < productCount; i++)
        {
            float angle = i * Mathf.PI*2f / 8;
            Vector3 newPos = new Vector3(Mathf.Cos(angle)*radius, 0f, Mathf.Sin(angle)*radius);
            GameObject go = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), gameObject.transform);            
            go.transform.localPosition = newPos;
            go.transform.localScale = new Vector3(.01f, .01f, .01f);            
        }
    }
}
