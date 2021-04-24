using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
class ObjectPoolItem
{
    public GameObject objectToPool;
    public int amountToPool;
    public List<GameObject> pooledObjects = new List<GameObject>();
}

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private List<ObjectPoolItem> objectsToPool = new List<ObjectPoolItem>();
    private static ObjectPooler sharedInstance;

    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        //Spawning objects
        foreach (ObjectPoolItem current in objectsToPool)
        {
            for (int i = 0; i < current.amountToPool; i++)
            {
                GameObject currentGameObject = Instantiate(current.objectToPool, transform.position, transform.rotation, transform);
                currentGameObject.SetActive(false);
                current.pooledObjects.Add(currentGameObject);
            }
        }
    }

    public static GameObject GetObject(GameObject gameObject)
    {
        foreach (ObjectPoolItem current in sharedInstance.objectsToPool)
        {
            if (gameObject.Equals(current.objectToPool))
            {
                for (int i = 0; i < current.amountToPool; i++)
                {
                    if (!current.pooledObjects[i].activeInHierarchy)
                    {
                        return current.pooledObjects[i];
                    }
                }
            }
        }
        
        Debug.LogWarning($"Object Pooler: I didn't find the {gameObject} in the pool");
        return null;
    }
}
