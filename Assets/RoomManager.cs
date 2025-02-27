using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
 //   public static RoomManager Instance { get; private set; }

    [SerializeField] private GameObject roomPrefab;
    [SerializeField] private int maxRooms = 15;
    [SerializeField] private int minRooms = 10;
    [SerializeField] public GameObject[] rooms;

    private int roomWidth = 17;
    private int roomHeight = 9;

    [SerializeField] int gridSizeX = 10;
    [SerializeField] int gridSizeY = 10;

    private List<GameObject> roomObjects = new List<GameObject>();
    private Queue<Vector2Int> roomQueue = new Queue<Vector2Int>();

    private int[,] roomGrid;
    private int roomCount;

    private bool generationComplete = false;

    private bool hasShop = false;
    private bool hasCampsite = false;

    private int randomSeed;

    private void Awake()
    {
        //// Ensure that only one instance of RoomManager exists
        //if (Instance != null && Instance != this)
        //{
        //    Destroy(gameObject);  // Destroy duplicate RoomManager instances
        //}
        //else
        //{
        //    Instance = this; // Set the static instance to this instance
        //    DontDestroyOnLoad(gameObject); // Ensure it persists across scenes
        //    Debug.Log("RoomManager Instance Init");
        //}
    }

    private void Start()
    {
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue = new Queue<Vector2Int>();

        if (RoomGenerationState.Instance != null)
        {
            // Load the previous state if available
            RoomGenerationState.Instance.LoadGenerationState(this);
        }
        else
        {
            // Otherwise, start fresh
            Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
            StartRoomGenerationFromRoom(initialRoomIndex);
        }

        //RoomGenerationState.Instance.SaveGenerationState(RoomManager.Instance);

    }

    private void Update()
    {
        if (roomQueue.Count > 0 && roomCount < maxRooms && !generationComplete)
        {
            Vector2Int roomIndex = roomQueue.Dequeue();
            int gridX = roomIndex.x;
            int gridY = roomIndex.y;

            TryGenerateRoom(new Vector2Int(gridX - 1, gridY));
            TryGenerateRoom(new Vector2Int(gridX + 1, gridY));
            TryGenerateRoom(new Vector2Int(gridX, gridY + 1));
            TryGenerateRoom(new Vector2Int(gridX, gridY - 1));
        }
        else if (roomCount < minRooms)
        {
            RegenerateRooms();
        }
        else if (!generationComplete)
        {
            generationComplete = true;
        }
    }

    // Set random seed for generation
    public void SetRandomSeed(int seed)
    {
        randomSeed = seed;
        Random.InitState(seed); // Set the random seed to ensure reproducibility
    }

    // Get the current random seed
    public int GetRandomSeed()
    {
        return randomSeed;
    }

    // Get a copy of the room grid (for saving the state)
    public int[,] GetRoomGridCopy()
    {
        return (int[,])roomGrid.Clone();
    }

    // Get the position of the room queue
    public Vector2Int GetRoomQueuePosition()
    {
        return roomQueue.Peek();
    }

    public void SetRoomGridCopy(int[,] grid)
    {
        roomGrid = grid;
    }

    public void SetRoomQueuePosition(Vector2Int position)
    {
        roomQueue.Enqueue(position);
    }

    public bool HasShop() => hasShop;
    public bool HasCampsite() => hasCampsite;

    public void SetHasShop(bool value) => hasShop = value;
    public void SetHasCampsite(bool value) => hasCampsite = value;

    // Try generating a room at the given position
    private bool TryGenerateRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;

        if (x >= gridSizeX || y >= gridSizeY || x < 0 || y < 0)
            return false;

        if (roomCount >= maxRooms)
        {
            return false;
        }

        if (Random.value < 0.5 && roomIndex != Vector2Int.zero)
        {
            return false;
        }

        if (CountAdjacentRooms(roomIndex) > 1)
            return false;

        if (roomGrid[x, y] != 0)
            return false;

        roomQueue.Enqueue(roomIndex);
        roomGrid[x, y] = 1;
        roomCount++;

        var newRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        newRoom.GetComponent<Room>().RoomIndex = roomIndex;
        newRoom.name = $"Room-{roomCount}";
        newRoom.transform.SetParent(transform);
        // Assign room type (Shop, Campsite, etc.)
        AssignRoomType(newRoom.GetComponent<Room>());

        roomObjects.Add(newRoom);

        OpenDoors(newRoom, x, y);

        return true;
    }

    // Assign room types (Shop, Campsite, etc.)
    private void AssignRoomType(Room roomScript)
    {
        if (!hasShop && Random.value < 0.1f) // 10% chance for Shop
        {
            roomScript.roomType = Room.RoomType.Settlement; // Assign as Shop
            hasShop = true;
        }
        else if (!hasCampsite && Random.value < 0.1f) // 10% chance for Campsite
        {
            roomScript.roomType = Room.RoomType.Campsite; // Assign as Campsite
            hasCampsite = true;
        }
        else
        {
            roomScript.roomType = (Room.RoomType)Random.Range(1, System.Enum.GetValues(typeof(Room.RoomType)).Length);
        }

        // Set up the room's features (like activating items, combat, etc.)
        roomScript.SetRooms();
    }

    private void RegenerateRooms()
    {
        roomObjects.ForEach(Destroy);
        roomObjects.Clear();
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue.Clear();
        roomCount = 0;
        generationComplete = false;

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

    // Open the doors to neighboring rooms
    void OpenDoors(GameObject room, int x, int y)
    {
        Room newRoomScript = room.GetComponent<Room>();

        // Check neighboring rooms and open doors accordingly
        Room leftRoomScript = GetRoomScriptAt(new Vector2Int(x - 1, y));
        Room rightRoomScript = GetRoomScriptAt(new Vector2Int(x + 1, y));
        Room topRoomScript = GetRoomScriptAt(new Vector2Int(x, y + 1));
        Room bottomRoomScript = GetRoomScriptAt(new Vector2Int(x, y - 1));

        // Left neighbor
        if (x > 0 && roomGrid[x - 1, y] != 0 && leftRoomScript != null)
        {
            newRoomScript.OpenDoor(Vector2Int.left);
            leftRoomScript.OpenDoor(Vector2Int.right);
        }

        // Right neighbor
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0 && rightRoomScript != null)
        {
            newRoomScript.OpenDoor(Vector2Int.right);
            rightRoomScript.OpenDoor(Vector2Int.left);
        }

        // Bottom neighbor (below)
        if (y > 0 && roomGrid[x, y - 1] != 0 && bottomRoomScript != null)
        {
            newRoomScript.OpenDoor(Vector2Int.down);
            bottomRoomScript.OpenDoor(Vector2Int.up);
        }

        // Top neighbor (above)
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0 && topRoomScript != null)
        {
            newRoomScript.OpenDoor(Vector2Int.up);
            topRoomScript.OpenDoor(Vector2Int.down);
        }
    }

    Room GetRoomScriptAt(Vector2Int index)
    {
        GameObject roomObject = roomObjects.Find(r => r.GetComponent<Room>().RoomIndex == index);
        if (roomObject != null)
            return roomObject.GetComponent<Room>();
        return null;
    }

    private int CountAdjacentRooms(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        int count = 0;

        if (x > 0 && roomGrid[x - 1, y] != 0) count++; // left neighbor
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0) count++; // right neighbor
        if (y > 0 && roomGrid[x, y - 1] != 0) count++; // bottom neighbor
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0) count++; // top neighbor

        return count;
    }

    private Vector3Int GetPositionFromGridIndex(Vector2Int gridIndex)
    {
        return new Vector3Int(
            roomWidth * (gridIndex.x - gridSizeX / 2),
            roomHeight * (gridIndex.y - gridSizeY / 2),
            0
        );
    }

    // For visual debugging
    private void OnDrawGizmos()
    {
        Color gizmoColor = new Color(0, 1, 1, 0.05f);
        Gizmos.color = gizmoColor;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 position = GetPositionFromGridIndex(new Vector2Int(x, y));
                Gizmos.DrawWireCube(
                    new Vector3(position.x, position.y, 0),
                    new Vector3(roomWidth, roomHeight, 1)
                );
            }
        }
    }

    private void StartRoomGenerationFromRoom(Vector2Int roomIndex)
    {
        roomQueue.Enqueue(roomIndex);
        int x = roomIndex.x;
        int y = roomIndex.y;
        roomGrid[x, y] = 1;
        roomCount++;
        var initialRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        initialRoom.name = $"Room-{roomCount}";
        initialRoom.GetComponent<Room>().RoomIndex = roomIndex;
        roomObjects.Add(initialRoom);

        initialRoom.transform.SetParent(transform);
       
    }
}
