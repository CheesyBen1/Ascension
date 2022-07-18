using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Mirror;

public class Timer : NetworkBehaviour
{

    public TextMeshProUGUI time;
    public TextMeshProUGUI maxTime;

    public RollingTracker tracker;


    private void Start()
    {
        time = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
        maxTime = GameObject.Find("Time/MaxTime").GetComponent<TextMeshProUGUI>();

        tracker = GetComponent<RollingTracker>();

    }


    private void Update()
    {
        
            time.text = tracker.levelTimer.ToString("F1");
            maxTime.text = tracker.maxTime.ToString();

            if (tracker.levelTimer > tracker.lavaRiseTime)
            {
                time.color = Color.red;
            }
            else
            {
                time.color = Color.white;
            }
        
    }

}
