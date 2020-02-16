using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ObjectGenerator : MonoBehaviour
{
    private ARSessionOrigin sessionOrigin;
    private ARRaycastManager raycastManager;
    // Start is called before the first frame update
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public float xExtent = 0.5f;
    public float zExtent = 0.5f;
    public Camera cam;
    public GameObject Bins;
    public GameObject[] TrashPrefab;
    private ARPlane arPlane;
    // Start is called before the first frame update
    void Start()
    {
        
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
        }
    }

    public void GenerateTrashBins()
    {
        print("Generating Bins");
        Instantiate(Bins, arPlane.transform.position, transform.rotation);
    }
    public void GenerateTrash(int numberOfTrash)
    {
        for (int i = 0; i < numberOfTrash; i++)
        {
            Instantiate(TrashPrefab[Random.Range(0, TrashPrefab.Length)],RandomPointSelect(),transform.rotation);
        }
    }
    public Vector3 RandomPointSelect()
    {
        float xCenter = arPlane.transform.position.x;
        float zCenter = arPlane.transform.position.z;
        float x = Random.Range(xCenter - xExtent, xCenter + xExtent);
        float y = arPlane.center.y;
        float z = Random.Range(zCenter - zExtent, zCenter + zExtent);
        print(xExtent);
        print(zExtent);
        return new Vector3(x, y, z);

    }
}
