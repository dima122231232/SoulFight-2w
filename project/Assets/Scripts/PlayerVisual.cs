using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private const string IS_RUNNING = "IsRunning";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // ���������� �� ������ ���� (FixedUpdate - ��������� ������ 60 ������)
    private void FixedUpdate()
    {
        if (Player.Instance == null)
        {
            Debug.LogWarning("Player.Instance is null");
            return;
        }

        animator.SetBool(IS_RUNNING, Player.Instance.IsRunning());
        AdjustPlayerFacingDirection();
    }

    // ����������� ������� (����� �������� ���������� �������� ������� � Player
    // � ���������� � �����
    private void AdjustPlayerFacingDirection()
    {
        // ��������� � ������ Player
        Vector2 lastMoveDir = Player.Instance.GetLastMoveDir();

        if (lastMoveDir.x < 0)
            spriteRenderer.flipX = true;
        else if (lastMoveDir.x > 0)
            spriteRenderer.flipX = false;
    }
}
