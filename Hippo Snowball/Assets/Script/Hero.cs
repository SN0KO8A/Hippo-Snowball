using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Animal
{
    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalSpeed = Input.GetAxis("Horizontal");
        Move(horizontalSpeed);
    }
}
