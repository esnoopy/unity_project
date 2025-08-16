/*using UnityEngine;
using UnityEngine.UI;

public class PlayerMinimapIcon : MonoBehaviour
{
    public RectTransform mapRectTransform; // UI map (RawImage parent)
    public RectTransform playerMarker;     // The red dot
    public Transform player;               // Player reference

    // These are the *actual world coordinates* of the map edges (manual from the level)
    public Vector2 worldMin = new Vector2(0f, 0f); // Actual MIN X, Actual MIN Z
    public Vector2 worldMax = new Vector2(1000f, 1000f); // Top-left corner of town

    void Update()
    {
        Vector2 worldSize = worldMax - worldMin;

        // Convert player world position to normalized position within the town area
        Vector2 normalizedPosition = new Vector2(
            Mathf.InverseLerp(worldMin.x, worldMax.x, player.position.x),
            Mathf.InverseLerp(worldMin.y, worldMax.y, player.position.z) // z in world space is y in 2D map
        );

        // Map size in UI space
        Vector2 mapSize = mapRectTransform.rect.size;

        // Convert normalized position to UI anchored position
        Vector2 anchoredPos = new Vector2(
            normalizedPosition.x * mapSize.x,
            normalizedPosition.y * mapSize.y
            
        );

        playerMarker.anchoredPosition = anchoredPos;

        Debug.Log("Player world pos: " + player.position);
        Debug.Log("Normalized: " + normalizedPosition);
        Debug.Log("Anchored UI pos: " + anchoredPos);
            Debug.Log($"Map Size: {mapSize}");
        }
}*/

/*using UnityEngine;
using UnityEngine.UI;

public class PlayerMinimapIcon : MonoBehaviour
{
    public RectTransform mapRectTransform; // Map UI image (parent)
    public RectTransform playerMarker;     // Red dot
    public Transform player;               // Player reference

    public Vector2 worldMin; // set from MapBoundsFinder output
    public Vector2 worldMax;

    void Update()
    {
        Vector2 worldSize = worldMax - worldMin;

        // Convert player world position to normalized position within bounds
        Vector2 normalizedPosition = new Vector2(
            Mathf.InverseLerp(worldMin.x, worldMax.x, player.position.x),
            Mathf.InverseLerp(worldMin.y, worldMax.y, player.position.z)
        );

        // Map size in UI space
        Vector2 mapSize = mapRectTransform.rect.size;

        // Convert normalized position to anchored position (center pivot assumed)
        Vector2 anchoredPos = new Vector2(
            normalizedPosition.x * mapSize.x,
            normalizedPosition.y * mapSize.y
        );

        playerMarker.anchoredPosition = anchoredPos;
    }
}*/

using UnityEngine;
using UnityEngine.UI;

public class PlayerMinimapIcon : MonoBehaviour
{
    public RectTransform mapRectTransform; // Map UI image (parent)
    public RectTransform playerMarker;     // Red dot
    public Transform player;               // Player reference

    public Vector2 worldMin = new Vector2(0f, 0f);  // Initial guess
    public Vector2 worldMax = new Vector2(1000f, 1000f); // Initial guess

    private float mapWidthWorld;
    private float mapHeightWorld;

    void Start()
    {
        // Store original size
        mapWidthWorld = worldMax.x - worldMin.x;
        mapHeightWorld = worldMax.y - worldMin.y;

        // Adjust horizontal center so player starts in the middle
        float playerX = player.position.x;
        float halfWidth = mapWidthWorld / 2f;
        worldMin.x = playerX - halfWidth;
        worldMax.x = playerX + halfWidth;

        // Adjust vertical bottom so player starts at bottom
        float playerZ = player.position.z;
        worldMin.y = playerZ;
        worldMax.y = playerZ + mapHeightWorld;
    }

    void Update()
    {
        // Convert player world position to normalized position
        Vector2 normalizedPosition = new Vector2(
            Mathf.InverseLerp(worldMin.x, worldMax.x, player.position.x),
            Mathf.InverseLerp(worldMin.y, worldMax.y, player.position.z)
        );

        // Map size in UI space
        Vector2 mapSize = mapRectTransform.rect.size;

        // Convert normalized position to anchored position
        Vector2 anchoredPos = new Vector2(
            normalizedPosition.x * mapSize.x,
            normalizedPosition.y * mapSize.y
        );

        playerMarker.anchoredPosition = anchoredPos;
    }
}
