using Assets.Character;
using Assets.UI.Script.Command;
using System;
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
                        break;
                    case ReloadCommand:
                        var currentScene = SceneManager.GetActiveScene();
                        yield return SceneManager.LoadSceneAsync(currentScene.name);
                        break;
                }
            yield return null;
        }
    }
    public void ActivateAbilities(object sender,EventArgs e)
    {
        if(e is EventOpenAbilities op)
        {
            backgrounds[0].sprite = op.Ability1 ? unlockSprite : lockSprite;
            backgrounds[1].sprite = op.Ability2 ? unlockSprite : lockSprite;
            backgrounds[2].sprite = op.Ability3 ? unlockSprite : lockSprite;
        }
    }
}
