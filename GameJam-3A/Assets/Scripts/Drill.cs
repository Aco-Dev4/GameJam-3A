using UnityEngine;

public class Drill : MonoBehaviour
{
    public GameObject[] drill_models;
    public AudioSource audioS;
    public AudioClip too_poor;
    public AudioClip repair;
    public AudioClip finish;
    int drill_level;
    bool isPlayerInZone = false;
    public string playerTag = "Player";
    void Start()
    {
        drill_level = GameManager.manager.drillLevel;
        GameObject drill = Instantiate(drill_models[drill_level], transform.position, transform.rotation, transform);
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
            if (GameManager.manager.money >= GameManager.manager.drillcost)
            {
                GameManager.manager.SubtractMoney(GameManager.manager.drillcost);
                UpgradeDrill();
            }
            else
                audioS.PlayOneShot(too_poor);
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
                GameManager.manager.IncreaseDrillCost(150);
                GameManager.manager.UpgradeModel();
                Destroy(transform.GetChild(0).gameObject);
                GameObject drill1 = Instantiate(drill_models[drill_level], transform.position, Quaternion.identity, transform);
                break;
            case 2:
                GameManager.manager.IncreaseDrillCost(150);
                break;
            case 3:
                GameManager.manager.IncreaseDrillCost(200);
                GameManager.manager.UpgradeModel();
                Destroy(transform.GetChild(0).gameObject);
                GameObject drill2 = Instantiate(drill_models[drill_level], transform.position, Quaternion.identity, transform);
                break;
            case 4:
                GameManager.manager.IncreaseDrillCost(300);
                break;
            case 5:
                GameManager.manager.IncreaseDrillCost(500);
                GameManager.manager.UpgradeModel();
                Destroy(transform.GetChild(0).gameObject);
                GameObject drill3 = Instantiate(drill_models[drill_level], transform.position, Quaternion.identity, transform);
                break;
            default:
                audioS.PlayOneShot(finish);
                // Game finished, max drill level reached
                return;
        }
    }
}
