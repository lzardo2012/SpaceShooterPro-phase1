using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartLevelText;
    [SerializeField]
    private Text _ammoCounterText;
    [SerializeField]
    private Image _LivesDisplay;
    [SerializeField]
    private Image _shieldDisplay;
    [SerializeField]
    private Image _thrustersCooldown;
    [SerializeField]
    private Sprite[] _lives;
    [SerializeField]
    private Sprite[] _shield;

    private bool _ThrustersCooDownIsOver;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "0000";
        _ammoCounterText.text = "Ammo: ";
        _gameOverText.gameObject.SetActive(false);
        _restartLevelText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("GameManager = NULL");
        }
    }

    public void UpdateScore (int playerScore)
    {
        _scoreText.text = playerScore.ToString();
    }

    public void UpdateAmmo (int currentAmmo)
    {
        _ammoCounterText.text = "Ammo: " + currentAmmo.ToString();
        if (currentAmmo <= 0)
        {
            _ammoCounterText.text = "Out of Ammo";
        }
    }

    public void UpdateLives(int currentlives)
    {
        _LivesDisplay.sprite = _lives[currentlives];

        if (currentlives == 0)
        {
            GameOverSequence();
        }
    }

    public void UpdateShield(int currentShield)
    {
        _shieldDisplay.sprite = _shield[currentShield];
    }

    public void UpdateThrustersCooldown(float remainingTime)
    {
        _thrustersCooldown.fillAmount = remainingTime / 5;
        Debug.Log(remainingTime);
    }

    void GameOverSequence()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartLevelText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
        _gameManager.GameOver();
        
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
