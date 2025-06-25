using UnityEngine;
using UnityEngine.UI;

public class PlayerMinimapIcon : MonoBehaviour
{
    public RectTransform mapRectTransform; // UI map (RawImage parent)
    public RectTransform playerMarker;     // The red dot
    public Transform player;               // Player reference

    // These are the *actual world coordinates* of the map edges (manual from the level)
    public Vector2 worldMin = new Vector2(206f, 306f); // Actual MIN X, Actual MIN Z
    public Vector2 worldMax = new Vector2(788f, 695f); // Top-left corner of town

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
}
