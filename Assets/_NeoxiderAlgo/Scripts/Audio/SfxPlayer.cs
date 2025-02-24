using UnityEngine;

[AddComponentMenu("_Neoxider/" + "Audio/" + nameof(SfxPlayer))]
public class SfxPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] soundClips; // Звуковой эффект для воспроизведения

    [SerializeField]
    private float volume = 1f;   // Громкость звука

    /// <summary>
    /// Метод для воспроизведения звукового эффекта через синглтон SoundEffectManager.
    /// </summary>
    public void PlaySound()
    {
        if (SoundEffectManager.Instance != null)
        {
            AudioClip clip = soundClips[Random.Range(0, soundClips.Length)];
            SoundEffectManager.Instance.PlaySfx(clip, volume);
        }
        else
        {
            Debug.LogWarning("SoundEffectManager не найден в сцене.");
        }
    }

    // Пример вызова: можно вызвать этот метод по событию, либо оставить для теста вызов на клавишу
    private void Update()
    {
        // Для теста: по нажатию на клавишу P воспроизводим звук
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlaySound();
        }
    }
} 