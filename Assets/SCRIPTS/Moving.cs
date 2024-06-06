using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Vector3 _groundCheckOffset;
    private Vector3 _input;
    private bool _isMoving;
    private bool _isGrounded;

    private Rigidbody2D _rigidboy;
    private MovingAnimations _animations;
    private SpriteRenderer _characterSprite;
    private Player _player;

    private void Start()
    {
        _rigidboy = GetComponent<Rigidbody2D>();
        _animations = GetComponent<MovingAnimations>();
        _characterSprite = GetComponent<SpriteRenderer>();
        _player = FindObjectOfType<Player>(); // ������� ������ ������ Player � �����
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
        // ���������, ������� �� �����
        if (_player && !_player.IsDead())
        {
            Move();
            if (_isGrounded && Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
    }

    private void Move()
{
    _input = new Vector2(Input.GetAxis("Horizontal"), 0);
    transform.position += _input * _speed * Time.deltaTime;
    _isMoving = _input.x != 0;

    if (_isMoving)
    {
        // ��������� ����������� ����� ��� ����������� ������������ flipX
        if (_input.x > 0)
        {
            _characterSprite.flipX = false; // ������
        }
        else if (_input.x < 0)
        {
            _characterSprite.flipX = true; // �����
        }
    }
    _animations.IsMoving = _isMoving;
}
}
