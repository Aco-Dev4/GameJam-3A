using UnityEngine;

public class Quota : MonoBehaviour
{
    public int quotaAmount;
    public int quotaTarget;
    int adder;
    int money;

    void Start()
    {
        quotaAmount = 0;
        money = 0;
        adder = 50;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            quotaAmount += 10; // Simulate mining ore and adding to quota
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (quotaAmount >= quotaTarget)
            {
                Debug.Log("Quota met!");
                money += (quotaAmount - quotaTarget);
                quotaTarget += adder;
                adder += 50;
                quotaAmount = 0;
                Debug.Log("Money: " + money);
                Debug.Log("New quota target: " + quotaTarget);
            }
        }
    }
}
