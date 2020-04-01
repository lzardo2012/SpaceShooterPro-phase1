using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private float _rotateSpeed;
    [SerializeField]
    private int _health = 5;
    [SerializeField]
    private int _points;

    private Collider2D _collider2D;
    private Player _player;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("SpawnManager is NULL");
        }

        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player = NULL");
        }

        _collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            _health -= 1;

            if (_health <= 0)
            {
                if (_player != null)
                {
                    _player.ScoreCount(_points);
                }
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                _collider2D.enabled = false;
                _spawnManager.StartSpawning();
                Destroy(this.gameObject, 0.5f);
            }
        } 
        
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _collider2D.enabled = false;
            _spawnManager.StartSpawning();
            Destroy(this.gameObject);
        }
    }
}
