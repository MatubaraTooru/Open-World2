using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private int _wallet = 0;

    public int Wallet { get => _wallet; set => _wallet = value; }

    [SerializeField]
    private GameObject _inventoryPanel;

    [SerializeField]
    private GameObject _buttonPrefab;

    private Dictionary<ItemData, int> _itemStacks = new Dictionary<ItemData, int>();

    private void Start()
    {
        
    }

    public void GetItem(ItemData item)
    {
        if (_itemStacks.ContainsKey(item))
        {
            _itemStacks[item]++;
            UpdateButtonStackCount(item, _itemStacks[item]);
        }
        else
        {
            _itemStacks.Add(item, 1);
            CreateInventoryButton(item);
        }
    }

    private void CreateInventoryButton(ItemData item)
    {
        var obj = Instantiate(_buttonPrefab);
        obj.transform.SetParent(_inventoryPanel.transform, false);
        obj.name = $"Button({item.Name})";

        var button = obj.GetComponent<Button>();
        button.onClick.AddListener(() => UseItem(item));

        var text = obj.GetComponentInChildren<Text>();
        UpdateButtonStackCount(item, _itemStacks[item]);
        text.text = item.Name;

        if (item.Sprite != null)
        {
            obj.GetComponent<Image>().sprite = item.Sprite;
        }
    }

    private void UpdateButtonStackCount(ItemData item, int stackCount)
    {
        var button = GetButtonByName($"Button({item.Name})");
        var text = button.GetComponentInChildren<Text>();
        text.text = $"{item.Name} ({stackCount})";
    }

    private Button GetButtonByName(string buttonName)
    {
        var buttons = _inventoryPanel.GetComponentsInChildren<Button>();
        foreach (var button in buttons)
        {
            if (button.name == buttonName)
            {
                return button;
            }
        }
        return null;
    }

    private void UseItem(ItemData item)
    {
        item.ApplyEffect(FindAnyObjectByType<PlayerController>().gameObject);

        _itemStacks[item]--;
        if (_itemStacks[item] <= 0)
        {
            _itemStacks.Remove(item);
            DestroyInventoryButton(item);
        }
        else
        {
            UpdateButtonStackCount(item, _itemStacks[item]);
        }
    }

    private void DestroyInventoryButton(ItemData item)
    {
        var button = GetButtonByName($"Button({item.Name})");
        if (button != null)
        {
            Destroy(button.gameObject);
        }
    }
}
