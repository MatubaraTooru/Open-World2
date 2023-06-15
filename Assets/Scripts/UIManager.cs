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
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void ShopPanelActivate()
    {
        if (_shopPanel.activeSelf == false) { _shopPanel.SetActive(true); }
        else { _shopPanel.SetActive(false); }
    }
    public void InventoryPanelActivate()
    {
        if (_inventoryPanel.activeSelf == false) _inventoryPanel.SetActive(true);
        else _inventoryPanel.SetActive(false);
    }
    public void ChangeWallet()
    {
        _shopWalletText.text = ($"Money {_wallet}G");
        _inventoryWalletText.text = ($"Money {_wallet}G");
    }
}
