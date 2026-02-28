using UnityEngine;

public class OreHandler : MonoBehaviour
{
    public OreData oreData;

    public AudioSource audio;
    public AudioClip hitSound;
    public AudioClip breakSound;

    private float maxScalePercent = 1.0f; // 100%
    private float minScalePercent = 0.7f; // 70%

    public float currentHealth;

    void Start()
    {
        currentHealth = oreData.maxHealth;
    }

    public void HitOre(int damage)
    {
        audio.PlayOneShot(hitSound);
        currentHealth -= damage;
        // 1. Vypočítame percento zostávajúcich životov (0.0 až 1.0)
        float healthPercent = currentHealth / oreData.maxHealth;

        // 2. Vypočítame novú cieľovú mierku
        // Lerp(od, do, pomer)
        float targetScaleValue = Mathf.Lerp(minScalePercent, maxScalePercent, healthPercent);

        // 3. Aplikujeme na objekt
        transform.localScale = new Vector3(targetScaleValue, targetScaleValue, targetScaleValue);

        if (currentHealth <= 0) Mined();
    }

    public void Mined()
    {
        audio.PlayOneShot(breakSound);
        GameManager.manager.AddToQuota(oreData.value);
        GameObject.Destroy(gameObject);
        // Play sound
    }
}
