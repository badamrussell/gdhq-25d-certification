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

    // Start is called before the first frame update
    void Start()
    {
        _controller = this.GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        if (_controller.isGrounded)
        {
            float h = Input.GetAxis("Horizontal");
            _direction = new Vector3(0, 0, h) * _speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _direction.y += _jumpHeight;
            }
        }

        _direction.y -= _gravity * Time.deltaTime;

        _controller.Move(_direction * Time.deltaTime);
    }
}
