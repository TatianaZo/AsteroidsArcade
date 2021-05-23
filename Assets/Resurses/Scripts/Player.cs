using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    [SerializeField] private GameObject bulletSpawner;
    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float respawnTime = 3.0f;
    [SerializeField] private Bullet bulletPref;
    private float playerTurn = 0.0f;
    private bool movement = false;
    private Rigidbody2D playerRB;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        gameObject.GetComponent<EdgeCollider2D>().enabled = false; //неуязвимость при перерождении на 3 сек

        Invoke("TurnOnCollisions", 3.0f);
    }

    //управление игроком
    private void Update() 
    {
        movement = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            playerTurn = 1.0f;
        } 
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            playerTurn = -1.0f;
        } 
        else {
            playerTurn = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            Shoot();
        }
    }
  
    private void FixedUpdate()
    {
        if (movement) {
            playerRB.AddForce(transform.up * movementSpeed); //ускорение
        }

        if (playerTurn != 0.0f) {
            playerRB.AddTorque(rotationSpeed * playerTurn); //поворот
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletPref, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
        bullet.Project(transform.up);
    }

    private void TurnOnCollisions()
    {
        gameObject.GetComponent<EdgeCollider2D>().enabled = true;
    }

    //Столкновение с астероидом
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Asteroids")
        {

            playerRB.velocity = Vector3.zero;
            playerRB.angularVelocity = 0.0f;


            gameObject.SetActive(false);

          gm.PlayerDeath(this);
        }
    }

}
