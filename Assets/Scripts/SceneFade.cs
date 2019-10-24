using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
