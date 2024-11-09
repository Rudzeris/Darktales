using UnityEngine;

public class EnemyController2D : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float visionRadius = 3f;
    [SerializeField] private float loseSightRadius = 4f; // Новый радиус для потери игрока
    private Transform player;
    private Vector2 initialPosition;
    public enum State { Stop, Home, Player };
    [SerializeField] private State state;

    private void Start()
    {
        state = State.Stop;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initialPosition = transform.position;
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        float distanceToInitial = Vector2.Distance(transform.position, initialPosition);

        switch (state)
        {
            case State.Stop:
                if (distanceToPlayer < visionRadius)
                {
                    state = State.Player;
                }
                else if (distanceToInitial > 1f)
                {
                    state = State.Home;
                }
                break;

            case State.Player:
                if (distanceToPlayer > loseSightRadius)
                {
                    state = State.Home;
                }
                else
                {
                    MoveTowards(player.position);
                }
                break;

            case State.Home:
                if (distanceToPlayer < visionRadius)
                {
                    state = State.Player;
                }
                else if (distanceToInitial <= 1f)
                {
                    state = State.Stop;
                }
                else
                {
                    MoveTowards(initialPosition);
                }
                break;
        }
    }

    private void MoveTowards(Vector2 target)
    {
        Vector2 position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.position = new Vector3(position.x, transform.position.y, transform.position.z);
    }
}
