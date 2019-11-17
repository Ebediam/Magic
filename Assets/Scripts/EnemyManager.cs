using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public RectTransform healthUI;

    public Player player;

    public delegate void EnemyTurnEndDelegate(float seconds);
    public static EnemyTurnEndDelegate EnemyTurnEndEvent;

    public delegate void EnemyDeadDelegate();
    public static EnemyDeadDelegate EnemyDeadEvent;

    public Enemy enemy;

    public float enemyTurnDuration;

    public float enemyAttackTime;

    public float maxHealth;
    float currentHealth;

    public bool missAttack;
    public bool isDead;

    public AudioSource hitSFX;  

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        GameManager.enemyTurn += EnemyTurn;
        GameManager.RestartEvent += OnReset;
    }

    public void OnReset()
    {
        GameManager.enemyTurn -= EnemyTurn;
        GameManager.RestartEvent -= OnReset;

    }

    public void EnemyTurn()
    {
        if (isDead)
        {
            return;
        }
        //whatever
        enemy.Attack();
        if (!missAttack)
        {
            player.Damaged(enemy.damage, enemyAttackTime);

        }
        else
        {
            missAttack = false;
        }

        EnemyTurnEndEvent?.Invoke(enemyTurnDuration);

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damaged(float damage, float timeDelay)
    {
        currentHealth -= damage;
        healthUI.localScale = new Vector3(Mathf.Max((currentHealth / maxHealth), 0f), healthUI.localScale.y, healthUI.localScale.z);

        if(damage != 0f)
        {
            if (currentHealth > 0f)
            {
                Invoke("DamagedAnimation", timeDelay);
            }
            else
            {
                isDead = true;
                EnemyDeadEvent?.Invoke();
                Invoke("KilledAnimation", timeDelay);

            }
        }

    }

    public void DamagedAnimation()
    {
        enemy.Damaged();
        hitSFX.Play();
    }

    public void KilledAnimation()
    {
        enemy.Killed();
        hitSFX.Play();
    }
}
