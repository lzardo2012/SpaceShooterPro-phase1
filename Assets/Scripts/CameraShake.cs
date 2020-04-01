using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originPosition;
    private Quaternion originRotation;
    private float _shake_decay;
    private float _shake_intensity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_shake_intensity > 0)
        {
            transform.position = originPosition + Random.insideUnitSphere * _shake_intensity;
            transform.rotation = new Quaternion(
            originRotation.x + Random.Range(-_shake_intensity, _shake_intensity) * .2f,
            originRotation.y + Random.Range(-_shake_intensity, _shake_intensity) * .2f,
            originRotation.z + Random.Range(-_shake_intensity, _shake_intensity) * .2f,
            originRotation.w + Random.Range(-_shake_intensity, _shake_intensity) * .2f);
            _shake_intensity -= _shake_decay;
        }
    }

    public void ShakeCamera()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
        _shake_intensity = .1f;
        _shake_decay = 0.002f;

        Debug.Log("Yay!");
    }
}
