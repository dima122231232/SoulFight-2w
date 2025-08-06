using Fusion;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player2DNetworkController : NetworkBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;

    public override void Spawned()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void FixedUpdateNetwork()
    {
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

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
    }
}
