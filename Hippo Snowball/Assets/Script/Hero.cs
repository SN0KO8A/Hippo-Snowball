using System;
using System.Collections;
using System.Collections.Generic;
using SimpleInputNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Hero : Animal
{
    [Header("Hero stats")]
    [Tooltip("In seconds")]
    [SerializeField] private float timeForThrow;
    
    [Header("Controls/UI")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private Button throwButton;
    [SerializeField] private SnowIndicator snowIndicator;
    [SerializeField] private StrengthIndicator strengthIndicator;

    private bool canThrow = true;

    void FixedUpdate()
    {
        float horizontalSpeed = joystick.xAxis.value;
        Move(horizontalSpeed);
    }

    public void Throw()
    {
        ThrowBall(strengthIndicator.StrengthValue);
        canThrow = false;
        throwButton.interactable = false;

        StartCoroutine(timeThrowCooldown());
    }

    private IEnumerator timeThrowCooldown()
    {
        //Setting up timer and cooldown
        snowIndicator.SetTime(timeForThrow);
        snowIndicator.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(timeForThrow);
        
        //And restoring timer and ability to throwing
        snowIndicator.gameObject.SetActive(false);
        snowIndicator.ResetIndicator();
        canThrow = true;
        throwButton.interactable = true;
    }
}
