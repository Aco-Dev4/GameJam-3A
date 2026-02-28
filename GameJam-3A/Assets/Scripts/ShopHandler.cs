using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopHandler : MonoBehaviour
{
    private ShopManager shopManager;

    private UIDocument _document;

    void Awake()
    {
        _document = GetComponent<UIDocument>();
        shopManager = GetComponent<ShopManager>();
    }

    private void OnEnable()
    {
        VisualElement root = _document.rootVisualElement;

        Button pickaxeButton = root.Q<Button>("PickaxeButton");
        Button bootsButton = root.Q<Button>("BootsButton");
        Button machineTimeButton = root.Q<Button>("MachineTimeButton");

        Button closeButton = root.Q<Button>("CloseButton");

        pickaxeButton.RegisterCallback<ClickEvent>(shopManager.PickaxeUpgrade);
        bootsButton.RegisterCallback<ClickEvent>(shopManager.BootsUpgrade);
        machineTimeButton.RegisterCallback<ClickEvent>(shopManager.MachineTimeUpgrade);

        closeButton.RegisterCallback<ClickEvent>(evt => _document.enabled = false);
    }
}
