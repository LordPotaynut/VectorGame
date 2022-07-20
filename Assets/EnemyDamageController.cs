using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageController : MonoBehaviour
{
    CombatManager CombatManager;

    void Start()
    {
        // Finds the Combat Manager object by tag and gets the CombatManager script component. Assigns it locally.
        CombatManager = (CombatManager) GameObject.FindGameObjectWithTag("CombatManager").GetComponent("CombatManager");
    }

    void Update()
    {
        
    }

    // Detects when contacted by hitbox
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Checks what hitbox collided with
        if (collider.CompareTag("SwordHitbox")) {
            Debug.Log("SWORD COLLISION");
            (float swordRawDamage, float swordFilterDamage) = CombatManager.SwordDamage();
            print(swordRawDamage + ", " + swordFilterDamage);
        } else if (collider.CompareTag("GunBulletHitbox")) {
            Debug.Log("BULLET COLLISION");
        } else if (collider.CompareTag("Player")) {
            Debug.Log("PLAYER COLLISION");
        }
    }
}
