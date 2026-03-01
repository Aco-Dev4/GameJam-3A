using UnityEngine;

public class OreHandler : MonoBehaviour
{
    public OreData oreData;

    public AudioSource audioS;
    public AudioClip hitSound;
    public AudioClip breakSound;

    // SEM potiahni Particle System, ktorý je dieťaťom (child) tohto objektu
    public ParticleSystem breakParticles;

    private float maxScalePercent;
    private float minScalePercent;

    public float currentHealth;

    void Start()
    {
        currentHealth = oreData.maxHealth;
        maxScalePercent = transform.localScale.x;
        minScalePercent = maxScalePercent * 0.7f;
    }

    public void HitOre(int damage)
    {
        // Pustíme partikle pri každom hite (ak ich chceš aj vtedy)
        if (breakParticles != null) breakParticles.Play();

        audioS.PlayOneShot(hitSound);
        currentHealth -= damage;

        float healthPercent = currentHealth / oreData.maxHealth;
        float targetScaleValue = Mathf.Lerp(minScalePercent, maxScalePercent, healthPercent);
        transform.localScale = new Vector3(targetScaleValue, targetScaleValue, targetScaleValue);

        if (currentHealth <= 0) Mined();
    }

    public void Mined()
    {
        // Zahráme finálny zvuk
        audioS.PlayOneShot(breakSound);

        if (breakParticles != null)
        {
            // TRIK: Odpojíme partikle od skaly, aby nezmizli spolu s ňou
            breakParticles.transform.SetParent(null);

            // Pustíme ich naposledy (ak by náhodou nestihli hitom)
            breakParticles.Play();

            // Zničíme objekt partiklov po tom, čo dobehnú (napr. po 2 sekundách)
            Destroy(breakParticles.gameObject, breakParticles.main.duration + 0.5f);
        }

        GameManager.Instance.AddMoney(oreData.value);

        // Zničíme samotnú skalu
        GameObject.Destroy(gameObject);
    }
}