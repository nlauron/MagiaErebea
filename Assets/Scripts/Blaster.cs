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
    public int m_Force = 200;
    public string m_CurrentElement;

    // REFERENCES
    public Transform m_Barrel = null;
    public GameObject m_ProjectilePrefab = null;

    public AudioSource m_SpellSounds;
    public AudioClip m_FireSpell;
    public AudioClip m_WaterSpell;
    public AudioClip m_WindSpell;
    public AudioClip m_EarthSpell;
    public AudioClip m_LightningSpell;
    public AudioClip m_IceSpell;

    private SteamVR_Behaviour_Pose m_Pose = null;
    private Animator m_Animator = null;

    private void Awake()
    {
        m_Pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
        m_Animator = GetComponent<Animator>();

        m_CurrentElement = "Fire";
        m_ProjectilePrefab.tag = m_CurrentElement;
    }

    private void Update()
    {
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
        projectileObject.tag = m_CurrentElement;
        Projectile targetProjectile = projectileObject.GetComponent<Projectile>();
        targetProjectile.Launch(this);
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
