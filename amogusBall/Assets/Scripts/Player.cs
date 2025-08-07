using Fusion;
using UnityEngine;

[RequireComponent(typeof(NetworkObject))]
public class Player : NetworkBehaviour
{
    [Networked]
    private Vector2 NetworkedPosition { get; set; }

    public float speed = 15f;

    private void Start()
    {
        if (Object.HasStateAuthority)
        {
            // »нициализаци€ позиции на сервере
            NetworkedPosition = transform.position;
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            if (data.direction != Vector2.zero)
                data.direction.Normalize();

            // ќбновл€ем позицию на основе направлени€ и скорости
            Vector2 newPosition = NetworkedPosition + data.direction * speed * Runner.DeltaTime;

            NetworkedPosition = newPosition;
            transform.position = newPosition; // обновл€ем позицию сразу дл€ хоста/сервера
        }
    }

    private void Update()
    {
        //  лиенты без авторитета плавно интерполируют позицию дл€ плавности
        if (!Object.HasStateAuthority)
        {
            transform.position = Vector2.Lerp(transform.position, NetworkedPosition, Time.deltaTime * 10f);
        }
    }
}
