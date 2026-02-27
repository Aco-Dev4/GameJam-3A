using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    [Header("Nastavenia rotácie")]
    [Tooltip("Rıchlos otáèania objektu")]
    public float rotationSpeed = 50f;

    [Tooltip("Os, okolo ktorej sa má objekt toèi (napr. 0, 1, 0 pre os Y)")]
    public Vector3 rotationAxis = Vector3.up;

    void Update()
    {
        // transform.Rotate zabezpeèí plynulé otáèanie
        // Time.deltaTime zaruèí, e rıchlos bude rovnaká bez oh¾adu na FPS
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}