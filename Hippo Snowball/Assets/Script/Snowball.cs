using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Snowball : MonoBehaviour
{
    [Tooltip("Curve of local scale during the flight")]
    [SerializeField] private AnimationCurve curveOfSize;
    [Tooltip("Velocity offset for despawning the snowball with velocity less than the offset")]
    [SerializeField] private float velocityOffset;
    [SerializeField] private GameObject emissionParticleEffect;
    
    //For falling effect
    private float startingVelocity;
    private Vector2 startingScale;
    
    private Rigidbody2D rigidbody2D;
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        startingScale = transform.localScale;
        startingVelocity = rigidbody2D.velocity.y;
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log($"Snowball hit: {other.gameObject.name}");
        Animal animal = other.gameObject.GetComponent<Animal>();

        if (animal)
        {
            animal.ToGetHit();
        }

        Despawn();
    }

    private void Update()
    {
        //Getting result of function (falling effect)
        float currentSize = curveOfSize.Evaluate(rigidbody2D.velocity.y / startingVelocity);
        transform.localScale = new Vector2(startingScale.x * currentSize, startingScale.y * currentSize);

        if (Mathf.Abs(rigidbody2D.velocity.y) <= 0 + velocityOffset)
        {
            Despawn();
        }
    }

    private void Despawn()
    {
        transform.localScale = startingScale;
        
        //Handing snowball emission (spawn and despawn)
        GameObject emission = ObjectPooler.GetObject(emissionParticleEffect);
        emission.SetActive(true);
        emission.transform.position = transform.position;
        ObjectPooler.DespawnObject(emissionParticleEffect, emission, 2f);
        
        gameObject.SetActive(false);
    }
}
