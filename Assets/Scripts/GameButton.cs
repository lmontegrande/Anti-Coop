using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton : Photon.PunBehaviour, Interactable {

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

    public IEnumerator PressButtonRoutine()
    {
        if (OnButtonPressed != null)
            OnButtonPressed.Invoke();
        photonView.RPC("RPCPress", PhotonTargets.All, true);

        yield return new WaitForSeconds(releaseTime);

        if (OnButtonReleased != null)
            OnButtonReleased.Invoke();
        photonView.RPC("RPCPress", PhotonTargets.All, false);
    }

    public void Interact()
    {
        if (isButtonPressed)
            return;

        StartCoroutine(PressButtonRoutine());
    }

    [PunRPC]
    private void RPCPress(bool isPressed)
    {
        if (isPressed)
        {
            _animator.SetBool("isPressed", true);
            isButtonPressed = true;
        }
        else
        {
            _animator.SetBool("isPressed", false);
            isButtonPressed = false;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //throw new NotImplementedException();
    }
}

    
