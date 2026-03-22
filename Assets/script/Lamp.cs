using UnityEngine;

public class Lamp : MonoBehaviour
{
    public bool isActivated = false;

    public Light lampLight;
    public GameObject hintUI; 

    private bool isPlayerInside = false;

    void Start()
    {
        lampLight.enabled = false;
        hintUI.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInside && !isActivated)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartMiniGame();
            }
        }
    }

    void StartMiniGame()
    {
        Debug.Log("Мини-игра началась");

        ActivateLamp();
    }

    public void ActivateLamp()
    {
        isActivated = true;
        lampLight.enabled = true;

        Debug.Log("Лампа включена");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            hintUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            hintUI.SetActive(false);
        }
    }
}