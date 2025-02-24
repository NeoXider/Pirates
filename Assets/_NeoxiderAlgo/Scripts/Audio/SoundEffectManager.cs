using UnityEngine;

[AddComponentMenu("_Neoxider/" + "Audio/" + nameof(SoundEffectManager))]
public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance { get; private set; }

    [SerializeField]
    private AudioSource sfxSource; // Источник звука для воспроизведения эффектов

    private void Awake()
    {
        // Организуем синглтон
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Если AudioSource не задан, создаём его
        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }
    }

    /// <summary>
    /// Воспроизводит звуковой эффект с заданной громкостью.
    /// </summary>
    /// <param name="clip">Аудиоклип для воспроизведения.</param>
    /// <param name="volume">Громкость воспроизведения (по умолчанию 1f).</param>
    public void PlaySfx(AudioClip clip, float volume = 1f)
    {
        if (clip == null)
            return;

        sfxSource.PlayOneShot(clip, volume);
    }
} 