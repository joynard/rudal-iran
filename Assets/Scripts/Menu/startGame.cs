using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
}
