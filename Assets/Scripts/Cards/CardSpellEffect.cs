using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpellEffect : MonoBehaviour
{
    [SerializeField] private Card _card;
    private CircleCollider2D _circCollider;
    public float damage;
    [SerializeField] private List<GameObject> listOfTargets = new List<GameObject>();

    private void Start()
    {
        damage = _card.Damage;
        _circCollider = GetComponent<CircleCollider2D>();
        _circCollider.radius = _card.Radius;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!listOfTargets.Contains(collision.gameObject))
            listOfTargets.Add(collision.gameObject);

    }

    private void Update()
    {
        foreach(GameObject target in listOfTargets)
        {
            if(target != null && target.TryGetComponent<Health>(out var health))
            {
                health.TakeDamage(damage);
            }
        }
        listOfTargets.Clear();
    }


}
