using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [HideInInspector] public CardStats defender;
    private GameObject activeTile;
    [Space]

    [SerializeField] private Text cardName;
    [SerializeField] private Text cardValue;
    [SerializeField] private Image cardImage;

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        cardName.text = defender.cardName;
        cardValue.text = defender.cardGold.ToString();
        cardImage.sprite = defender.cardImage;
    }

    public void OnDrag(PointerEventData eventData) => rectTransform.anchoredPosition += eventData.delta;

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Tile.chosenTile != null && !Tile.chosenTile.GetComponent<Tile>().isOccupied && GameStats.currentMoney >= defender.cardGold)
        {
            activeTile = Tile.chosenTile;

            GameObject newDefender = Instantiate(defender.defenderPrefab, activeTile.GetComponent<Tile>().defenderPosition.position, activeTile.transform.rotation);

            activeTile.GetComponent<Tile>().isOccupied = true;
            activeTile.GetComponent<Tile>().activeDefender = newDefender;

            newDefender.GetComponent<Defender>().activeTile = activeTile;
            newDefender.GetComponent<Defender>().defenderType = defender.defenderType;
            newDefender.GetComponent<Defender>().activeLine = activeTile.GetComponent<Tile>().lineNumber;

            GameStats.currentMoney -= defender.cardGold;
            Destroy(gameObject, 0.1f);
        }
        else
        {
            transform.localPosition = Vector3.zero;
        }
    }

    public void ResetCardButton()
    {
        GameStats.currentMoney -= 20;
        Destroy(gameObject);
    }
}