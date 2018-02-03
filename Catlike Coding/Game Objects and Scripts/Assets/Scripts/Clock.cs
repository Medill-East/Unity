using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clock : MonoBehaviour {

    public Transform hoursTransform, minutesTransform, secondsTransform;

    public bool continuous;

    const float
        degreesPerHour = 30f,
        degreesPerMinute = 6f,
        degreesPerSecond = 6f;
    

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
		
	}

    private void Update()
    {
        if(continuous)
        {
            UpdateContinuous();
        }
        else
        {
            UpdateDiscrete();
        }
    }

    // Update is called once per frame
    void UpdateContinuous() {
        TimeSpan time = DateTime.Now.TimeOfDay;
        hoursTransform.localRotation = Quaternion.Euler(0f, (float)time.TotalHours * degreesPerHour, 0f);
        minutesTransform.localRotation = Quaternion.Euler(0f, (float)time.TotalMinutes * degreesPerMinute, 0f);
        secondsTransform.localRotation = Quaternion.Euler(0f, (float)time.TotalSeconds * degreesPerSecond, 0f);

        Debug.Log(DateTime.Now);
    }

    void UpdateDiscrete()
    {
        DateTime time = DateTime.Now;
        hoursTransform.localRotation = Quaternion.Euler(0f, time.Hour * degreesPerHour, 0f);
        minutesTransform.localRotation = Quaternion.Euler(0f, time.Minute * degreesPerMinute, 0f);
        secondsTransform.localRotation = Quaternion.Euler(0f, time.Second * degreesPerSecond, 0f);

        Debug.Log(DateTime.Now);
    }
}
