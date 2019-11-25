using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Manages the progress of the game. Tracks the number of enemies in the wave and progesses the 
 * game when all enemies in the wave are killed. Spawns in enemies and increases their numbers
 * based on the current wave. 
 */
public class EnemyManager : MonoBehaviour
{
    // Enemy prefab to be spawned
    public GameObject m_EnemyPrefab;
    // List of spawnpoints around the map
    public Transform[] m_SpawnPoints;
    // Time between enemy spawns
    public float m_TimeBetweenEnemies = 2f;
    // Current Wave number
    public int m_CurrentWave;

    // Game progression SFX
    public AudioSource m_GameSounds;
    public AudioClip m_WaveComplete;

    // Current Wave status
    private int m_TotalEnemiesInCurrentWave;
    private int m_EnemiesInWaveLeft;
    private int m_SpawnedEnemies;

    // Number of enemies to be spawned
    private int m_Enemies;

    // Starts game at wave #0 with 3 enemies
    private void Start()
    {
        m_CurrentWave = -1;
        m_Enemies = 2;

        StartNextWave();
    }

    // Starts the next wave
    private void StartNextWave()
    {
        // Plays SFX
        m_GameSounds.clip = m_WaveComplete;
        m_GameSounds.Play();

        // Increases wave and enemy count by x1.5
        m_CurrentWave++;
        m_Enemies = Mathf.RoundToInt(m_Enemies * 1.5f);
        
        // Sets wave status
        m_TotalEnemiesInCurrentWave = Mathf.RoundToInt(m_Enemies);
        m_EnemiesInWaveLeft = 0;
        m_SpawnedEnemies = 0;

        // Starts spawning enemies
        StartCoroutine(SpawnEnemies());
    }

    // Called at the start of a wave
    private IEnumerator SpawnEnemies()
    {
        // Slowly spawns enemies at random spawnpoints
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

    // When enemy is killed, check for enemies left and start new wave when 0
    public void EnemyDefeated()
    {
        m_EnemiesInWaveLeft--;

        if (m_EnemiesInWaveLeft == 0 && m_SpawnedEnemies == m_TotalEnemiesInCurrentWave)
        {
            StartNextWave();
        }
    }
}
