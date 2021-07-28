using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public float damage;
    [SerializeField] private float speed;
    [Space]

    [SerializeField] private GameObject bleedingEffect;
    [HideInInspector] public GameObject target;

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.transform.position + new Vector3(0f, 1f, 0f) - transform.position;
        float distance = speed * Time.deltaTime;

        transform.Translate(direction.normalized * distance, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject && other.CompareTag("Enemy"))
        {
            other.SendMessage("TakeDamage", damage);

            GameObject bleed = Instantiate(bleedingEffect, transform.position, transform.rotation);
            Destroy(bleed, .2f);

            Destroy(gameObject, .1f);
        }
    }
}