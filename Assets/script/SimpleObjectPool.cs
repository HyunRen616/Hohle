using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObjectPool : MonoBehaviour
{
    [SerializeField] GameObject objectPrefab;
    [SerializeField] int poolSisze;
    [SerializeField] bool expandable;
    int objectIndex;

    List<GameObject> usedObjects = new List<GameObject>();
    List<GameObject> freeObjects = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < poolSisze; i++)
        {
            GenerateNewObjects();
        }
    }

    public GameObject GetObject()
    {
        int totalFree = freeObjects.Count;
        if (totalFree == 0 && !expandable) return null;
        else if (totalFree == 0)
        {
            GenerateNewObjects();
        }

        GameObject obj = freeObjects[totalFree - 1];
        freeObjects.RemoveAt(totalFree - 1);
        usedObjects.Add(obj);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        usedObjects.Remove(obj);
        freeObjects.Add(obj);
    }

    private void GenerateNewObjects()
    {
        GameObject obj = Instantiate(objectPrefab);
        obj.transform.parent = transform;
        obj.SetActive(false);
        freeObjects.Add(obj);
    }
}