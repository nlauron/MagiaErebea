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
    private bool[] damaged;

    private void Awake()
    {
        m_EnemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        hearts = new Image[m_Hearts.transform.childCount];
        damaged = new bool[m_Hearts.transform.childCount];
        for (int i = 0; i < m_Hearts.transform.childCount; i++)
        {
            hearts[i] = m_Hearts.transform.GetChild(i).GetComponent<Image>();
            hearts[i].sprite = m_Empty;
            damaged[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckDamage();
        UpdateKiilCount();
        UpdateWave();
    }

    private void CheckDamage()
    {
        for (int i = 0; i < m_Player.m_Health; i++)
        {
            hearts[i].sprite = m_Full;
        }
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
