using UnityEngine;

namespace Assets.UI.Dialogue
{
    public class DialogTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Player entered trigger");
            other?.GetComponent<DialogueSender>()?.SendMessage();
        }
    }
}
