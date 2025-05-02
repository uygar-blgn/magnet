using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    Animator anim;
    private Vector2 lastMoveDirection;
    private bool facingLeft = true;

    public Transform Aim;
    bool isWalking = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if ((moveX == 0 && moveY == 0) && (movement.x != 0 || movement.y != 0))
        {
            isWalking = false;
            lastMoveDirection = movement;
            Vector3 vector3 = Vector3.right * lastMoveDirection.x + Vector3.up * lastMoveDirection.y;
            Aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        }
        else if (moveX != 0 || moveY != 0)
        {
            isWalking = true;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        Animate();
        if (movement.x < 0 && !facingLeft || movement.x > 0 && facingLeft)
        {
            Flip();
        }
    }

    void Animate()
    {
        anim.SetFloat("MoveX", movement.x);
        anim.SetFloat("MoveY", movement.y);
        anim.SetFloat("MoveMagnitude", movement.magnitude);
        anim.SetFloat("LastMoveX", lastMoveDirection.x);
        anim.SetFloat("LastMoveY", lastMoveDirection.y);
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingLeft = !facingLeft;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        if (isWalking)
        {
            Vector3 vector3 = Vector3.right * movement.x + Vector3.up * movement.y;
            Aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        }
    }
}
