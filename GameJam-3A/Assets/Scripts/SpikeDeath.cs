using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeDeath : MonoBehaviour
{
    // GameManager u h¾ada nemusíme cez Find, pouijeme tvoj Singleton "manager"

    [Header("UI Prvky")]
    public MonoBehaviour playerMovementScript;
    public CanvasGroup fadeCanvasGroup;
    public GameObject deathText;

    [Header("Nastavenia")]
    public int sceneIndexToLoad;
    public float waitBeforeLoad = 3f;
    public float fadeDuration = 1f;

    private bool isTriggered = false;

    // OnTriggerEnter musí by samostatne, nie v Start()!
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;

            // LOGIKA PRE PENIAZE:
            // Ak stratíš 70%, znamená to, e ti zostane 30% (pôvodná hodnota * 0.3)
            // (int) tam dávame preto, lebo peniaze sú celé èísla a 0.3f je desatinné
            GameManager.manager.money = (int)(GameManager.manager.money * 0.3f);

            // Hneï aktualizujeme UI, aby hráè videl tú stratu
            GameManager.manager.SyncUI();

            StartCoroutine(SpikeSequence());
        }
    }

    // Coroutine musí by tie samostatne
    IEnumerator SpikeSequence()
    {
        // 1. ZASTAVÍME HRÁÈA
        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

        // 2. STMAVNUTIE
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

        // 4. PAUZA A NOVÁ SCÉNA
        yield return new WaitForSeconds(waitBeforeLoad);
        SceneManager.LoadScene(sceneIndexToLoad);
    }
}