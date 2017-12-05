using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton : Photon.PunBehaviour, IPunObservable, Interactable {

    public delegate void OnButtonPressedHandler();
    public OnButtonPressedHandler OnButtonPressed;
    public delegate void OnButtonReleasedHandler();
    public OnButtonReleasedHandler OnButtonReleased;

    public float releaseTime = 1f;

    private Animator _animator;
    private bool isButtonPressed = false;

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }

    [PunRPC]
    public void PressButton()
    {
        StartCoroutine(PressButtonRoutine());
    }

    public IEnumerator PressButtonRoutine()
    {
        _animator.SetBool("isPressed", true);
        isButtonPressed = true;
        if (OnButtonPressed != null)
            OnButtonPressed.Invoke();

        yield return new WaitForSeconds(releaseTime);

        _animator.SetBool("isPressed", false);
        isButtonPressed = false;
        if (OnButtonReleased != null)
            OnButtonReleased.Invoke();
    }

    public void Interact()
    {
        if (isButtonPressed)
            return;

        photonView.RPC("PressButton", PhotonTargets.All, null);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //throw new NotImplementedException();
    }
}

    
