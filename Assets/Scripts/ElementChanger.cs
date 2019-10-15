using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementChanger : MonoBehaviour
{
    private GameObject handRight;
    private GameObject handLeft;
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
        print("Setting Element");
        print(elementTag);
        print(elementID);
        handRight = GameObject.Find("RightHand");
        handLeft = GameObject.Find("LeftHand");

        //handRight.GetComponent<Blaster>().ChangeElement(elementID);
        foreach (Projectile projectile in handRight.GetComponent<Blaster>().m_ProjectilePool.m_Projectiles)
        {
            projectile.tag = elementTag;
            handRight.GetComponent<Blaster>().m_CurrentElement = elementTag;
        }

        //andLeft.GetComponent<Blaster>().ChangeElement(elementID);
        foreach (Projectile projectile in handLeft.GetComponent<Blaster>().m_ProjectilePool.m_Projectiles)
        {
            projectile.tag = elementTag;
            handLeft.GetComponent<Blaster>().m_CurrentElement = elementTag;
        }
    }
}