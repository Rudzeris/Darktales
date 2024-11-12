using Assets.Character;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.Dialogue
{
    public class DialogueSender : MonoBehaviour
    {
        [SerializeField] private Sprite dialogueSprite;
        [SerializeField] private List<string> messages = new List<string>();
        [SerializeField] private float duration = 2f;
        [SerializeField] int numberMessage = 0;
        [SerializeField] bool isRandom = false;
        [SerializeField] float countWordsPerSecond = 3.33f;
        [SerializeField] DialogueSender nextSender;
        public Sprite Image => dialogueSprite;
        public event EventHandler OnSendMessage;

        public void SendMessage(int i = -1)
        {
            if (messages.Count <= 0) return;
            if (i == -1)
            {
                if (!isRandom)
                    numberMessage = numberMessage + 1;
                else
                    numberMessage = UnityEngine.Random.Range(0, messages.Count) + 1;
            }
            else
                numberMessage = i;
            Debug.Log($"Sending message: {messages[numberMessage]}");
            if(numberMessage < messages.Count)
            OnSendMessage?.Invoke(this, new EventMessage(
                messages[numberMessage], dialogueSprite, 
                duration ,nextSender));
        }
    }
}
