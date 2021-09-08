using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public List<CardStats> cardsContainer = new List<CardStats>();
    [Space]

    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private List<Transform> cardSlots = new List<Transform>();

    private void Update()
    {
        foreach (Transform cardSlot in cardSlots)
        {
            if (cardSlot.childCount == 0)
                AddCard(cardSlot);
        }
    }

    private void AddCard(Transform _cardSlot)
    {
        int random = Random.Range(0, cardsContainer.Count);

        GameObject newCard = Instantiate(cardPrefab, _cardSlot.position, transform.rotation);
        newCard.transform.SetParent(_cardSlot, false);

        newCard.transform.localPosition = Vector3.zero;
        newCard.transform.localRotation = Quaternion.identity;
        newCard.transform.localScale = Vector3.one;

        newCard.GetComponent<Card>().card = cardsContainer[random];
    }
}