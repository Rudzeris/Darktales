using Assets.UI.Script.Command;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelHUD : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject pauseElement;
    [SerializeField] private GameObject imageElement;
    [SerializeField] private GameObject saturationElement;
    [SerializeField] private Image[] backgrounds;
    [SerializeField] private Sprite lockSprite;
    [SerializeField] private Sprite unlockSprite;

    private RectTransform saturationTransform;
    private RectTransform imageTransform;

    public int[] Lvls;

    private readonly UICommandQueue commandQueue = new UICommandQueue();

    public void Win()
    {
        commandQueue.TryEnqueueCommand(new WinCommand());
    }

    private void Start()
    {
        Time.timeScale = 1f;

        saturationTransform = saturationElement.GetComponent<RectTransform>();
        imageTransform = imageElement.GetComponent<RectTransform>();

        StartCoroutine(UpdateTask());
    }

    public void UpdateSaturation(int saturation)
    {
        commandQueue.TryEnqueueCommand(new SaturationCommand(saturation));
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    private IEnumerator UpdateTask()
    {
        while (true)
        {
            if (commandQueue.TryDequeueCommand(out var command))
                switch (command)
                {
                    case SaturationCommand st:
                        imageTransform.offsetMax =
                        new Vector2(
                            -(1-(float)st.Saturation / (float)Lvls[Lvls.Length-1])*saturationTransform.rect.width,
                            imageTransform.sizeDelta.y
                    );

                        for (int i = 1; i < Lvls.Length - 1 && i - 1 < backgrounds.Length; i++)
                            backgrounds[i - 1].sprite = (st.Saturation >= Lvls[i]?
                                unlockSprite : lockSprite);
                        

                        break;
                    case ReloadCommand:
                        var currentScene = SceneManager.GetActiveScene();
                        yield return SceneManager.LoadSceneAsync(currentScene.name);
                        break;
                }
            yield return null;
        }
    }
}
