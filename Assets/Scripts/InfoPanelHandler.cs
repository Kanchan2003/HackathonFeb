using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelHandler : MonoBehaviour
{
    public Sprite[] pages;
    private int pageCounter;
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextPage()
    {
        if (pageCounter < pages.Length - 1)
        {
            pageCounter++;
        }
        SetImage(pages[pageCounter]);
    }

    public void PreviousPage()
    {
        if (pageCounter > 0)
        {
            pageCounter--;
        }
        SetImage(pages[pageCounter]);
    }

    public void SetPage(int pageNum)
    {
        pageCounter = pageNum;
        SetImage(pages[pageCounter]);
    }

    public void SetImage(Sprite img)
    {
        image.sprite = img;
    }

    public void ReloadPage()
    {
        SetImage(pages[pageCounter]);
    }
}
