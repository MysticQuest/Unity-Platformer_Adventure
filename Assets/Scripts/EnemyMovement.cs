using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed = 1f;

    float debugTimer = 0f; //enemy bugged with other colliders on exit and flipped infinitely
    Rigidbody2D rbody;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = new Vector2(moveSpeed, 0f);
    }

    private void Update()
    {
        debugTimer += Time.deltaTime;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (debugTimer > 1)
        {
            transform.localScale = new Vector2(-(Mathf.Sign(rbody.velocity.x)), 1f);
            rbody.velocity = new Vector2(moveSpeed * transform.localScale.x, 0f);
            debugTimer = 0;
        }

    }
}
