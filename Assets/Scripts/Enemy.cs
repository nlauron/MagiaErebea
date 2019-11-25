using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Enemy
 * 
 * Players main adversaries. When spawned, each enemy is randomly given their own element. Has
 * two health points and decreases when hit by a correct element from the players spells. Is
 * able to damage player when in range. When health is depleted, enemy is killed and recorded
 * in players kill count and progresses the wave.
 */
public class Enemy : MonoBehaviour
{
    // Color when hit by correct element
    public Color m_FlashDamageColor = Color.white;
    // Enemy element
    public int m_ElementID = 0;

    // Enemy SFX
    public AudioSource m_AudioSource;
    public AudioClip m_Hit;
    public AudioClip m_Miss;
    public AudioClip m_Dying;

    // Reference to player
    private Player m_Player;
    // Enemy GameObject References
    private GameObject m_Enemy;
    private EnemyManager m_Spawner;
    private Animator m_Anim = null;
    private SkinnedMeshRenderer m_MeshRenderer = null;
    private Color m_OriginalColor = Color.white;

    // Enemy Settings
    private int m_MaxHealth = 2;
    private int m_Health = 0;
    private int m_MoveSpeed = 4;
    private int m_MinimumDistance = 3;
    private float m_DeathTime = 2.3f;
    private bool m_Spawned = false;
    private bool m_Dead = false;

    // Initialized when an enemy is spawned
    private void Awake()
    {
        // Finds player
        m_Player = GameObject.Find("Player").GetComponent<Player>();
        // Available spawn points for enemys
        m_Spawner = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        // Enemy Animator
        m_Anim = GetComponent<Animator>();
        // Enemy MeshRenderer
        m_MeshRenderer = transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>();
    }

    // Checking for element and animator status
    private void Update()
    {
        // Once spawned, sets element
        if (!m_Spawned)
        {
            CheckElement();
        }

        // Animates enemy based on distance from player.
        // Sets to Walking when away from player
        if (Vector3.Distance(transform.position, m_Player.transform.position) >= m_MinimumDistance)
        {
            m_Anim.SetInteger("Walk Condition", 1);
            m_Anim.SetInteger("Attack Condition", 0);
            transform.position += transform.forward * m_MoveSpeed * Time.deltaTime;

            // Sets to Attacking when in contact with player
            if (Vector3.Distance(transform.position, m_Player.transform.position) <= m_MinimumDistance)
            {
                m_Anim.SetInteger("Walk Condition", 0);
                m_Anim.SetInteger("Attack Condition", 1);
            }
        }

        // If Attacking, damage player
        if (m_Anim.GetInteger("Attack Condition") == 1 && !m_Dead)
        {
            m_Player.Damage();
        }
    }

    // Sets Health back to full
    private void OnEnable()
    {
        ResetHealth();
    }

    // Sets renderer material and its original color based on its element when spawned
    private void CheckElement()
    {
        switch (m_ElementID)
        {
            case 1:
                m_MeshRenderer.material.SetColor("_Color", Color.red);
                m_OriginalColor = m_MeshRenderer.material.color;
                break;
            case 2:
                m_MeshRenderer.material.SetColor("_Color", Color.blue);
                m_OriginalColor = m_MeshRenderer.material.color;
                break;
            case 3:
                m_MeshRenderer.material.SetColor("_Color", Color.cyan);
                m_OriginalColor = m_MeshRenderer.material.color;
                break;
            case 4:
                m_MeshRenderer.material.SetColor("_Color", Color.yellow);
                m_OriginalColor = m_MeshRenderer.material.color;
                break;
            case 5:
                m_MeshRenderer.material.SetColor("_Color", Color.green);
                m_OriginalColor = m_MeshRenderer.material.color;
                break;
            case 6:
                m_MeshRenderer.material.SetColor("_Color", Color.magenta);
                m_OriginalColor = m_MeshRenderer.material.color;
                break;
        }

        m_Spawned = true;
    }

    // Detects when hit with an elemental projectile
    private void OnCollisionEnter(Collision collision)
    {
        // If not dead, damage if correct element
        if (!m_Dead)
        {
            if (collision.gameObject.CompareTag("Fire") && m_ElementID == 3)
                Damage();

            else if (collision.gameObject.CompareTag("Water") && m_ElementID == 4)
                Damage();

            else if (collision.gameObject.CompareTag("Ice") && m_ElementID == 5)
                Damage();

            else if (collision.gameObject.CompareTag("Earth") && m_ElementID == 6)
                Damage();

            else if (collision.gameObject.CompareTag("Wind") && m_ElementID == 1)
                Damage();

            else if (collision.gameObject.CompareTag("Lightning") && m_ElementID == 2)
                Damage();

            // If incorrect element, play miss SFX
            else
            {
                m_AudioSource.clip = m_Miss;
                m_AudioSource.Play();
            }
        }
    }

    // Called when hit by correct element
    private void Damage()
    {
        // Play hit SFX and flash to indicate damage
        m_AudioSource.clip = m_Hit;
        StopAllCoroutines();
        StartCoroutine(Flash());

        // Decrease health
        RemoveHealth();
    }

    // Changes color to indicate damage when hit by correct element temporarily
    private IEnumerator Flash()
    {
        m_AudioSource.Play();
        m_MeshRenderer.material.SetColor("_Color", m_FlashDamageColor);

        WaitForSeconds wait = new WaitForSeconds(0.2f);
        yield return wait;

        m_MeshRenderer.material.color = m_OriginalColor;
    }

    // Decrease health and check for death
    private void RemoveHealth()
    {
        m_Health--;
        CheckForDeath();
    }

    // Set health back to full
    private void ResetHealth()
    {
        m_Health = m_MaxHealth;
    }

    // If health is depleted, enemy is killed and cannot be interacted with
    private void CheckForDeath()
    {
        if (m_Health <= 0 && !m_Dead)
        {
            m_Dead = true;
            Kill();
        }
    }

    // Called when killed
    private void Kill()
    {
        // Sets death SFX
        m_AudioSource.clip = m_Dying;
        // Increase player score
        m_Player.m_Kills++;
        // Death coroutine
        StartCoroutine(Death());
        // Tells spawner enemy has been killed
        m_Spawner.EnemyDefeated();
    }

    // Plays Death SFX, animation and destroys gameobject
    private IEnumerator Death()
    {
        m_AudioSource.Play(); 
        m_Anim.SetTrigger("Death Condition");
        yield return new WaitForSeconds(m_DeathTime);
        Destroy(gameObject);
    }
}