using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f, _rotateSpeed = 2.0f;

    private Transform _target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _target = GameObject.FindGameObjectWithTag("Enemy").transform;

        Debug.Log(_target.position);
     /*
        Vector3 targetDirection = _target.position - transform.position;

        float step = _rotateSpeed * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);

        newDirection.z -= 45;

        Debug.DrawRay(transform.position, targetDirection, Color.red);

        transform.rotation = Quaternion.LookRotation(newDirection);
     */
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        //transform.LookAt(_target);

    }
}
