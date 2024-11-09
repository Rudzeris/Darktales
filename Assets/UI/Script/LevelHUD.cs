using Assets.UI.Script.Command;
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
    [SerializeField] private GameObject[] saturationImages;
    [SerializeField] private GameObject[] saturationPanels;

    private RectTransform[] saturationPanelTransform;
    private RectTransform[] imageTransforms;

    public int[] Lvls;

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
        saturationPanelTransform = new RectTransform[saturationPanels.Length];
        for (int i = 0; i < saturationPanels.Length; i++)
            saturationPanelTransform[i] = saturationPanels[i].GetComponent<RectTransform>();

        imageTransforms = new RectTransform[saturationImages.Length];
        for (int i = 0; i < saturationImages.Length; i++)
            imageTransforms[i] = saturationImages[i].GetComponent<RectTransform>();

        foreach (var i in imageTransforms)
        {
            i.offsetMax =
    new Vector2(
        (float)0 - saturationPanelTransform[0].sizeDelta.x,
        i.sizeDelta.y
);
        }

        StartCoroutine(UpdateTask());
    }

    public void UpdateSaturation(int saturation)
    {
        commandQueue.TryEnqueueCommand(new SaturationCommand(saturation));
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
                    case SaturationCommand st:
                        scoreText.text = $"Saturation: {st.Saturation}";
                        for (int i = 0; i < Lvls.Length; i++)
                        {
                            if (st.Saturation < Lvls[i])
                            {
                                imageTransforms[i].offsetMax =
                                new Vector2(
                                    ( i==0
? (float)st.Saturation / Lvls[i] * saturationPanelTransform[i].sizeDelta.x - saturationPanelTransform[i].sizeDelta.x
: (((float)st.Saturation) - Lvls[i - 1]) / (Lvls[i] - Lvls[i-1]) * saturationPanelTransform[i].sizeDelta.x - saturationPanelTransform[i].sizeDelta.x
                                    ),
                                    imageTransforms[i].sizeDelta.y
                            );
                                break;
                            }
                            else
                            {
                                imageTransforms[i].offsetMax =
                                new Vector2(
                                    0,
                                    imageTransforms[i].sizeDelta.y
                            );
                            }
                        }

                        break;
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
