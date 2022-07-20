using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    // Public
    public Animator animator;
    public bool inCombat = false;

    public SwordStats SwordStats;

    void Start()
    {
        SwordStats = (SwordStats) SwordStats.GetComponent("SwordStats");
    }

    void Update()
    {
        // Animator triggers
        if(Input.GetKeyDown("space")) {
            animator.SetBool("Combat", !animator.GetBool("Combat"));
            inCombat = animator.GetBool("Combat");
        }

        if(Input.GetMouseButtonDown(0) && animator.GetBool("Combat")) {
            animator.SetTrigger("Attacking");
        }
    }

    public (float, float) SwordDamage()
    {
        float swordDamage = SwordStats.damage;
        swordDamage = Random.Range(swordDamage - (Mathf.Floor(swordDamage / 10)), swordDamage + (Mathf.Ceil(swordDamage / 10)));

        if(SwordStats.effectType == "overdrive") { return (swordDamage, 0); }
        if(SwordStats.effectType == "mastering") { return (0, swordDamage); }

        switch(SwordStats.waveType) {
            case "square":
                return (Mathf.Ceil(swordDamage / 2), Mathf.Floor(swordDamage / 2));
            case "triangle":
                return (Mathf.Ceil(swordDamage / 3), Mathf.Floor(2 * (swordDamage / 3)));
            case "saw":
                return (Mathf.Ceil(2 * (swordDamage / 3)), Mathf.Floor(swordDamage / 3));
            default:
                return (swordDamage, 0);
        }
    }

    public string SwordFilter() { return SwordStats.filterType; }
    public string SwordEffect() { return SwordStats.effectType; }

    public void Bingus() { print("Bingus"); } // Test function: CombatManager.Bingus().
}
