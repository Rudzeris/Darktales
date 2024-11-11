using Assets.Player.Script.Abilities;
using System;
using System.Collections;
using UnityEngine;

public class DashAbility : MonoBehaviour, IAbility
{
    [SerializeField] private float invulnerabilityDuration = 0.4f;
    [SerializeField] private float dashDistance = 3f;
    [SerializeField] private float cooldown = 3f;

    [SerializeField] private float cooldownTimer = 0f;

    private CharacterController2d characterController;

    private string originalTag;

    public float Cooldown { get { return cooldown; } }

    private void Awake()
    {
        characterController = GetComponent<CharacterController2d>();
        originalTag = gameObject.tag;
    }

    public void Activate()
    {
        if (cooldownTimer > 0)
        {
            Debug.Log("Ability is on cooldown.");
            return;
        }

        cooldownTimer = cooldown;
        bool isLeft = characterController.facingLeft;
        Vector3 dashPosition = transform.position + (isLeft?-1:1)*transform.right * dashDistance;
        transform.position = dashPosition;

        StartCoroutine(Invulnerability());
    }

    private IEnumerator Invulnerability()
    {
        gameObject.tag = "Untagged";
        yield return new WaitForSeconds(invulnerabilityDuration); 
        gameObject.tag = originalTag;
    }

    private void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
}
