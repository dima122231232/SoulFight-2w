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
}
