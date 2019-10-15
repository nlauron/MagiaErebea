using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
[System.Serializable]
public class Wave
{
    public int EnemiesPerWave;
    public GameObject EnemyPrefab;
}
*/

public class EnemyManager : MonoBehaviour
{
    // public Wave[] m_Waves;
    public GameObject m_EnemyPrefab;
    public Transform[] m_SpawnPoints;
    public float m_TimeBetweenEnemies = 2f;

    private int m_TotalEnemiesInCurrentWave;
    private int m_EnemiesInWaveLeft;
    private int m_SpawnedEnemies;

    private int m_CurrentWave;
    private int m_Enemies;
    // private int m_TotalWaves;

    private void Start()
    {
        m_CurrentWave = -1;
        m_Enemies = 4;
        //m_TotalWaves = m_Waves.Length - 1;

        StartNextWave();
    }

    private void StartNextWave()
    {
        m_CurrentWave++;

        //if (m_CurrentWave > m_TotalWaves)
        //  return;

        // m_TotalEnemiesInCurrentWave = m_Waves[m_CurrentWave].EnemiesPerWave;
        m_TotalEnemiesInCurrentWave = Mathf.RoundToInt(m_Enemies * 1.5f);
        m_EnemiesInWaveLeft = 0;
        m_SpawnedEnemies = 0;

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (m_SpawnedEnemies < m_TotalEnemiesInCurrentWave)
        {
            m_SpawnedEnemies++;
            m_EnemiesInWaveLeft++;

            int spawnPointIndex = Random.Range(0, m_SpawnPoints.Length);

            GameObject enemy;
            enemy = Instantiate(m_EnemyPrefab, m_SpawnPoints[spawnPointIndex].position, m_SpawnPoints[spawnPointIndex].rotation);
            enemy.GetComponent<Enemy>().m_ElementID = Random.Range(1, 7);
            yield return new WaitForSeconds(m_TimeBetweenEnemies);
        }

        yield return null;
    }

    public void EnemyDefeated()
    {
        m_EnemiesInWaveLeft--;

        if (m_EnemiesInWaveLeft == 0 && m_SpawnedEnemies == m_TotalEnemiesInCurrentWave)
        {
            StartNextWave();
        }
    }
}
