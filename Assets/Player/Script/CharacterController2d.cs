using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2d : MonoBehaviour
{
    private Rigidbody2D body;
    private Vector2 movementInput;
    private bool isJump;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float acceleration = 18f;
    [SerializeField] private float maxSpeed = 36f;
    [SerializeField] private float jumpForce = 240f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private CircleCollider2D groundCollider;
    [SerializeField] private Animator animator;

    public bool facingLeft { get; private set; } = true;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        groundCollider = GetComponent<CircleCollider2D>();
        //animator = GetComponent<Animator>();
    }

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>().normalized;
    }

    public void OnJump()
    {
        if (isGrounded)
        {
            isJump = true;
        }
    }

    private void FixedUpdate()
    {
        GroundCheck();
        MoveCharacter();
        //UpdateAnimation();
    }

    private void MoveCharacter()
    {
        if (isJump)
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJump = false;
        }

        Vector2 velocity = body.velocity;
        float targetSpeed = movementInput.x * maxSpeed;

        float speedDifference = targetSpeed - velocity.x;
        float accelerationRate = Mathf.Abs(targetSpeed) > 0.01f ? acceleration : acceleration * 2;
        float movement = Mathf.Pow(Mathf.Abs(speedDifference) * accelerationRate, 0.9f) * Mathf.Sign(speedDifference);

        body.AddForce(Vector2.right * movement);

        if (Mathf.Abs(body.velocity.x) > maxSpeed)
        {
            body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * maxSpeed, body.velocity.y);
        }

        // Поворот персонажа в зависимости от направления движения
        if ((!facingLeft && movementInput.x < 0) || (facingLeft && movementInput.x > 0))
        {
            facingLeft = !facingLeft;
            transform.localScale = new Vector3(facingLeft ? 1 : -1, 1, 1);
            
        }
    }

    private void GroundCheck()
    {
        Collider2D[] colliders = new Collider2D[16];
        int count = groundCollider.OverlapCollider(new ContactFilter2D { layerMask = groundLayer, useLayerMask = true }, colliders);
        isGrounded = (count > 0);
    }

    private void UpdateAnimation()
    {
        bool isWalking = Mathf.Abs(body.velocity.x) > 0.1f;

        if (isWalking)
        {
            animator?.SetBool("isWalking", true);
        }
        else
        {
            animator?.SetBool("isWalking", false);
        }
        animator?.SetBool("isGrounded", isGrounded);
        animator?.SetFloat("yVelocity", body.velocity.y);
    }
}
