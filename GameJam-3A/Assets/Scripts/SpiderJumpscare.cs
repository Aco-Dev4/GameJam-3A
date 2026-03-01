using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Potrebné pre prepínanie scén

public class SpiderJumpscare : MonoBehaviour
{
    [Header("Objekty v scéne")]
    public Transform spider;
    public MonoBehaviour playerMovementScript;
    public CanvasGroup fadeCanvasGroup;
    public GameObject scareText;

    [Header("Nastavenia Scény")]
    public int sceneIndexToLoad; // Sem napíš èíslo scény (napr. 1)
    public float jumpHeight = 2f;

    private float totalWaitTime = 5f;
    private float jumpDuration = 0.4f;
    private float fadeDuration = 1f;

    private bool isTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            StartCoroutine(JumpscareSequence(other.transform));
        }
    }

    IEnumerator JumpscareSequence(Transform playerTransform)
    {
        // 1. VYPÍNAME MOVEMENT
        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

        // 2. SKOK PAVÚKA
        Vector3 startPos = spider.position;
        float elapsed = 0f;
        while (elapsed < jumpDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / jumpDuration;
            float currentHeight = 4f * jumpHeight * t * (1f - t);
            Vector3 currentPos = Vector3.Lerp(startPos, playerTransform.position, t);
            currentPos.y += currentHeight;
            spider.position = currentPos;
            yield return null;
        }

        // 3. STMAVNUTIE OBRAZOVKY
        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            if (fadeCanvasGroup != null)
                fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            yield return null;
        }
        if (fadeCanvasGroup != null) fadeCanvasGroup.alpha = 1f;

        // 4. ZOBRAZENIE TEXTU
        if (scareText != null)
            scareText.SetActive(true);

        // Poèkáme, kým uplynie celkový èas 5 sekúnd
        yield return new WaitForSeconds(totalWaitTime - jumpDuration - fadeDuration);

        // 5. NAÈÍTANIE NOVEJ SCÉNY
        SceneManager.LoadScene(sceneIndexToLoad);
    }
}