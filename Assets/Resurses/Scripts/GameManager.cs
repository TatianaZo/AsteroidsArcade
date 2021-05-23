using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Sprite explosion;
    [SerializeField] private GameObject restartUI;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;


    public int score { get; private set; }

    public int lives { get; private set; }


    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        // рестарт игры после нажатия enter
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return))
        {
            NewGame();
        }
    }

    public void NewGame()
    {
        // очещаем поле от астероидов
        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();
        for (int i = 0; i < asteroids.Length; i++) 
        {
            Destroy(asteroids[i].gameObject);
        }

        restartUI.SetActive(false);

        SetScore(0);
        SetLives(3);
        Respawn();
    }

    public void Respawn()
    {
        // перемещаем игрока после смерти в центр экрана
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        // начисление очков за астероиды в зависимости от размера
        if (asteroid.size < 0.7f) 
        {
            SetScore(score + 100); //маленький
        }
        else if (asteroid.size < 1.4f) 
        {
            SetScore(score + 50); // средний
        } 
        else 
        {
            SetScore(score + 25); // большой
        }
    }

    public void PlayerDeath(Player player)
    {

        // -1  жизнь
        SetLives(lives - 1);

        // проверка на наличие жизней
        if (lives <= 0)
        {
            GameOver();
        }
        else 
        {
            // перерождение игрока
            Invoke("Respawn",3.0f);
        }
    }

    public void GameOver()
    {
        // показ интерфейса проигрыша
        restartUI.SetActive(true);
    }

    private void SetScore(int score)
    {
        // показ кол-ва очков
        this.score = score;
        scoreText.text = score.ToString();
    }

    private void SetLives(int lives)
    {
        // показ жизней
        this.lives = lives;
        livesText.text = lives.ToString();
    }

}
