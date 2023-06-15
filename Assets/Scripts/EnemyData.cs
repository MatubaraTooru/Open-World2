using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string id;

    public string charName;

    public float hp;
    public float strength;

    public string ID { get => id; }
    public string Name { get => charName; }
    public float HP { get => hp; }
    public float Strength { get => strength; }
}