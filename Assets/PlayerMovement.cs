using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Public
    public float moveSpeed = 5f;
    public float sprintSpeed = 1.5f;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public GameObject SwordHitbox;

    public Rigidbody2D rigidbody2d;

    // Private
    float sprintModifier = 1f;
    CombatManager CombatManager;

    Vector2 movement;

    void Start()
    {
        // Finds the Combat Manager object by tag and gets the CombatManager script component. Assigns it locally.
        CombatManager = (CombatManager) GameObject.FindGameObjectWithTag("CombatManager").GetComponent("CombatManager");
    }

    void Update()
    {
        // Gets input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Sprite Flip Facing
        if (movement.x < 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerSwordAttack")) {
            spriteRenderer.flipX = true;
            SwordHitbox.transform.localRotation = Quaternion.Euler(0, 180, 0);
        } else if (movement.x > 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerSwordAttack")) {
            spriteRenderer.flipX = false;
            SwordHitbox.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        // Normalizes the vector so the hypotenuse of the movement's distance is equal to 1
        movement = movement.normalized;

        // Checks if the Sprint key is down and out of combat, and sets the sprintModifier value. Also updates the Animator.
        if (Input.GetButton("Sprint") && !CombatManager.inCombat) {
            sprintModifier = sprintSpeed;
            animator.SetBool("Sprinting", true);
        } else {
            sprintModifier = 1f;
            animator.SetBool("Sprinting", false);
        }

        // Animator Moving Update
        if(movement.x != 0 || movement.y != 0) {
            animator.SetBool("Moving", true);
        } else {
            animator.SetBool("Moving", false);
        }
    }

    void FixedUpdate()
    {
        // Apply Movement
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerSwordAttack")) {
            SwordHitbox.transform.position = rigidbody2d.position;
        } else {
            rigidbody2d.MovePosition(rigidbody2d.position + movement * (moveSpeed * sprintModifier) * Time.fixedDeltaTime);
        }
    }
}
