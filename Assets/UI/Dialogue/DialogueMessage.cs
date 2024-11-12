using Assets.Character;
using Assets.UI.Dialogue;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueMessage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private RawImage avatar;
    [SerializeField] private float duration;
    private float timerMessage;
    private bool isMessageActive = false;  // Флаг для отслеживания состояния сообщения
    private DialogueSender nextSender;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void ShowMessage(object sender, EventArgs e)
    {
        if (e is EventMessage message)
        {
            this.nextSender = message.NextSender;
            Debug.Log($"Message received: {message.Message}");
            messageText.text = message.Message;
            avatar.texture = message.Sprite.texture;
            timerMessage = message.Duration;
            isMessageActive = true;
            gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (isMessageActive && timerMessage > 0)
        {
            timerMessage -= Time.deltaTime;
            if (timerMessage <= 0)
            {
                isMessageActive = false;
                gameObject.SetActive(false);
                if (nextSender != null)
                    nextSender.SendMessage();
            }
        }
    }
}
