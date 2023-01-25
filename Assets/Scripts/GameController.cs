using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Reflection.Emit;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameController : MonoBehaviour
{
    public GameObject[] rooms;
    public Item[] items;
    public Sprite[] sprites;

    public string startingRoom;
    public ItemDisplay itemDisplay;

    public static GameController Instance;

    public float ViewDragThreshold = 20f;

    RoomViewController _currentRoom;

    Item _currentItem = null;
    Item CurrentItem
    {
        get { return _currentItem; }
        set 
        {
            int i = items.ToList().IndexOf(value);
            if (i != -1)
            {
                itemDisplay.SetImage(sprites[i], value.displayName);
            }
            _currentItem = value; 
        }
    }

    private void Awake()
    {
        Instance = this;        
        GotoRoom(startingRoom);
        NewItem();
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
        if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended && !RoomViewController.Dragging)
        {
            Camera cam = _currentRoom.GetComponentInChildren<Camera>();
            Ray ray = cam.ScreenPointToRay(Input.touches[0].position);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                Debug.Log(hit.transform.name);

                if (hit.transform.TryGetComponent<RoomExit>(out var roomExit))
                {
                    GotoRoom(roomExit.targetRoom);
                }

                if (hit.transform.TryGetComponent<Item>(out var item))
                {
                    if (item == CurrentItem)
                    {
                        NewItem();
                    }
                }
            }
        }
    }

    public void NewItem()
    {
        var itemlist = new List<Item>(items);
        itemlist.Remove(CurrentItem);
        CurrentItem = itemlist[Random.Range(0, itemlist.Count- 1)];
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(GameController))]
public class GameControllerEditor : Editor
{
    SerializedProperty items;
    SerializedProperty sprites;

    private void OnEnable()
    {
        items = serializedObject.FindProperty("items");
        sprites = serializedObject.FindProperty("sprites");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawDefaultInspector();

        GUILayout.Space(20f);
        EditorGUILayout.LabelField("Taulukkonäkymä");

        sprites.arraySize = items.arraySize;

        GUILayout.BeginHorizontal();
        GUILayout.Label("Malli");
        GUILayout.Label("Kuva");
        GUILayout.Label("Nimi");
        GUILayout.EndHorizontal();

        int selection = -1;
        for (int i = 0; i < items.arraySize; i++)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(items.GetArrayElementAtIndex(i), GUIContent.none);
            EditorGUILayout.PropertyField(sprites.GetArrayElementAtIndex(i), GUIContent.none);

            var item = items.GetArrayElementAtIndex(i);
            if (item != null)
            {
                GUILayout.Label((target as GameController).items[i].displayName, GUILayout.ExpandWidth(true), GUILayout.MinWidth(100f));
                if (GUILayout.Button("->"))
                {
                    selection = i;
                }
            }
            else
            {
                GUILayout.Label("-", GUILayout.MinWidth(100f));
                if (GUILayout.Button("->"))
                {
                    selection = -1;
                }
            }

            GUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();

        if (selection != -1)
        {
            Selection.activeTransform = (target as GameController).items[selection].transform;
        }
    }
}
#endif