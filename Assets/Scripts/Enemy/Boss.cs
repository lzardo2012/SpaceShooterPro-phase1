using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private int _health = 5;
    [SerializeField]
    private int _points;

    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _explosionAudio;

    [SerializeField]
    private GameObject _explosionPrefab;

    private Collider2D _collider2D;

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player = NULL");
        }

        _collider2D = GetComponent<Collider2D>();
        if (_collider2D == null)
        {
            Debug.LogError("Collider is NULL");
        }

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("AudioSource is NULL");
        }
        else
        {
            _audioSource.clip = _explosionAudio;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -6)
        {
            float randomX = Random.Range(-9, 9);
            transform.position = new Vector3(randomX, 8, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
        }

        if (other.tag == "Laser")
        {
            _health -= 1;

            if (_health <= 0)
            {
                if (_player != null)
                {
                    _player.ScoreCount(_points);
                }
                _collider2D.enabled = false;
                foreach (Transform child in transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                _audioSource.Play();
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                this.GetComponent<SpriteRenderer>().enabled = false;
                Destroy(this.gameObject, 2.3f);
            }
            Destroy(other.gameObject);
        }
    }
}
