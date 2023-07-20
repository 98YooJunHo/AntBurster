using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    private int gold;
    private int killCount;
    private float time;
    private int timeM;
    private int timeS;
    private TMP_Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        timeText = GameObject.Find("Time").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        NowTime();
    }

    void NowTime()
    {
        timeM = Convert.ToInt32(Math.Floor(time / 60f));
        timeS = Convert.ToInt32(Math.Floor(time % 60f));
        timeText.text = "Time " + timeM.ToString() + ":" + timeS.ToString();
    }
}
