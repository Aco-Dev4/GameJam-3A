using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeDeath : MonoBehaviour
{
    // GameManager uû hæadaù nemusÌme cez Find, pouûijeme tvoj Singleton "manager"

    [Header("UI Prvky")]
    public MonoBehaviour playerMovementScript;
    public CanvasGroup fadeCanvasGroup;
    public GameObject deathText;

    [Header("Nastavenia")]
    public AudioSource audioSource;
    public AudioClip hit;
    public int sceneIndexToLoad;
    public float waitBeforeLoad = 3f;
    public float fadeDuration = 1f;

    private bool isTriggered = false;

    // OnTriggerEnter musÌ byù samostatne, nie v Start()!
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;

            // LOGIKA PRE PENIAZE:
            // Ak stratÌö 70%, znamen· to, ûe ti zostane 30% (pÙvodn· hodnota * 0.3)
            // (int) tam d·vame preto, lebo peniaze s˙ celÈ ËÌsla a 0.3f je desatinnÈ
            GameManager.manager.money = (int)(GameManager.manager.money * 0.3f);

            // HneÔ aktualizujeme UI, aby hr·Ë videl t˙ stratu
            GameManager.manager.SyncUI();

            StartCoroutine(SpikeSequence());
        }
    }

    // Coroutine musÌ byù tieû samostatne
    IEnumerator SpikeSequence()
    {
        // 1. ZASTAVÕME HR¡»A
        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

        // 2. STMAVNUTIE
        audioSource.PlayOneShot(hit);
        float elapsed = 0f;
        if (fadeCanvasGroup != null)
        {
            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
                yield return null;
            }
            fadeCanvasGroup.alpha = 1f;
        }

        // 3. TEXT
        if (deathText != null)
            deathText.SetActive(true);

        // 4. PAUZA A NOV¡ SC…NA
        yield return new WaitForSeconds(waitBeforeLoad);
        SceneManager.LoadScene(sceneIndexToLoad);
    }
}