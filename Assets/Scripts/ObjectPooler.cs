using UnityEngine;
using System.Collections.Generic;
public class ObjectPooler : MonoBehaviour
{
    [SerializeField]private GameObject prefab;
    [SerializeField] private int poolSize = 5;
    private List<GameObject> pool;

    private void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
         pool= new List<GameObject>();
        for(int i=0; i<poolSize; i++)
        {
            CreateNewObject();  
        }


    }
    private GameObject CreateNewObject()
    {
        GameObject obj = Instantiate(prefab,transform);
        obj.SetActive(false);
        pool.Add(obj);
        return obj; 
    }
    public GameObject GetPooledObject()
    {
        foreach(GameObject obj in pool)
        {
            if(!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return CreateNewObject();
    }
}
