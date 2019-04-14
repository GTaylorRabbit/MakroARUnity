using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Time.deltaTime, 0, 0);
    }
}
