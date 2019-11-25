using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * HUDDisplay
 * 
 * Displays player info
 */
public class HUDDisplay : MonoBehaviour
{
    // HUD References
    public Player m_Player;
    public EnemyManager m_EnemyManager;

    // Visual representation of player health
    public GameObject m_Hearts;
    // Player score
    public Text m_KillCount;
    // Game progression, current wave
    public Text m_CurrentWave;
    // Heart status when undamaged
    public Sprite m_Full;
    // Heart status when damaged
    public Sprite m_Empty;
 
    // Hearts based on health
    private Image[] hearts;
    private int currentHealth;
    private int damage;

    // Initializes at the start of the game
    private void Awake()
    {
        currentHealth = 3;
        damage = -1;
        m_EnemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        hearts = new Image[m_Hearts.transform.childCount];
        for (int i = 0; i < m_Hearts.transform.childCount; i++)
        {
            hearts[i] = m_Hearts.transform.GetChild(i).GetComponent<Image>();
            hearts[i].sprite = m_Full;
        }
    }

    // Checks player health, score and progression
    void Update()
    {
        CheckHealth();
        UpdateKiilCount();
        UpdateWave();
    }

    // When damaged, changes heart staus based on health
    private void CheckHealth()
    {
        if (m_Player.m_Health < currentHealth)
        {
            currentHealth--;
            damage++;
            LoseHealth();
        }
    }

    // Change heart sprite from full to empty
    private void LoseHealth()
    {
        if (m_Player.m_Health >= 0)
            hearts[damage].sprite = m_Empty;
    }

    // Tracks player score
    private void UpdateKiilCount()
    {
        m_KillCount.text = m_Player.m_Kills.ToString() + " Kills";
    }

    // Tracks game progression based on waves
    private void UpdateWave()
    {
        m_CurrentWave.text = "Wave: " + m_EnemyManager.m_CurrentWave.ToString();
    }
}
