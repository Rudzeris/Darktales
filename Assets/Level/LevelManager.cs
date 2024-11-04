using UnityEngine;

// Класс, который знает информацию про всех и добавляем им event 
public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelHUD levelHUD;

    private LevelProgress progress;

    private void Awake()
    {
        progress = new LevelProgress();
    }

}
