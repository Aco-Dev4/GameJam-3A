using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpiderJumpscare : MonoBehaviour
{
    [Header("Objekty v sc�ne")]
    public Transform spider;
    public MonoBehaviour playerMovementScript;
    public CanvasGroup fadeCanvasGroup;
    public GameObject scareText;

    [Header("Nastavenia Sc�ny")]
    public AudioSource audioSource;
    public AudioClip hit;
    public int sceneIndexToLoad;
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

            // LOGIKA PRE PENIAZE (Strata 70%, zost�va 30%)
            if (GameManager.Instance != null)
            {
                GameManager.Instance.money = (int)(GameManager.Instance.money * 0.3f);
                GameManager.Instance.SyncUI(); // Okam�it� aktualiz�cia UI
            }

            StartCoroutine(JumpscareSequence(other.transform));
        }
    }

    IEnumerator JumpscareSequence(Transform playerTransform)
    {
        // 1. VYP�NAME MOVEMENT
        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

        // 2. SKOK PAV�KA
        audioSource.PlayOneShot(hit);
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

        // Po�k�me, k�m uplynie celkov� �as
        yield return new WaitForSeconds(totalWaitTime - jumpDuration - fadeDuration);

        // 5. NA��TANIE NOVEJ SC�NY
        SceneManager.LoadScene(sceneIndexToLoad);
    }
}