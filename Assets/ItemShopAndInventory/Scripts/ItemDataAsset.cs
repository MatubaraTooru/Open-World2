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
    [SerializeField, Header("アイテムの名前")]
    private string _itemName = "None";

    [SerializeField, Header("アイテムの画像"), Tooltip("無くても良い")]
    private Sprite _sprite;

    [SerializeField, Header("アイテムの効果")]
    private ActionEffect _effect = ActionEffect.Recovery;
    [SerializeField, Header("アイテムの効果量")]
    private float _effectValue = 20;

    [SerializeField, Header("アイテムの値段")]
    private int _price = 0;

    public string Name { get => _itemName; }
    public Sprite Sprite { get => _sprite; }
    public ActionEffect Effect { get => _effect; }
    public int Price { get => _price; }

    public enum ActionEffect
    {
        Recovery, // 回復
        PowerUp, // 攻撃力上昇
        DefenseUp, // 防御力上昇
    }

    public void ApplyEffect(GameObject target)
    {
        var s = target.GetComponent<PlayerController>();
        switch (Effect)
        {
            case ActionEffect.Recovery:
                // 回復効果の処理
                s.CullentHP += _effectValue;
                if (s.CullentHP >= s.MaxHP) s.CullentHP = s.MaxHP;
                Debug.Log("回復効果が発動しました");
                break;
            case ActionEffect.PowerUp:
                // 攻撃力上昇効果の処理
                s.Strength += _effectValue;
                Debug.Log("攻撃力上昇効果が発動しました");
                break;
            case ActionEffect.DefenseUp:
                // 防御力上昇効果の処理
                s.Defense += _effectValue;
                Debug.Log("防御力上昇効果が発動しました");
                break;
            default:
                Debug.LogWarning("未知のアイテム効果です");
                break;
        }
    }
}
