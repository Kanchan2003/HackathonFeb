using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanelHandler : MonoBehaviour
{
    private bool infoPanelState = false;
    private bool leaderBoardPanelState = false;
    public GameObject infoPanel;
    public GameObject leaderBoardPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleInfoPanel()
    {
        infoPanelState = !infoPanelState;
        infoPanel.SetActive(infoPanelState);
    }

    public void InfoPanelState(bool state)
    {
        infoPanelState = state;
        infoPanel.SetActive(infoPanelState);
        infoPanel.GetComponent<InfoPanelHandler>().ReloadPage();
    }

    public void ToggleLeaderboardPanel()
    {
        leaderBoardPanelState = !leaderBoardPanelState;
        leaderBoardPanel.SetActive(leaderBoardPanelState);
    }

    public void OpenURL(string URL){
        Application.OpenURL(URL);
    }
}
