using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class Blaster : MonoBehaviour
{
    // INPUT
    public SteamVR_Action_Boolean m_FireAction = null;
    // public SteamVR_Action_Boolean m_ReloadAction = null;

    // SETTINGS
    public int m_Force = 10;
    public int m_MaxProjecileCount = 5;
    public float m_ReloadTime = 0.2f;
    public string m_CurrentElement;

    // REFERENCES
    public Transform m_Barrel = null;
    public GameObject m_ProjectilePrefab = null;
    public Text m_AmmoOutput = null;
    public ProjectilePool m_ProjectilePool = null;

    private bool m_IsReloading = false;
    private int m_FiredCount = 0;

    private SteamVR_Behaviour_Pose m_Pose = null;
    private Animator m_Animator = null;

    private void Awake()
    {
        m_Pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
        m_Animator = GetComponent<Animator>();

        m_CurrentElement = "Fire";
        m_ProjectilePrefab.tag = m_CurrentElement;
        m_ProjectilePool = new ProjectilePool(m_ProjectilePrefab, m_MaxProjecileCount);
    }

    private void Start()
    {
        UpdateFiredCount(0);
    }

    private void Update()
    {
        m_ProjectilePrefab.tag = m_CurrentElement;
        if (m_IsReloading)
            return;

        if (m_FireAction.GetLastStateDown(m_Pose.inputSource))
        {
            Fire();
        }

        if (m_FiredCount == m_MaxProjecileCount)
            StartCoroutine(Reload());

    }

    private void Fire()
    {
        if (m_FiredCount >= m_MaxProjecileCount)
            return;

        Projectile targetProjectile = m_ProjectilePool.m_Projectiles[m_FiredCount];
        targetProjectile.Launch(this);

        UpdateFiredCount(m_FiredCount + 1);
    }

    private IEnumerator Reload()
    {
        m_AmmoOutput.text = "-";
        m_IsReloading = true;

        yield return new WaitForSeconds(1.0f);

        m_ProjectilePool.SetAllProjectiles();
        m_ProjectilePool = new ProjectilePool(m_ProjectilePrefab, m_MaxProjecileCount);

        yield return new WaitForSeconds(m_ReloadTime);

        UpdateFiredCount(0);
        m_IsReloading = false;
        m_ProjectilePrefab.tag = m_CurrentElement;

    }

    private void UpdateFiredCount(int newValue)
    {
        m_FiredCount = newValue;
        m_AmmoOutput.text = (m_MaxProjecileCount - m_FiredCount).ToString();
    }

    /*
    public void ChangeElement(int newElement)
    {
        switch (newElement)
        {
            case 1:
                m_ProjectilePrefab.tag = "Fire";
                StartCoroutine(Reload());
                break;
            case 2:
                m_ProjectilePrefab.tag = "Water";
                StartCoroutine(Reload());
                break;
            case 3:
                m_ProjectilePrefab.tag = "Wind";
                StartCoroutine(Reload());
                break;
            case 4:
                m_ProjectilePrefab.tag = "Earth";
                StartCoroutine(Reload());
                break;
            case 5:
                m_ProjectilePrefab.tag = "Lightning";
                StartCoroutine(Reload());
                break;
            case 6:
                m_ProjectilePrefab.tag = "Ice";
                StartCoroutine(Reload());
                break;
        }
    }
    */
}
