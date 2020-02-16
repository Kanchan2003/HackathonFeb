using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text areaText;
    public Text widthText;
    public Text heightText;
    public Text scoreText;
    public Text remainingText;
    public Text timerText;

    private float area;
    private float width;
    private float height;
    private float initialTime;
    private float currentTime;
    private int score;
    private int remaining;
    private bool timerState;
    private bool timerDirection = true;
    void Start()
    {
        
    }
    void Update()
    {
        if (timerState)
        {
            // Counting up
            if (timerDirection)
            {
                currentTime += Time.deltaTime;
            } else
            {
                currentTime -= Time.deltaTime;
            }
            timerText.text = string.Format("{0}", (int)currentTime);
        }
    }

    // Update is called once per frame
    public void SetHeight(float h)
    {
        height = h;
        heightText.text = string.Format("Height: {0}", height);
        UpdateArea();
    }

    public void SetWidth(float w)
    {
        width = w;
        widthText.text = string.Format("Width: {0}", width);
        UpdateArea();
    }

    public void SetScore(int s)
    {
        score = s;
        scoreText.text = string.Format("{0}", score);
    }

    public void IncreaseScore(int s)
    {
        score += s;
        scoreText.text = string.Format("{0}", score);
    }

    public void SetRemaining(int r)
    {
        remaining = r;
        remainingText.text = string.Format("{0}", remaining);
    }

    public void IncreaseRemaining(int r)
    {
        remaining += r;
        remainingText.text = string.Format("{0}", remaining);
    }
    public void SetTimer(float time, bool dir = true)
    {
        initialTime = time;
        timerDirection = dir;
        if (timerDirection)
        {
            currentTime = 0;
        } else
        {
            currentTime = time;
        }
        
    }
    public void TimerState(bool state)
    {
        timerState = state;
    }

    public float GetArea()
    {
        return area;
    }
    public int GetTimer()
    {
        return (int)currentTime;
    }

    public int GetRemaining()
    {
        return remaining;
    }

    public int GetScore()
    {
        return score;
    }
    public void UpdateArea()
    {
        area = width * height;
        areaText.text = string.Format("Area: {0}", area);
    }
}
