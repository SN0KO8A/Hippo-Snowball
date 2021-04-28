using System.Collections;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Enemy : Animal
{
    [Header("Enemy Stats")]
    [SerializeField] private int scorePrice;

    [Header("Enemy Move AI")] 
    [Tooltip("Time before enemy begins run (min, max)")]
    [SerializeField] private Vector2 minMaxTimeToThink;
    [Tooltip("Duration of running (min, max)")]
    [SerializeField] private Vector2 minMaxTimeOfRunning;

    [Header("Spawn/Despawn options")]
    [Tooltip("The offset need for left and right boundary position appearing")]
    [SerializeField] private float leftOffset;
    [SerializeField] private float rightOffset;

    private Coroutine runAICoroutine;
    
    private IEnumerator EnemyRunAI()
    {
        while (true)
        {
            //Deciding to run
            if (transform.position.x + transform.localScale.x >= RightBoundary.position.x)
            {
                Move(Random.Range(-1f, -0.25f));
            }

            else if(transform.position.x - transform.localScale.x <= LeftBoundary.position.x)
            {
                Move(Random.Range(0.25f, 1f));
            }

            else
            {
                Move(Random.Range(-1f, 1f));
            }
            
            yield return new WaitForSeconds(Random.Range(minMaxTimeOfRunning.x, minMaxTimeOfRunning.y));
            
            Move(0f);
            yield return new WaitForSeconds(Random.Range(minMaxTimeToThink.x, minMaxTimeToThink.y));
        }
    }
    
    //Init method
    public IEnumerator EnemyAppear()
    {
        CountBoundary = false;
        Move(2f);
        
        //Until enemy reaches left boundary
        yield return new WaitUntil(() => transform.position.x >= LeftBoundary.position.x + leftOffset);
        
        CountBoundary = true;
        runAICoroutine = StartCoroutine(EnemyRunAI());
    }

    public IEnumerator EnemyDisappear()
    {
        CountBoundary = false;
        Move(2f);
        
        //Until enemy reaches right boundary
        yield return new WaitUntil(() => transform.position.x > RightBoundary.position.x + rightOffset);
        
        CountBoundary = true;
        EnemyManager.SpawnEnemy();
        EnemyManager.DespawnEnemy(gameObject);
    }

    public void ThrowBall()
    {
        ThrowBall(-1f);
    }

    protected override void Die()
    {
        GameController.AddScore(scorePrice);
        EnemyHitEffect.Display();
        
        StopCoroutine(runAICoroutine);
        StartCoroutine(EnemyDisappear());
    }

    public void SetBoundaries(Transform left, Transform right)
    {
        LeftBoundary = left;
        RightBoundary = right;
        CountBoundary = true;
    }
}
