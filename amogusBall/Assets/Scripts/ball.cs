using Fusion;
using Fusion.Addons.Physics;
using UnityEngine;

[RequireComponent(typeof(NetworkObject), typeof(Rigidbody2D), typeof(NetworkRigidbody2D))]
public class Ball : NetworkBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float kickForce = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Kick(Vector2 direction)
    {
        rb.AddForce(direction.normalized * kickForce, ForceMode2D.Impulse);
    }
    public void KickRaw(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}