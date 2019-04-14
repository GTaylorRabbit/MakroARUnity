using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using GoogleARCore;

public class AugmentedImageVisualizer : MonoBehaviour
{
    [SerializeField] private VideoClip[] _videoClips;
    public AugmentedImage Image;
    private VideoPlayer _videoPlayer;
    public GameObject cube;
    public GameObject shoppingCart;
    public GameObject videoPlane;

    void Start()
    {
        _videoPlayer = videoPlane.GetComponent<VideoPlayer>();
        _videoPlayer.loopPointReached += OnStop;
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
            shoppingCart.SetActive(false);
            cube.SetActive(false);
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

                //Update the video to mach the image position
                videoPlane.transform.localPosition = imageCenter;
                
                videoPlane.SetActive(true);   
            }
            if(Image.Name.ToString().Contains("Prod_")){
                PositionCube(imageCenter);
                PositionCart(imageCenter);
                
                shoppingCart.SetActive(true);
                cube.SetActive(true);
            }
        }
    }   

    private void PositionCube(Vector3 imageCenter){
        imageCenter.z -= 0.1f;
        imageCenter.x -= 0.1f;
        cube.transform.localPosition = imageCenter;
    }

    private void PositionCart(Vector3 imageCenter){
        imageCenter.z -= 0.1f;
        imageCenter.x += 0.1f;
        shoppingCart.transform.localPosition = imageCenter;
        
        shoppingCart.transform.Rotate(0, Time.deltaTime * 50, 0);
    }

    private void ScaleVideo() { 
        var f = Image.ExtentX / Image.ExtentZ; 
        if (Image.ExtentX > Image.ExtentZ) 
        { 
            videoPlane.transform.localScale = new Vector3(f * Image.ExtentZ / 10,1, Image.ExtentZ /10);             
        } 
        else
        { 
            videoPlane.transform.localScale = new Vector3(Image.ExtentX / 10, 1,f * Image.ExtentX /10);
        } 
    }
}
