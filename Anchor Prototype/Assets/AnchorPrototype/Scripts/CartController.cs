using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CartController : MonoBehaviour
{
    private string cartProductName;
    public TextMeshPro cartCount;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0, Time.deltaTime * 50, 0);
        cartCount.text = CartState.cartItems.Count.ToString();
    }

    public void SetCartItem(string productName){
        cartProductName = productName;
    }

    public string GetCartItem(){
        return cartProductName;
    }
}
