using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class cubeAugmentedVisualizer : MonoBehaviour
{
    public AugmentedImage Image;

    public GameObject cube;

    // Update is called once per frame
    void Update()
    {
        if(Image == null || Image.TrackingState != TrackingState.Tracking)
        {            
            cube.SetActive(false);
            return;
        }

        //cube.transform.localPosition = ((Image.ExtentX + 1f) * Vector3.left) + ((Image.ExtentZ + 1f) * Vector3.back);

        cube.SetActive(true);  
    }
}
