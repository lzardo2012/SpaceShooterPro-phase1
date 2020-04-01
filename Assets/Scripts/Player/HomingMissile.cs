using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    private Transform _target;

    private Rigidbody2D _rigid;

    [SerializeField]
    private float _speed, _rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Enemy").transform;
        if (_target == null)
        {
            _target.position = Vector3.up;
        }

        _rigid = GetComponent<Rigidbody2D>();
        if (_rigid == null)
        {
            Debug.LogError("RigidBody2D is NULL");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = (Vector2)_target.position - _rigid.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        _rigid.angularVelocity = -rotateAmount * _rotateSpeed;

        _rigid.velocity = transform.up * _speed;
    }
}
