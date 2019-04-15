using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public GameObject starExplosion;
    private ParticleSystem particleSystem;

    void Start(){
        particleSystem = starExplosion.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //Check if the user touches the screen
        Touch touch;
        if(Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        if(Input.GetMouseButton(0))
        {
            Vector3 mousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
            Vector3 mousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);            
            
            Vector3 mousePosF = Camera.main.ScreenToWorldPoint(mousePosFar);
            Vector3 mousePosN = Camera.main.ScreenToWorldPoint(mousePosNear);

            RaycastHit hit;
            if(Physics.Raycast(mousePosN, mousePosF-mousePosN, out hit))
            {
                CartController cartController = hit.transform.gameObject.GetComponent<CartController>();
                CartState.AddItemToCart(cartController.GetCartItem());
                
                starExplosion.transform.position = hit.transform.gameObject.transform.position;                    
                particleSystem.Stop();
                particleSystem.Play();
            }
        }
    }
}
