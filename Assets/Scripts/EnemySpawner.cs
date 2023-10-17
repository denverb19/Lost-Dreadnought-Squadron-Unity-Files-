using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveList;
    [SerializeField] float timeBetweenWaves = 0.1f;
    WaveConfigSO localWaveConfigSO;
    [SerializeField] bool isLooping = true;
    void Start()
    {
        localWaveConfigSO = waveList[0];
        StartCoroutine(SpawnEnemies());
    }
    public WaveConfigSO GetCurrentWave()
    {
        return localWaveConfigSO;
    }
    IEnumerator SpawnEnemies()
    {
        do
        {
            for (int j = 0; j < waveList.Count; j++)
            {
                localWaveConfigSO = waveList[j];
                Quaternion rotateForward = Quaternion.Euler(new Vector3(0,0,180));
                int localCount = localWaveConfigSO.GetEnemyCount();
                for (int i = 0; i < localCount; i++)
                {
                    Instantiate(localWaveConfigSO.GetEnemyPrefab(i),
                                localWaveConfigSO.GetStartingWaypoint().position,
                                rotateForward, transform);
                    yield return new WaitForSeconds(localWaveConfigSO.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        } while(isLooping);
    }
}
