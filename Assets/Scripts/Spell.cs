using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]
public class Spell : ScriptableObject
{
    public new string name;
    public GameObject prefab;
    public float damage;
    public float speed;
    public float turnDuration;
    public float spellDuration;
    public bool enemyMisses;
    public bool mergedSpell;
    public Spell spellComponent1;
    public Spell spellComponent2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
