using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Levels.Rooms;
using Utils;

public class BorderPlacement
{
    GameObject borderPrefab;
    List<GameObject> borders = new List<GameObject>();
    // Start is called before the first frame update
    public void Initialize()
    {
        borderPrefab = Resources.Load<GameObject>("UI/Border");
        Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom += OnLeaveRoom;
        for (int i = 0; i < 4; i++)
        {
            borders.Add(Object.Instantiate(borderPrefab));
        }
        UpdateBorderLocations(Managers.GameManager.Instance.LevelManager.CurrentRoom);
    }

    // Update is called once per frame
    void UpdateBorderLocations(Room room)
    {
        borders[0].transform.position = new Vector3(room.transform.position.x, 8, room.transform.position.z + 21); //Top
        borders[1].transform.position = new Vector3(room.transform.position.x - 27.7f, 8, room.transform.position.z -0.5f); //Left
        borders[1].transform.rotation = Quaternion.Euler(90, 90, 0);
        borders[2].transform.position = new Vector3(room.transform.position.x + 27.7f, 8, room.transform.position.z -0.5f); //Right
        borders[2].transform.rotation = Quaternion.Euler(90, 90, 0);
        borders[3].transform.position = new Vector3(room.transform.position.x, 8, room.transform.position.z - 25); // Bottom -18
    }

    void OnLeaveRoom(Room leaving, Room entering)
    {
        entering.LeaveRoom += OnLeaveRoom;
        UpdateBorderLocations(entering);
    }
}
