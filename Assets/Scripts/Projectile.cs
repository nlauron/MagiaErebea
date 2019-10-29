using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float m_Lifetime = 5.0f;

    public AudioSource m_AudioSource;
    public AudioClip m_SpellSFX;

    private Rigidbody m_Rigidbody = null;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        SetInnactive();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
            Destroy(gameObject);
                //SetInnactive();
    }

    public void Launch(Blaster blaster)
    {
        // Postion
        transform.position = blaster.m_Barrel.position;
        transform.rotation = blaster.m_Barrel.rotation;

        // Activate
        gameObject.SetActive(true);

        m_AudioSource.clip = m_SpellSFX;
        m_AudioSource.Play();

        // Fire and Track
        m_Rigidbody.AddRelativeForce(Vector3.forward * blaster.m_Force, ForceMode.Impulse);
        StartCoroutine(TrackLifetime());
    }

    private IEnumerator TrackLifetime()
    {
        yield return new WaitForSeconds(m_Lifetime);
        SetInnactive();
    }

    public void SetInnactive()
    {
        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.angularVelocity = Vector3.zero;

        gameObject.SetActive(false);
    }

    public void ResetElement()
    {
        Destroy(gameObject);
    }

}
