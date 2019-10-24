using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementChanger : MonoBehaviour
{
    private Blaster handRight;
    private Blaster handLeft;
    private string elementTag = null;
    private int elementID = 0;

    public void SetFire()
    {
        elementID = 1;
        elementTag = "Fire";
        SetElement();
    }

    public void SetWater()
    {
        elementID = 2;
        elementTag = "Water";
        SetElement();
    }

    public void SetWind()
    {
        elementID = 3;
        elementTag = "Wind";
        SetElement();
    }

    public void SetEarth()
    {
        elementID = 4;
        elementTag = "Earth";
        SetElement();
    }

    public void SetLightning()
    {
        elementID = 5;
        elementTag = "Lightning";
        SetElement();
    }

    public void SetIce()
    {
        elementID = 6;
        elementTag = "Ice";
        SetElement();
    }

    private void SetElement()
    {
        handRight = GameObject.Find("RightHand").GetComponent<Blaster>();
        handLeft = GameObject.Find("LeftHand").GetComponent<Blaster>();

        foreach (Projectile projectile in handRight.m_ProjectilePool.m_Projectiles)
        {
            projectile.tag = elementTag;
            handRight.m_CurrentElement = elementTag;
            handRight.ElementSFX(elementID);
        }

        foreach (Projectile projectile in handLeft.m_ProjectilePool.m_Projectiles)
        {
            projectile.tag = elementTag;
            handLeft.m_CurrentElement = elementTag;
            handRight.ElementSFX(elementID);
        }
    }
}