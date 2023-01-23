using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] rooms;

    public string startingRoom;

    public static GameController Instance;

    public float ViewDragThreshold = 20f;

    private void Awake()
    {
        Instance = this;
        GotoRoom(startingRoom);
    }

    public void GotoRoom(string ID)
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            rooms[i].SetActive(false);
            if (rooms[i].name == ID)
            {
                rooms[i].SetActive(true);
            }
        }
    }
}
