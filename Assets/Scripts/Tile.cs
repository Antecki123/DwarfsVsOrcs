using UnityEngine;

public class Tile : MonoBehaviour
{
    public static GameObject chosenTile;

    public Transform defenderPosition;
    public Transform orePosition;
    [Space]

    public bool isOccupied;
    public bool isOreAvailable;
    public int lineNumber;

    [HideInInspector] public GameObject activeDefender;
    [HideInInspector] public GameObject activeOre;

    private Renderer rend;

    private void Start() => rend = GetComponent<Renderer>();

    private void OnMouseOver()
    {
        if (!isOccupied)
            rend.material.color = Color.green;
        else
            rend.material.color = Color.red;

        chosenTile = this.gameObject;
    }

    private void OnMouseExit()
    {
        rend.material.color = Color.white;
        chosenTile = null;
    }
}