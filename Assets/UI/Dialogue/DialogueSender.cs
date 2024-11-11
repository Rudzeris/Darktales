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
        [SerializeField] private float duration = 5f;
        [SerializeField] int numberMessage = 0;
        [SerializeField] bool isRandom = false;
        public Sprite Image => dialogueSprite;
        public event EventHandler OnSendMessage;

        [SerializeField] private float coolTimer = 0f;

        public void SendMessage(int i = -1)
        {
            if (coolTimer > 0f) return;
            if (messages.Count <= 0) return;
            if (i == -1)
            {
                if (!isRandom)
                    numberMessage = (numberMessage + 1) % messages.Count;
                else
                    numberMessage = UnityEngine.Random.Range(0, messages.Count) + 1;
            }
            else
                numberMessage = i;
            coolTimer = duration;
            Debug.Log($"Sending message: {messages[numberMessage]}");
            OnSendMessage?.Invoke(this, new EventMessage(
                messages[numberMessage], dialogueSprite, duration));
        }
        private void Update()
        {
            if (coolTimer > 0)
            {
                coolTimer -= Time.fixedDeltaTime;
            }
        }
    }
}
