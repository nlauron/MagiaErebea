using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class ButtonHandler : MonoBehaviour
{
    private float m_FadeTime = 3.0f;

    public void StartGame()
    {
        StartCoroutine(FadeToGame());
    }

    public void MainMenu()
    {
        StartCoroutine(FadeToMenu());
    }

    private IEnumerator FadeToGame()
    {
        SteamVR_Fade.Start(Color.black, 1.0f, true);
        yield return new WaitForSeconds(m_FadeTime);
        SceneManager.LoadScene("SampleScene");
    }

    private IEnumerator FadeToMenu()
    {
        SteamVR_Fade.Start(Color.black, m_FadeTime, true);
        yield return new WaitForSeconds(m_FadeTime);
        SceneManager.LoadScene("GameStart");
    }
}
