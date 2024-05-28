using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverText;

    private int score = 0;
    private bool gameOver = false;
    private int bestScore = 0;
    public TextMeshProUGUI startText; // Nuevo TextMeshProUGUI para el texto inicial
    public TextMeshProUGUI bestScoreText; // Nuevo TextMeshProUGUI para el texto inicial

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    void Start()
    {
        // Empezar la corrutina que aumenta el puntaje cada 5 segundos
        StartCoroutine(IncreaseScoreOverTime());
        scoreText.text = "Score: 0";
        bestScoreText.text = "BestScore: " + bestScore.ToString();
    }

    void Update()
    {
        if (gameOver && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.X)))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.X))
        {
            startText.gameObject.SetActive(false); // Ocultar el texto de inicio
        }
    }

    IEnumerator IncreaseScoreOverTime()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(5f);
            BirdScored();
        }
    }

    public void BirdScored()
    {
        if (gameOver)
            return;
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    public void BirdDied()
    {
        gameOverText.SetActive(true);
        gameOver = true;
        startText.gameObject.SetActive(true); // Mostrar el texto de inicio

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            bestScoreText.text = "BestScore: " + bestScore.ToString();
        }
        PlayerPrefs.Save();
    }
}
