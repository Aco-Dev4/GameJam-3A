using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public int value;
    public int money;
    public int drillLevel;
    public int drillModel;
    public int drillcost;

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
        value += amount;
    }
    public void SubtractValue(int amount)
    {
        value -= amount;
    }
    public void ResetValue()
    {
        value = 0;
    }
    public void AddMoney(int amount)
    {
        money += amount;
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
    public void IncreaseDrillCost(int amount)
    {
        drillcost += amount;
    }
}
