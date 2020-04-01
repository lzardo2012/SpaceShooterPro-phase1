using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;
    [SerializeField]
    private int _powerupID;  // 0 = tripleShot; 1 = speedUP; 2 = Shield; 3 = ammo
    [SerializeField]
    private int _ammo;

    //private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _powerUpAudio;

    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

        PlayerWeapons playerWeapons = other.GetComponent<PlayerWeapons>();

        if (other.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(_powerUpAudio, Camera.main.transform.position);

            if (player != null)
            {
                switch (_powerupID)
                {
                    case 0:
                        playerWeapons.TripleShotActive();
                        break;
                    case 1:
                        playerMovement.SpeedUp();
                        break;
                    case 2:
                        player.Shield();
                        break;
                    case 3:
                        playerWeapons.AddAmmo( _ammo);
                        break;
                    case 4:
                        player.AddLife();
                        break;
                    case 5:
                        playerWeapons.SuperWeaponActive();
                        break;
                    default:
                        Debug.Log("Default");
                        break;
                }
            }
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(this.gameObject);
        }
    }
}
