using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderStorm : MonoBehaviour
{
    public ParticleSystem cloud;
    public ParticleSystem lightning;
    public Collider lightningCollider;
    public SpellController spellController;




    public void Lightning()
    {
        if (spellController)
        {
            Invoke("InvokeLightning", spellController.spellData.spellDuration);
        }


    }

    public void InvokeLightning()
    {
        if (lightning)
        {
            lightning.Play();
            lightningCollider.enabled = true;
        }

    }

    public void StopLightning()
    {
        if (lightning)
        {
            lightning.Stop();
            lightningCollider.enabled = false;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.enemyTurn += StopLightning;
        GameManager.playerTurn += Lightning;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
