using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Color m_FlashDamageColor = Color.red;

    private GameObject m_Player;
    private EnemyManager m_Spawner;
    private Animator m_Anim = null;
    private SkinnedMeshRenderer m_MeshRenderer = null;
    private Color m_OriginalColor = Color.white;
    public int m_ElementID;

    private int m_MaxHealth = 2;
    private int m_Health = 0;
    private int m_MoveSpeed = 4;
    private int m_MinimumDistance = 2;

    private void Awake()
    {
        m_Player = GameObject.Find("Player");
        m_Spawner = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        m_Anim = GetComponent<Animator>();
        m_MeshRenderer = transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>();
        m_OriginalColor = transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().material.color;
    }

    private void Update()
    {
        transform.LookAt(m_Player.transform);

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

        if (m_Anim.GetInteger("Attack Condition") == 1)
        {

        }
    }

    private void OnEnable()
    {
        ResetHealth();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fire") && m_ElementID == 3)
            Damage();

        if (collision.gameObject.CompareTag("Water") && m_ElementID == 4)
            Damage();

        if (collision.gameObject.CompareTag("Ice") && m_ElementID == 5)
            Damage();

        if (collision.gameObject.CompareTag("Earth") && m_ElementID == 6)
            Damage();

        if (collision.gameObject.CompareTag("Wind") && m_ElementID == 1)
            Damage();

        if (collision.gameObject.CompareTag("Lightning") && m_ElementID == 2)
            Damage();
    }

    private void Damage()
    {
        StopAllCoroutines();
        StartCoroutine(Flash());

        RemoveHealth();
    }

    private IEnumerator Flash()
    {
        print("flashing");
        print(m_MeshRenderer.material.color);
        m_MeshRenderer.material.color = m_FlashDamageColor;
        print(m_MeshRenderer.material.color);

        WaitForSeconds wait = new WaitForSeconds(1f);
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
        if (m_Health <= 0)
            Kill();
    }

    private void Kill()
    {
        Destroy(gameObject);
        m_Spawner.EnemyDefeated();
    }
}