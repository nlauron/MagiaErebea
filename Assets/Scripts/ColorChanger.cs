using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Material fireMaterial;
    public Material waterMaterial;
    public Material windMaterial;
    public Material earthMaterial;
    public Material lightningMaterial;
    public Material iceMaterial;
    public GameObject projectile;
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
        MeshRenderer renderer = projectile.GetComponent<MeshRenderer>();
        renderer.material = newElement;

        handRight = GameObject.Find("RightHand");
        handLeft = GameObject.Find("LeftHand");
        // handRight.GetComponent<Blaster>().ChangeElement(elementID);
        // handLeft.GetComponent<Blaster>().ChangeElement(elementID);
    }
}
