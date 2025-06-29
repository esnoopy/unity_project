using UnityEngine;
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
}