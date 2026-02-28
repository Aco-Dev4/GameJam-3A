using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    [Header("Game Stats")]
    public int quotaTarget;
    public int quotaAmount;
    public int money;

    [Header("Drill")]
    public int drillLevel;
    public int drillModel;
    public int drillcost;

    public PlayerData uiData; // Priraď v Inspectore

    // Zavolaj vždy, keď sa zmenia peniaze alebo level
    public void SyncUI()
    {
        if (uiData != null) uiData.UpdateValues(money, quotaTarget, quotaAmount);
    }

    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddValue(int amount)
    {
        quotaAmount += amount;
    }
    public void SubtractMoney(int amount)
    {
        money -= amount;
    }
    public void IncreaseDrillLevel()
    {
        drillLevel++;
    }
    public void UpgradeModel()
    {
        drillModel++;
    }
    public void IncreaseDrillCost(int amount)
    {
        drillcost += amount;
    }
}
