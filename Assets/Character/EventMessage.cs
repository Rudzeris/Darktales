using System;
using UnityEngine;

namespace Assets.Character
{
    public class EventMessage : EventArgs
    {
        public string Message { get; private set; }
        public Sprite Sprite { get; private set; }
        public float Duration { get; private set; }
        
        public EventMessage(string message, Sprite sprite, float duration)
        {
            Message = message;
            Sprite = sprite;
            Duration = duration;
        }
    }
}
