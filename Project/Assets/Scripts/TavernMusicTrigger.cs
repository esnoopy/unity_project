using UnityEngine;
using System.Collections; // Required for Coroutines

public class TavernMusicTrigger : MonoBehaviour
{
    public AudioSource tavernMusic;
    public float fadeDuration = 2.0f;
    public float maxVolume = 0.7f;

    private Coroutine fadeOutCoroutine;
    private Coroutine fadeInCoroutine;

    void Start()
    {
        if (tavernMusic == null)
        {
            tavernMusic = GetComponent<AudioSource>();
        }

        if (tavernMusic != null)
        {
            // Set initial volume to 0 and play the audio.
            // This preloads the music into memory without making it audible.
            tavernMusic.volume = 0f;
            tavernMusic.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the tavern music zone!");

            if (fadeOutCoroutine != null)
            {
                StopCoroutine(fadeOutCoroutine);
            }

            if (tavernMusic != null)
            {
                // The music is already silently playing, so we just start the fade-in.
                fadeInCoroutine = StartCoroutine(FadeAudio(tavernMusic, maxVolume, fadeDuration));
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the tavern music zone!");

            if (fadeInCoroutine != null)
            {
                StopCoroutine(fadeInCoroutine);
            }

            if (tavernMusic != null)
            {
                // The FadeAudio coroutine will fade out the music.
                // We'll no longer stop the music at the end of the fade-out,
                // as it needs to remain "playing" silently for the next trigger.
                fadeOutCoroutine = StartCoroutine(FadeAudio(tavernMusic, 0f, fadeDuration));
            }
        }
    }

    IEnumerator FadeAudio(AudioSource audioSource, float targetVolume, float duration)
    {
        float startVolume = audioSource.volume;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float newVolume = Mathf.Lerp(startVolume, targetVolume, timer / duration);
            audioSource.volume = newVolume;
            yield return null;
        }

        audioSource.volume = targetVolume;
    }
}