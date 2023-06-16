using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private int _wallet;
    [SerializeField] private Text _shopWalletText;
    [SerializeField] private Text _inventoryWalletText;
    [SerializeField] private Text _hpText;
    [SerializeField] private Text _strengthText;
    [SerializeField] private Text _defenseText;
    [SerializeField] private GameObject _hpSlider;
    private PlayerController _playerController;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        _wallet = FindAnyObjectByType<Inventory>().Wallet;
        _shopPanel.SetActive(false);
        _inventoryPanel.SetActive(false);

        ChangeWallet();

        _playerController = FindAnyObjectByType<PlayerController>();
        _hpText.text = $" HP {_playerController.CullentHP} / {_playerController.MaxHP}";
        _strengthText.text = $"STR : {_playerController.Strength}";
        _defenseText.text = $"DEF : {_playerController.Defense}";
        _hpSlider.GetComponent<Slider>().maxValue = _playerController.MaxHP;
    }
    void Update()
    {
        _wallet = FindAnyObjectByType<Inventory>().Wallet;
        HPSlider();
    }
    private void HPSlider()
    {
        _hpSlider.GetComponent<Slider>().value = _playerController.CullentHP;
    }
    public void ShopPanelActivate()
    {
        if (_shopPanel.activeSelf == false) { _shopPanel.SetActive(true); _hpSlider.SetActive(false); }
        else { _shopPanel.SetActive(false); _hpSlider.SetActive(true); }
    }
    public void InventoryPanelActivate()
    {
        if (_inventoryPanel.activeSelf == false) { _inventoryPanel.SetActive(true); _hpSlider.SetActive(false); }
        else { _inventoryPanel.SetActive(false); _hpSlider.SetActive(true); }
    }
    public void ChangeWallet()
    {
        _shopWalletText.text = ($"Money {_wallet}G");
        _inventoryWalletText.text = ($"Money {_wallet}G");
    }
    public void ChangeStatus(ItemData itemData)
    {
        if (itemData.Effect == ItemData.ActionEffect.Recovery) _hpText.text = $" HP {_playerController.CullentHP} / {_playerController.MaxHP}";
        else if (itemData.Effect == ItemData.ActionEffect.PowerUp) _strengthText.text = $"STR : {_playerController.Strength}";
        else if (itemData.Effect == ItemData.ActionEffect.DefenseUp) _defenseText.text = $"DEF : {_playerController.Defense}";
    }
}
