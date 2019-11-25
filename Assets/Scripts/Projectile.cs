using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Projectile 
 * 
 *  Spells fired from the players hands. Damages enemies that come into contact
 *  with them if they are the correct element. 
 */
public class Projectile : MonoBehaviour
{
    // Lifetime of projectile after being fired
    public float m_Lifetime = 5.0f;

    // Projectile SFX
    public AudioSource m_AudioSource;
    public AudioClip m_SpellSFX;

    private Rigidbody m_Rigidbody = null;

    // Deactivates projectile when spawned
    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        SetInnactive();
    }

    // Destroys projectile on contact with anything aside from the player themself
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
            Destroy(gameObject);
    }

    // Activates and fires projectile from blaster barrel
    public void Launch(Blaster blaster)
    {
        // Set postion to blaster barrel (front of hand)
        transform.position = blaster.m_Barrel.position;
        transform.rotation = blaster.m_Barrel.rotation;

        // Activates projectile
        gameObject.SetActive(true);

        // Projectile SFX
        m_AudioSource.clip = m_SpellSFX;
        m_AudioSource.Play();

        // Fires projectile and tracks lifetime
        m_Rigidbody.AddRelativeForce(Vector3.forward * blaster.m_Force, ForceMode.Impulse);
        StartCoroutine(TrackLifetime());
    }

    // Destroys projectile if when lifetime expires
    private IEnumerator TrackLifetime()
    {
        yield return new WaitForSeconds(m_Lifetime);
        Destroy(gameObject);
    }

    // Projectile is deactivated and hidden until fired
    public void SetInnactive()
    {
        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.angularVelocity = Vector3.zero;

        gameObject.SetActive(false);
    }

    // When a new element is set, destroys projectile to set the new elemental projectile prefab
    public void ResetElement()
    {
        Destroy(gameObject);
    }

}
