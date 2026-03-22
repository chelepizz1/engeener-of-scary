using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class GameOverController : MonoBehaviour
{
    public GameObject gameOverText;
    public TextMeshProUGUI timerText;
    public float timeToLose = 2f;

    private Animator animator;
    private NavMeshAgent agent;

    private float timer = 0f;
    private bool isGhostInside = false;
    private bool isGameOver = false;

    void Start()
    {
        gameOverText.SetActive(false);
        timerText.gameObject.SetActive(false);

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isGameOver) return;

        // 🔹 СКОРОСТЬ ДЛЯ АНИМАЦИИ
        float speed = agent.velocity.magnitude;

        if (isGhostInside)
        {
            // 💥 ОСТАНАВЛИВАЕМ NPC
            agent.isStopped = true;
            speed = 0f;

            // ⏳ ТАЙМЕР
            timer += Time.deltaTime;

            float timeLeft = timeToLose - timer;

            // 🕒 ПОКАЗ ТАЙМЕРА
            timerText.gameObject.SetActive(true);
            timerText.text = Mathf.Clamp(timeLeft, 0f, timeToLose).ToString("F1");

            // 🎨 ЦВЕТ
            if (timeLeft < 1f)
                timerText.color = Color.red;
            else
                timerText.color = Color.white;

            // 💀 GAME OVER
            if (timer >= timeToLose)
            {
                GameOver();
            }
        }
        else
        {
            // 🔄 ВОЗВРАТ В ПАТРУЛЬ
            agent.isStopped = false;
            timer = 0f;
            timerText.gameObject.SetActive(false);
        }

        // 🎬 ОБНОВЛЕНИЕ АНИМАЦИИ
        if (speed < 0.1f) speed = 0f;
        animator.SetFloat("Speed", speed, 0.1f, Time.deltaTime);
    }

    void GameOver()
    {
        isGameOver = true;
        gameOverText.SetActive(true);
        timerText.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            isGhostInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            isGhostInside = false;
        }
    }
}