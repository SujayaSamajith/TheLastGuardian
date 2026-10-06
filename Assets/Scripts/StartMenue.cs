using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;

    private void Start()
    {
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        startMenu.SetActive(false);

        Time.timeScale = 1f;
    }
}