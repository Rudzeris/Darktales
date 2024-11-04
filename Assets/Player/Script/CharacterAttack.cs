using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAttack : MonoBehaviour
{
    private CharacterController2d characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController2d>();
    }

    public void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Attack!");
        }
    }
}
