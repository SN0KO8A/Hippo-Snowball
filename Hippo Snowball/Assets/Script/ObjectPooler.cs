using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using UnityEngine;

//Pooled object template
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

    public static GameObject GetObject(GameObject templatePooledObject)
    {
        foreach (ObjectPoolItem current in sharedInstance.objectsToPool)
        {
            //Finding pooledObject template
            if (templatePooledObject.Equals(current.objectToPool))
            {
                for (int i = 0; i < current.amountToPool; i++)
                {
                    //Getting the object
                    if (!current.pooledObjects[i].activeInHierarchy)
                    {
                        return current.pooledObjects[i];
                    }
                }
            }
        }
        
        Debug.LogWarning($"Object Pooler [Spawn]: I didn't find the {templatePooledObject} in the pool");
        return null;
    }

    public static void DespawnObject(GameObject templatePooledObject, GameObject objectToDespawn, float timeToDespawn = 0f)
    {
        foreach (ObjectPoolItem current in sharedInstance.objectsToPool)
        {
            //Finding pooledObject template
            if (templatePooledObject.Equals(current.objectToPool))
            {
                for (int i = 0; i < current.amountToPool; i++)
                {
                    //Finding the object
                    if (objectToDespawn.Equals(current.pooledObjects[i]))
                    {
                        //Despawn
                        if (timeToDespawn <= 0f)
                        {
                            objectToDespawn.SetActive(false);
                        }

                        else
                        {
                            //Despawning after some time
                            sharedInstance.StartCoroutine(sharedInstance.DespawnCoroutine(objectToDespawn, timeToDespawn));
                        }

                        return;
                    }
                }
            }
        }
        
        Debug.LogWarning($"Object Pooler [Despawn]: I didn't find the {templatePooledObject} in the pool");
    }

    private IEnumerator DespawnCoroutine(GameObject objectToDespawn, float time)
    {
        yield return new WaitForSeconds(time);
        objectToDespawn.SetActive(false);
    }
}
