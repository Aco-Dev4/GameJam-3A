using UnityEngine;

public class Quota : MonoBehaviour
{
    int quotaAmount;
    int quotaTarget;
    int money;
    private void Start()
    {
        quotaAmount = GameManager.manager.quotaAmount;
        quotaTarget = GameManager.manager.quotaTarget;
        money = GameManager.manager.money;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (quotaAmount >= quotaTarget)
            {
                Debug.Log("Quota met!");
                GameManager.manager.AddMoney(quotaAmount - quotaTarget);
                GameManager.manager.ResetQuota();
                Debug.Log("Money: " + money);
                Debug.Log("New quota target: " + quotaTarget);
            }
        }
    }
}
