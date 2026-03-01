using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeDeath : MonoBehaviour
{
    // GameManager u� h�ada� nemus�me cez Find, pou�ijeme tvoj Singleton "manager"

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

    // OnTriggerEnter mus� by� samostatne, nie v Start()!
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;

            // LOGIKA PRE PENIAZE:
            // Ak strat� 70%, znamen� to, �e ti zostane 30% (p�vodn� hodnota * 0.3)
            // (int) tam d�vame preto, lebo peniaze s� cel� ��sla a 0.3f je desatinn�
            GameManager.Instance.money = (int)(GameManager.Instance.money * 0.3f);

            // Hne� aktualizujeme UI, aby hr�� videl t� stratu
            GameManager.Instance.SyncUI();

            StartCoroutine(SpikeSequence());
        }
    }

    // Coroutine mus� by� tie� samostatne
    IEnumerator SpikeSequence()
    {
        // 1. ZASTAV�ME HR��A
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

        // 4. PAUZA A NOV� SC�NA
        yield return new WaitForSeconds(waitBeforeLoad);
        SceneManager.LoadScene(sceneIndexToLoad);
    }
}