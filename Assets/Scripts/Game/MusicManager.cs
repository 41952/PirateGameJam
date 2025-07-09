using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [Header("Tracks")]
    public AudioClip menuClip;
    public AudioClip battleClip;
    public AudioClip intermissionClip;

    [Header("Settings")]
    [Tooltip("Duration of crossfade when switching tracks (seconds)")]
    public float crossfadeDuration = 1.5f;

    private AudioSource menuSource;
    private AudioSource battleSource;
    private AudioSource intermissionSource;

    private Coroutine menuFadeCoroutine;
    private Coroutine battleFadeCoroutine;
    private Coroutine intermissionFadeCoroutine;

    private Dictionary<AudioSource, Coroutine> activeCoroutines = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeSources();
        PlayMenu();
    }

    private void InitializeSources()
    {
        menuSource = gameObject.AddComponent<AudioSource>();
        battleSource = gameObject.AddComponent<AudioSource>();
        intermissionSource = gameObject.AddComponent<AudioSource>();

        menuSource.loop = true;
        battleSource.loop = true;
        intermissionSource.loop = true;

        menuSource.playOnAwake = false;
        battleSource.playOnAwake = false;
        intermissionSource.playOnAwake = false;

        if (menuClip) menuSource.clip = menuClip;
        if (battleClip) battleSource.clip = battleClip;
        if (intermissionClip) intermissionSource.clip = intermissionClip;
    }

    /// <summary>
    /// Play menu music and stop other tracks.
    /// </summary>
    public void PlayMenu()
    {
        StartFade(menuSource, 1f);
        StopFade(battleSource);
        StopFade(intermissionSource);
    }

    /// <summary>
    /// Play battle music and fade out intermission/menu.
    /// </summary>
    public void PlayBattle()
    {
        StopFade(menuSource);
        StartFade(battleSource, 1f);
        StopFade(intermissionSource);
    }

    /// <summary>
    /// Play intermission music and fade out battle/menu.
    /// </summary>
    public void PlayIntermission()
    {
        StopFade(menuSource);
        StopFade(battleSource);
        StartFade(intermissionSource, 1f);
    }

    /// <summary>
    /// Blend battle and intermission tracks together by setting volumes accordingly.
    /// blendFactor: 0 = full battle, 1 = full intermission
    /// </summary>
    public void BlendBattleIntermission(float blendFactor)
    {
        // Ensure both sources are playing
        if (!battleSource.isPlaying)
        {
            battleSource.Play();
            battleSource.volume = 0f;
        }
        if (!intermissionSource.isPlaying)
        {
            intermissionSource.Play();
            intermissionSource.volume = 0f;
        }

        // Lerp volumes
        battleSource.volume = Mathf.Lerp(1f, 0f, blendFactor);
        intermissionSource.volume = Mathf.Lerp(0f, 1f, blendFactor);
    }

    private void StartFade(AudioSource source, float targetVolume)
    {
        if (source == null) return;

        StopAllFadeFor(source); // останавливаем любые активные корутины

        if (!source.isPlaying) source.Play();

        Coroutine fade = StartCoroutine(FadeRoutine(source, targetVolume));
        activeCoroutines[source] = fade;
    }

    private void StopFade(AudioSource source)
    {
        if (source == null) return;

        StopAllFadeFor(source);

        Coroutine fadeOut = StartCoroutine(FadeOutAndStop(source));
        activeCoroutines[source] = fadeOut;
    }

    private void StopAllFadeFor(AudioSource source)
    {
        if (activeCoroutines.TryGetValue(source, out Coroutine existing))
        {
            StopCoroutine(existing);
            activeCoroutines.Remove(source);
        }
    }

    private void StopFadeOnly(AudioSource source)
    {
        if (source == menuSource && menuFadeCoroutine != null)
        {
            StopCoroutine(menuFadeCoroutine);
            menuFadeCoroutine = null;
        }
        else if (source == battleSource && battleFadeCoroutine != null)
        {
            StopCoroutine(battleFadeCoroutine);
            battleFadeCoroutine = null;
        }
        else if (source == intermissionSource && intermissionFadeCoroutine != null)
        {
            StopCoroutine(intermissionFadeCoroutine);
            intermissionFadeCoroutine = null;
        }
    }


    private IEnumerator FadeRoutine(AudioSource source, float targetVolume)
    {
        float startVolume = source.volume;
        float elapsed = 0f;

        while (elapsed < crossfadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            source.volume = Mathf.Lerp(startVolume, targetVolume, elapsed / crossfadeDuration);
            yield return null;
        }
        source.volume = targetVolume;
        activeCoroutines.Remove(source);

    }

    private IEnumerator FadeOutAndStop(AudioSource source)
    {
        float startVolume = source.volume;
        float elapsed = 0f;

        while (elapsed < crossfadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            source.volume = Mathf.Lerp(startVolume, 0f, elapsed / crossfadeDuration);
            yield return null;
        }
        source.volume = 0f;
        source.Stop();
        activeCoroutines.Remove(source);

    }
}
