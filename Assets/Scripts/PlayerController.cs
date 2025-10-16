using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private float jumpForce = 12f;

    [Header("Jump Physics")]
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;

    [Header("Ground Check (Raycast)")]
    [SerializeField] private float rayLength = 0.15f;
    [SerializeField] private int rayCount = 5;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput;

    private bool isGrounded;
    private bool facingRight = true;
    public bool canAttack = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!GameStateManager.Instance.IsState(GameState.Playing))
        {
            moveInput = Vector2.zero;
            animator.SetBool("isWalking", false);
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        moveInput = new Vector2(moveX, 0).normalized;

        // jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        // attack / interact
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Attack();
        }

        // walking animaiton
        bool isMoving = Mathf.Abs(rb.linearVelocity.x) > 0.05f;
        animator.SetBool("isWalking", isMoving);

        // flip player
        if (moveInput.x > 0 && !facingRight) Flip(true);
        else if (moveInput.x < 0 && facingRight) Flip(false);
    }

    private void FixedUpdate()
    {
        if (!GameStateManager.Instance.IsState(GameState.Playing))
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

        float currentSpeed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentSpeed *= sprintMultiplier;
        }

        rb.linearVelocity = new Vector2(moveInput.x * currentSpeed, rb.linearVelocity.y);

        isGrounded = CheckGrounded();
        ApplyJumpPhysics();
    }

    private void Flip(bool faceRight)
    {
        facingRight = faceRight;
        Vector3 scale = transform.localScale;
        scale.x = faceRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void ApplyJumpPhysics()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    public void Attack()
    {
        canAttack = false;
        animator.SetTrigger("attack");
    }

    public void ResetAttack()
    {
        canAttack = true;
    }

    private bool CheckGrounded()
    {
        Bounds bounds = GetComponent<Collider2D>().bounds;
        float width = bounds.size.x;
        Vector2 origin = new Vector2(bounds.min.x, bounds.min.y);

        for (int i = 0; i < rayCount; i++)
        {
            float xOffset = (width / (rayCount - 1)) * i;
            Vector2 rayOrigin = new Vector2(origin.x + xOffset, origin.y);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, groundLayer);
            if (hit.collider != null)
                return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        if (GetComponent<Collider2D>() == null) return;

        Gizmos.color = Color.red;
        Bounds bounds = GetComponent<Collider2D>().bounds;
        float width = bounds.size.x;
        Vector2 origin = new Vector2(bounds.min.x, bounds.min.y);

        for (int i = 0; i < rayCount; i++)
        {
            float xOffset = (width / (rayCount - 1)) * i;
            Vector2 rayOrigin = new Vector2(origin.x + xOffset, origin.y);
            Gizmos.DrawLine(rayOrigin, rayOrigin + Vector2.down * rayLength);
        }
    }
}
