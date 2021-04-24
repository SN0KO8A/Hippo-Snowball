using System;
using System.Collections;
using System.Collections.Generic;
using SimpleInputNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Hero : Animal
{
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

        StartCoroutine(TimeThrowCooldown());
    }

    private IEnumerator TimeThrowCooldown()
    {
        //Setting up timer and cooldown
        snowIndicator.SetTime(TimeForThrow);
        snowIndicator.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(TimeForThrow);
        
        //And restoring timer and ability to throwing
        snowIndicator.gameObject.SetActive(false);
        snowIndicator.ResetIndicator();
        canThrow = true;
        throwButton.interactable = true;
    }
}
