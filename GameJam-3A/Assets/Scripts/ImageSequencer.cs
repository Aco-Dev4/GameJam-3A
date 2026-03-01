using UnityEngine;
using UnityEngine.UI; // Pre RawImage
using UnityEngine.SceneManagement; // Pre prepínanie scén

public class ImageSequencer : MonoBehaviour
{
    [Header("Nastavenia obrázkov")]
    public RawImage[] storyImages; // Tu v Inspectore vlož tých 5 obrázkov
    public int sceneIndexToLoad;   // Index scény, ktorá sa naèíta po poslednom kliknutí

    private int currentIndex = 0;

    void Start()
    {
        // Na zaèiatku vypneme všetky obrázky, aby sme mali istotu
        foreach (RawImage img in storyImages)
        {
            if (img != null) img.gameObject.SetActive(false);
        }

        // Zapneme iba prvý obrázok v poradí
        ShowImage(0);
    }

    void Update()
    {
        // Input.anyKeyDown zachytí kliknutie myšou aj stlaèenie akejko¾vek klávesy
        if (Input.anyKeyDown)
        {
            ShowNextImage();
        }
    }

    void ShowNextImage()
    {
        // Vypneme aktuálny obrázok
        if (currentIndex < storyImages.Length)
        {
            storyImages[currentIndex].gameObject.SetActive(false);
        }

        currentIndex++;

        // Skontrolujeme, èi sme už na konci zoznamu
        if (currentIndex < storyImages.Length)
        {
            // Ak sú ešte ïalšie obrázky, zapneme ïalší
            ShowImage(currentIndex);
        }
        else
        {
            // Ak sme odklikli posledný, prepneme scénu
            LoadNextScene();
        }
    }

    void ShowImage(int index)
    {
        if (storyImages[index] != null)
        {
            storyImages[index].gameObject.SetActive(true);
        }
    }

    void LoadNextScene()
    {
        Debug.Log("Sekvencia ukonèená, naèítavam scénu: " + sceneIndexToLoad);
        SceneManager.LoadScene(sceneIndexToLoad);
    }
}