using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Photon.PunBehaviour, IPunObservable {

    public GameButton[] gameButtons1;
    public GameObject[] doors1;

    public GameButton[] gameButtons2;
    public GameObject[] doors2;

    private int buttonsPressed1;
    private int buttonsPressed2;

    private void Start()
    {
        buttonsPressed1 = 0;
        buttonsPressed2 = 0;

        foreach(GameButton button in gameButtons1)
        {
            button.OnButtonPressed += ButtonPressed1;
            button.OnButtonReleased += ButtonReleased1;
        }

        foreach(GameButton button in gameButtons2)
        {
            button.OnButtonPressed += ButtonPressed2;
            button.OnButtonReleased += ButtonReleased2;
        }
    }

    private void ButtonPressed1()
    {
        photonView.RPC("RPCPress1", PhotonTargets.All, null);
    }

    private void ButtonReleased1()
    {
        photonView.RPC("RPCRelease1", PhotonTargets.All, null);
    }

    [PunRPC]
    private void RPCPress1()
    {
        buttonsPressed1++;
        if (buttonsPressed1 >= gameButtons1.Length)
        {
            Debug.Log("WIN");
            foreach(GameObject door in doors1)
            {
                door.SetActive(false);
            }
        }
    }

    [PunRPC]
    private void RPCRelease1()
    {
        buttonsPressed1--;
    }

    private void ButtonPressed2()
    {
        photonView.RPC("RCPPress2", PhotonTargets.All, null);
    }

    private void ButtonReleased2()
    {
        photonView.RPC("RPCRelease2", PhotonTargets.All, null);
    }

    [PunRPC]
    private void RPCPress2()
    {
        buttonsPressed2++;
        if (buttonsPressed2 >= gameButtons2.Length)
        {
            Debug.Log("WIN");
            foreach (GameObject door in doors2)
            {
                door.SetActive(false);
            }
        }
    }

    [PunRPC]
    private void RPCRelease2()
    {
        buttonsPressed2--;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //throw new NotImplementedException();
    }
}
