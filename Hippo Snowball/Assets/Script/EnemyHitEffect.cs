using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitEffect : MonoBehaviour
{
    [SerializeField] private float displayingTime;
    [SerializeField] private GameObject hitEffect;

    private static EnemyHitEffect enemyHitEffect;

    private void Awake()
    {
        enemyHitEffect = this;
    }

    public static void Display()
    {
        enemyHitEffect.StartCoroutine(enemyHitEffect.HitEffect(enemyHitEffect.displayingTime));
    }

    private IEnumerator HitEffect(float time)
    {
        hitEffect.SetActive(true);
        yield return new WaitForSeconds(time);
        hitEffect.SetActive(false);
    }
}
