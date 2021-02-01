using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryViewController : View {
    [SerializeField] private GameObject upperSectionContainer;
    [SerializeField] private GameObject lowerSection;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button craftingButton;
    [SerializeField] private Button defenceButton;
    [SerializeField] private Button tileUpgradesButton;
    [SerializeField] private Transform xTransform;
    [SerializeField] private Transform yTransform;

    private const int DEFAULT_NUMBER_OF_SLOTS = 24;

    private EquipmentView equipmentView;
    private CraftingView craftingView;
    private Board boardView;
    private ItemListView itemListView;

    private enum Tab {
        Inventory,
        Crafting,
        Defence,
        TileUpgrades
    }

    private Tab _currentTab;
    private Tab currentTab {
        get => _currentTab;
        set {
            _currentTab = value;
            switch(value) {
                case Tab.Inventory:
                    ShowUpper(equipmentView);
                    break;
                case Tab.Crafting:
                    ShowUpper(craftingView);
                    break;
                case Tab.Defence:
                    ShowUpper(boardView);
                    break;
                case Tab.TileUpgrades:
                    ShowUpper(boardView);
                    break;
                default:
                    throw new Exception("Tab not found. Please check for errors.");
            }
        }
    }

    private void ShowUpper(View view) {
        equipmentView.gameObject.SetActive(false);
        craftingView.gameObject.SetActive(false);
        boardView.gameObject.SetActive(false);

        view.gameObject.SetActive(true);
    }

    void Start() {
        equipmentView = Factory.CreateView<EquipmentView>();
        equipmentView.transform.SetParent(upperSectionContainer.transform,false);

        craftingView = Factory.CreateView<CraftingView>();
        craftingView.transform.SetParent(upperSectionContainer.transform, false);

        // TODO: Data drive board based on user's level
        boardView = Factory.CreateView<Board4>();
        boardView.transform.SetParent(upperSectionContainer.transform, false);
        // boardView.yTransform.rotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
        // boardView.xTransform.rotation = Quaternion.identity;
        // boardView.xTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        itemListView = Factory.CreateView<ItemListView>();
        itemListView.transform.SetParent(lowerSection.transform, false);

        ShowUpper(equipmentView);
        PopulateInventory();

        closeButton.onClick.AddListener(() => { Destroy(gameObject); });
        inventoryButton.onClick.AddListener(() => { currentTab = Tab.Inventory; });
        craftingButton.onClick.AddListener(() => { currentTab = Tab.Crafting; });
        defenceButton.onClick.AddListener(() => { currentTab = Tab.Defence; });
        tileUpgradesButton.onClick.AddListener(() => { currentTab = Tab.TileUpgrades; });
    }

    private void PopulateInventory() {
        //TODO: Need to create a list of items and populate
        for (int slotNumber = 0; slotNumber < DEFAULT_NUMBER_OF_SLOTS; slotNumber++) {
            var itemSlot = Factory.CreateView<ItemSlotView>();
            itemSlot.type = ItemSlotType.Any;
            itemSlot.transform.SetParent(itemListView.grid.transform);
        }
    }
}