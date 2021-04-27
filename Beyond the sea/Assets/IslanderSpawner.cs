using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class IslanderSpawner : MonoBehaviour
{
    private List<Transform> spawnLocations;
    [SerializeField] private List<GameObject> islanderToSpawn;
    [SerializeField] private List<GameObject> tentsToSpawn;

    [SerializeField] private Personality[] types; 

  

    private void Start()
    {

        spawnLocations = new List<Transform>();

        // gettting direct children rather than all decendants. 
        foreach (Transform child in transform)
        {
            spawnLocations.Add(child.GetComponent<Transform>());
        }
        
        if (spawnLocations !=null)
        {
            GenerateIsland();
        }
    }

    private void GenerateIsland()
    {
        if (islanderToSpawn.Count == 0) return;
        foreach (var person in islanderToSpawn)
        {
            if (spawnLocations.Count == 0) return; // if there are no locations to spawn at dont spawn. 
            var locId = Random.Range(0, spawnLocations.Count); // pick random location from list
            var location = spawnLocations[locId];
            var position = location.position;
            var islander = Instantiate(person,position,quaternion.identity,location);
           
            var color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            var stats = islander.GetComponent<Islander>();
                stats.SetShirtColor(color);
                stats._islanderData.personalityType = types[Random.Range(0, types.Length)];
            var tentLoc = location.GetChild(0);
            var tentID = Random.Range(0, tentsToSpawn.Count);
            var tent = Instantiate(tentsToSpawn[tentID],tentLoc.position, quaternion.identity, tentLoc);

            tent.GetComponentInChildren<MeshRenderer>().material.color = color; 
            spawnLocations.RemoveAt(locId); // remove location from possible spawn locations
        }
    }
}
