using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private bool isPaused = false;

    private void OnPause()
    {
        SceneManager.LoadScene(0);
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }
}
