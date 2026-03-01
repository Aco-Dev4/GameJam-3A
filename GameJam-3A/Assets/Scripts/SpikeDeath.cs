using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeDeath : MonoBehaviour
{
    [Header("UI Prvky")]
    public MonoBehaviour playerMovementScript;
    public CanvasGroup fadeCanvasGroup;
    public GameObject deathText; // Text typu "Nabodol si sa!"

    [Header("Nastavenia")]
    public int sceneIndexToLoad;
    public float waitBeforeLoad = 3f; // Ako dlho m· byù tma a text, k˝m sa prepne scÈna
    public float fadeDuration = 1f;

    private bool isTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            StartCoroutine(SpikeSequence());
        }
    }

    IEnumerator SpikeSequence()
    {
        // 1. ZASTAVÕME HR¡»A
        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

        // 2. STMAVNUTIE (pouûijeme ten ist˝ FadeTrans panel)
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            if (fadeCanvasGroup != null)
                fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            yield return null;
        }
        if (fadeCanvasGroup != null) fadeCanvasGroup.alpha = 1f;

        // 3. TEXT
        if (deathText != null)
            deathText.SetActive(true);

        // 4. PAUZA A NOV¡ SC…NA
        yield return new WaitForSeconds(waitBeforeLoad);
        SceneManager.LoadScene(sceneIndexToLoad);
    }
}