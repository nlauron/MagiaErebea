using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class Blaster : MonoBehaviour
{
    [System.Serializable]
    public struct ElementProjectilePair
    {
        public string element;
        public GameObject projectile;
    }

    public ElementProjectilePair[] elements;

    // INPUT
    public SteamVR_Action_Boolean m_FireAction = null;

    // SETTINGS
    public int m_Force = 100;
    public int m_MaxProjecileCount = 5;
    public float m_ReloadTime = 0.2f;
    public string m_CurrentElement;

    // REFERENCES
    public Transform m_Barrel = null;
    public GameObject m_ProjectilePrefab = null;
    public Text m_AmmoOutput = null;

    public AudioSource m_SpellSounds;
    public AudioClip m_FireSpell;
    public AudioClip m_WaterSpell;
    public AudioClip m_WindSpell;
    public AudioClip m_EarthSpell;
    public AudioClip m_LightningSpell;
    public AudioClip m_IceSpell;

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
    }

    private void Fire()
    {
        GameObject projectileObject = null;

        foreach (var element in elements)
        {
            if (element.element == m_CurrentElement)
            {
                projectileObject = Instantiate(element.projectile);
            }
        }

        Projectile targetProjectile = projectileObject.GetComponent<Projectile>();
        targetProjectile.Launch(this);

        UpdateFiredCount(m_FiredCount + 1);
    }

    private void UpdateFiredCount(int newValue)
    {
        m_FiredCount = newValue;
        m_AmmoOutput.text = (m_MaxProjecileCount - m_FiredCount).ToString();
    }

    public void ElementSFX(int elementID)
    {
        switch (elementID)
        {
            case 1:
                m_SpellSounds.clip = m_FireSpell;
                m_SpellSounds.Play();
                break;
            case 2:
                m_SpellSounds.clip = m_WaterSpell;
                m_SpellSounds.Play();
                break;
            case 3:
                m_SpellSounds.clip = m_WindSpell;
                m_SpellSounds.Play();
                break;
            case 4:
                m_SpellSounds.clip = m_EarthSpell;
                m_SpellSounds.Play();
                break;
            case 5:
                m_SpellSounds.clip = m_LightningSpell;
                m_SpellSounds.Play();
                break;
            case 6:
                m_SpellSounds.clip = m_IceSpell;
                m_SpellSounds.Play();
                break;
        }
    }
}
