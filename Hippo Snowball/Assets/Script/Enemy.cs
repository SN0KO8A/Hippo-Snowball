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

    [Header("Despawn options")] 
    [SerializeField] private float despawnOffset;
    
    private IEnumerator EnemyRunAI()
    {
        while (true)
        {
            //Deciding to run
            yield return new WaitForSeconds(Random.Range(minMaxTimeToThink.x, minMaxTimeToThink.y));
            Move(Random.Range(-1f, 1f));
            
            yield return new WaitForSeconds(Random.Range(minMaxTimeOfRunning.x, minMaxTimeOfRunning.y));
            Move(0f);
        }
    }
    
    //Init method
    public IEnumerator EnemyAppear(float ToPosX)
    {
        CountBoundary = false;
        Move(2f);
        //Until enemy reaches ToPosX
        yield return new WaitUntil(() => transform.position.x >= ToPosX);
        Move(0f);
        
        CountBoundary = true;
        StartCoroutine(EnemyRunAI());
    }

    public IEnumerator EnemyDisappear()
    {
        StopCoroutine(EnemyRunAI());
        CountBoundary = false;

        //BUG: So bug is when I Stop coroutine "RunAI", it doesn't stop until last new WaitOfSeconds
        //So problem is where I need to set up coroutines which shouldn't interference each other 
        //Solution is here, but I'm not sure that it is correctly...
        
        //Until enemy reaches ToPosX
        while (transform.position.x <= RightBoundary.position.x + despawnOffset)
        {
            Move(2f);
            yield return new WaitForFixedUpdate();
        }
        
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
        base.Die();
        StartCoroutine(EnemyDisappear());
    }

    public void SetBoundaries(Transform left, Transform right)
    {
        LeftBoundary = left;
        RightBoundary = right;
        CountBoundary = true;
    }
}
