using UnityEngine;
using System.Collections.Generic;
public class ObjectSpawner : MonoBehaviour
{
    [Header("Position Points")]
    [SerializeField] private Transform Upedge;
    [SerializeField] private Transform Downedge;

    [SerializeField] public int ObjectsSpawnerIndex;
    [SerializeField] private List<Wave> waves;





    [System.Serializable]
    public class Wave
    {
       
        public ObjectPooler objectPooler;
        [SerializeField] public float SpawnCounter;
        [SerializeField] public float Interval;
        [SerializeField] public int ObjectsNumber;
        [SerializeField] public int MaxObject;

    }

    private void Update()
    {

        waves[ObjectsSpawnerIndex].SpawnCounter -= GameManager.instance.adjustedWorldSpeed;
        if (waves[ObjectsSpawnerIndex].SpawnCounter <= 0)
        {
            waves[ObjectsSpawnerIndex].SpawnCounter += waves[ObjectsSpawnerIndex].Interval;
            ObjectsSpawner();


        }
        if(waves[ObjectsSpawnerIndex].ObjectsNumber >= waves[ObjectsSpawnerIndex].MaxObject)
        {
            waves[ObjectsSpawnerIndex].ObjectsNumber = 0;
            ObjectsSpawnerIndex ++;
        }
        if(ObjectsSpawnerIndex >= waves.Count)
        {
            ObjectsSpawnerIndex =0;
        }
    }


    private void ObjectsSpawner()
    {
       
        GameObject obj = waves[ObjectsSpawnerIndex].objectPooler.GetPooledObject();
        obj.transform.position = RandomRepawnPoint();
        
        obj.SetActive(true);
        waves[ObjectsSpawnerIndex].ObjectsNumber++;
    }

    private Vector2 RandomRepawnPoint()
    {
        Vector2 randompoint;
        randompoint.x = Upedge.position.x;
        randompoint.y = Random.Range(Upedge.position.y, Downedge.position.y);
        return randompoint;


    }
}

