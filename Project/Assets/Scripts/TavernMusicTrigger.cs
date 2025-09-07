using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;
using Unity.Mathematics;

// ============== INSTRUCTION ==============
// Create empty game object and add "Cinemachine Path" component
// Add waypoints and draw shape -> set to "Looped" to close shape
// Create another empty game object and add this script
// Select "Path" as well as "Player" in the inspector
// Add sound to the object

public class TavernMusicTrigger : MonoBehaviour
{
    [Tooltip("Spline Container to follow")]
    public SplineContainer m_SplineContainer;
    [Tooltip("Character to track")]
    public GameObject Player;

    // The position along the spline
    private float m_Position;

    void Update()
    {   
            // Find the closest point on the spline to the player's position
        SplineUtility.Get=ClosestPoint(m_SplineContainer.Spline, Player.transform.position, out Vector3 closestPoint, out float t);

            // Set the object's position and rotation based on the closest point
        SetCartPosition(t);

            // Define vectors for the dot product
        Vector3 Sub = transform.position - Player.transform.position;
        Vector3 Spline = transform.right;

            // Attach object to player on enter
        if(Vector3.Dot(Sub, Spline) > 0)
        {
            transform.position = Player.transform.position;
            transform.rotation = Player.transform.rotation;
        }   
    }

        // Set cart's position to closest point
    void SetCartPosition(float t)
    {
            // Evaluate the position and tangent (direction) at the parameter t
        Vector3 position = m_SplineContainer.Spline.EvaluatePosition(t);
        Vector3 tangent = m_SplineContainer.Spline.EvaluateTangent(t);
            
        transform.position = position;
        transform.rotation = Quaternion.LookRotation(tangent);
    }
}

/*using UnityEngine;
using System.Collections; // Required for Coroutines

public class TavernMusicTrigger : MonoBehaviour
{
    public AudioSource tavernMusic; // Drag your Audio Source here in the Inspector
    public float fadeDuration = 2.0f; // Duration of the fade in seconds
    public float maxVolume = 0.7f; // Set this to your desired max volume for the music

    private Coroutine fadeOutCoroutine; // To keep track of the fade out coroutine
    private Coroutine fadeInCoroutine;  // To keep track of the fade in coroutine

    // Make sure you have a reference to the AudioSource
    void Start()
    {
        if (tavernMusic == null)
        {
            tavernMusic = GetComponent<AudioSource>();
        }

        // Ensure the music starts fully stopped and volume is at desired level
        if (tavernMusic != null)
        {
            tavernMusic.volume = maxVolume; // Set initial volume (it will be faded in later)
            tavernMusic.Stop(); // Ensure it's stopped at the start
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the tavern music zone!");

            // Stop any ongoing fade-out coroutine
            if (fadeOutCoroutine != null)
            {
                StopCoroutine(fadeOutCoroutine);
            }

            // Start a new fade-in coroutine
            if (tavernMusic != null)
            {
                if (!tavernMusic.isPlaying)
                {
                    tavernMusic.volume = 0f; // Start from silent
                    tavernMusic.Play();
                }
                fadeInCoroutine = StartCoroutine(FadeAudio(tavernMusic, maxVolume, fadeDuration));
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the tavern music zone!");

            // Stop any ongoing fade-in coroutine
            if (fadeInCoroutine != null)
            {
                StopCoroutine(fadeInCoroutine);
            }

            // Start a new fade-out coroutine
            if (tavernMusic != null && tavernMusic.isPlaying)
            {
                fadeOutCoroutine = StartCoroutine(FadeAudio(tavernMusic, 0f, fadeDuration));
            }
        }
    }

    // Coroutine to smoothly fade audio
    IEnumerator FadeAudio(AudioSource audioSource, float targetVolume, float duration)
    {
        float startVolume = audioSource.volume;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float newVolume = Mathf.Lerp(startVolume, targetVolume, timer / duration);
            audioSource.volume = newVolume;
            yield return null; // Wait for the next frame
        }

        audioSource.volume = targetVolume; // Ensure it reaches the exact target volume

        // If fading out to 0, stop the music after the fade is complete
        if (targetVolume == 0f)
        {
            audioSource.Stop();
        }
    }
}*/