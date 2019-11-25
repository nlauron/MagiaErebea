using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

/**
 * Button functions on the GameStart and GameOver scenes.
 */
public class ButtonHandler : MonoBehaviour
{
    // Fade time when switching scenes
    private float m_FadeTime = 3.0f;

    // Changes scene from "GameStart" to "GameScene"
    public void StartGame()
    {
        StartCoroutine(FadeToGame());
    }

    //Changes scene from "GameOver" to "GameStart"
    public void MainMenu()
    {
        StartCoroutine(FadeToMenu());
    }

    // Fades screen when switching scenes
    private IEnumerator FadeToGame()
    {
        SteamVR_Fade.Start(Color.black, 1.0f, true);
        yield return new WaitForSeconds(m_FadeTime);
        SceneManager.LoadScene("GameScene");
    }

    // Fades screen when switching scenes
    private IEnumerator FadeToMenu()
    {
        SteamVR_Fade.Start(Color.black, m_FadeTime, true);
        yield return new WaitForSeconds(m_FadeTime);
        SceneManager.LoadScene("GameStart");
    }
}
