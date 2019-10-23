using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int m_Health;
    public int m_Kills;
    public float m_IFrame = 1.0f;
    public Canvas m_HUD;
    public AudioSource m_PlayerSounds;
    public AudioClip m_HurtSFX;

    private bool m_GameOver = false;
    private bool m_Invincible = false;

    private void Awake()
    {
        m_Health = 3;
        m_Kills = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_GameOver)
            GameOver();
    }

    public void Damage()
    {
        if (!m_Invincible)
        {
            m_PlayerSounds.clip = m_HurtSFX;
            m_PlayerSounds.Play();
            m_Health--;
            CheckHealth();
            StartCoroutine(Invulnerable());
        }
    }

    public IEnumerator Invulnerable()
    {
        m_Invincible = true;
        yield return new WaitForSeconds(m_IFrame);
        m_Invincible = false;   
    }

    private bool CheckHealth()
    {
        if (m_Health <= 0)
            m_GameOver = true;
        else
            return m_GameOver;

        return m_GameOver;
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
