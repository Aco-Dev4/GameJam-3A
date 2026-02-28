using UnityEngine;

public class LockZPosition : MonoBehaviour
{
    private float zamknuteZ;

    void Start()
    {
        // Uloíme si poèiatoènú Z pozíciu hneï na zaèiatku
        zamknuteZ = transform.position.z;
    }

    void LateUpdate()
    {
        // Vrátime Z pozíciu spä na uloenú hodnotu (LateUpdate zaruèí, e sa to stane a po všetkıch pohyboch)
        transform.position = new Vector3(transform.position.x, transform.position.y, zamknuteZ);
    }
}