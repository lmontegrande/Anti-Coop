using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton : MonoBehaviour, Interactable {

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

    public void PressButton()
    {
        if (isButtonPressed)
            return;

        _animator.SetBool("isPressed", true);
        isButtonPressed = true;
        if (OnButtonPressed != null)
            OnButtonPressed.Invoke();

        StartCoroutine(ReleaseButton());
    }

    public IEnumerator ReleaseButton()
    {
        yield return new WaitForSeconds(releaseTime);

        _animator.SetBool("isPressed", false);
        isButtonPressed = false;
        if (OnButtonReleased != null)
            OnButtonReleased.Invoke();
    }

    public void Interact()
    {
        PressButton();
    }
}

    
