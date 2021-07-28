using UnityEngine;

public class Ore : MonoBehaviour
{
    [SerializeField] private int oreValue;

    [SerializeField] private int minOreValue;
    [SerializeField] private int maxOreValue;

    private void Start() => oreValue = Random.Range(minOreValue, maxOreValue);

    public void TakeDamage(int damage)
    {
        oreValue -= damage;
        GameStats.currentMoney += damage * 3;

        if (oreValue <= 0)
        {
            Destroy(gameObject);

            OreManager.oreCount--;
        }
    }
}