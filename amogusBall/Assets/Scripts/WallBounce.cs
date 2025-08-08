using Fusion;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WallBounce : NetworkBehaviour
{
    [SerializeField] private float bounceForce = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!Object.HasStateAuthority)
            return;

        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            // Направление от центра бортика к мячу
            Vector2 direction = (ball.transform.position - transform.position).normalized;
            ball.Kick(direction * bounceForce);
        }
    }
}
