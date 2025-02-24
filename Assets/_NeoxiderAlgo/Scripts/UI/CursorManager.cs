using UnityEngine;

[AddComponentMenu("_Neoxider/" + "UI/" + nameof(CursorManager))]
public class CursorManager : MonoBehaviour
{
   void OnEnable()
    {
        Player.Instance.examplePlayer.enabled = false;
        // Включаем курсор и делаем его видимым
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnDisable()
    {
        Player.Instance.examplePlayer.enabled = true;
        // Выключаем курсор, скрываем его от пользователя
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}