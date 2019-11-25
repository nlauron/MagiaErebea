using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

/**
 * Blaster
 * 
 * Player's Hands, each hand is given their own element and is able to launch projectiles
 * based on their current element.
 */
public class Blaster : MonoBehaviour
{
    // Holds the elemental projectile prefab
    [System.Serializable]
    public struct ElementProjectilePair
    {
        public string element;
        public GameObject projectile;
    }

    // The list of elements available
    public ElementProjectilePair[] elements;

    // Controller Input
    public SteamVR_Action_Boolean m_FireAction = null;

    // Blaster Settings
    public int m_Force = 200;
    public string m_CurrentElement;

    // Blaster References
    public Transform m_Barrel = null;
    public GameObject m_ProjectilePrefab = null;

    // SFX
    public AudioSource m_SpellSounds;
    public AudioClip m_FireSpell;
    public AudioClip m_WaterSpell;
    public AudioClip m_WindSpell;
    public AudioClip m_EarthSpell;
    public AudioClip m_LightningSpell;
    public AudioClip m_IceSpell;

    // Controller tracking
    private SteamVR_Behaviour_Pose m_Pose = null;
    private Animator m_Animator = null;

    // Initializes at the start of the game
    private void Awake()
    {
        // Initializes the hands/blasters position and anmations
        m_Pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
        m_Animator = GetComponent<Animator>();

        // Default element set to Fire
        m_CurrentElement = "Fire";
        m_ProjectilePrefab.tag = m_CurrentElement;
    }

    // Checks for input, shoot spell when shoot button is pressed
    private void Update()
    {
        if (m_FireAction.GetLastStateDown(m_Pose.inputSource))
        {
            Fire();
        }
    }

    // Initializes projectile and sets element and tag then launches
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

    // Sets and plays SFX according to current element
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
