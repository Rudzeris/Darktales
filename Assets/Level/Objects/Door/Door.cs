using System;
using UnityEngine;

namespace Assets.Level.Objects.Door
{
    public class Door : MonoBehaviour, IContactObject
    {
        [SerializeField] private DoorState doorState = DoorState.Closed;
        [SerializeField] private Sprite openDoorSprite;
        [SerializeField] private Sprite closeDoorSprite;
        [SerializeField] private bool NoClose = false;
        public DoorState DoorState => doorState;

        public EventHandler OnContact;
        private Collider2D doorCollider;
        private SpriteRenderer doorSpriteRenderer;
        public AudioSource doorOpenClose;

        private void Awake()
        {
            doorCollider = GetComponent<Collider2D>();
            doorSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Contact(object sender, EventArgs args)
        {
            if (doorOpenClose != null)
                doorOpenClose.Play();
            if (args is EventDoor eventDoor || NoClose)
            {
                if (doorState != DoorState.Open)
                    OpenDoor();
            }
            else
            {
                DoorState prev = doorState;
                switch (doorState)
                {
                    case DoorState.Open:
                        CloseDoor();
                        break;
                    case DoorState.Closed:
                        OpenDoor();
                        break;
                    default:
                        Debug.LogWarning("The status of the door is not defined");
                        return;
                }
                OnContact?.Invoke(this, new EventDoor(this, prev, doorState));
            }
        }
        private void OpenDoor()
        {
            doorState = DoorState.Open;
            doorCollider.isTrigger = true;
            doorSpriteRenderer.sprite = openDoorSprite ?? doorSpriteRenderer.sprite;
        }
        private void CloseDoor()
        {
            doorState = DoorState.Closed;
            doorCollider.isTrigger = false;
            doorSpriteRenderer.sprite = closeDoorSprite ?? doorSpriteRenderer.sprite;
        }

    }
}