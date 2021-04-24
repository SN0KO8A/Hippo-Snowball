using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrengthIndicator : MonoBehaviour
{
    [SerializeField] private RectTransform indicator;
    
    [Tooltip("In seconds")]
    [SerializeField] private float timeToMax;
    
    [SerializeField] private Color colorMax;
    [SerializeField] private Color colorMin;

    private Image indicatorImage;
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
        
        indicatorImage = indicator.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        strengthValue = Mathf.PingPong(Time.time / timeToMax, 1);
        
        indicatorImage.color = Color.Lerp(colorMin, colorMax, strengthValue);
        indicator.localScale = new Vector2(indicator.localScale.x,strengthValue);
    }
}
