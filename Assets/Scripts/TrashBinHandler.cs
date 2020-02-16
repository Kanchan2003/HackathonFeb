using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TrashBinHandler : MonoBehaviour
{
    [Tooltip("The object tag of the trash accepted")]
    public string tagType;
    private SphereCollider sphereCollider;
    private UIManager uiManager;
    ARPlane arPlane;
    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (arPlane == null)
        {
            foreach (var plane in GameObject.Find("AR Session Origin").GetComponent<ARPlaneManager>().trackables)
            {
                arPlane = plane;
                break;
            }
        } else
        {
            print(arPlane.transform.position.y);
            transform.parent.position = new Vector3(transform.parent.position.x, arPlane.transform.position.y, transform.parent.position.z);
        }
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == tagType)
        {
            // Decrements the number of objects remaining
            print(string.Format("Score item of tag: {0}", tagType));
            uiManager.IncreaseRemaining(-1);
            uiManager.IncreaseScore(1);
            Destroy(col.gameObject);
        }
    }
}
