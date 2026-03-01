using UnityEngine;
using UnityEngine.SceneManagement; // 1. DÔLEITÉ: Musíme prida túto kninicu pre prácu so scénami

public class TimerManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    public static TimerManager Instance;

    public float CurrentTime;
    public bool IsRunning;

    [Header("Nastavenia scény po vypršaní èasu")]
    public int sceneIndexToLoad; // 2. TU si v Inspectore nastavíš èíslo scény z Build Settings

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartTimer(float startTime)
    {
        CurrentTime = startTime;
        IsRunning = true;
    }

    public void StopTimer()
    {
        IsRunning = false;
    }

    public void ResetTimer()
    {
        CurrentTime = 1f;
        IsRunning = false;
    }

    public void AddTime(float timeToAdd)
    {
        CurrentTime += timeToAdd;
    }

    private void Update()
    {
        playerData.currentTime = Mathf.RoundToInt(CurrentTime); // Synchronizuj s PlayerData

        if (!IsRunning) return;

        CurrentTime -= Time.deltaTime;

        if (CurrentTime <= 0f)
        {
            IsRunning = false;
            CurrentTime = 0f;

            Debug.Log("Timer finished. Naèítavam novú scénu!");

            // 3. PREPNUTIE SCÉNY
            SceneManager.LoadScene(sceneIndexToLoad);
        }
    }
}