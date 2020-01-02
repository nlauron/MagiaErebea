using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/**
 * Changes button colors based on status, Normal, Hovering and Down.
 */
public class ButtonTransition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    // Button colors depending on status
    public Color32 m_NormalColor = Color.white;
    public Color32 m_HoverColor = Color.grey;
    public Color32 m_DownColor = Color.white;

    // Button appearance
    private Image m_Image = null;

    // Reference button 
    private void Awake()
    {
        m_Image = GetComponent<Image>();
    }

    // Change button color when hovering over
    public void OnPointerEnter(PointerEventData eventData)
    {
        print("Enter");

        m_Image.color = m_HoverColor;
    }

    // Change button color back to normal 
    public void OnPointerExit(PointerEventData eventData)
    {
        print("Exit");

        m_Image.color = m_NormalColor;
    }

    // Change button color when pressed
    public void OnPointerDown(PointerEventData eventData)
    {
        print("Down");

        m_Image.color = m_DownColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    // Change button back to hover after being pressed
    public void OnPointerClick(PointerEventData eventData)
    {
        print("Click");

        m_Image.color = m_HoverColor;
    }
}
