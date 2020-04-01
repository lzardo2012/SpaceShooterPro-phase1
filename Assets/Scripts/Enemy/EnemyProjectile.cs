using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private float _duration;

    private Player _player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        Destroy(this.gameObject,_duration);
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        Player player = other.transform.GetComponent<Player>();
        if (other.tag == "Player")
        {
            if (player != null)
            {
                player.Damage();
                Destroy(this.gameObject);
            }
        }
    }
}
