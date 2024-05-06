using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Moving : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Vector3 _groundCheckOffset;
    private Vector3 _input;
    private bool _isMoving;
    private bool _isGrounded;
    private bool _isFlying;
    LayerMask groundMask;

    private Rigidbody2D _rigidboy;
    private MovingAnimations _animations;
    [SerializeField] private SpriteRenderer _characterSprite;
    private void Start()
    {
        _rigidboy = GetComponent<Rigidbody2D>();
        _animations = GetComponent<MovingAnimations>();
    }

    private void Jump()
    {
        _rigidboy.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        _isGrounded = collider.Length > 1;
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        Move();
        if (_isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        /*CheckGround();
        if(Input.GetKeyUp(KeyCode.Space))
        {
            if(_isGrounded )
            {
                Jump();
                _animations.Jump();
            }
        }
        _animations.IsMoving = _isMoving;
        _animations.IsFlying = isFLying(); */
    }
    /*private bool isFLying()
    {
        if (_rigidboy.velocity.y < -0.1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    } */

    /*private void CheckGround()
    {
        float rayLength = 0.3f;
        Vector3 rayStartPosition = transform.position + _groundCheckOffset;
        RaycastHit2D hit = Physics2D.Raycast(rayStartPosition, rayStartPosition + Vector3.down, rayLength, groundMask);

        if(hit.collider != null)
        {
            _isGrounded = hit.collider.CompareTag("Ground") ? true : false;
        }
        else
        {
            _isGrounded = false;
        }
    } */



    private void Move()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), 0);
        transform.position += _input * _speed * Time.deltaTime;
        _isMoving = _input.x != 0 ? true : false;


        if (_isMoving)
        {
            _characterSprite.flipX = _input.x > 0 ? false : true;
        }
        _animations.IsMoving = _isMoving;
    }

    /* private void Jump()
     {
         Debug.Log("Jump");
         _rigidboy.AddForce(transform.up * _jumpForce, ForceMode2D.Force);
     }
 } */
}