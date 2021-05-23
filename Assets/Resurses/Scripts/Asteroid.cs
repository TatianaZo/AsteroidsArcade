using UnityEngine;


public class Asteroid : MonoBehaviour
{
   
    public Sprite[] sprites;

    [HideInInspector] public float size = 1.0f;
    public float minSize = 0.35f;
    public float maxSize = 1.65f;
    [SerializeField] private float movementSpeed = 50.0f;
    [SerializeField] private float maxLifetime = 30.0f;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D asteroidRB;

    private void Awake()
    {
        asteroidRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)]; // назначение случайного спрайта астороида
        
        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f); // назначение случайного вращения асторида

        transform.localScale = Vector3.one * size;// установка размера и массы астероида
        asteroidRB.mass = size;

        Destroy(gameObject, maxLifetime); //удаление астороида по окончании времени жизни
    }

    public void SetTrajectory(Vector2 direction)
    {
        asteroidRB.AddForce(direction * movementSpeed);//движение астероида с учетом его скорости
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // проверка на попадание пульки в астероид
        if (collision.gameObject.tag == "Bullet")
        {
            //проверка размера астероида для разбивания напополам
            if ((size * 0.5f) >= minSize)
            {
                //делим астероид на 2 части создавая новые
                CreateSplit();
                CreateSplit();
            }

            //добавляем очки за уничтоженный астероид
            FindObjectOfType<GameManager>().AsteroidDestroyed(this);

            Destroy(gameObject);
        }
    }

    private Asteroid CreateSplit() //создаем половинки астероида
    {
        //указываем позиции астероидов с небольшим сдвигом
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f; 

        // создаем астероид в половину меньше уничтоженного
        Asteroid half = Instantiate(this, position, transform.rotation);
        half.size = size * 0.5f;

        half.SetTrajectory(Random.insideUnitCircle.normalized); //задаем рандомную траекторию

        return half;
    }

}
