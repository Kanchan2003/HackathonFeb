using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceonPlane : MonoBehaviour
{
    private ARRaycastManager sessionOrigin;
    // Start is called before the first frame update
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public GameObject objectToPlace;
    public Camera camera;
    void Start()
    {
        sessionOrigin = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = camera.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Instantiate(objectToPlace, hit.transform.position, hit.transform.rotation);
                }
                else if (sessionOrigin.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose pose = hits[0].pose;
                    Instantiate(objectToPlace, pose.position, pose.rotation);
                }
            }
        }
    }
}
