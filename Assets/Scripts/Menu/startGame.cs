using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Level2");
    }
    public void Play1()
    {
        SceneManager.LoadScene("Level1");
    }
}
