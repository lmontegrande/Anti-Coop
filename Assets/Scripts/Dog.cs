using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dog : MonoBehaviour {

    public Slider slider;
    public float speed = 2f;
    public float useCoolDown = 2f;

    private PhotonView _photonView;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Interactable selectedInteractable;

    private float useCoolDownTimer;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_photonView.isMine) {
            Camera.main.GetComponent<CameraFollow>().target = gameObject;
        } else {
            _rigidbody2D.isKinematic = true;
            slider.gameObject.SetActive(false);
        }
    }

    private void Update ()
    {

        if (!_photonView.isMine)
            return;

        HandleAxisInput();
        HandleInput();
        HandleCooldowns();
	}

    private void HandleAxisInput()
    {
        Vector2 velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = velocity;
        _animator.SetFloat("velocityX", velocity.x);
    }

    private void HandleInput()
    {
        if (Input.GetButtonDown("Use"))
        {
            if (selectedInteractable != null && useCoolDownTimer >= useCoolDown)
            {
                useCoolDownTimer = 0;
                selectedInteractable.Interact();
            }
        }
    }

    private void HandleCooldowns()
    {
        if (useCoolDownTimer < useCoolDown)
        {
            useCoolDownTimer += Time.deltaTime;
            slider.value = Mathf.Clamp(useCoolDownTimer / useCoolDown, 0, 1);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Interactable>() != null)
        {
            selectedInteractable = collision.GetComponent<Interactable>();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Interactable>() != null && collision.GetComponent<Interactable>() == selectedInteractable)
        {
            selectedInteractable = null;
        }
    }
}
