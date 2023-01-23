using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomExit : MonoBehaviour
{
    public string targetRoom;

    public void OnClick()
    {
        GameController.Instance.GotoRoom(targetRoom);
    }
}
