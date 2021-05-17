using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Actor
{
    public GameObject healthCounter;

    private Text healthCounterText = null;

    void Start()
    {
        setHealthCounter();
    }

    void Update()
    {
        setHealthCounter();
    }

    private void setHealthCounter()
    {
        if (healthCounterText == null)
        {
            healthCounterText = healthCounter.GetComponent<Text>();
        }
        healthCounterText.text = getHealth().ToString();
    }
}
