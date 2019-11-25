using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Controls GameScene Fade through animating the canvas
 */
public class SceneFade : MonoBehaviour
{
    public Animator m_Fader;

    public void Awake()
    {
        //Fade();
    }

    private void Fade()
    {
        m_Fader.SetTrigger("FadeOut");
    }
}
