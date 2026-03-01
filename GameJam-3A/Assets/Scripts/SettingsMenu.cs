using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public TMPro.TMP_Dropdown qualityDropdown;

    private const string VolumePref = "UserVolume";
    private const string QualityPref = "UserQuality";

    void Start()
    {
        // 1. NaËÌtanie uloûenej hlasitosti (ak neexistuje, daj 0.75)
        float savedVolume = PlayerPrefs.GetFloat(VolumePref, 0.75f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);

        // 2. NaËÌtanie uloûenej grafiky (ak neexistuje, daj 2 - Medium/High)
        int savedQuality = PlayerPrefs.GetInt(QualityPref, 2);
        qualityDropdown.value = savedQuality;
        SetQuality(savedQuality);
    }

    public void SetVolume(float volume)
    {
        // V˝poËet decibelov: log10(0.0001) * 20 = -80dB (ticho)
        // log10(1) * 20 = 0dB (pln· hlasitosù)
        float decibels = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;

        audioMixer.SetFloat("MasterVolume", decibels);
        PlayerPrefs.SetFloat(VolumePref, volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt(QualityPref, qualityIndex);
    }
}