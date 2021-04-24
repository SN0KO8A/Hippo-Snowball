using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject objectToPool;
    [SerializeField] private int amountToPool;

    private List<GameObject> pooledObjects = new List<GameObject>();

    private static ObjectPooler sharedInstance;

    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject current = Instantiate(objectToPool, transform.position, transform.rotation, transform);
            current.SetActive(false);
            pooledObjects.Add(current);
        }
    }

    public static GameObject GetObject()
    {
        for (int i = 0; i < sharedInstance.amountToPool; i++)
        {
            if (!sharedInstance.pooledObjects[i].activeInHierarchy)
            {
                return sharedInstance.pooledObjects[i];
            }
        }

        return null;
    }
}
