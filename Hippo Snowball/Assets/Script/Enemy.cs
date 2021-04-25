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

    protected override void Start()
    {
        base.Start();
        StartCoroutine(EnemyRunAI());
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

    public void ThrowBall()
    {
        ThrowBall(-1f);
    }

    protected override void Die()
    {
        base.Die();
        EnemyManager.DespawnEnemy(gameObject);
        EnemyManager.SpawnEnemy();
    }
}
