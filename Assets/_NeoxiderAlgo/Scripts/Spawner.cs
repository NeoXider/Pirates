using UnityEngine;
using System;
using System.Collections;
using Random = UnityEngine.Random;

[AddComponentMenu("_Neoxider/"+ nameof(Spawner))]
public class Spawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField]
    private GameObject projectilePrefab;
    [Tooltip("Если задан хотя бы один трансформ, снаряд будет спавниться в случайной точке из этого массива.")]
    [SerializeField]
    private Transform[] spawnPoints;         // Массив позиций спавна; если не пуст, выбирается случайная точка

    [Tooltip("Количество снарядов, которые нужно заспавнить (по умолчанию 1).")]
    [SerializeField]
    private int spawnQuantity = 1;           // Количество снарядов для спавна (по умолчанию 1)

    [Tooltip("Задержка")]
    [SerializeField]
    private float delaySpawn = 0;           // Количество снарядов для спавна (по умолчанию 1)

    [Tooltip("Спавнить снаряды внутри выбранного Transform.")]
    public bool spawnInsideTransform = false;

    public bool playInAwake = true;

    public void Awake()
    {
        if(playInAwake)
        {
            Spawn();
        }
    }

    /// <summary>
    /// Спавнит снаряды.
    /// Если в массиве spawnPoints есть элементы – для каждого снаряда случайно выбирается точка из него,
    /// иначе если задан spawnPoint, он используется, а если и он не задан – используется позиция самого спавнера.
    /// </summary>
    public void Spawn()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        for (int i = 0; i < spawnQuantity; i++)
        {
            Transform spawnTransform = spawnPoints.Length > 0
                ? spawnPoints[Random.Range(0, spawnPoints.Length)]
                : transform;
            Vector3 spawnPos = spawnTransform.position;      // базовая позиция – позиция объекта-спавнера
            Quaternion spawnRot = spawnTransform.rotation;     // базовая ротация – ротация объекта-спавнера

            // Спавним снаряд. Сам снаряд добавляет силу в Start.
            Instantiate(projectilePrefab, spawnPos, spawnRot, spawnInsideTransform ? spawnTransform : null);

            yield return new WaitForSeconds(delaySpawn);
        }
    }
}