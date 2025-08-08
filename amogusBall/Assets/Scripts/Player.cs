//using Fusion;
//using Fusion.Addons.Physics;
//using UnityEngine;

//[RequireComponent(typeof(NetworkObject))]
//public class Player : NetworkBehaviour
//{
//    [Networked]
//    private Vector2 NetworkedPosition { get; set; }

//    public float speed = 15f;

//    private void Start()
//    {
//        if (Object.HasStateAuthority)
//        {
//            // ������������� ������� �� �������
//            NetworkedPosition = transform.position;
//        }
//    }

//    public override void FixedUpdateNetwork()
//    {
//        if (GetInput(out NetworkInputData data))
//        {
//            if (data.direction != Vector2.zero)
//                data.direction.Normalize();

//            // ��������� ������� �� ������ ����������� � ��������
//            Vector2 newPosition = NetworkedPosition + data.direction * speed * Runner.DeltaTime;

//            NetworkedPosition = newPosition;
//            transform.position = newPosition; // ��������� ������� ����� ��� �����/�������
//        }
//    }

//    private void Update()
//    {
//        // ������� ��� ���������� ������ ������������� ������� ��� ���������
//        if (!Object.HasStateAuthority)
//        {
//            transform.position = Vector2.Lerp(transform.position, NetworkedPosition, Time.deltaTime * 10f);
//        }
//    }
//}


using UnityEngine; // 👈 Добавь это
using Fusion;
using Fusion.Addons.Physics;

public class Player : NetworkBehaviour
{
    private NetworkRigidbody2D _cc;

    private void Awake()
    {
        _cc = GetComponent<NetworkRigidbody2D>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            data.direction.Normalize();
            Vector2 targetVelocity = 5 * data.direction;
            _cc.Rigidbody.linearVelocity = targetVelocity;
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
            ball.Kick(direction);
        }
    }
}