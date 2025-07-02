using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // Existing variables
    public RoomManager _manager;
    public Transform _managerTransform;
    [SerializeField] GameObject topDoor;
    [SerializeField] GameObject bottomDoor;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;

    public GameObject mediationMedallion;
    public GameObject campsite;
    public GameObject combat;
    public GameObject shop;
    public GameObject item;
    public GameObject rope;

    public RoomType roomType;

    public enum RoomType
    {
        None,
        Passageway,
        Encounter,
        Campsite,
        Settlement,
        Narrative
    }

    void Start()
    {
        // Default type, will be overwritten by RoomManager
        roomType = RoomType.None;
        SetRooms(); // Set room-related objects based on room type
    }

    // Activate objects based on room type
    public void SetRooms()
    {
        switch (roomType)
        {
            case RoomType.None:
                print("No room type set.");
                break;
            case RoomType.Passageway:
                item.SetActive(true);
                break;
            case RoomType.Encounter:
                combat.SetActive(true);
                break;
            case RoomType.Campsite:
                campsite.SetActive(true);
                break;
            case RoomType.Settlement:
                shop.SetActive(true);
                break;
            case RoomType.Narrative:
                mediationMedallion.SetActive(true);
                break;
            default:
                print("no room.");
                break;
        }
        this.gameObject.transform.parent = _managerTransform;
    }

    public Vector2Int RoomIndex { get; set; }

    public void OpenDoor(Vector2Int direction)
    {
        if (direction == Vector2Int.up)
        {
            topDoor.SetActive(true);
            rope.SetActive(true);
        }
        if (direction == Vector2Int.down)
        {
            bottomDoor.SetActive(true);
            rope.SetActive(true);
        }
        if (direction == Vector2Int.left)
        {
            leftDoor.SetActive(true);
        }
        if (direction == Vector2Int.right)
        {
            rightDoor.SetActive(true);
        }
    }
}
