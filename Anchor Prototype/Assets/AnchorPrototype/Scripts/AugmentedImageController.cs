using System.Collections;
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
                // Create an anchor to ensure that ARCore keeps tracking this augmented image.
                AddVisualizer(image, visualizer);
            }
            else if(image.TrackingState == TrackingState.Stopped && visualizer != null)
            {
                RemoveVisualizer(image, visualizer);
            }
        }
    }

    private void AddVisualizer(AugmentedImage image, AugmentedImageVisualizer visualizer){
        Anchor anchor = image.CreateAnchor(image.CenterPose);
        visualizer = (AugmentedImageVisualizer)Instantiate(augmentedImageVisualizerPrefab, anchor.transform);
        visualizer.Image = image;
        _visualizers.Add(image.DatabaseIndex, visualizer);

        

            /*//Create a new Anchor
            Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);

            //Set the position of the portal to be the same as the hit position
            portal.transform.position = hit.Pose.position;
            portal.transform.rotation = hit.Pose.rotation;

            
            Vector3 cameraPosition = arCamera.transform.position;
            //portal should only rotate around the Y axis
            cameraPosition.y = hit.Pose.position.y;

            //Rotate the portal to face the camera
            portal.transform.LookAt(cameraPosition, portal.transform.up);

            //ARCore will keep understanding the world and update the anchors accordingly hence we need to attach our portal to the anchor
            portal.transform.parent = anchor.transform;*/
    }

    private void RemoveVisualizer(AugmentedImage image, AugmentedImageVisualizer visualizer){
        _visualizers.Remove(image.DatabaseIndex);
        GameObject.Destroy(visualizer.gameObject);
    }

}
