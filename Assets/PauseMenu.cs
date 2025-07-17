using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    GameObject pauseMenuUI;

    bool isMainMenu;



    void Start()
    {
        pauseMenuUI = transform.GetChild(0).gameObject;
        isMainMenu = SceneManager.GetActiveScene().buildIndex == 0;
    }

    void Update()
    {
        if (isMainMenu) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        Time.timeScale = 1f;

        if (isMainMenu)
        {
            return;
        }
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);

        bool isOn = pauseMenuUI.activeSelf;

        if (isOn)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;

        }
    }

    public void Play()
    {
        if (isMainMenu)
        {
            SceneManager.LoadScene(1);
            return;
        }
        TogglePauseMenu();
        Time.timeScale = 1f;

    }

    public void Quit()
    {
        if (isMainMenu)
        {
            Application.Quit();
            return;
        }
        Time.timeScale = 1f;

        SceneManager.LoadScene(0);

    }
}
