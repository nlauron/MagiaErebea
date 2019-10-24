using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject m_EnemyPrefab;
    public Transform[] m_SpawnPoints;
    public float m_TimeBetweenEnemies = 2f;
    public int m_CurrentWave;

    public AudioSource m_GameSounds;
    public AudioClip m_WaveComplete;

    private int m_TotalEnemiesInCurrentWave;
    private int m_EnemiesInWaveLeft;
    private int m_SpawnedEnemies;

    private int m_Enemies;

    private void Start()
    {
        m_CurrentWave = -1;
        m_Enemies = 2;

        StartNextWave();
    }

    private void StartNextWave()
    {
        m_GameSounds.clip = m_WaveComplete;
        m_GameSounds.Play();

        m_CurrentWave++;
        m_Enemies = Mathf.RoundToInt(m_Enemies * 1.5f);
        
        m_TotalEnemiesInCurrentWave = Mathf.RoundToInt(m_Enemies);
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
