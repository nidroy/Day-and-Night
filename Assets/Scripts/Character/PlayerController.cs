using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int healthPoints = 100;

    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 moveVector;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float direction = Input.GetAxisRaw("Horizontal");

        moveVector = new Vector2(direction, 0).normalized;

        anim.SetFloat("Direction", direction);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVector * moveSpeed * Time.fixedDeltaTime);
    }
}
