using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damaged()
    {
        animator.Play("Damaged");
    }

    public void Attack()
    {
        animator.Play("Attack");
    }

    public void Killed()
    {
        animator.Play("Killed");
    }
}
