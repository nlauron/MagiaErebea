using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class Player : MonoBehaviour
{
    public int m_Health;
    public int m_Kills;
    public float m_IFrame = 1.0f;
    public Canvas m_HUD;
    public AudioSource m_PlayerSounds;
    public AudioClip m_HurtSFX;
    public AudioClip m_Death;
    public float m_FadeTime = 9.0f;

    private bool dead = false;
    private bool invincible = false;

    private void Awake()
    {
        m_Health = 3;
        m_Kills = 0;
    }

    public void Damage()
    {
        if (!invincible && !dead)
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
        invincible = true;
        yield return new WaitForSeconds(m_IFrame);
        invincible = false;   
    }

    private void CheckHealth()
    {
        if (m_Health <= 0)
            StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        dead = true;
        m_PlayerSounds.clip = m_Death;
        m_PlayerSounds.Play();
        SteamVR_Fade.Start(Color.black, m_FadeTime, true);

        yield return new WaitForSeconds(m_FadeTime);
        SceneManager.LoadScene("GameOver");
    }
}
