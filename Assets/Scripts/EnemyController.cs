using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer sprite;

    public float runSpeed = 10;

    public CircleCollider2D foot;

    public LayerMask ground;

    public float jumpForce = 20;

    private bool isAttack;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Anda pra frente e para trás
        AndaFrente();
        AndaTras();
	}

    private void AndaTras()
    {
        throw new NotImplementedException();
    }

    private void AndaFrente()
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
