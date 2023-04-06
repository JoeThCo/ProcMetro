using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] TileSet tileSet;
    [SerializeField] Room roomPrefab;

    public void MakeRoom()
    {
        while (transform.childCount != 0)
        {
            foreach (Transform t in transform)
            {
                DestroyImmediate(t.gameObject);
            }
        }

        Room room = Instantiate(roomPrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<Room>();
        room.RoomInit(tileSet);
    }
}