using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public delegate void AttackDelegate(float seconds);

    public static AttackDelegate attackEvent;

    public Animator animator;

    public Transform spawnPoint;

    public List<Spell> spells;
    SpellController spell;
    Quaternion originalRotation;
    Vector3 originalPosition;

    public RectTransform healthUI;
    

    public float maxHealth;
    float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.rotation;
        originalPosition = transform.position;
        currentHealth = maxHealth;

        
    }


    
    // Update is called once per frame
    void Update()
    {

    }





    public void Attack(int spellNumber)
    {

        attackEvent?.Invoke(spells[spellNumber].turnDuration);
        animator.Play("Attack");
        spell = Instantiate(spells[spellNumber].prefab, spawnPoint.position, spawnPoint.rotation).GetComponent<SpellController>();
        spell.rb.AddForce(spells[spellNumber].speed * spell.transform.forward, ForceMode.VelocityChange);
        spell.spellData = spells[spellNumber];
        transform.rotation = originalRotation;
        transform.position = originalPosition;
    }



    public void Damaged(float damage)
    {
        animator.Play("Damaged");

        currentHealth -= damage;
        healthUI.localScale = new Vector3((currentHealth/maxHealth), healthUI.localScale.y, healthUI.localScale.z);

        if (currentHealth <= 0f)
        {
            Die();
            return;
        }


    }

    public void Die()
    {

    }
}
