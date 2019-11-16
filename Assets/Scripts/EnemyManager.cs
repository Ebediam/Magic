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

    public Enemy enemy;

    public float enemyTurnDuration;

    public float maxHealth;
    float currentHealth;

    public bool missAttack;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        GameManager.enemyTurn += EnemyTurn;
    }

    public void EnemyTurn()
    {
        //whatever
        enemy.Attack();
        if (!missAttack)
        {
            player.Damaged(enemy.damage);
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

    public void Damaged(float damage)
    {
        currentHealth -= damage;
        healthUI.localScale = new Vector3((currentHealth / maxHealth), healthUI.localScale.y, healthUI.localScale.z);
    }
}
