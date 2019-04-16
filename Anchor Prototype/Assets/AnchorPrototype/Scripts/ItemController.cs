using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
   public GameObject itemPrefab;

    public void updateItemList(){        
        Instantiate(itemPrefab, gameObject.transform);
    }

    //Invoked when a button is pressed.
    private void SetParent()
    {
        //Makes the GameObject "newParent" the parent of the GameObject "player".
        itemPrefab.transform.parent = gameObject.transform;
    }

    private void DetachFromParent()
    {
        // Detaches the transform from its parent.
        transform.parent = null;
    }
}
