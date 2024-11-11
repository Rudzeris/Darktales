using Assets.Character;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueMessage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private RawImage avatar;
    [SerializeField] private float duration;
    [SerializeField] private float timerMessage;
    private float displayTime;

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    public void ShowMessage(object sender, EventArgs e)
    {
        if (e is EventMessage message)
        {
            Debug.Log($"Message received: {message.Message}");
            messageText.text = message.Message;
            displayTime = message.Duration;
            duration = message.Duration;
            timerMessage = duration;
            avatar.texture = message.Sprite.texture;
            gameObject.SetActive(true);
            StartCoroutine(HideMessageAfterTime());
        }
    }

    private IEnumerator HideMessageAfterTime()
    {
        yield return new WaitForSeconds(displayTime);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (timerMessage > 0)
        {
            timerMessage -= Time.fixedDeltaTime;
        }
    }
}