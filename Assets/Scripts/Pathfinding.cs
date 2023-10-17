using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    EnemySpawner localEnemySpawner;
    WaveConfigSO localWaveConfigSO;
    int currentIndex = 0;
    List<Transform> localWaypoints;
    void Awake()
    {
        localEnemySpawner = FindObjectOfType<EnemySpawner>();
    }
    void Start()
    {
        localWaveConfigSO = localEnemySpawner.GetCurrentWave();
        localWaypoints = localWaveConfigSO.GetWaypoints();
        transform.position = localWaypoints[currentIndex].position;
    }
    void Update()
    {
        FollowPath();
    }
    void FollowPath()
    {
        if (currentIndex < localWaypoints.Count)
        {
            Vector3 targetPosition = localWaypoints[currentIndex].position;
            float localDelta = localWaveConfigSO.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, localDelta);
            if (transform.position == targetPosition)
            {
                currentIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
