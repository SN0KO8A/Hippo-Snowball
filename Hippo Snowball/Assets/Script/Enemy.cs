using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Enemy : Animal
{
    [Header("Enemy Stats")]
    [SerializeField] private int level;

    [Header("Enemy Move AI")] 
    [Tooltip("Time before enemy begins run (min, max)")]
    [SerializeField] private Vector2 minMaxTimeToThink;
    [Tooltip("Duration of running (min, max)")]
    [SerializeField] private Vector2 minMaxTimeOfRunning;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(EnemyRunAI());
        StartCoroutine(EnemyThrowAI());
    }

    private IEnumerator EnemyRunAI()
    {
        while (true)
        {
            //Deciding to run
            yield return new WaitForSeconds(Random.Range(minMaxTimeToThink.x, minMaxTimeToThink.y));
            Move(Random.Range(-1,1));
            
            yield return new WaitForSeconds(Random.Range(minMaxTimeOfRunning.x, minMaxTimeOfRunning.y));
            Move(0f);
        }
    }

    private IEnumerator EnemyThrowAI()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeForThrow);
            ThrowBall(-1f);
        }
    }
    
}
