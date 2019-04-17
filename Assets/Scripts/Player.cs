using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Config
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 3f;
    [SerializeField] Vector2 deathKick = new Vector2(15f, 15f);

    //State
    bool isAlive = true;
    bool xMovement;
    bool yMovement;

    //Cache
    Rigidbody2D rBody;
    Animator anim;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D moveCollider;
    float normalGravity;

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        moveCollider = GetComponent<BoxCollider2D>();
        normalGravity = rBody.gravityScale;
    }


    void Update()
    {
        if (!isAlive) { return; }

        xMovement = Mathf.Abs(rBody.velocity.x) > Mathf.Epsilon;
        yMovement = Mathf.Abs(rBody.velocity.y) > Mathf.Epsilon;

        Run();
        Jump();
        Flip();
        Climb();
        Die();
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // -1 to +1
        Vector2 pVelocity = new Vector2(controlThrow * speed, rBody.velocity.y);
        rBody.velocity = pVelocity;

        anim.SetBool("IsRunning", xMovement);
    }

    private void Climb()
    {
        if (!moveCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            anim.SetBool("IsClimbing", false);
            rBody.gravityScale = normalGravity;
            return;
        }

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(rBody.velocity.x, controlThrow * climbSpeed);
        rBody.velocity = climbVelocity;

        rBody.gravityScale = 0;

        anim.SetBool("IsClimbing", yMovement);
    }

    private void Jump()
    {
        if (!moveCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
            rBody.velocity += jumpVelocity;
        }

    }

    private void Flip()
    {

        if (xMovement)
        {
            transform.localScale = new Vector2(Mathf.Sign(rBody.velocity.x), 1);
        }
    }

    private void Die()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            anim.SetTrigger("Die");
            rBody.velocity = deathKick;
            isAlive = false;

            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Game Over 2");
    }
}
