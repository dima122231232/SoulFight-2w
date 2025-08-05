using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    /* ссылочная переменная через которую мы открываем доступ к функциям и переменным
    но значение private set запрещает изменять их */
    public static Player Instance { get; private set; }

    [SerializeField] private float movingSpeed = 5f;
    
    private Rigidbody2D rb;

    private float minMovingSpeed = 0.1f;
    private bool isRunning = false;
    private Vector2 lastMoveDir = Vector2.right;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // сохраняет ссылку на текущий объект, чтобы другие скрипты могли удобно к нему обращаться
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
            lastMoveDir = inputVector.normalized;
        }
        else {
            isRunning = false;
        }
    }

    public Vector2 GetLastMoveDir() { 
        return lastMoveDir;
    }

    public bool IsRunning() {
        return isRunning;
    }
}
