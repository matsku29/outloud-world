using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] rooms;

    public string startingRoom;

    public static GameController Instance;

    public float ViewDragThreshold = 20f;

    RoomViewController _currentRoom;

    private void Awake()
    {
        Instance = this;        
        GotoRoom(startingRoom);
    }

    public void GotoRoom(string ID)
    {
        if (rooms.FirstOrDefault((x) => { return x.name == ID; }) == null)
            return;
        for (int i = 0; i < rooms.Length; i++)
        {
            rooms[i].SetActive(false);
            if (rooms[i].name.Equals(ID, System.StringComparison.InvariantCultureIgnoreCase))
            {
                rooms[i].SetActive(true);
            }
        }
        _currentRoom = FindObjectOfType<RoomViewController>(false);
    }

    private void Update()
    {
        if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Began)
        {
            Camera cam = _currentRoom.GetComponentInChildren<Camera>();
            Ray ray = cam.ScreenPointToRay(Input.touches[0].position);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                Debug.Log(hit.transform.name);
                Debug.Log("hit");
                if (hit.transform.TryGetComponent<RoomExit>(out var roomExit))
                {
                    GotoRoom(roomExit.targetRoom);
                }
            }
        }
    }
}
