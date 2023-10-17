using System.Text.RegularExpressions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

[CreateAssetMenu(menuName = "WaveConfigSO", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] Transform pathPrefab;
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float spawnDelay = 1f;
    [SerializeField] float spawnVariance = 0.5f;
    [SerializeField] float minSpawnTime = 0.25f;
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }
    public List<Transform> GetWaypoints()
    {
        List<Transform> pointsToReturn = new List<Transform>();
        foreach (Transform child in pathPrefab)
        {
            pointsToReturn.Add(child);
        }
        return pointsToReturn;
    }
    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }
    public GameObject GetEnemyPrefab(int desiredIndex)
    {
        return enemyPrefabs[desiredIndex];
    }
    public float GetRandomSpawnTime()
    {
        float timeToReturn = Random.Range(spawnDelay - spawnVariance, spawnDelay + spawnVariance);
        return Mathf.Clamp(timeToReturn, minSpawnTime, float.MaxValue);
    }
}