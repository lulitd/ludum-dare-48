using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Collider areaToSpawn;
    [SerializeField] private Transform player;
    [SerializeField] private float playerRadius; 
    [SerializeField] private int poolLimit;
    [SerializeField] private List<GameObject> prefabs; 
    private List<GameObject> activeObjects;
    private Queue<GameObject> objectPool;

    [SerializeField] private int AmountToSpawn = 10; 
    // Start is called before the first frame update
    private void Start()
    {
        objectPool = new Queue<GameObject>(poolLimit);
        activeObjects = new List<GameObject>(poolLimit);

        for (var i = 0; i < poolLimit; i++)
        {
            var o = GameObject.Instantiate(prefabs[Random.Range(0, prefabs.Count)],Vector3.one*-1000,Quaternion.identity, this.transform);
            o.SetActive(false);
            objectPool.Enqueue(o);
        }
    }

    public void CleanUpItems()
    {

        while (activeObjects.Count > 0)
        {
            var item = activeObjects[0];
            activeObjects.RemoveAt(0);
            
            item.SetActive(false);
            objectPool.Enqueue(item);
        }
        
    }

    public void SpawnItems()
    {
        var i = 0;
    
        while (i < AmountToSpawn && objectPool.Count > 0)
        {
            i++;
            
            var dir = (Random.insideUnitSphere * playerRadius);
            var pos =  new Vector3(dir.x, dir.y)+ player.position;
             var g = objectPool.Dequeue();

             if (!areaToSpawn.bounds.Contains(pos))
             {
                 pos = areaToSpawn.ClosestPoint(pos);
             }
             
             g.transform.SetPositionAndRotation(pos, quaternion.identity);
             g.SetActive(true);
             activeObjects.Add(g);
             var pool = g.AddComponent<PooledItem>();
             pool.SetPool(this);
        }

    }

    public void DeactiveObject(GameObject gameObject)
    {
        var id = activeObjects.FindIndex(g => g.GetInstanceID() == gameObject.GetInstanceID());

        if (id != -1)
        {
            var item = activeObjects[id];
            activeObjects.RemoveAt(id);
            
            item.SetActive(false);
            objectPool.Enqueue(item);
        }
    }

}
