using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Stats")]
    public int quotaTarget;
    public int quotaAmount;
    public int money;
    public int ores;

    [Header("Drill")]
    public int drillLevel;
    public int drillModel;

    public PlayerData uiData; // Priraď v Inspectore

    // Zavolaj vždy, keď sa zmenia peniaze alebo level
    public void SyncUI()
    {
        if (uiData != null) uiData.UpdateValues(money, quotaTarget, quotaAmount);
    }

    void Update()
    {
        SyncUI();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddValue(int amount)
    {
        quotaAmount += amount;
    }
    public void ResetValue()
    {
        quotaAmount = 0;
    }
    public void AddMoney(int amount)
    {
        money += amount;
    }
    public void IncreaseTarget(int amount)
    {
        quotaTarget += amount;
    }
    public void ResetTarget()
    {
        quotaTarget = 200;
    }
    public void SubtractMoney(int amount)
    {
        money -= amount;
    }
    public void ResetMoney()
    {
        money = 0;
    }
    public void IncreaseDrillLevel()
    {
        drillLevel++;
    }
    public void UpgradeModel()
    {
        drillModel++;
    }
}
