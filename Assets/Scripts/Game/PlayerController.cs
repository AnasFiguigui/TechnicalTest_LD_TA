using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private HeroStatsSO heroStats;

    private Vector2 moveInput;
    private Vector2 lastMoveDir = Vector2.down;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private EnemyController enemyController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyController = FindFirstObjectByType<EnemyController>();
    }

    private void Update()
    {
        MovePlayer();
        UpdateAnimation();
    }

    private void MovePlayer()
    {
        Vector3 move = new Vector3(moveInput.x, moveInput.y, 0f);
        transform.position += move * moveSpeed * Time.deltaTime;
    }

    private void UpdateAnimation()
    {
        bool isMoving = moveInput.sqrMagnitude > 0.01f;

        if (isMoving)
        {
            lastMoveDir = moveInput.normalized;
        }

        // Update animator parameters
        animator.SetBool("IsMoving", isMoving);
        animator.SetFloat("MoveX", moveInput.x);
        animator.SetFloat("MoveY", moveInput.y);

        // Handle facing directions
        // Handle facing directions
        if (isMoving)
        {
            if (moveInput.x > 0.1f)
                spriteRenderer.flipX = true;  // Right (east)
            else if (moveInput.x < -0.1f)
                spriteRenderer.flipX = false; // Left (west)
        }
        else
        {
            // Idle direction control
            animator.SetFloat("MoveX", lastMoveDir.x);
            animator.SetFloat("MoveY", lastMoveDir.y);
        }
    }

    // Input System: called automatically
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && enemyController != null)
        {
            enemyController.TakeDamage(heroStats.ATK);
        }
    }
}
