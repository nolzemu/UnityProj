using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Vector3 _groundCheckOffset;
    public GameObject player;
    private Vector3 _input;
    private bool _isMoving;
    private bool _isGrounded;

    private Rigidbody2D _rigidbody;
    private MovingAnimations _animations;
    private SpriteRenderer _characterSprite;
    private Player _player;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animations = GetComponent<MovingAnimations>();
        _characterSprite = GetComponent<SpriteRenderer>();
        _player = FindObjectOfType<Player>(); // ������� ������ ������ Player � �����
    }

    private void Jump()
    {
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
        _animations.Jump(); // �������� ����� Jump �� MovingAnimations ��� ������� �������� ������
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + _groundCheckOffset, 0.2f);
        _isGrounded = colliders.Length > 1; // ���� ���� ������ ������ ���������� (������ ���������� ������), ������, ��������� �� �����
    }

    private void FixedUpdate()
    {
        CheckGround();
        if (_isGrounded)
        {
            _animations.IsFlying = false; // �������� �� �����, ������������� ���� ������ � false
        }
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
                player.GetComponent<MovingAnimations>().attackPoint.localPosition = new Vector3(0.5f, 0.8f, 0f);
            }
            else if (_input.x < 0)
            {
                _characterSprite.flipX = true; // �����
                player.GetComponent<MovingAnimations>().attackPoint.localPosition = new Vector3(-0.5f, 0.8f, 0f);
            }
        }
        _animations.IsMoving = _isMoving;
        _animations.IsFlying = !_isGrounded; // ������������� ���� ������ � true, ���� �������� �� �� �����
    }
}
