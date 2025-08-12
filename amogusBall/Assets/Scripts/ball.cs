using Fusion;
using Fusion.Addons.Physics;
using UnityEngine;

[RequireComponent(typeof(NetworkRigidbody2D))]
public class Ball : NetworkBehaviour
{
    private NetworkRigidbody2D _networkRb;

    [Networked] private Vector2 NetworkStartPosition { get; set; }

    private void Awake()
    {
        _networkRb = GetComponent<NetworkRigidbody2D>();
    }

    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            NetworkStartPosition = transform.position; // запоминаем стартовую позицию
        }
    }

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
            float damping = Mathf.Pow(0.99f, Runner.DeltaTime * 60f);
            _networkRb.Rigidbody.linearVelocity *= damping;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!HasStateAuthority)
            return;

        if (other.CompareTag("Goal"))
        {
            ResetBallPositionRPC();
        }
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void ResetBallPositionRPC()
    {
        transform.position = NetworkStartPosition;
        _networkRb.Rigidbody.linearVelocity = Vector2.zero;
    }
}
