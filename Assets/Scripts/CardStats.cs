using UnityEngine;

public enum DefenderType { Melee, Crossbowman, Rifleman, Obstacle, Miner, Spell }

[CreateAssetMenu(fileName = "New Deck", menuName = "Scriptable Objects/New Deck")]
public class CardStats : ScriptableObject
{
    public DefenderType defenderType;
    [Space]

    public string cardName;
    public Sprite cardImage;
    public GameObject defenderPrefab;
    [Space]

    public int cardGold;

    [Header("Statistics")]
    public int maxHealth;
    public float damage;

    public float attackDelay;

    [Header("Spells only")]
    public GameObject spellPrefab;
    public float spellRadius;
    public float spellStrength;
    public float spellTime;
}