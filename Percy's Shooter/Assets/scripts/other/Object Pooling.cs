using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;
    public Transform parentTransform;

    private int lastUsedIndex = -1;

    void Awake()
    {
        SharedInstance = this;
        parentTransform = transform;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool, parentTransform != null ? parentTransform : this.transform);
            //tmp.GetComponent<MeshRenderer>().enabled = false;
            //tmp.GetComponent<SphereCollider>().enabled = false;
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }
    public GameObject GetPooledObject()
    {
        int startIndex = (lastUsedIndex + 1) % amountToPool;
        for (int i = 0; i < amountToPool; i++)
        {
            int currentIndex = (startIndex + i) % amountToPool;
            if (pooledObjects[i].activeInHierarchy)
            {
                lastUsedIndex = currentIndex;
                return pooledObjects[i];
            }
        }
        return null;
    }
}
