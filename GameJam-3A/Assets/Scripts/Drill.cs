using UnityEngine;

public class Drill : MonoBehaviour
{
    public GameObject[] drill_models;
    public AudioSource audioS;
    public AudioClip repair;
    public AudioClip finish;
    public float scale;
    int drill_level;
    int drill_model;
    bool isPlayerInZone = false;
    public string playerTag = "Player";
    void Start()
    {
        drill_level = GameManager.Instance.drillLevel;
        drill_model = GameManager.Instance.drillModel;
        GameObject drill = Instantiate(drill_models[drill_model], transform.position, transform.rotation, transform);
    }
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
        if (isPlayerInZone && Input.GetKeyDown(KeyCode.E))
        {
            int amountNeeded = GameManager.Instance.quotaTarget - GameManager.Instance.quotaAmount;
            int transfer = Mathf.Min(GameManager.Instance.money, amountNeeded);
            GameManager.Instance.AddValue(transfer);
            GameManager.Instance.AddMoney(-transfer);
            if (GameManager.Instance.quotaAmount == GameManager.Instance.quotaTarget)
            {
                GameManager.Instance.ResetValue();
                UpgradeDrill();
            }
        }
    }
    public void UpgradeDrill()
    {
        audioS.PlayOneShot(repair);
        drill_level++;
        GameManager.Instance.IncreaseDrillLevel();
        switch (drill_level)
        {
            case 1:
                TimerManager.Instance.StartTimer(120);
                GameManager.Instance.IncreaseTarget(150);
                GameManager.Instance.UpgradeModel();
                drill_model++;
                Destroy(transform.GetChild(0).gameObject);
                GameObject drill1 = Instantiate(drill_models[drill_model], transform.position, transform.rotation, transform);
                drill1.transform.localScale *= scale;
                break;
            case 2:
                TimerManager.Instance.AddTime(120);
                GameManager.Instance.IncreaseTarget(150);
                break;
            case 3:
                TimerManager.Instance.AddTime(150);
                GameManager.Instance.IncreaseTarget(200);
                GameManager.Instance.UpgradeModel();
                drill_model++;
                Destroy(transform.GetChild(0).gameObject);
                GameObject drill2 = Instantiate(drill_models[drill_model], transform.position, transform.rotation, transform);
                drill2.transform.localScale *= scale;
                break;
            case 4:
                TimerManager.Instance.AddTime(200);
                GameManager.Instance.IncreaseTarget(300);
                break;
            case 5:
                TimerManager.Instance.AddTime(250);
                GameManager.Instance.IncreaseTarget(500);
                GameManager.Instance.UpgradeModel();
                drill_model++;
                Destroy(transform.GetChild(0).gameObject);
                GameObject drill3 = Instantiate(drill_models[drill_model], transform.position, transform.rotation, transform);
                drill3.transform.localScale *= scale;
                break;
            default:
                audioS.PlayOneShot(finish);
                // Game finished, max drill level reached
                return;
        }
    }
}
