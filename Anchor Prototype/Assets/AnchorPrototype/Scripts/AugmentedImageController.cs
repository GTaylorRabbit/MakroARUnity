﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class AugmentedImageController : MonoBehaviour
{
    public AugmentedImageVisualizer augmentedImageVisualizerPrefab;

    private Dictionary<int, AugmentedImageVisualizer> _visualizers = new Dictionary<int, AugmentedImageVisualizer>();

    private List<AugmentedImage> _images = new List<AugmentedImage>();

    private void Update()
    {
        // Exit the app when the 'back' button is pressed.
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        //check that motion tracking is tracking
        if(Session.Status != SessionStatus.Tracking)
        {
            return;
        }

        //Get updated augmented images for this frame.
        Session.GetTrackables<AugmentedImage>(_images, TrackableQueryFilter.Updated);
        
        VisualizeTrackables();       
    }    

    // Create visualizers and anchors for updated augmented images that are tracking and do
    // not previously have a visualizer. Remove visualizers for stopped images.
    private void VisualizeTrackables()
    {
        foreach(var image in _images)
        {
            AugmentedImageVisualizer visualizer = null;
            _visualizers.TryGetValue(image.DatabaseIndex, out visualizer);
            if(image.TrackingState == TrackingState.Tracking && visualizer == null)
            {               
                Anchor anchor = image.CreateAnchor(image.CenterPose);
                // Create an anchor to ensure that ARCore keeps tracking this augmented image.
                visualizer = AddVisualizer(image, visualizer);
            }
            else if(image.TrackingState == TrackingState.Stopped && visualizer != null)
            {
                RemoveVisualizer(image, visualizer);
            }
        }
    }

    private AugmentedImageVisualizer AddVisualizer(AugmentedImage image, AugmentedImageVisualizer visualizer){
        Anchor anchor = image.CreateAnchor(image.CenterPose);
        visualizer = (AugmentedImageVisualizer)Instantiate(augmentedImageVisualizerPrefab, anchor.transform);
        visualizer.Image = image;
        _visualizers.Add(image.DatabaseIndex, visualizer);
    
        //ARCore will keep understanding the world and update the anchors accordingly hence we need to attach our portal to the anchor
        visualizer.transform.parent = anchor.transform;
        return visualizer;
    }

    private void RemoveVisualizer(AugmentedImage image, AugmentedImageVisualizer visualizer){
        _visualizers.Remove(image.DatabaseIndex);
        GameObject.Destroy(visualizer.gameObject);
    }

}
