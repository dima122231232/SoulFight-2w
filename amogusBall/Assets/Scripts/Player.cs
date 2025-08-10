using UnityEngine;
using Fusion;
using Fusion.Addons.Physics;

public class Player : NetworkBehaviour
{
    private NetworkRigidbody2D _networkRigidbody;

    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float collisionCheckDistance = 0.6f;

    private GameObject _indicator;

    private void Awake()
    {
        _networkRigidbody = GetComponent<NetworkRigidbody2D>();
    }

    public override void Spawned()
    {
        // Создаём треугольник только для локального игрока
        if (Object.HasInputAuthority)
        {
            CreateIndicator();
        }
    }

    void CreateIndicator()
    {
        _indicator = new GameObject("PlayerIndicator");

        // Рендерим спрайт
        var sr = _indicator.AddComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>("triangle"); // triangle.png в Assets/Resources/
        Color color;
        if (ColorUtility.TryParseHtmlString("#0000FF", out color))
        {
            sr.color = color;
        }
        sr.sortingOrder = 10;

        // Привязываем к игроку
        _indicator.transform.SetParent(transform);
        _indicator.transform.localPosition = new Vector3(0, 0.75f, 0);
        _indicator.transform.localScale = new Vector3(0.25f, 0.25f, 1);
        _indicator.transform.localRotation = Quaternion.Euler(0, 0, 180);
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            if (data.direction.sqrMagnitude > 0)
                data.direction.Normalize();
            else
            {
                _networkRigidbody.Rigidbody.linearVelocity = Vector2.zero;
                return;
            }

            // Проверка столкновения со стеной по направлению движения
            RaycastHit2D hit = Physics2D.Raycast(transform.position, data.direction, collisionCheckDistance, wallLayer);
            if (hit.collider == null)
            {
                Vector2 targetVelocity = speed * data.direction;
                _networkRigidbody.Rigidbody.linearVelocity = targetVelocity;
            }
            else
            {
                _networkRigidbody.Rigidbody.linearVelocity = Vector2.zero;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!Object.HasStateAuthority)
            return;

        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            Vector2 direction = (ball.transform.position - transform.position).normalized;
            float playerSpeed = _networkRigidbody.Rigidbody.linearVelocity.magnitude;
            float kickForce = playerSpeed * 2f;
            ball.Kick(direction, kickForce);
        }
    }
}
