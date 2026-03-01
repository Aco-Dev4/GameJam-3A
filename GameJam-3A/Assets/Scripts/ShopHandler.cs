using UnityEngine;
using UnityEngine.UIElements;

public class ShopHandler : MonoBehaviour
{
    private ShopManager shopManager;

    [SerializeField] private UIDocument _document;
    [SerializeField] private UIDocument _guiDoc; // ostáva zapnutý, len ho tu nechávam kvôli referencii
    [SerializeField] private GameObject _shopText;

    private Button pickaxeButton, bootsButton, machineTimeButton, closeButton;

    private bool _inShopZone;
    public bool _shopOpen;

    void Awake()
    {
        shopManager = GetComponent<ShopManager>();
        Debug.Log($"[ShopHandler] UIDocument: {_document != null}, ShopManager: {shopManager != null}");

        if (_document != null) _document.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shop"))
        {
            _inShopZone = true;
            _shopText.SetActive(true);
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Shop"))
        {
            _inShopZone = false;
            _shopText.SetActive(false);
        }
    }

    void Update()
    {
        if (!_inShopZone) return;
        if (!Input.GetKeyDown(KeyCode.E)) return;

        if (_shopOpen) CloseShop();
        else OpenShop();
    }

    private void OpenShop()
    {
        if (_document == null) { Debug.LogError("[ShopHandler] Missing _document reference."); return; }
        if (shopManager == null) { Debug.LogError("[ShopHandler] Missing ShopManager on same GameObject."); return; }

        _document.enabled = true;
        _shopOpen = true;

        BindUI(); // vždy pri otvorení znova bindni (bezpečne)

        Debug.Log("[ShopHandler] Shop opened.");
    }

    private void BindUI()
    {
        VisualElement root = _document.rootVisualElement;
        if (root == null) { Debug.LogError("[ShopHandler] rootVisualElement is null."); return; }

        pickaxeButton = root.Q<Button>("PickaxeButton");
        bootsButton = root.Q<Button>("BootsButton");
        machineTimeButton = root.Q<Button>("MachineTimeButton");
        closeButton = root.Q<Button>("CloseButton");

        Debug.Log($"[ShopHandler] Buttons found: pickaxe={pickaxeButton != null}, boots={bootsButton != null}, machine={machineTimeButton != null}, close={closeButton != null}");

        if (pickaxeButton == null || bootsButton == null || machineTimeButton == null || closeButton == null)
        {
            Debug.LogError("[ShopHandler] One or more buttons not found. In UXML set name=\"PickaxeButton\" etc.");
            return;
        }

        // Najprv odpoj (aby si nemal duplicitné handlery), potom pripoj.
        pickaxeButton.clicked -= shopManager.PickaxeUpgrade;
        bootsButton.clicked -= shopManager.BootsUpgrade;
        machineTimeButton.clicked -= shopManager.MachineTimeUpgrade;
        closeButton.clicked -= CloseShop;

        pickaxeButton.clicked += shopManager.PickaxeUpgrade;
        bootsButton.clicked += shopManager.BootsUpgrade;
        machineTimeButton.clicked += shopManager.MachineTimeUpgrade;
        closeButton.clicked += CloseShop;

        // Debug: ak toto nikdy nevypíše pri kliknutí, input nejde do UI Toolkitu v tej scéne.
        root.RegisterCallback<PointerDownEvent>(e => Debug.Log("[UITK] PointerDown reached root"), TrickleDown.TrickleDown);
    }

    private void CloseShop()
    {
        if (_document != null) _document.enabled = false;
        _shopOpen = false;

        Debug.Log("[ShopHandler] Shop closed.");
    }
}