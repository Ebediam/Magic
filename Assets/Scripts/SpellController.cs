using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    public EnemyManager enemy;
    public Rigidbody rb;
    public Spell spellData;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void StopParticles()
    {


    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<EnemyManager>())
        {
            enemy = other.transform.GetComponent<EnemyManager>();
            enemy.Damaged(spellData.damage);
            enemy.missAttack = spellData.enemyMisses;
            Destroy(this.gameObject, spellData.spellDuration);
        }

    }

}
