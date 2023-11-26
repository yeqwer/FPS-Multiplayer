using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    [SerializeField] private int _hpMax;
    [SerializeField] private int _hpEnemy;
    [SerializeField] private Slider _hpSlider;
    private Collider[] _enemyColliders;

    public void Start() 
    {
        Spawn();
    }

    public void Spawn()
    {
        _enemyColliders = gameObject.GetComponentsInChildren<Collider>();

        _hpEnemy = _hpMax;
        _hpSlider.maxValue = _hpMax;
        _hpSlider.gameObject.SetActive(false);
    }

    public void Damage(int damage, Collider collider)
    {
        if (_hpEnemy > 0)
        {
            if (collider.gameObject.name == "Head")
            {
                _hpEnemy -= damage * 4;
            } 
            else
            {
                _hpEnemy -= damage;
            }
            CheckHP();
        }  
    }

    private void CheckHP()
    {
        if (_hpEnemy <= 0)
        {
            Death();
        }
        _hpSlider.gameObject.SetActive(true);
        _hpSlider.value = _hpEnemy;
    }

    public void Death()
    {
        foreach (Collider coll in _enemyColliders)
        {
            coll.transform.parent = null;

            MeshRenderer mr = coll.GetComponent<MeshRenderer>();
            mr.material.color = Color.red;
        
            Rigidbody rb = coll.AddComponent<Rigidbody>();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Collider playerCollider = player.GetComponentInChildren<CapsuleCollider>();

            rb.AddForce(playerCollider.transform.forward.normalized * 10f, ForceMode.Impulse);
        }
        
        Destroy(_hpSlider.gameObject);
        Destroy(gameObject);

        EnemyActions.EnemyKilled();
    }
}