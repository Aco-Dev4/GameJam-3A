using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public int money;
    public int quotaAmount;
    public int quotaTarget;
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
    public void AddToQuota(int amount)
    {
        quotaAmount += amount;
    }
    public void ResetQuota()
    {
        quotaAmount = 0;
    }
    public void SetQuotaTarget(int amount)
    {
        quotaTarget = amount;
    }
    public void IncreaseQuotaTarget(int amount)
    {
        quotaTarget += amount;
    }
}
