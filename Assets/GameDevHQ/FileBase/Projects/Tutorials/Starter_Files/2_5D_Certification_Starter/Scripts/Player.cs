using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float _speed = 14.0f;
    private float _gravity = 26.0f;
    private Vector3 _direction;
    private float _jumpHeight = 12.0f;

    private CharacterController _controller;
    private Animator _anim;
    private bool _jumping = false;
    private bool _onLedge = false;
    private Ledge _activeLedge;

    void Start()
    {
        _controller = this.GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        CalculateMovement();

        if (_onLedge)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _anim.SetTrigger("ClimbUp");
            }
        }
    }

    private void CalculateMovement()
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

        if (_controller.enabled)
        {
            _controller.Move(_direction * Time.deltaTime);
        }
    }

    public void GrabLedge(Vector3 handPos, Ledge currentLedge)
    {
        _controller.enabled = false;
        _anim.SetBool("GrabLedge", true);
        _anim.SetFloat("Speed", 0.0f);
        _jumping = false;
        _anim.SetBool("Jump", _jumping);
        _onLedge = true;

        transform.position = handPos;
        _activeLedge = currentLedge;
    }

    public void ClimbUpComplete()
    {
        Debug.Log("ClimbUpComplete");
        transform.position = _activeLedge.GetStandPos();
        _anim.SetBool("GrabLedge", false);
        //_onLedge = false;
        _controller.enabled = true;
    }
}
