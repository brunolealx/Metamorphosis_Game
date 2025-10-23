using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Vector2 moveInput;
    private bool jumpPressed;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [Header("Player Settings")]
    public float speed = 5f;
    public float jumpForce = 5f;

    [Header("Input")]
    public InputActionAsset inputActions;

    private InputActionMap playerActionMap;
    private InputAction moveAction;
    private InputAction jumpAction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Pega o Action Map chamado "Player"
        playerActionMap = inputActions.FindActionMap("Player");
        if (playerActionMap == null)
        {
            Debug.LogError("Action Map 'Player' não encontrado!");
            return;
        }

        moveAction = playerActionMap.FindAction("Move");
        jumpAction = playerActionMap.FindAction("Jump");

        if (moveAction == null || jumpAction == null)
        {
            Debug.LogError("Actions 'Move' ou 'Jump' não encontradas!");
            return;
        }

        playerActionMap.Enable();

        moveAction.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        moveAction.canceled += ctx => moveInput = Vector2.zero;
        jumpAction.performed += ctx => jumpPressed = true;
    }

    void FixedUpdate()
    {
        // Movimento horizontal
        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);

        // Atualiza a animação de movimento
        bool isWalking = Mathf.Abs(moveInput.x) > 0.1f;
        animator.SetBool("isWalking", isWalking);

        // Faz o personagem virar pro lado que anda
        if (moveInput.x > 0.1f)
            spriteRenderer.flipX = false;
        else if (moveInput.x < -0.1f)
            spriteRenderer.flipX = true;

        // Pulo
        if (jumpPressed && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpPressed = false;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 1.1f);
    }

    // Método para o botão de pulo no mobile
    public void JumpButtonPressed()
    {
        if (IsGrounded())
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    // Métodos para botões de movimentação (opcional)
    public void MoveLeftPressed() { moveInput.x = -1f; }
    public void MoveRightPressed() { moveInput.x = 1f; }
    public void MoveReleased() { moveInput.x = 0f; }
}
