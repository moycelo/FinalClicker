using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GamePlay"); //Start -> main game play auto clicker

    }
    public void Credit() //credit area
    {
        SceneManager.LoadScene("Credit");
    }
    public void BackToMenu() //back to main menu
    {
        SceneManager.LoadScene("SampleScene");
    }
}
