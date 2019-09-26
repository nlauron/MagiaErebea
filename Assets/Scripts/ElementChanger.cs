using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementChanger : MonoBehaviour
{
    public Material fireMaterial;
    public Material waterMaterial;
    public Material windMaterial;
    public Material earthMaterial;
    public Material lightningMaterial;
    public Material iceMaterial;
    private GameObject handRight;
    private GameObject handLeft;

    private int elementID = 0;

    public void SetFire()
    {
        SetElement(fireMaterial);
        elementID = 1;
    }

    public void SetWater()
    {
        SetElement(waterMaterial);
        elementID = 2;
    }

    public void SetWind()
    {
        SetElement(windMaterial);
        elementID = 3;
    }

    public void SetEarth()
    {
        SetElement(earthMaterial);
        elementID = 4;
    }

    public void SetLightning()
    {
        SetElement(lightningMaterial);
        elementID = 5;
    }

    public void SetIce()
    {
        SetElement(iceMaterial);
        elementID = 6;
    }

    private void SetElement(Material newElement)
    {
        handRight = GameObject.Find("RightHand");
        handLeft = GameObject.Find("LeftHand");

        handRight.GetComponent<Blaster>().ChangeElement(elementID);
        foreach (Projectile projectile in handRight.GetComponent<Blaster>().m_ProjectilePool.m_Projectiles)
        {
            projectile.GetComponent<MeshRenderer>().material = newElement;
           
        }

        handLeft.GetComponent<Blaster>().ChangeElement(elementID);
        foreach (Projectile projectile in handLeft.GetComponent<Blaster>().m_ProjectilePool.m_Projectiles)
            projectile.GetComponent<MeshRenderer>().material = newElement;
    }
}
