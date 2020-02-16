using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TrashHandler : MonoBehaviour
{
    private ARPlane arPlane;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (arPlane == null)
        {
            foreach (var plane in GameObject.Find("AR Session Origin").GetComponent<ARPlaneManager>().trackables)
            {
                arPlane = plane;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (arPlane == null)
        {
            foreach (var plane in GetComponent<ARPlaneManager>().trackables)
            {
                arPlane = plane;
                break;
            }
        } else
        if (transform.position.y < arPlane.transform.position.y)
        {
            print("Object Underground");
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x, arPlane.transform.position.y, transform.position.z);
        }
    }
}
