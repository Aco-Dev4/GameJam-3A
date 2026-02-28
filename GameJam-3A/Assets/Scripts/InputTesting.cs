using UnityEngine;
using UnityEngine.InputSystem;

public class InputTesting : MonoBehaviour
{

        private InputAction crouchAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        crouchAction = InputSystem.actions.FindAction("Crouch");

    }

    // Update is called once per frame
    void Update()
    {
        if (crouchAction.triggered)
            Debug.Log("Ide Input");
    }
}
