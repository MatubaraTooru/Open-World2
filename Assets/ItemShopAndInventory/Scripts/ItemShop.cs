using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    [SerializeField] ItemDataAsset _itemData;
    [SerializeField] GameObject _buttonPrefab;
    private Inventory _playerInventory;
    private UIManager _uIManager;

    private void Awake()
    {
        _playerInventory = FindObjectOfType<Inventory>();
        _uIManager = FindAnyObjectByType<UIManager>();
    }

    void Start()
    {
        SetItem();
    }

    private void SetItem()
    {
        foreach (var item in _itemData.ItemDataList)
        {
            var obj = Instantiate(_buttonPrefab, transform);
            obj.name = $"Button({item.Name})";

            var itemName = item.Name;

            var button = obj.GetComponent<Button>();
            button.onClick.AddListener(() => BuyItem(itemName));

            var text = obj.GetComponentInChildren<Text>();
            text.text = item.Name;

            var image = obj.GetComponent<Image>();
            if (item.Sprite != null)
            {
                image.sprite = item.Sprite;
            }
        }
    }

    public void BuyItem(string itemName)
    {
        var itemParam = _itemData.ItemDataList.Find(item => item.Name == itemName);
        if (itemParam != null && _playerInventory.Wallet >= itemParam.Price)
        {
            _playerInventory.Wallet -= itemParam.Price;
            _uIManager.ChangeWallet();
            _playerInventory.GetItem(itemParam);
        }
    }
}
