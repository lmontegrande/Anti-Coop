using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour {
	
    private PhotonView _photonView;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        if (_photonView.isMine) {
            Camera.main.GetComponent<CameraFollow>().target = gameObject;
        } else {
            _rigidbody2D.isKinematic = true;
        }
    }

    void Update () {
		if (_photonView.isMine)
        {
            _rigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
	}
}
