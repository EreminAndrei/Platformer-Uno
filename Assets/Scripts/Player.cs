using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private float _groungOffsetY;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private Animator _animator;

    private Rigidbody2D _rb;
    private float _horizontalMove = 0f;
    private bool _facingRight = true;

    private void Start()
    {
       _rb = GetComponent <Rigidbody2D> (); 
    }
   
    private void Update()
    {
        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(transform.up*_jumpForce, ForceMode2D.Impulse);
        }
        
        _horizontalMove = Input.GetAxisRaw("Horizontal")*_speed;

        _animator.SetFloat("_horizontalMove", Mathf.Abs(_horizontalMove));

        if (_horizontalMove < 0f && _facingRight)
        {
            Flip();
        }
        else if (_horizontalMove > 0f && !_facingRight)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(_horizontalMove, _rb.velocity.y);
        _rb.velocity = targetVelocity;  

        CheckGround();
    }

    private void CheckGround()
    {
        Collider2D[] Colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y + _groungOffsetY), _groundCheckRadius);

        if (Colliders.Length > 1)
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }       
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
}
