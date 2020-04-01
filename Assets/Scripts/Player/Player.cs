using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _shieldPrefab;
    [SerializeField]
    private GameObject _damageFXRight;
    [SerializeField]
    private GameObject _damageFXLeft;
    [SerializeField]
    private GameObject _explosionPrefab;

    private bool _isShieldActive;
    [SerializeField]
    private int _shieldStrength = 0;

    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;
    
    [SerializeField]
    private int _score;

    private UIManager _uiManager;

    private CameraShake _cameraShake;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is NULL");
        }

        _cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        if (_cameraShake == null)
        {
            Debug.LogError("CameraShake is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage()
    {
        if (_isShieldActive == true)
        {
            if (_shieldStrength > 1)
            {
                _shieldStrength--;
                _uiManager.UpdateShield(_shieldStrength);
            }
            else
            {
                _isShieldActive = false;
                _shieldPrefab.GetComponent<SpriteRenderer>().enabled = false;
                return;
            }
        }
        else if (_isShieldActive == false)
        {
            _lives -= 1;
            _uiManager.UpdateLives(_lives);
        }

        if (_lives <= 0)
        {
            _spawnManager.OnPlayerDeath();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.5f);
        }
        PlayerStatus();
        _cameraShake.ShakeCamera();
    }

    private void PlayerStatus()
    {
        if (_lives == 3)
        {
            _damageFXRight.SetActive(false);
            _damageFXLeft.SetActive(false);
        }

        if (_lives == 2)
        {
            _damageFXRight.SetActive(true);
            _damageFXLeft.SetActive(false);
        }

        if (_lives == 1)
        {
            _damageFXRight.SetActive(true);
            _damageFXLeft.SetActive(true);
        }
    }

    public void Shield()
    {
        StartCoroutine(ShieldRoutine());
    }

    IEnumerator ShieldRoutine()
    {
        _isShieldActive = true;
        _shieldStrength = 3;
        _uiManager.UpdateShield(_shieldStrength);
        _shieldPrefab.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(10.0f);
        _isShieldActive = false;
        _shieldStrength = 0;
        _uiManager.UpdateShield(_shieldStrength);
        _shieldPrefab.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void ScoreCount(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    public void AddLife()
    { 
        if (_lives <3)
        {
            _lives++;
            _uiManager.UpdateLives(_lives);
        }
        PlayerStatus();
    }
}
