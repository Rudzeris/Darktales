﻿using UnityEngine;

namespace Assets.UI.Dialogue
{
    public class DialogTrigger : MonoBehaviour
    {
        [SerializeField] private float cooldown = 10f;
        [SerializeField] private float coolTimer = 0f;
        [SerializeField] public bool IsOneTriger = false;
        [SerializeField] private bool IsActivated = false;
        [SerializeField] public int numberText = -1;

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (IsOneTriger && IsActivated) return;
            if (coolTimer <= 0)
            {
                if(IsOneTriger)
                    IsActivated = true;
                coolTimer = cooldown;
                Debug.Log("Player entered trigger");
                other?.GetComponent<DialogueSender>()?.SendMessage(numberText);
            }
        }
        private void Update()
        {
            if(coolTimer > 0) coolTimer -= Time.deltaTime;
        }
    }
}
