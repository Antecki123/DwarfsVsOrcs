using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [HideInInspector] public CardStats card;
    private GameObject activeTile;
    [Space]

    [SerializeField] private Text cardName;
    [SerializeField] private Text cardValue;
    [SerializeField] private Image cardUnitType;
    [SerializeField] private Image cardImage;

    [SerializeField] private Sprite[] typeImages;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        cardName.text = card.cardName;
        cardValue.text = card.cardGold.ToString();
        cardImage.sprite = card.cardImage;

        if (card.defenderType == DefenderType.Melee)
            cardUnitType.sprite = typeImages[0];
        else if (card.defenderType == DefenderType.Rifleman || card.defenderType == DefenderType.Crossbowman)
            cardUnitType.sprite = typeImages[1];
        else if (card.defenderType == DefenderType.Miner)
            cardUnitType.sprite = typeImages[2];
        else if (card.defenderType == DefenderType.Obstacle)
            cardUnitType.sprite = typeImages[3];
        else if (card.defenderType == DefenderType.Spell)
            cardUnitType.sprite = typeImages[4];
    }

    public void OnBeginDrag(PointerEventData eventData) => rectTransform.localScale = new Vector3(.5f, .5f, .5f);

    public void OnDrag(PointerEventData eventData) => rectTransform.anchoredPosition += eventData.delta;

    public void OnEndDrag(PointerEventData eventData)
    {
        activeTile = Tile.chosenTile;

        if (Tile.chosenTile != null && GameStats.currentMoney >= card.cardGold && card.defenderType == DefenderType.Spell)
        {
            var newSpell = Instantiate(card.defenderPrefab, activeTile.transform.position, activeTile.transform.rotation);
            newSpell.GetComponent<CardSpells>().card = card;
            
            GameStats.currentMoney -= card.cardGold;
            Destroy(gameObject, 0.1f);
        }
        else if (Tile.chosenTile != null && !Tile.chosenTile.GetComponent<Tile>().isOccupied && GameStats.currentMoney >= card.cardGold)
        {
            var newDefender = Instantiate(card.defenderPrefab, activeTile.GetComponent<Tile>().defenderPosition.position,
                activeTile.transform.rotation);

            activeTile.GetComponent<Tile>().isOccupied = true;
            activeTile.GetComponent<Tile>().activeDefender = newDefender;

            newDefender.GetComponent<Defender>().activeTile = activeTile;
            newDefender.GetComponent<Defender>().defenderType = card.defenderType;
            newDefender.GetComponent<Defender>().activeLine = activeTile.GetComponent<Tile>().lineNumber;

            GameStats.currentMoney -= card.cardGold;
            Destroy(gameObject, 0.1f);
        }
        else
        {
            transform.localPosition = Vector3.zero;
            rectTransform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void ResetCardButton()
    {
        if (GameStats.currentMoney >= 20)
        {
            GameStats.currentMoney -= 20;
            Destroy(gameObject);
        }
    }
}