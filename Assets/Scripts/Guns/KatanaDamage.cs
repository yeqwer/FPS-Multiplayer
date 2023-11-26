using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaDamage : MonoBehaviour
{
    [SerializeField] private int _damage;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.GetComponentInParent<CharacterControl>().SetDamage(_damage);
        }
    }
}
