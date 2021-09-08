using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpells : MonoBehaviour
{
    public CardStats card;
    public RuntimeAnimatorController collapsingAnimation;

    [SerializeField]
    private List<GameObject> affectObjects;
    private SphereCollider _collider;

    private void Start()
    {
        gameObject.name = card.cardName;
        _collider = GetComponent<SphereCollider>();
        _collider.radius = card.spellRadius;

        StartCoroutine(CastSpell());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Defender"))
            affectObjects.Add(other.gameObject);
    }

    private IEnumerator CastSpell()
    {
        var effect = Instantiate(card.spellPrefab, transform.position + new Vector3(0, .5f, 0), transform.rotation);
        effect.transform.parent = gameObject.transform;

        var _animator = effect.AddComponent<Animator>();
        _animator.runtimeAnimatorController = collapsingAnimation;

        if (card.cardName == "Poison")
            FindObjectOfType<AudioManager>().PlaySound("Poison");
        else if (card.cardName == "Healing")
            FindObjectOfType<AudioManager>().PlaySound("Heal");
        else if (card.cardName == "Conflagration")
            FindObjectOfType<AudioManager>().PlaySound("Conflagration");
        else if (card.cardName == "Kiss of Death")
            FindObjectOfType<AudioManager>().PlaySound("DeathKiss");

        yield return new WaitForSeconds(card.spellTime);

        _animator.Play("Collaps_anim");

        if (affectObjects != null)
        {
            foreach (GameObject item in affectObjects)
            {
                if (item)
                    item.SendMessage("TakeDamage", card.spellStrength);
            }
        }

        Destroy(this.gameObject, 1f);
    }
}