using UnityEngine;

public class RoomGenerationState : MonoBehaviour
{
    public static RoomGenerationState Instance;

    public int[,] roomGrid;
    public Vector2Int roomQueuePosition;
    public bool hasShop;
    public bool hasCampsite;

    // Store the random seed here (or use a predefined one)
    private int randomSeed;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate
        }
    }

    public void SaveGenerationState(RoomManager roomManager)
    {
        // Save grid data and random state
        randomSeed = roomManager.GetRandomSeed();
        roomGrid = roomManager.GetRoomGridCopy();
        roomQueuePosition = roomManager.GetRoomQueuePosition();
        hasShop = roomManager.HasShop();
        hasCampsite = roomManager.HasCampsite();
    }

    public void LoadGenerationState(RoomManager roomManager)
    {
        // Load the saved state and reapply it
        roomManager.SetRandomSeed(randomSeed);
        roomManager.SetRoomGridCopy(roomGrid);
        roomManager.SetRoomQueuePosition(roomQueuePosition);
        roomManager.SetHasShop(hasShop);
        roomManager.SetHasCampsite(hasCampsite);
    }
}
