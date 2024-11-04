using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2d : MonoBehaviour
{
    private Rigidbody2D body;
    private Vector2 movementInput;
    public bool isJump { get; private set; }
    public int jumpCount { get; private set; }
    [SerializeField] public bool isGrounded { get; private set; }
    [SerializeField] private float acceleration = 18f;
    [SerializeField] private float maxSpeed = 6f;
    [SerializeField] private float jumpForce = 32f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private CircleCollider2D groundCollider;

    // Булевые переменные для направления движения
    [SerializeField] public bool isLeft { get; private set; }
    [SerializeField] public bool isRight { get; private set; }
    [SerializeField] public bool isUp { get; private set; }
    [SerializeField] public bool isDown { get; private set; }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        groundCollider = GetComponent<CircleCollider2D>();
    }

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>().normalized;
    }

    public void OnJump()
    {
        if (isGrounded || jumpCount < 2)
        {
            isJump = true;
        }
    }

    private void FixedUpdate()
    {
        GroundCheck();

        if (isJump)
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJump = false;
            jumpCount++;
        }

        Vector2 velocity = body.velocity;
        float targetSpeed = movementInput.x * maxSpeed;

        // Ускорение
        float speedDifference = targetSpeed - velocity.x;
        float accelerationRate = Mathf.Abs(targetSpeed) > 0.01f ? acceleration : acceleration * 2;
        float movement = Mathf.Pow(Mathf.Abs(speedDifference) * accelerationRate, 0.9f) * Mathf.Sign(speedDifference);

        body.AddForce(Vector2.right * movement);

        // Ограничение максимальной скорости
        if (Mathf.Abs(body.velocity.x) > maxSpeed)
        {
            body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * maxSpeed, body.velocity.y);
        }

        // Обновление булевых переменных направления
        UpdateDirectionFlags(body.velocity);
    }

    private void GroundCheck()
    {
        Collider2D[] colliders = new Collider2D[16];
        int count = groundCollider.OverlapCollider(new ContactFilter2D { layerMask = groundLayer, useLayerMask = true }, colliders);
        isGrounded = (count > 0);

        if (isGrounded)
        {
            jumpCount = 0; // Сброс счетчика прыжков при приземлении
        }
    }

    private void UpdateDirectionFlags(Vector2 velocity)
    {
        isLeft = velocity.x < 0;
        isRight = velocity.x > 0;
        isUp = velocity.y > 0;
        isDown = velocity.y < 0;
    }
}
