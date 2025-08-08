//using Fusion;
//using Fusion.Addons.Physics;
//using UnityEngine;

//[RequireComponent(typeof(NetworkRigidbody2D))]
//public class Ball : NetworkBehaviour
//{
//    private NetworkRigidbody2D _networkRb;

//    private void Awake()
//    {
//        _networkRb = GetComponent<NetworkRigidbody2D>();
//    }

//    public void Kick(Vector2 direction)
//    {
//        if (!HasStateAuthority)
//            return;

//        _networkRb.Rigidbody.linearVelocity = direction.normalized * 10f;
//    }

//    public override void FixedUpdateNetwork()
//    {
//        if (HasStateAuthority)
//        {
//            // ����������� ���������� �������
//            Rigidbody2D rb = GetComponent<Rigidbody2D>();
//            rb.linearVelocity *= 0.99f; // 1% ������ ������� �� ��� � ������� ��� ����
//        }
//    }
//}


using Fusion;
using Fusion.Addons.Physics;
using UnityEngine;

[RequireComponent(typeof(NetworkRigidbody2D))]
public class Ball : NetworkBehaviour
{
    private NetworkRigidbody2D _networkRb;

    private void Awake()
    {
        _networkRb = GetComponent<NetworkRigidbody2D>();
    }

    /// <summary>
    /// Пинок мяча с определённой силой
    /// </summary>
    public void Kick(Vector2 direction, float force)
    {
        if (!HasStateAuthority)
            return;

        _networkRb.Rigidbody.linearVelocity = direction.normalized * force;
    }

    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority)
        {
            // Постепенное замедление мяча
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.linearVelocity *= 0.99f; // 1% потери скорости за тик
        }
    }
}
