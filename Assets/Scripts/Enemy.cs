using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Color m_FlashDamageColor = Color.white;
    public int m_ElementID = 0;
    public AudioSource m_AudioSource;
    public AudioClip m_Hit;
    public AudioClip m_Miss;
    public AudioClip m_Dying;

    private GameObject m_Enemy;
    private Player m_Player;
    private EnemyManager m_Spawner;
    private Animator m_Anim = null;
    private SkinnedMeshRenderer m_MeshRenderer = null;
    private Color m_OriginalColor = Color.white;

    private int m_MaxHealth = 2;
    private int m_Health = 0;
    private int m_MoveSpeed = 4;
    private int m_MinimumDistance = 3;
    private float m_DeathTime = 2.3f;
    private bool m_Spawned = false;
    private bool m_Dead = false;

    private void Awake()
    {
        m_Player = GameObject.Find("Player").GetComponent<Player>();
        m_Spawner = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        m_Anim = GetComponent<Animator>();
        m_MeshRenderer = transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>();
    }

    private void Update()
    {
        if (!m_Spawned)
        {
            CheckElement();
        }

        //transform.LookAt(m_Player.transform);

        if (Vector3.Distance(transform.position, m_Player.transform.position) >= m_MinimumDistance)
        {

            m_Anim.SetInteger("Walk Condition", 1);
            m_Anim.SetInteger("Attack Condition", 0);
            transform.position += transform.forward * m_MoveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, m_Player.transform.position) <= m_MinimumDistance)
            {
                m_Anim.SetInteger("Walk Condition", 0);
                m_Anim.SetInteger("Attack Condition", 1);
            }
        }

        if (m_Anim.GetInteger("Attack Condition") == 1 && !m_Dead)
        {
            m_Player.Damage();
        }
    }

    private void OnEnable()
    {
        ResetHealth();
    }

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

    private void OnCollisionEnter(Collision collision)
    {
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

            else
            {
                m_AudioSource.clip = m_Miss;
                m_AudioSource.Play();
            }
        }
    }

    private void Damage()
    {
        m_AudioSource.clip = m_Hit;
        StopAllCoroutines();
        StartCoroutine(Flash());

        RemoveHealth();
    }

    private IEnumerator Flash()
    {
        m_AudioSource.Play();
        m_MeshRenderer.material.SetColor("_Color", m_FlashDamageColor);

        WaitForSeconds wait = new WaitForSeconds(0.2f);
        yield return wait;

        m_MeshRenderer.material.color = m_OriginalColor;
    }

    private void RemoveHealth()
    {
        m_Health--;
        CheckForDeath();
    }

    private void ResetHealth()
    {
        m_Health = m_MaxHealth;
    }

    private void CheckForDeath()
    {
        if (m_Health <= 0 && !m_Dead)
        {
            m_Dead = true;
            Kill();
        }
    }

    private void Kill()
    {
        m_AudioSource.clip = m_Dying;
        m_Player.m_Kills++;
        StartCoroutine(Death());
        m_Spawner.EnemyDefeated();
    }

    private IEnumerator Death()
    {
        m_AudioSource.Play(); 
        m_Anim.SetTrigger("Death Condition");
        yield return new WaitForSeconds(m_DeathTime);
        Destroy(gameObject);
    }
}