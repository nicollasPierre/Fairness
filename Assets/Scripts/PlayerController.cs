using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer sprite;

    public float runSpeed = 10;

    public CircleCollider2D foot;

    public LayerMask ground;
    public LayerMask door;

    public float jumpForce = 20;

    private bool isAttack;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GetForwardInput();
        GetJumpInput();
        //GetAttackInput();
        GetEnterDoorInput();
    }

    private void GetEnterDoorInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)){
            if (foot.IsTouchingLayers(door))
            {
                if (GameVariables.Key)
                {
                    GameVariables.Key = false;
                    //fim da fase, vai para a proxima
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
    }

    private void GetAttackInput()
    {
        if (Input.GetKey(KeyCode.F))
        {
            animator.SetBool("IsAttack", true);
            isAttack = true;
            Invoke("StopAttack", 0.8f);
        }
    }

    private void StopAttack()
    {
        animator.SetBool("IsAttack", false);
        isAttack = false;
    }

    private void GetJumpInput()
    {
        if (Input.GetButtonDown("Jump") && (foot.IsTouchingLayers(ground) || foot.IsTouchingLayers(door)))
        {
            body.AddForce(Vector2.up * jumpForce);
        }
        animator.SetBool("IsOnGround", (foot.IsTouchingLayers(ground) || foot.IsTouchingLayers(door)));
    }

    private void GetForwardInput()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * runSpeed, body.velocity.y);
        animator.SetFloat("xSpeed", Mathf.Abs(body.velocity.x));
        if (!sprite.flipX && body.velocity.x < 0)
        {
            sprite.flipX = true;
        }
        else if (sprite.flipX && body.velocity.x > 0)
        {
            sprite.flipX = false;
        }
    }
}
