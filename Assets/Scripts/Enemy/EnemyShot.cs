using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    [SerializeField]
    private Transform[] _shotPoints;

    [SerializeField]
    private float _fireRate;

    [SerializeField]
    private GameObject _projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FireProjectilesRoutine());
    }

    IEnumerator FireProjectilesRoutine()
    {
        var length = _shotPoints.Length;  

        while (true)
        {
            for (int i = 0; i < length; i++)
            {
                Instantiate(_projectilePrefab, _shotPoints[i].position, _shotPoints[i].rotation);
            }
            yield return new WaitForSeconds(_fireRate);
        }
    }
}
