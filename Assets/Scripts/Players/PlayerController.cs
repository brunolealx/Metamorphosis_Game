using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; // necessário para Image

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
    private InputAction damageAction; // ação de dano

    [Header("Health Settings")]
    public Image healthBarFill;   // arraste HealthBar_Fill aqui
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Inventory")]
    public GameObject inventoryIcon; // arraste a Image do slot da chave aqui

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Inicializa vida
        currentHealth = maxHealth;
        UpdateHealthBar();

        // Pega o Action Map chamado "Player"
        playerActionMap = inputActions.FindActionMap("Player");
        if (playerActionMap == null)
        {
            Debug.LogError("Action Map 'Player' não encontrado!");
            return;
        }

        moveAction = playerActionMap.FindAction("Move");
        jumpAction = playerActionMap.FindAction("Jump");
        damageAction = playerActionMap.FindAction("Damage"); // pega ação Damage

        if (moveAction == null || jumpAction == null || damageAction == null)
        {
            Debug.LogError("Actions 'Move', 'Jump' ou 'Damage' não encontradas!");
            return;
        }

        playerActionMap.Enable();

        moveAction.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        moveAction.canceled += ctx => moveInput = Vector2.zero;
        jumpAction.performed += ctx => jumpPressed = true;
        damageAction.performed += ctx => TakeDamage(10); // chama TakeDamage ao apertar Z
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

    // Métodos para botões de pulo no mobile
    public void JumpButtonPressed()
    {
        if (IsGrounded())
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    // Métodos para botões de movimentação no mobile
    public void MoveLeftPressed() { moveInput.x = -1f; }
    public void MoveRightPressed() { moveInput.x = 1f; }
    public void MoveReleased() { moveInput.x = 0f; }

    // ======== Barra de vida ========
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthBarFill != null)
            healthBarFill.fillAmount = currentHealth / maxHealth;
    }

    // ======== Inventário da chave ========
    public void ShowKeyInInventory()
    {
        if (inventoryIcon != null)
            inventoryIcon.SetActive(true); // mostra o ícone da chave no inventário
    }
}
