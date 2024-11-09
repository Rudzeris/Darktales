using Assets.UI.Script.Command;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button play;
    [SerializeField] private Button exit;
    [SerializeField] private int numberScene = 1;

    private UICommandQueue commandQueue = new UICommandQueue();

    private void Start()
    {
        Time.timeScale = 1f;
        play.onClick.AddListener(
            () => commandQueue.TryEnqueueCommand(new PlayCommand())
            );

        exit.onClick.AddListener(
            () => commandQueue.TryEnqueueCommand(new ExitCommand())
            );

        StartCoroutine(UpdateTask());
    }
    private void OnDestroy()
    {
        play.onClick.RemoveAllListeners();
        exit.onClick.RemoveAllListeners();
        StopAllCoroutines();
    }
    private IEnumerator UpdateTask()
    {
        while (true)
        {
            if (commandQueue.TryDequeueCommand(out var command))
                switch (command)
                {
                    case PlayCommand:
                        SceneManager.LoadScene(numberScene);
                        break;
                    case ExitCommand:
                        Application.Quit();
                        break;
                    default:
                        Debug.LogWarning("Неизвестная команда: " + command);
                        break;

                }
            yield return null;
        }
    }
}
