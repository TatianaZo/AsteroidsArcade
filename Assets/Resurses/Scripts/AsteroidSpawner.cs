using UnityEngine;


public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private Asteroid asteroidPrefab;
    [SerializeField] private float spawnDistance = 12.0f;
    [SerializeField] private float spawnRate = 1.0f;
    [SerializeField] private int amountPerSpawn = 1;
    [Range(0.0f, 45.0f)] public float trajectoryVariance = 15.0f;

    private void Start()
    {
        // появление астероидов с определенным проммежутком времени
        InvokeRepeating("Spawn", spawnRate, spawnRate);
    }

    public void Spawn()
    {
        for (int i = 0; i < amountPerSpawn; i++) 
        {
            // появление астероидов в случайном месте и направление в сторону центра экрана
            Vector2 spawnDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPoint = spawnDirection * spawnDistance;

            //смещение точки появления 
            spawnPoint += transform.position;

            //рассчет случайного вращения с изменением траектории
            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            //создание нового астороида со случайным размером
            Asteroid asteroid = Instantiate(asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);

            //движение астероида к центру экрана
            Vector2 trajectory = rotation * -spawnDirection;
            asteroid.SetTrajectory(trajectory);
        }
    }

}
