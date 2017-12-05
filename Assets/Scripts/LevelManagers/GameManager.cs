using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Photon.PunBehaviour, IPunObservable {

    public GameButton[] gameButtons;
    public GameObject[] doors;

    private int buttonsPressed;

    private void Start()
    {
        buttonsPressed = 0;

        foreach(GameButton button in gameButtons)
        {
            button.OnButtonPressed += ButtonPressed;
            button.OnButtonReleased += ButtonReleased;
        }
    }

    private void ButtonPressed()
    {
        photonView.RPC("RPCPress", PhotonTargets.All, null);
    }

    private void ButtonReleased()
    {
        photonView.RPC("RPCRelease", PhotonTargets.All, null);
    }

    [PunRPC]
    private void RPCPress()
    {
        buttonsPressed++;
        if (buttonsPressed >= gameButtons.Length)
        {
            Debug.Log("WIN");
            foreach(GameObject door in doors)
            {
                door.SetActive(false);
            }
        }
    }

    [PunRPC]
    private void RPCRelease()
    {
        buttonsPressed--;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //throw new NotImplementedException();
    }
}
