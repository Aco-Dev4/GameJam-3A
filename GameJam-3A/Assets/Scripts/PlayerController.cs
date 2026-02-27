using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    private CharacterController controller;

    private Vector3 velocity;

    private InputAction moveAction;
    private InputAction jumpAction;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {
        Vector2 playerMove = moveAction.ReadValue<Vector2>();
        Vector3 move = transform.right * playerMove.x * playerData.speed;

        if (controller.isGrounded && jumpAction.triggered)
        {
            // Fyzikálny vzorec pre výskok do konkrétnej výšky: sqrt(h * -2 * g)
            velocity.y = Mathf.Sqrt(playerData.jumpHeight * -2f * playerData.gravity);
        }

        // Resetovanie gravitácie, keď sme na zemi
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += playerData.gravity * Time.deltaTime;

        controller.Move((move + velocity) * Time.deltaTime);
    }
}
