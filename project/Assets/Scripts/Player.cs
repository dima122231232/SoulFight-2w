using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private float movingSpeed = 5f;
    
    private Rigidbody2D rb;

    private float minMovingSpeed = 0.1f;
    private bool isRunning = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (GameInput.Instance == null)
        {
            Debug.LogWarning("GameInput.Instance is null");
            return;
        }

        HandleMovement();
    }

    private void HandleMovement() 
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();
        rb.MovePosition(rb.position + inputVector * (movingSpeed * Time.fixedDeltaTime));

        if (Mathf.Abs(inputVector.x) > minMovingSpeed || Mathf.Abs(inputVector.y) > minMovingSpeed) {
            isRunning = true;
        }
        else {
            isRunning = false;
        }
    }

    public bool IsRunning() {
        return isRunning;
    }

    public Vector3 GetPlayerScreenPosition() {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }

    
}
