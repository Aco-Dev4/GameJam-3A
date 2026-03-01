using UnityEngine;
using UnityEngine.SceneManagement; // Potrebné pre naèítavanie scén

public class PauseMenu : MonoBehaviour
{
    // Statická premenná, vïaka ktorej môu aj iné skripty vedie, èi je hra pauznutá
    public static bool GameIsPaused = false;

    // Sem v inšpektore potiahneš tvoj Empty Object s tlaèidlami
    public GameObject pauseMenuUI;

    void Update()
    {
        // Kontrola, èi hráè stlaèil ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Metóda na odpausovanie hry (verejná, aby sme ju mohli zavola z Buttonu)
    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Vypne Pause Menu UI
        Time.timeScale = 1f;          // Vráti èas do normálu
        GameIsPaused = false;
    }

    // Metóda na pauznutie hry
    void Pause()
    {
        pauseMenuUI.SetActive(true);  // Zapne Pause Menu UI
        Time.timeScale = 0f;          // Zmrazí èas v hre
        GameIsPaused = true;
    }

    // Metóda na naèítanie inej scény pod¾a indexu
    public void LoadMenu(int sceneIndex)
    {
        // Ve¾mi DÔLEITÉ: Pred naèítaním novej scény musíme vráti èas do normálu!
        // Inak by nová scéna zostala zmrazená.
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneIndex);
    }
}