using UnityEngine;
using UnityEngine.UIElements;

public class ShopManager : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private GameManager manager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private UIDocument _document;

    [Header("Data")]
    [SerializeField] private PickaxeUpgradeData pickaxeUpgradeData;
    [SerializeField] private BootUpgradeDtat bootUpgradeDtat;
    [SerializeField] private MachineTimeUpgradedta machineTimeUpgradedta;

    [Header("Boot Effect")]
    [SerializeField] private float bootUpgrade = 5f;

    private PlayerData playerData;

    private VisualElement _pickaxeUpgrade, _bootsUpgrade, _timeUpgrade;

    private const int MaxLevel = 3;

    void Awake()
    {
        manager = GameManager.Instance;
        if (playerController == null) playerController = FindFirstObjectByType<PlayerController>();
        if (playerController != null) playerData = playerController.playerData;

        Debug.Log($"[ShopManager] manager={(manager != null)}, playerController={(playerController != null)}, playerData={(playerData != null)}, doc={(_document != null)}");
    }

    void OnEnable()
    {
        CacheUI();
        RefreshUI();
        RefreshPrices();
    }

    private void CacheUI()
    {
        if (_document == null) { Debug.LogError("[ShopManager] UIDocument is NULL"); return; }

        VisualElement root = _document.rootVisualElement;
        if (root == null) { Debug.LogError("[ShopManager] rootVisualElement is null."); return; }

        _pickaxeUpgrade = root.Q<VisualElement>("PickaxeUpgrade");
        _bootsUpgrade = root.Q<VisualElement>("BootsUpgrade");
        _timeUpgrade = root.Q<VisualElement>("MachineTimeUpgrade");

        Debug.Log($"[ShopManager] UI Found: pickaxe={_pickaxeUpgrade != null}, boots={_bootsUpgrade != null}, time={_timeUpgrade != null}");
    }

    private bool TrySpend(int cost)
    {
        if (manager == null) { Debug.LogError("[ShopManager] GameManager is NULL"); return false; }
        if (cost <= 0) return false;
        if (manager.money < cost) { Debug.Log("[ShopManager] Not enough money"); return false; }
        manager.money -= cost;
        return true;
    }

    private int PriceForLevel(int level)
    {
        // UPRAV SI CENY TU (level je aktuálny level pred upgrade)
        if (level <= 1) return 50;   // keď je level 1 -> ďalší upgrade stojí 50
        if (level == 2) return 150;  // keď je level 2 -> ďalší upgrade stojí 150
        return 0;                    // max
    }

    private void RefreshPrices()
    {
        if (playerData == null) return;

        pickaxeUpgradeData.price = playerData.pickaxeLevel >= MaxLevel ? 0 : PriceForLevel(playerData.pickaxeLevel);
        bootUpgradeDtat.price = playerData.bootLevel >= MaxLevel ? 0 : PriceForLevel(playerData.bootLevel);
        machineTimeUpgradedta.picrce = playerData.machineTimeLevel >= MaxLevel ? 0 : PriceForLevel(playerData.machineTimeLevel);
    }

    private void RefreshUI()
    {
        if (playerData == null) return;

        if (_pickaxeUpgrade != null)
            _pickaxeUpgrade.style.display = playerData.pickaxeLevel >= MaxLevel ? DisplayStyle.None : DisplayStyle.Flex;

        if (_bootsUpgrade != null)
            _bootsUpgrade.style.display = playerData.bootLevel >= MaxLevel ? DisplayStyle.None : DisplayStyle.Flex;

        if (_timeUpgrade != null)
            _timeUpgrade.style.display = playerData.machineTimeLevel >= MaxLevel ? DisplayStyle.None : DisplayStyle.Flex;
    }

    public void PickaxeUpgrade()
    {
        if (playerData == null) return;
        if (playerData.pickaxeLevel >= MaxLevel) return;

        RefreshPrices();
        if (!TrySpend(pickaxeUpgradeData.price)) return;

        playerData.pickaxeLevel++;
        RefreshPrices();
        RefreshUI();
    }

    public void BootsUpgrade()
    {
        if (playerData == null) return;
        if (playerData.bootLevel >= MaxLevel) return;

        RefreshPrices();
        if (!TrySpend(bootUpgradeDtat.price)) return;

        playerData.speed += bootUpgrade;
        playerData.bootLevel++;

        RefreshPrices();
        RefreshUI();
    }

    public void MachineTimeUpgrade()
    {
        if (playerData == null) return;
        if (playerData.machineTimeLevel >= MaxLevel) return;

        RefreshPrices();
        if (!TrySpend(machineTimeUpgradedta.picrce)) return;

        TimerManager.Instance.AddTime(30);
        playerData.machineTimeLevel++;

        RefreshPrices();
        RefreshUI();
    }
}