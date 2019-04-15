﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using GoogleARCore;
using TMPro;

public class AugmentedImageVisualizer : MonoBehaviour
{
    [SerializeField] private VideoClip[] _videoClips;
    public AugmentedImage Image;
    private VideoPlayer _videoPlayer;
    public GameObject videoPlane;
    public GameObject productDetails;
    public TextMeshPro productTitle;
    public TextMeshPro productPrice;
    public CartController cartController;
    [SerializeField] private GameObject[] stars;

    void Start()
    {
        _videoPlayer = videoPlane.GetComponent<VideoPlayer>();
        _videoPlayer.loopPointReached += OnStop;

        if(Image != null && Image.Name.ToString().Contains("Prod_")){
            ProductDetail.SetDetailValues(Image.Name.ToString());            
            productTitle.text = ProductDetail.name;
            productPrice.text = ProductDetail.price;
            cartController.SetCartItem(Image.Name.ToString());
        }
    }

    private void OnStop(VideoPlayer source)
    {
        videoPlane.SetActive(false);
    }


    void Update()
    {
        if(Image == null || Image.TrackingState != TrackingState.Tracking)
        {            
            videoPlane.SetActive(false);
            productDetails.SetActive(false);

            return;
        }

        if(Image.Name.ToString().Contains("Vid_")){
            if(!_videoPlayer.isPlaying)
            {
                _videoPlayer.clip = _videoClips[Image.DatabaseIndex];
                _videoPlayer.Play();
            }
        }            
        
        if(Image.TrackingState == TrackingState.Tracking){
            
            float halfWidth = Image.ExtentX / 200;
            float halfHeight = Image.ExtentZ / 200;
            Vector3 imageCenter = (halfWidth * Vector3.left) + (halfHeight * Vector3.back);

            if(Image.Name.ToString().Contains("Vid_")){
                //Scale the video to mach the Image Size
                ScaleVideo();

                imageCenter.z += 0.02f;
                //Update the video to mach the image position
                videoPlane.transform.localPosition = imageCenter;
                
                videoPlane.SetActive(true);   
            }
            if(Image.Name.ToString().Contains("Prod_")){
                //PositionProduct(imageCenter);

                //Rotate the Stars
                foreach(var star in stars)
                {
                    star.transform.Rotate(0, 0, Time.deltaTime * 50);
                }    

                productDetails.SetActive(true);
            }
        }
    }   

    private void PositionProduct(Vector3 imageCenter){
        productDetails.transform.localPosition = imageCenter;
    }

    private void ScaleVideo() { 
        var f = Image.ExtentX / Image.ExtentZ; 
        if (Image.ExtentX > Image.ExtentZ) 
        { 
            videoPlane.transform.localScale = new Vector3(f * Image.ExtentZ , Image.ExtentZ, 1 );             
        } 
        else
        { 
            videoPlane.transform.localScale = new Vector3(Image.ExtentX, f * Image.ExtentX, 1);
        } 
    }
}
