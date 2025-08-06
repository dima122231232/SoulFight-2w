using Fusion;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(NetworkObject))]
public class Player2DNetworkController : NetworkBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;

    [Networked]
    private Vector2 NetworkedPosition { get; set; }

    public override void Spawned()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = NetworkedPosition;
    }

    public override void FixedUpdateNetwork()
    {
        if (!HasInputAuthority) return; // <-- важно!

        if (GetInput(out NetworkInputData data))
        {
            Vector2 direction = data.direction.normalized;
            rb.linearVelocity = direction * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }



    private void LateUpdate()
    {
        if (Object != null)
        {
            Vector3 pos = transform.position;
            pos.z = 0;
            transform.position = pos;
        }
    }
}
