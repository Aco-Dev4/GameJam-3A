using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    [SerializeField] private Animator animator;
    private CharacterController controller;

    private Vector3 velocity;
    private float nextMineTime = 0f;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction mineAction;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        mineAction = InputSystem.actions.FindAction("Mine");
    }
    void Update()
    {
        Movement();
        Mining();
    }

    void Movement()
    {
        Vector2 playerMove = moveAction.ReadValue<Vector2>();

        // Smer pohybu (globálny)
        Vector3 moveDirection = new Vector3(playerMove.x, 0).normalized;
        Vector3 horizontalMove = moveDirection * playerData.speed;

        // 1. Plynulé otáčanie TVÁROU ku kamere
        if (moveDirection.magnitude > 0.1f)
        {
            // Ak sa teraz díva chrbtom, pridanie mínus ho otočí čelom
            // Týmto povieš: "Dívaj sa presne na opačnú stranu, než kam ideš"
            Quaternion targetRotation = Quaternion.LookRotation(-moveDirection);

            float rotationSpeed = 15f; // Rýchlosť otáčania
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        // --- Zvyšok tvojho kódu (Skok a Gravitácia) ---
        if (controller.isGrounded && jumpAction.triggered)
        {
            velocity.y = Mathf.Sqrt(playerData.jumpHeight * -2f * playerData.gravity);
            animator.SetBool("isJumping", true);
        }

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            animator.SetBool("isJumping", false);
        }

        velocity.y += playerData.gravity * Time.deltaTime;

        // Pohybujeme sa v smere moveDirection, ale postava sa díva opačne
        controller.Move((horizontalMove + velocity) * Time.deltaTime);

        animator.SetBool("isMoving", playerMove.magnitude > 0);
    }

    void Mining()
    {
        // .IsPressed() vráti true po celú dobu, čo držíš ľavé tlačidlo
        if (mineAction.IsPressed() && Time.time >= nextMineTime)
        {
            animator.SetTrigger("Mine");

            // Toto určí pauzu medzi údermi (napr. 0.8 sekundy)
            nextMineTime = Time.time + playerData.miningCooldown;
        }
    }
}
