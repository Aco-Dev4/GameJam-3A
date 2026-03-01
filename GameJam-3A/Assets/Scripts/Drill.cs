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
        drill_level = GameManager.manager.drillLevel;
        drill_model = GameManager.manager.drillModel;
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
            int amountNeeded = GameManager.manager.quotaTarget - GameManager.manager.quotaAmount;
            int transfer = Mathf.Min(GameManager.manager.money, amountNeeded);
            GameManager.manager.AddValue(transfer);
            GameManager.manager.AddMoney(-transfer);
            if (GameManager.manager.quotaAmount == GameManager.manager.quotaTarget)
            {
                GameManager.manager.ResetValue();
                UpgradeDrill();
            }
        }
    }
    public void UpgradeDrill()
    {
        audioS.PlayOneShot(repair);
        drill_level++;
        GameManager.manager.IncreaseDrillLevel();
        switch (drill_level)
        {
            case 1:
                GameManager.manager.IncreaseTarget(150);
                GameManager.manager.UpgradeModel();
                drill_model++;
                Destroy(transform.GetChild(0).gameObject);
                GameObject drill1 = Instantiate(drill_models[drill_model], transform.position, transform.rotation, transform);
                drill1.transform.localScale *= scale;
                break;
            case 2:
                GameManager.manager.IncreaseTarget(150);
                break;
            case 3:
                GameManager.manager.IncreaseTarget(200);
                GameManager.manager.UpgradeModel();
                drill_model++;
                Destroy(transform.GetChild(0).gameObject);
                GameObject drill2 = Instantiate(drill_models[drill_model], transform.position, transform.rotation, transform);
                drill2.transform.localScale *= scale;
                break;
            case 4:
                GameManager.manager.IncreaseTarget(300);
                break;
            case 5:
                GameManager.manager.IncreaseTarget(500);
                GameManager.manager.UpgradeModel();
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
