using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//It's a snowball timer indicator class
public class SnowIndicator : MonoBehaviour
{
    [SerializeField] private RectTransform indicator;
    // Update is called once per frame

    private Vector3 defaultScaleValue;
    [Tooltip("In seconds")]
    private float timeRemaining;

    private void Start()
    {
        defaultScaleValue = indicator.localScale;
    }

    public void SetTime(float time)
    {
        timeRemaining = time;
    }

    public void ResetIndicator()
    {
        indicator.localScale = defaultScaleValue;
    }
    
    void Update()
    {
        //Decreasing the timer indicator 
        if (indicator.localScale.x >= 0)
        {
            float newValue = indicator.localScale.x - Time.deltaTime / timeRemaining;
            indicator.localScale = new Vector2(newValue, indicator.localScale.y);
        }
    }
}
