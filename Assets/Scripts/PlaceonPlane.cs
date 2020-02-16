using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceonPlane : MonoBehaviour
{
    private ARSessionOrigin sessionOrigin;
    private ARRaycastManager raycastManager;
    private ARPlaneManager arPlaneManager;
    private RaycastHit hit;
    private ObjectGenerator objectGenerator;
    public SceneHandler sceneHandler;
    // Start is called before the first frame update
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private GameObject newObject;

    private GameObject trackedObject;

    private Pose pose;

    private bool countdownStarted = false;

    private ARPlane arPlane;

    public int trashNumber = 10;
    public float planeSetThreshold = 5f;
    public float timerValue = 60;
    public float dragHeight = 1f;
    public Camera cam;
    public GamePanelHandler gamePanel;
    
    public UIManager uiManager;

    public GameObject objectToPlace;
    void Start()
    {
        arPlaneManager = GetComponent<ARPlaneManager>();
        raycastManager = GetComponent<ARRaycastManager>();
        sessionOrigin = GetComponent<ARSessionOrigin>();
        objectGenerator = GetComponent<ObjectGenerator>();

        SetAllPlanesActive(true);
        arPlaneManager.enabled = true;
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
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = cam.ScreenPointToRay(touch.position);
                Physics.Raycast(ray, out hit);
                print(hit.transform.parent.gameObject.tag);
                if (hit.transform.parent.gameObject.tag == "Compost" || hit.transform.parent.gameObject.tag == "Recycle" || hit.transform.parent.gameObject.tag == "Trash")
                {
                    trackedObject = hit.transform.gameObject;
                    hit.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
                }
            }
            if (touch.phase == TouchPhase.Moved)
            {
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinInfinity))
                {
                    pose = hits[0].pose;

                }
                if (trackedObject != null)
                {
                    trackedObject.transform.position = pose.position + Vector3.up * dragHeight;
                }
                
            }
            if (touch.phase == TouchPhase.Ended)
            {
                if (trackedObject != null)
                {
                    hit.transform.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    trackedObject = null;
                }

            }
        }

        if (arPlane != null && uiManager != null)
        {
            uiManager.SetHeight(arPlane.size.y);
            uiManager.SetWidth(arPlane.size.x);

            if (uiManager.GetArea() > planeSetThreshold && !countdownStarted)
            {
                if (gamePanel != null)
                {
                    StartCoroutine(Countdown());
                } else
                {
                    StartGame();
                }
            } else if (uiManager.GetRemaining() <= 0 && !arPlaneManager.enabled)
            {
                EndGame();
            }
        } else
        {
            print("ARPlane and/or UIManager not found!");
        }
    }

    void SetAllPlanesActive(bool value)
    {
        foreach (var plane in arPlaneManager.trackables)
            plane.gameObject.SetActive(value);
    }

    void StartGame()
    {
        SetAllPlanesActive(false);
        arPlaneManager.enabled = false;
        print("Generating Trash Bins");
        objectGenerator.GenerateTrashBins();
        print("Spawning Trash");
        objectGenerator.GenerateTrash(trashNumber);
        uiManager.SetRemaining(trashNumber);
        uiManager.SetTimer(0,true);
        // Starts timer
        uiManager.TimerState(true);
    }
    void EndGame()
    {
        uiManager.TimerState(false);
        gamePanel.PanelState(true);
        int score = uiManager.GetTimer();
        gamePanel.SetText(string.Format("Game Completed in {0} seconds", score));
        int[] scores;
        if (sceneHandler != null)
        {
            scores = sceneHandler.GetLeaderboard();
            Array.Sort(scores);
            var tmpList = new List<int>();
            tmpList.AddRange(scores);
            for (int i = 0; i < scores.Length; i++)
            {
                // This means the high score is on the leaderboard!
                if (score < scores[i])
                {
                    tmpList.Insert(i, score);
                    break;
                }
            }
            for (int i = 0; i < scores.Length; i++)
            {
                scores[i] = tmpList[i];
            }
            sceneHandler.SaveLeaderboard(scores);
        }
    }
    IEnumerator Countdown()
    {
        countdownStarted = true;
        yield return new WaitForSeconds(1);
        gamePanel.SetText("3");
        yield return new WaitForSeconds(1);
        gamePanel.SetText("2");
        yield return new WaitForSeconds(1);
        gamePanel.SetText("1");
        yield return new WaitForSeconds(1);
        // Disables panel
        gamePanel.PanelState(false);
        StartGame();
    }

}
