using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

    [SerializeField]
    private float _horizontalBound;
    [SerializeField]
    private float _topBound;
    [SerializeField]
    private float _bottomBound;

    private float _speedMultiplayer = 1;
    private float _afterburner = 1;

    private Animator _anim;

    private UIManager _uiManager;

    [SerializeField]
    private bool _thrustersCoolingDown, _thrustersEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GameObject.Find("Player").GetComponent<Animator>();

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * _speedMultiplayer * _afterburner * Time.deltaTime);

        if (transform.position.x >= _horizontalBound)
        {
            transform.position = new Vector3(-_horizontalBound, transform.position.y, 0);
        }
        else if (transform.position.x <= -_horizontalBound)
        {
            transform.position = new Vector3(_horizontalBound, transform.position.y, 0);
        }

        if (horizontalInput < 0)
        {
            _anim.SetBool("TurnLeft", true);
            _anim.SetBool("TurnRight", false);
        }
        else if (horizontalInput > 0)
        {
            _anim.SetBool("TurnRight", true);
            _anim.SetBool("TurnLeft", false);
        }
        else if (horizontalInput == 0)
        {
            _anim.SetBool("TurnLeft", false);
            _anim.SetBool("TurnRight", false);
        }

        if (Input.GetKey(KeyCode.LeftShift) && _thrustersEnabled == true)
        {
            _thrustersCoolingDown = true;
            _afterburner = 3;
            StartCoroutine(ThrustersCooldownRoutine());
        }
        else
        {
            _afterburner = 1;
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _bottomBound, _topBound), 0);
    }

    IEnumerator SpeedUpRoutine()
    {
        _speedMultiplayer = 2;
        yield return new WaitForSeconds(5.0f);
        _speedMultiplayer = 1;
    }

    public void SpeedUp()
    {
        StartCoroutine(SpeedUpRoutine());
    }

    IEnumerator ThrustersCooldownRoutine()
    {
        for (float remainingTime = 5.0f; remainingTime > 0; remainingTime -= Time.deltaTime)
        {
            yield return null;
            if (_thrustersEnabled)
            {
                _uiManager.UpdateThrustersCooldown(remainingTime);
            }
        }
        _thrustersEnabled = false;
        StartCoroutine(ThrustersEnablerRoutine());
    }

    IEnumerator ThrustersEnablerRoutine()
    {
        yield return new WaitForSeconds(5);
        _thrustersCoolingDown = false;
        _thrustersEnabled = true;
        _uiManager.UpdateThrustersCooldown(5);
    }
}
