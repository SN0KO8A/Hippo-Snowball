using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class StrengthIndicator : MonoBehaviour
{
    [SerializeField] private Image indicatorImage;
    
    [Tooltip("In seconds")]
    [SerializeField] private float timeToMax;
    
    [SerializeField] private Color colorMax;
    [SerializeField] private Color colorMin;
    
    private float strengthValue;

    public float StrengthValue
    {
        get => strengthValue;
    }

    private void Start()
    {
        if (timeToMax <= 0f)
        {
            Debug.LogError("StrengthIndicator: timeToMax cannot be less than or equal to 0f");
        }
    }

    // Update is called once per frame
    void Update()
    {
        strengthValue = Mathf.PingPong(Time.time / timeToMax, 1);
        
        indicatorImage.color = Color.Lerp(colorMin, colorMax, strengthValue);
        indicatorImage.fillAmount = strengthValue;
    }
}
