using UnityEngine;

// �����, ������� ����� ���������� ��� ���� � ��������� �� event 
public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelHUD levelHUD;

    private LevelProgress progress;

    private void Awake()
    {
        progress = new LevelProgress();
    }

}
