using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float _speed = 9.0f;
    private float _gravity = 30.0f;
    private Vector3 _direction;
    private float _jumpHeight = 12.0f;

    private CharacterController _controller;
    private Animator _anim;
    private bool _jumping = false;

    void Start()
    {
        _controller = this.GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (_controller.isGrounded)
        {
            if (_jumping)
            {
                _jumping = false;
                _anim.SetBool("Jump", _jumping);
            }

            float h = Input.GetAxisRaw("Horizontal");
            _direction = new Vector3(0, 0, h) * _speed;
            _anim.SetFloat("Speed", Mathf.Abs(h));

            if (h != 0)
            {
                Vector3 facing = transform.localEulerAngles;
                facing.y = _direction.z > 0 ? 0 : 180;
                transform.localEulerAngles = facing;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _direction.y += _jumpHeight;
                _jumping = true;
                _anim.SetBool("Jump", _jumping);
            }
        }

        _direction.y -= _gravity * Time.deltaTime;

        _controller.Move(_direction * Time.deltaTime);
    }
}
