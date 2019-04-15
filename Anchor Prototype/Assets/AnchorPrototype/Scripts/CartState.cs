using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CartState
{
    public static List<string> cartItems = new List<string>();

    public static void AddItemToCart(string prodName){
        cartItems.Add(prodName);
    }
}

