using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanelHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Text textBox;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string text)
    {
        textBox.text = text;
    }
    public void AddText(string text)
    {
        textBox.text += text;
    }
    public void PanelState(bool state)
    {
        GetComponent<Image>().enabled = state;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(state);
        }
    }
}
