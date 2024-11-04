using Assets.UI;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelHUD : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Button reload;
    [SerializeField] private GameObject winText;

    private readonly UICommandQueue commandQueue = new UICommandQueue();

    public void Win()
    {
        commandQueue.TryEnqueueCommand(new WinCommand());
    }

    private void Start()
    {
        Time.timeScale = 1f;
        reload.onClick.AddListener(
            () => commandQueue.TryEnqueueCommand(new ReloadCommand())
            );
        StartCoroutine(UpdateTask());
    }

    private void OnDestroy()
    {
        reload.onClick.RemoveAllListeners();
        StopAllCoroutines();
    }
    private IEnumerator UpdateTask()
    {
        while (true)
        {
            if (commandQueue.TryDequeueCommand(out var command))
                switch (command)
                {
                    case ReloadCommand:
                        var currentScene = SceneManager.GetActiveScene();
                        yield return SceneManager.LoadSceneAsync(currentScene.name);
                        break;
                    case WinCommand:
                        winText.SetActive(true);
                        break;
                }
            yield return null;
        }
    }
}
