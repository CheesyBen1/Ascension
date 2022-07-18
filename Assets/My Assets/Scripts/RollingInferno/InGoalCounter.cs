using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Mirror;

public class InGoalCounter : NetworkBehaviour
{

    public TextMeshProUGUI count;
    public TextMeshProUGUI maxCount;

    public RollingTracker tracker;


    private void Start()
    {
        count = GameObject.Find("GoalCount").GetComponent<TextMeshProUGUI>();
        maxCount = GameObject.Find("GoalCount/MaxCount").GetComponent<TextMeshProUGUI>();

        tracker = GetComponent<RollingTracker>();

    }


    private void Update()
    {
        
            count.text = tracker.playerInGoal.ToString();

       
            maxCount.text = (tracker.playerCount - 1).ToString();

            if (tracker.playerInGoal >= tracker.playerCount - 1)
            {
                count.color = Color.yellow;
            }
            else
            {
                count.color = Color.white;
            }
        
    }

}
