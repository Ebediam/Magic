using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public delegate void AttackDelegate(float seconds);

    public static AttackDelegate attackEvent;

    public Animator animator;

    public Transform spawnPoint;

    public List<Spell> spells;
    public List<Spell> mergedSpells;

    public Spell lastSpell;
    public Spell lastMergedSpell;
    public Spell currentSpell;
    SpellController spell;
    Quaternion originalRotation;
    Vector3 originalPosition;

    public RectTransform healthUI;

    public TextMeshProUGUI spellText;

    public float maxHealth;
    float currentHealth;

    public AudioSource hitSFX;

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


    public void Reset()
    {
        
    }


    public void Attack(int spellNumber)
    {
        currentSpell = spells[spellNumber];
        spellText.text = "Last spell: " + currentSpell.name;
       
        if (lastSpell)
        {
            foreach(Spell mergeSpell in mergedSpells)
            {
                if(currentSpell == mergeSpell.spellComponent1)
                {
                    if (lastSpell == mergeSpell.spellComponent2)
                    {
                        if(mergeSpell != lastMergedSpell)
                        {
                            currentSpell = mergeSpell;
                            lastMergedSpell = currentSpell;
                        }

                        break;
                    }
                }else if(currentSpell == mergeSpell.spellComponent2)
                {
                    if(lastSpell == mergeSpell.spellComponent1)
                    {
                        if (mergeSpell != lastMergedSpell)
                        {
                            currentSpell = mergeSpell;
                            lastMergedSpell = currentSpell;
                        }
                        break;
                    }
                }
            }


        }            



        attackEvent?.Invoke(currentSpell.turnDuration);
        animator.Play("Attack");
        spell = Instantiate(currentSpell.prefab, spawnPoint.position, spawnPoint.rotation).GetComponent<SpellController>();
        spell.rb.AddForce(currentSpell.speed * spell.transform.forward, ForceMode.VelocityChange);
        spell.spellData = currentSpell;
        transform.rotation = originalRotation;
        transform.position = originalPosition;

        lastSpell = spells[spellNumber];
        if (currentSpell.mergedSpell)
        {
            lastMergedSpell = currentSpell;
        }
        else
        {
            lastMergedSpell = null;
        }
    }



    public void Damaged(float damage, float delay)
    {

        Invoke("PlayDamagedAnimation", delay);


        currentHealth -= damage;
        healthUI.localScale = new Vector3((currentHealth/maxHealth), healthUI.localScale.y, healthUI.localScale.z);

        if (currentHealth <= 0f)
        {
            Die();
            return;
        }


    }

    public void PlayDamagedAnimation()
    {
        animator.Play("Damaged");
        hitSFX.Play();
    }

    public void Die()
    {
        gameManager.Restart();
    }
}
