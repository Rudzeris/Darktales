using Assets.UI.Dialogue;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.UI.Script
{
    public class WinZone : MonoBehaviour
    {
        [SerializeField] LevelManager levelManager;
        private void Awake()
        {
            levelManager=FindObjectOfType<LevelManager>();
        }
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
                levelManager?.Win();

        }
    }
}
