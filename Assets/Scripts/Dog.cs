using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour {

    public float speed = 2f;

    private PhotonView _photonView;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Interactable selectedInteractable;

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
        }
    }

    private void Update ()
    {

        if (!_photonView.isMine)
            return;

        HandleAxisInput();
        HandleInput();
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
            if (selectedInteractable != null)
            {
                selectedInteractable.Interact();
            }
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
