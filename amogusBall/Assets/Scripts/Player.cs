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
            // ������������� ������� �� �������
            NetworkedPosition = transform.position;
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            if (data.direction != Vector2.zero)
                data.direction.Normalize();

            // ��������� ������� �� ������ ����������� � ��������
            Vector2 newPosition = NetworkedPosition + data.direction * speed * Runner.DeltaTime;

            NetworkedPosition = newPosition;
            transform.position = newPosition; // ��������� ������� ����� ��� �����/�������
        }
    }

    private void Update()
    {
        // ������� ��� ���������� ������ ������������� ������� ��� ���������
        if (!Object.HasStateAuthority)
        {
            transform.position = Vector2.Lerp(transform.position, NetworkedPosition, Time.deltaTime * 10f);
        }
    }
}
