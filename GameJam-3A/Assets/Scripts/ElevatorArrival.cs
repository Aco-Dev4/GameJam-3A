using System.Collections;
using UnityEngine;

public class ElevatorArrival : MonoBehaviour
{
    [Header("Nastavenia Hr·Ëa")]
    // Skript pre pohyb hr·Ëa, ktor˝ na zaËiatku vypneme a na konci zapneme
    public MonoBehaviour playerMovementScript;

    [Header("Objekty na posunutie (V˝ùah)")]
    public Transform object1ToMove;
    public Transform object2ToMove;

    [Header("Nastavenia pohybu")]
    public float posunY = -3f; // O koæko sa eöte posun˙ pri prÌchode (napr. doöliapnutie v˝ùahu)
    public float trvaniePohybu = 2.5f; // Ako dlho bude trvaù pohyb a zamknutie

    [Header("Odtmievanie (Fade In)")]
    [Tooltip("Panel v UI s CanvasGroup, ktor˝ bude na zaËiatku Ëierny")]
    public CanvasGroup fadeCanvasGroup;
    public float casOdtmievania = 1.5f; // Ako r˝chlo Ëierna obrazovka zmizne

    [Header("Zvuk V˝ùahu")]
    public AudioSource audioSource;
    public AudioClip elevatorSound;

    private void Start()
    {
        // HneÔ ako sa naËÌta scÈna, spustÌme t˙to sekvenciu
        StartCoroutine(ArrivalSequence());
    }

    private IEnumerator ArrivalSequence()
    {
        // 1. ZAMKNUTIE POHYBU HR¡»A
        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

        // 2. NASTAVENIE OBRAZOVKY NA ⁄PLNE »IERNU
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 1f;
            fadeCanvasGroup.gameObject.SetActive(true);
        }

        // 3. SPUSTENIE ZVUKU
        if (audioSource != null && elevatorSound != null)
        {
            audioSource.clip = elevatorSound;
            audioSource.Play();
        }

        // Uloûenie pozÌciÌ pre posun
        Vector3 startPos1 = object1ToMove.position;
        Vector3 startPos2 = object2ToMove.position;

        Vector3 targetPos1 = startPos1 + new Vector3(0, posunY, 0);
        Vector3 targetPos2 = startPos2 + new Vector3(0, posunY, 0);

        float elapsedTime = 0f;

        // 4. SAMOTN¡ ANIM¡CIA POHYBU A ODTMIEVANIA
        while (elapsedTime < trvaniePohybu)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / trvaniePohybu;

            // Plynul˝ posun objektov
            object1ToMove.position = Vector3.Lerp(startPos1, targetPos1, normalizedTime);
            object2ToMove.position = Vector3.Lerp(startPos2, targetPos2, normalizedTime);

            // Zosvetæovanie obrazovky (z Alpha 1 na 0)
            if (fadeCanvasGroup != null && elapsedTime <= casOdtmievania)
            {
                float fadeProgress = elapsedTime / casOdtmievania;
                fadeCanvasGroup.alpha = Mathf.Lerp(1f, 0f, fadeProgress);
            }

            yield return null;
        }

        // Uistenie sa, ûe po skonËenÌ Ëasu je vöetko na spr·vnom mieste
        object1ToMove.position = targetPos1;
        object2ToMove.position = targetPos2;

        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 0f;
            fadeCanvasGroup.gameObject.SetActive(false);
        }

        // Zastavenie zvuku (aby sa uùal presne so skonËenÌm anim·cie)
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();

        // 5. ODOMKNUTIE POHYBU HR¡»A
        if (playerMovementScript != null)
            playerMovementScript.enabled = true;
    }
}