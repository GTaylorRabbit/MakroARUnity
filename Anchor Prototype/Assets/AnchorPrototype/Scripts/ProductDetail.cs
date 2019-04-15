using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProductDetail
{
    public static string name;
    public static string price;

    public static void SetDetailValues(string prodName){
        switch(prodName){
            case ("Prod_RomanyCreams"):
            {
                name = "Romany Creams";
                price = "R28.99";
            }
            break;
            case ("Prod_SpecialK"):
            {
                name = "Special K - Cereal Bars";
                price = "R19.50";
            }
            break;                
        }
    }
}
