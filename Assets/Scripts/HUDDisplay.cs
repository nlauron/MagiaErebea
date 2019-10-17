using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDDisplay : MonoBehaviour
{
    public Player m_Player;
    public EnemyManager m_EnemyManager;
    public GameObject m_Hearts;
    public Text m_KillCount;
    public Text m_CurrentWave;
    public Sprite m_Full;
    public Sprite m_Empty;
 
    private Image[] hearts;
    private int currentHealth;
    private int damage;

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

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        UpdateKiilCount();
        UpdateWave();
    }

    private void CheckHealth()
    {
        if (m_Player.m_Health < currentHealth)
        {
            currentHealth--;
            damage++;
            LoseHealth();
        }
    }

    private void LoseHealth()
    {
        hearts[damage].sprite = m_Empty;
    }

    private void UpdateKiilCount()
    {
        m_KillCount.text = m_Player.m_Kills.ToString() + " Kills";
    }

    private void UpdateWave()
    {
        m_CurrentWave.text = "Wave: " + m_EnemyManager.m_CurrentWave.ToString();
    }
}
