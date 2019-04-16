using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CartUIController : MonoBehaviour
{
    private RectTransform rectTransform;
    public  RectTransform productListRectTransform;
    public ScrollRect scrollRect;     

    // Start is called before the first frame update
    void Start()
    {        
        gameObject.SetActive(false);
        rectTransform = GetComponent<RectTransform>();
        DefaultPosition();
    }

    // Update is called once per frame
    void Update()
    {
        float height = rectTransform.sizeDelta.y;
        float listHeight = productListRectTransform.sizeDelta.y;

        if(gameObject.transform.localPosition.y > (-Screen.height + (CartState.cartItems.Count * 200f) + 250f)){
            scrollRect.inertia = false;
            Vector3 position = gameObject.transform.localPosition;
            position.y = -Screen.height + (CartState.cartItems.Count * 200f) + 250f;
            gameObject.transform.localPosition  = position;
        }       
        else if(gameObject.transform.localPosition.y < (-Screen.height + 200f)){
            scrollRect.inertia = false;
            DefaultPosition();
        }
        else{            
            scrollRect.inertia = true;
        }
    }

    private void DefaultPosition(){
         Vector3 position = gameObject.transform.localPosition;
         position.y = -Screen.height + 100f;
         gameObject.transform.localPosition  = position;
    }

    public void showCart() {
         if(gameObject.activeSelf) {
             gameObject.SetActive(false);
         }
         else {
             DefaultPosition();
             gameObject.SetActive(true);
         }
     }
}
