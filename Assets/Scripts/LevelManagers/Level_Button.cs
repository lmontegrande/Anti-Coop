using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Button : Photon.PunBehaviour, IPunObservable {

    public GameButton[] gameButtons;
    public string levelToLoad;

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
        if (buttonsPressed >= gameButtons.Length-1)
        {
            Debug.Log("WIN");
            PhotonNetwork.LoadLevel(levelToLoad);
            PhotonNetwork.Disconnect();
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
