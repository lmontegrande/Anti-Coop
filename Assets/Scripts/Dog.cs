using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour {

    public float speed = 2f;

    private PhotonView _photonView;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

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

    void Update () {
		if (_photonView.isMine)
        {
            Vector2 velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, _rigidbody2D.velocity.y);
            _rigidbody2D.velocity = velocity;
            _animator.SetFloat("velocityX", velocity.x);
        }
	}
}
