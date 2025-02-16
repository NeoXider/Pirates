using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public HealthSystem healthSystem;

    [SerializeField]
    private float interactRange = 2f; // Радиус взаимодействия с объектами

    [SerializeField]
    private Transform handTransform; // Точка, куда помещается подобранный объект

    [SerializeField]
    private KeyCode pickupKey = KeyCode.E;      // Клавиша для подбирания и выбрасывания
    [SerializeField]
    private KeyCode useKey = KeyCode.Mouse1;     // Клавиша для использования (правая кнопка мыши)

    private GameObject currentHeldItem;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        // Перезапуск сцены по клавише R
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartCurrentScene();
        }

        // Если нажата клавиша подбирания/выбрасывания (E)
        if (Input.GetKeyDown(pickupKey))
        {
            if (currentHeldItem != null)
            {
                DropItem();
            }
            else
            {
                AttemptPickup();
            }
        }

        // Если нажата клавиша использования (правая кнопка мыши)
        if (Input.GetKeyDown(useKey))
        {
            if (currentHeldItem != null)
            {
                IUsable usable = currentHeldItem.GetComponent<IUsable>();
                if (usable != null)
                {
                    usable.Use();
                }
            }
        }
    }

    /// <summary>
    /// Перезапускает текущую сцену.
    /// </summary>
    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Пытается подобрать объект через рейкаст от позиции курсора.
    /// Если объект имеет компонент InteractableObject с включённой возможностью подбора или реализует IUsable,
    /// он прикрепляется к руке игрока.
    /// </summary>
    private void AttemptPickup()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, interactRange))
        {
            // Если объект обладает компонентом InteractableObject и его можно подобрать
            InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();
            if (interactable != null && interactable.IsPickupable)
            {
                PickupItem(interactable.gameObject);
                return;
            }
            
            // Альтернативно, если объект реализует IUsable, считаем его подбираемым
            IUsable usable = hit.collider.GetComponent<IUsable>();
            if (usable != null)
            {
                PickupItem(hit.collider.gameObject);
            }
        }
    }

    /// <summary>
    /// Прикрепляет объект к руке игрока.
    /// </summary>
    public void PickupItem(GameObject item)
    {
        if (handTransform != null)
        {
            item.transform.SetParent(handTransform);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
            currentHeldItem = item;
        }
    }

    /// <summary>
    /// Выбрасывает (отцепляет) текущий объект.
    /// </summary>
    public void DropItem()
    {
        if (currentHeldItem != null)
        {
            currentHeldItem.transform.SetParent(null);
            // Можно добавить физику выброса (например, силу)
            currentHeldItem = null;
        }
    }

    private void OnValidate()
    {
        if (healthSystem == null)
            healthSystem = GetComponent<HealthSystem>();
    }
} 