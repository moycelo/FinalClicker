using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("CharacterSelect");

    }
    public void Credit()
    {
        SceneManager.LoadScene("Credit");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Gambler()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void Hustler()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void FavoredOne()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void Loafer()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void MiniGameButton()
    {
        SceneManager.LoadScene("MiniGameScene");
    }
    public void BackToGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

}
