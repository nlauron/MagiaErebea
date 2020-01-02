using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Manages elements from the radial menu. When element is selected, sends element
 * info to the players hands to set their new element.
 */
public class ElementChanger : MonoBehaviour
{
    // Refernces to both hands
    private Blaster handRight;
    private Blaster handLeft;
    
    // Element name and ID
    private string elementTag = null;
    private int elementID = 0;

    // Sets element to fire
    public void SetFire()
    {
        elementID = 1;
        elementTag = "Fire";
        SetElement();
    }

    // Sets element to water
    public void SetWater()
    {
        elementID = 2;
        elementTag = "Water";
        SetElement();
    }

    // Sets element to Wind
    public void SetWind()
    {
        elementID = 3;
        elementTag = "Wind";
        SetElement();
    }

    // Sets element to Earth
    public void SetEarth()
    {
        elementID = 4;
        elementTag = "Earth";
        SetElement();
    }

    // Sets eleemnt to Lightning
    public void SetLightning()
    {
        elementID = 5;
        elementTag = "Lightning";
        SetElement();
    }

    // Sets element to Ice
    public void SetIce()
    {
        elementID = 6;
        elementTag = "Ice";
        SetElement();
    }

    // Sends set element info to hands and plays respective SFX
    private void SetElement()
    {
        handRight = GameObject.Find("RightHand").GetComponent<Blaster>();
        handLeft = GameObject.Find("LeftHand").GetComponent<Blaster>();

        handRight.m_CurrentElement = elementTag;
        handRight.ElementSFX(elementID);
        
        handLeft.m_CurrentElement = elementTag;
        handRight.ElementSFX(elementID);
        
    }
}