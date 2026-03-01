using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject pickaxe1;
    [SerializeField] private GameObject pickaxe2;
    [SerializeField] private GameObject pickaxe3;


    public PlayerData playerData;
    public Pickaxe pickaxe;

    private ShopHandler _shopHandler;

    [SerializeField] private Animator animator;
    private CharacterController controller;

    private Vector3 velocity;
    private float nextMineTime = 0f;

    bool moving;

    public AudioSource walkAudio;
    public AudioSource jumpAudio;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction mineAction;

    void Start()
    {
        _shopHandler = GetComponent<ShopHandler>();
        controller = GetComponent<CharacterController>();
        
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        mineAction = InputSystem.actions.FindAction("Mine");
    }

    void Update()
    {
        if (_shopHandler._shopOpen) return;
        Movement();
        Mining();
        LevelOfPickaxe();
    }

    void LevelOfPickaxe()
    {
        if (playerData.pickaxeLevel == 1) 
        {
            pickaxe.damage = 5;
            pickaxe1.SetActive(true);
            pickaxe2.SetActive(false);
            pickaxe3.SetActive(false);
        }
        else if (playerData.pickaxeLevel == 2)
        {
            pickaxe.damage = 15;
            pickaxe1.SetActive(false);
            pickaxe2.SetActive(true);
            pickaxe3.SetActive(false);
        }
        else if (playerData.pickaxeLevel == 3)
        {
            pickaxe.damage = 30;
            pickaxe1.SetActive(false);
            pickaxe2.SetActive(false);
            pickaxe3.SetActive(true);
        }
    }

    void Movement()
    {
        /*
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

            // Zvuk kroku
            if (!walkAudio.isPlaying)
            {
                walkAudio.loop = true;
                walkAudio.Play();
            }

            moving = true;
        }
        else
        {
            if (walkAudio.isPlaying)
                walkAudio.Stop();

            moving = false;
        }

        // --- Zvyšok tvojho kódu (Skok a Gravitácia) ---
        if (controller.isGrounded && jumpAction.triggered)
        {
            jumpAudio.Play();
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
        */

        // --- NÁHRADA ZA NOVÝ INPUT SYSTÉM (Vlož do Update metódy) ---

        // Získanie vstupu zo šípok alebo WASD (Input Manager)
        float moveX = Input.GetAxisRaw("Horizontal"); 
        Vector2 playerMove = new Vector2(moveX, 0);

        // Smer pohybu (globálny)
        Vector3 moveDirection = new Vector3(playerMove.x, 0).normalized;
        Vector3 horizontalMove = moveDirection * playerData.speed;

        // 1. Plynulé otáčanie TVÁROU ku kamere
        if (moveDirection.magnitude > 0.1f)
        {
            // Ak sa teraz díva chrbtom, pridanie mínus ho otočí čelom
            Quaternion targetRotation = Quaternion.LookRotation(-moveDirection);

            float rotationSpeed = 15f; // Rýchlosť otáčania
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // Zvuk kroku
            if (walkAudio != null && !walkAudio.isPlaying)
            {
                walkAudio.loop = true;
                walkAudio.Play();
            }

            moving = true;
        }
        else
        {
            if (walkAudio != null && walkAudio.isPlaying)
                walkAudio.Stop();

            moving = false;
        }

        // --- Skok a Gravitácia (Starý Input System) ---
        // "Jump" je štandardne nastavený na Medzerník v Input Manageri
        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            if (jumpAudio != null) jumpAudio.Play();
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

        // Animácie
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
