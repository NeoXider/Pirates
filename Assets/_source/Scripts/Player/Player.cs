using UnityEngine;
using UnityEngine.SceneManagement;
using KinematicCharacterController.Examples;

[RequireComponent(typeof(HealthSystem))]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public ExamplePlayer examplePlayer;

    public HealthSystem healthSystem;

    [SerializeField]
    private float interactRange = 4; // Радиус взаимодействия с объектами

    [SerializeField]
    private Transform handTransform; // Точка, куда помещается подобранный объект

    [SerializeField]
    private KeyCode pickupKey = KeyCode.E;      // Клавиша для подбирания

    [SerializeField]
    private KeyCode dropKey = KeyCode.G;      // Клавиша для выбрасывания

    [SerializeField]
    private KeyCode useKey = KeyCode.Mouse0;     // Клавиша для использования (левая кнопка мыши)

    [SerializeField]
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

        // Если нажата клавиша подбирания
        if (Input.GetKeyDown(pickupKey))
        {
            AttemptPickup();
        }

        // Если нажата клавиша выбрасывания
        if (Input.GetKeyDown(dropKey))
        {
            if (currentHeldItem != null)
            {
                DropItem();
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
    /// Пытается подобрать объект через рейкаст.
    /// Луч бросается из центра камеры (в направлении взгляда камеры).
    /// Игрок игнорируется, а также используются столкновения с триггерами.
    /// </summary>
    private void AttemptPickup()
    {
        // Луч из центра экрана
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        // Используем QueryTriggerInteraction.Collide, чтобы луч получал столкновения с триггерами
        if (Physics.Raycast(ray, out hit, interactRange, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Collide))
        {
            // Игнорируем самого игрока, если он случайно попался в луч
            if (hit.transform.gameObject == gameObject)
            {
                return;
            }

            // Если объект обладает компонентом InteractableObject и его можно подобрать
            InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();

            //если не получилось получить по колайдеру, пытаемся получить по RigidBody
            if (interactable == null && hit.rigidbody != null)
                interactable = hit.rigidbody.GetComponent<InteractableObject>();

            if (interactable != null)
            {
                interactable.InteractByKey();
                
                if (interactable.IsPickupable)
                {
                    //если в руках нет объекта можем подобрать
                    if (currentHeldItem == null)
                    {
                        PickupItem(interactable.gameObject);
                        interactable.PickupItem();

                        Debug.Log("Мы кликнули на " + interactable.name);
                        return;
                    }
                }
            }

            //если в руках нет объекта можем подобрать
            if (currentHeldItem == null)
            {            // Альтернативно, если объект реализует IUsable, считаем его подбираемым
                IUsable usable = hit.collider.GetComponent<IUsable>();
                if (usable != null)
                {
                    Debug.Log("Мы кликнули на " + hit.collider.name);
                    PickupItem(hit.collider.gameObject);
                }
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

            if (currentHeldItem.TryGetComponent(out InteractableObject interactableObject))
            {
                interactableObject.DropItem();
            }

            // Дополнительно можно добавить импульс для "броска" предмета
            currentHeldItem = null;
        }
    }

    private void OnValidate()
    {
        healthSystem ??= GetComponent<HealthSystem>();

        examplePlayer ??= FindObjectOfType<ExamplePlayer>();
    }

    /// <summary>
    /// Для проверки дальности взаимодействия рисуем линию-луч (грисмо)
    /// от камеры в направлении её взгляда.
    /// </summary>
    private void OnDrawGizmos()
    {
        if (Camera.main != null)
        {
            Gizmos.color = Color.green;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * interactRange);
        }
    }
}