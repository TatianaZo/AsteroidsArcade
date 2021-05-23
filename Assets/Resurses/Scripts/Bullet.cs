using UnityEngine;



public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 500.0f;
    [SerializeField] private float maxLifetime = 10.0f; //время жизни пульки
    private Rigidbody2D bulletRB;

    private void Awake()
    {
        bulletRB = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        bulletRB.AddForce(direction * speed);//скорость и направление пульки
        Destroy(gameObject, maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject); //уничтожение пульки при столкновении
    }

}
