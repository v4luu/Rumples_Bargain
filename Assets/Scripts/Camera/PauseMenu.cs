using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;

    public GameObject pauseCanvas;
    public GameObject hudCanvas;
    public Slider sliderMusica;
    public Slider sliderEfectos;

    private bool isPaused = false;

    // volumen de efectos separado del de música
    public static float effectsVolume = 1f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        pauseCanvas.SetActive(false);

        // inicializa sliders con volumen actual
        if (sliderMusica != null && AudioManager.Instance != null)
        {
            sliderMusica.value = AudioManager.Instance.musicSource.volume;
            sliderMusica.onValueChanged.AddListener(OnMusicaChanged);
        }

        if (sliderEfectos != null)
        {
            sliderEfectos.value = effectsVolume;
            sliderEfectos.onValueChanged.AddListener(OnEfectosChanged);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseCanvas.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void Resume()
    {
        isPaused = false;
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayMenuMusic();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    void OnMusicaChanged(float value)
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetVolume(value);
    }

    void OnEfectosChanged(float value)
    {
        effectsVolume = value;
    }
}
