using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _missilePrefab;
    [SerializeField]
    private GameObject _superWeaponPowerup;

    [SerializeField]
    private AudioClip _laserAudio;
    private AudioSource _audioSource;

    private bool _tripleShotEnabled;

    private Vector3 _firingPosition;
    [SerializeField]
    private float _firingOffset = 1.0f;
    [SerializeField]
    private float _fireRate;
    private float _canFire = 0f;

    [SerializeField]
    private int _ammo = 30;
    [SerializeField]
    private int _maxAmmo;

    [SerializeField]
    private bool _superWeapon;

    private UIManager _uiManager;

    [SerializeField]
    private Transform[] _firePoints;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("AudioSource is NULL");
        }
        else
        {
            _audioSource.clip = _laserAudio;
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is NULL");
        }

        _uiManager.UpdateAmmo(_ammo);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire && _ammo > 0)
        {
            _ammo--;
            FireLaser();
            _uiManager.UpdateAmmo(_ammo);
        }
    }

    private void FireLaser()
    {
        _firingPosition = new Vector3(transform.position.x, transform.position.y + _firingOffset, transform.position.z);

        _canFire = Time.time + _fireRate;
        if (_superWeapon == true)
        {
            var length = _firePoints.Length;
            for (int i = 0; i < length; i++)
            {
                Instantiate(_missilePrefab, _firePoints[i].position, Quaternion.identity);
            }
        }
        else if (_tripleShotEnabled == true)
        {
            Instantiate(_tripleShotPrefab, _firingPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, _firingPosition, Quaternion.identity);
        }

        _audioSource.Play();
    }

    public void TripleShotActive()
    {
        _tripleShotEnabled = true;

        var superWeaponChance = Random.Range(0, 10);
        if (superWeaponChance >= 0)
        {
            Instantiate(_superWeaponPowerup, new Vector3(Random.Range(-9, 9), 9, 0), Quaternion.identity);
        }
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SuperWeaponActive()
    {
        _superWeapon = true;

        StartCoroutine(SuperWeaponPowerDownRoutine());
    }

    public void AddAmmo(int ammo)
    {
        if (_ammo <= _maxAmmo)
        {
            _ammo += _ammo;
        }
        else
        {
            _ammo = _maxAmmo;
        }
        _uiManager.UpdateAmmo(_ammo);
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _tripleShotEnabled = false;
    }

    IEnumerator SuperWeaponPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _superWeapon = false;
    }
}
