using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : View {
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

    private List<Item> allItems;
    private Dictionary<ItemType, Item> equippedItems = new Dictionary<ItemType, Item>();
    private List<Item> unequippedItems = new List<Item>();

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

        allItems = new List<Item> {
            new Item {
                id = "100",
                name = "Ice sword",
                equipped = true,
                type = ItemType.MainHand,
                stats = new List<Stat> {
                    new Stat {
                        type = StatType.Damage,
                        value = 15
                    }
                }
            },
            new Item {
                id = "101",
                name = "Fire sword",
                equipped = false,
                type = ItemType.MainHand,
                stats = new List<Stat> {
                    new Stat {
                        type = StatType.Crit,
                        value = 5
                    }
                }
            },
            new Item {
                id = "200",
                name = "Noble shield",
                equipped = true,
                type = ItemType.OffHand,
                stats = new List<Stat> {
                    new Stat {
                        type = StatType.Crit,
                        value = 5
                    }
                }
            },
            new Item {
                id = "201",
                name = "Holy shield",
                equipped = false,
                type = ItemType.OffHand,
                stats = new List<Stat> {
                    new Stat {
                        type = StatType.Crit,
                        value = 5
                    }
                }
            },
            new Item {
                id = "202",
                name = "Unholy shield",
                equipped = false,
                type = ItemType.OffHand,
                stats = new List<Stat> {
                    new Stat {
                        type = StatType.Crit,
                        value = 5
                    }
                }
            },
        };

        ShowUpper(equipmentView);
        SplitAllItemsListByEquipped();
        PopulateAllItems();

        closeButton.onClick.AddListener(() => {
            NotificationCenter.Notify(Notifications.Camera.tableTopActive);
            navigationView.Pop();
        });
        inventoryButton.onClick.AddListener(() => { currentTab = Tab.Inventory; });
        craftingButton.onClick.AddListener(() => { currentTab = Tab.Crafting; });
        defenceButton.onClick.AddListener(() => { currentTab = Tab.Defence; });
        tileUpgradesButton.onClick.AddListener(() => { currentTab = Tab.TileUpgrades; });

        NotificationCenter.Notify(Notifications.Camera.avatarSpotlightActive);
    }

    private void SplitAllItemsListByEquipped() {
        foreach (var item in allItems) {
            if (item.equipped) { equippedItems.Add(item.type, item); }
            else { unequippedItems.Add(item); }
        }
    }

    private void PopulateAllItems() {
        PopulateEquippedItems();
        PopulateUnequippedItems();
    }

    private void DestroyAllItems() {
        foreach (Transform child in itemListView.grid.transform) {
            Destroy(child.gameObject);
        }
        if (equipmentView.helmetSlot.transform.childCount != 0) { Destroy(equipmentView.helmetSlot.transform.GetChild(0).gameObject); }
        if (equipmentView.chestSlot.transform.childCount != 0) { Destroy(equipmentView.chestSlot.transform.GetChild(0).gameObject); }
        if (equipmentView.legsSlot.transform.childCount != 0) { Destroy(equipmentView.legsSlot.transform.GetChild(0).gameObject); }
        if (equipmentView.glovesSlot.transform.childCount != 0) { Destroy(equipmentView.glovesSlot.transform.GetChild(0).gameObject); }
        if (equipmentView.bootsSlot.transform.childCount != 0) { Destroy(equipmentView.bootsSlot.transform.GetChild(0).gameObject); }
        if (equipmentView.petSlot.transform.childCount != 0) { Destroy(equipmentView.petSlot.transform.GetChild(0).gameObject); }
        if (equipmentView.mainHandSlot.transform.childCount != 0) { Destroy(equipmentView.mainHandSlot.transform.GetChild(0).gameObject); }
        if (equipmentView.offHandSlot.transform.childCount != 0) { Destroy(equipmentView.offHandSlot.transform.GetChild(0).gameObject); }
    }

    // TODO: Is this the right place for this?
    private Sprite SetItemSprite(string id) {
        switch (id) {
            case "100":
                return Factory.GetSprite("sword-ice");
            case "101":
                return Factory.GetSprite("sword-fire");
            case "200":
                return Factory.GetSprite("shield-noble");  
            case "201":
                return Factory.GetSprite("shield-holy");
            case "202":
                return Factory.GetSprite("shield-unholy");            
            default:
                throw new Exception($"Icon not found for item id: {id}");
        }
    }

    private void EquipItem(Item item) {
        if (equippedItems.ContainsKey(item.type)) { unequippedItems.Add(equippedItems[item.type]); }
        equippedItems[item.type] = item;
        unequippedItems.Remove(item);
        Refresh();
    }

    private void Refresh() {
        DestroyAllItems();
        PopulateAllItems();
    }

    private void PopulateEquippedItems() {
        foreach (var item in equippedItems) {
            switch(item.Key) {
                case ItemType.Helm:
                    CreateItemPerSlot(equipmentView.helmetSlot.transform, item.Value);
                    break;
                case ItemType.Chest:
                    CreateItemPerSlot(equipmentView.chestSlot.transform, item.Value);
                    break;
                case ItemType.Legs:
                    CreateItemPerSlot(equipmentView.legsSlot.transform, item.Value);
                    break;
                case ItemType.Gloves:
                    CreateItemPerSlot(equipmentView.glovesSlot.transform, item.Value);
                    break;
                case ItemType.Boots:
                    CreateItemPerSlot(equipmentView.bootsSlot.transform, item.Value);
                    break;
                case ItemType.Pet:
                    CreateItemPerSlot(equipmentView.petSlot.transform, item.Value);
                    break;
                case ItemType.MainHand:
                    CreateItemPerSlot(equipmentView.mainHandSlot.transform, item.Value);
                    break;
                case ItemType.OffHand:
                    CreateItemPerSlot(equipmentView.offHandSlot.transform, item.Value);
                    break;
                case ItemType.Trap:
                    break;
                default:
                    throw new Exception("Item type not found when trying to equip an item");
            }
        }
    }

    private void CreateItemPerSlot(Transform itemSlotTransform, Item item) {
        var itemView = Factory.CreateView<ItemView>(view => {
            view.sprite = SetItemSprite(item.id);
            view.type = item.type;
        });
        itemView.transform.SetParent(itemSlotTransform, false);
    }

    private void PopulateUnequippedItems() {
        foreach (var item in unequippedItems) {
            var itemSlot = Factory.CreateView<ItemSlotView>(slotView => {
                slotView.type = ItemSlotType.Any;
            });
            itemSlot.transform.SetParent(itemListView.grid.transform, false);

            var itemView = Factory.CreateView<ItemView>(view => {
                view.sprite = SetItemSprite(item.id);
                view.type = item.type;
                view.OnClick(() => { EquipItem(item); });
            });
            itemView.transform.SetParent(itemSlot.transform, false);
        }

        if (unequippedItems.Count < 24) {
            for (int i = unequippedItems.Count; i < 24; i++) {
                var itemSlot = Factory.CreateView<ItemSlotView>(slotView => {
                    slotView.type = ItemSlotType.Any;
                });
                itemSlot.transform.SetParent(itemListView.grid.transform);
            }
        }
    }
}