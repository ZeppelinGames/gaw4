using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class Player : MonoBehaviour
{

    [SerializeField] float _moveSpeed = 3;
    [SerializeField] float _rotateSpeed = 3;
    [SerializeField] float _transitionSpeed = 1f;

    CharacterController _controller;
    Animator _animator;
    float _walkProp;

    float _holdWeight;

    // Start is called before the first frame update
    void Start() {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * v;
        _controller.SimpleMove(move * _moveSpeed);

        _walkProp = Mathf.MoveTowards(_walkProp, (move.magnitude > 0 ? 1 : 0), _transitionSpeed * Time.deltaTime);
        _animator.SetFloat("Walk", _walkProp);

        _holdWeight = Mathf.MoveTowards(_holdWeight, (Input.GetKey(KeyCode.E) ? 1 : 0), _transitionSpeed * Time.deltaTime);
        _animator.SetLayerWeight(1, _holdWeight);

        transform.rotation *= Quaternion.Euler(0, h * _rotateSpeed * Time.deltaTime, 0);
    }
}
