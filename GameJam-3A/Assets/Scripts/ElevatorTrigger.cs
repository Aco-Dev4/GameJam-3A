using System.Collections;

using UnityEngine;

using UnityEngine.SceneManagement;



public class ElevatorTrigger : MonoBehaviour

{

    [Header("Nastavenia Hr��a")]

    public string playerTag = "Player";

    public MonoBehaviour playerMovementScript;



    [Header("Objekty na posunutie (V��ah)")]

    public Transform object1ToMove;

    public Transform object2ToMove;



    [Header("Nastavenia pohybu")]

    public float posunY = -5f;

    public float trvaniePohybu = 5f;



    [Header("Sc�na a Stmievanie")]

    // 1 - Obchod

    // 2 - Podzemie

    public int indexNovejSceny = 1;



    public CanvasGroup fadeCanvasGroup;

    public float casStmievania = 2f;



    [Header("Zvuk V��ahu")]

    public AudioSource audioSource; // Komponent Audio Source

    public AudioClip elevatorSound; // Zvukov� s�bor (napr. v�zganie)



    private bool isPlayerInZone = false;

    private bool isSequenceRunning = false;



    private void OnTriggerEnter(Collider other)

    {

        if (other.CompareTag(playerTag))

            isPlayerInZone = true;

    }



    private void OnTriggerExit(Collider other)

    {

        if (other.CompareTag(playerTag))

            isPlayerInZone = false;

    }



    private void Update()

    {

        if (isPlayerInZone && Input.GetKeyDown(KeyCode.E) && !isSequenceRunning)

            StartCoroutine(ElevatorSequence());

    }



    private IEnumerator ElevatorSequence()
    {
        isSequenceRunning = true;
        // Vypnutie pohybu
        if (playerMovementScript != null)
        playerMovementScript.enabled = false;
         // Spustenie zvuku
        if (audioSource != null && elevatorSound != null)
        {
            audioSource.clip = elevatorSound;
            audioSource.Play();
        }


        Vector3 startPos1 = object1ToMove.position;
        Vector3 startPos2 = object2ToMove.position;

        Vector3 targetPos1 = startPos1 + new Vector3(0, posunY, 0);
        Vector3 targetPos2 = startPos2 + new Vector3(0, posunY, 0);

        float elapsedTime = 0f;
        float fadeStartTime = trvaniePohybu - casStmievania;

        if (fadeCanvasGroup != null)

        {

            fadeCanvasGroup.alpha = 0f;

            fadeCanvasGroup.gameObject.SetActive(true);

        }

        while (elapsedTime < trvaniePohybu)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / trvaniePohybu;
            object1ToMove.position = Vector3.Lerp(startPos1, targetPos1, normalizedTime);
            object2ToMove.position = Vector3.Lerp(startPos2, targetPos2, normalizedTime);



            if (fadeCanvasGroup != null && elapsedTime >= fadeStartTime)

            {

                float fadeProgress = (elapsedTime - fadeStartTime) / casStmievania;

                fadeCanvasGroup.alpha = Mathf.Clamp01(fadeProgress);

            }
            yield return null;
        }

        object1ToMove.position = targetPos1;

        object2ToMove.position = targetPos2;
        if (fadeCanvasGroup != null)

            fadeCanvasGroup.alpha = 1f;
        SceneManager.LoadScene(indexNovejSceny);

    }

}