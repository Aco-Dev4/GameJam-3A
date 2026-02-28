using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    public float CurrentTime;
    public bool IsRunning;

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
        if (!IsRunning) return;

        CurrentTime -= Time.deltaTime;

        if (CurrentTime <= 0f)
        {
            IsRunning = false;
            CurrentTime = 0f;

            Debug.Log("Timer finished");
            // trigger event here
        }
    }
}