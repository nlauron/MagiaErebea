using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

/**
 * Player
 * 
 * The player controlled by the user in the GameScene. Mainly tracks health and 
 * score. If health is depleted, player is killed and it is Game Over.
 */
public class Player : MonoBehaviour
{
    // Player Health
    public int m_Health;
    // Player Score
    public int m_Kills;
    // Invulnerability window when damaged
    public float m_IFrame = 1.0f;
    // Visual representation of player info
    public Canvas m_HUD;

    // Player SFX
    public AudioSource m_PlayerSounds;
    public AudioClip m_HurtSFX;
    public AudioClip m_Death;

    // Scene transition fade time
    public float m_FadeTime = 9.0f;

    // Player status
    private bool dead = false;
    private bool invincible = false;

    // Initializes player at start of game with 3 Health points and a score of 0
    private void Awake()
    {
        m_Health = 3;
        m_Kills = 0;
    }

    // When damaged, play SFX, decrease health and become temporarily invulnerable
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
    
    // Prevents player from taking damage temporarily
    public IEnumerator Invulnerable()
    {
        invincible = true;
        yield return new WaitForSeconds(m_IFrame);
        invincible = false;   
    }

    // Checks if health is depleted for Game Over
    private void CheckHealth()
    {
        if (m_Health <= 0)
            StartCoroutine(GameOver());
    }

    // If dead, fades scene out and transitions to GameOver scene
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
