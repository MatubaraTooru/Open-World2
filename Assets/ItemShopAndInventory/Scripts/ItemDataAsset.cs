using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/CreateItemDataAsset")]
public class ItemDataAsset : ScriptableObject
{
    [SerializeField]
    private List<ItemData> _itemDataList = new List<ItemData>();

    public List<ItemData> ItemDataList { get => _itemDataList; }
}

[System.Serializable]
public class ItemData
{
    [SerializeField, Header("�A�C�e���̖��O")]
    private string _itemName = "None";

    [SerializeField, Header("�A�C�e���̉摜"), Tooltip("�����Ă��ǂ�")]
    private Sprite _sprite;

    [SerializeField, Header("�A�C�e���̌���")]
    private ActionEffect _effect = ActionEffect.Recovery;
    [SerializeField, Header("�A�C�e���̌��ʗ�")]
    private float _effectValue = 20;

    [SerializeField, Header("�A�C�e���̒l�i")]
    private int _price = 0;

    public string Name { get => _itemName; }
    public Sprite Sprite { get => _sprite; }
    public ActionEffect Effect { get => _effect; }
    public int Price { get => _price; }

    public enum ActionEffect
    {
        Recovery, // ��
        PowerUp, // �U���͏㏸
        DefenseUp, // �h��͏㏸
    }

    public void ApplyEffect(GameObject target)
    {
        var s = target.GetComponent<PlayerController>();
        switch (Effect)
        {
            case ActionEffect.Recovery:
                // �񕜌��ʂ̏���
                s.CullentHP += _effectValue;
                if (s.CullentHP >= s.MaxHP) s.CullentHP = s.MaxHP;
                Debug.Log("�񕜌��ʂ��������܂���");
                break;
            case ActionEffect.PowerUp:
                // �U���͏㏸���ʂ̏���
                s.Strength += _effectValue;
                Debug.Log("�U���͏㏸���ʂ��������܂���");
                break;
            case ActionEffect.DefenseUp:
                // �h��͏㏸���ʂ̏���
                s.Defense += _effectValue;
                Debug.Log("�h��͏㏸���ʂ��������܂���");
                break;
            default:
                Debug.LogWarning("���m�̃A�C�e�����ʂł�");
                break;
        }
    }
}
