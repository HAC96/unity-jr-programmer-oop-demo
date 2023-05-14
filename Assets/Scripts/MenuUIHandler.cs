using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] TMP_Dropdown difficultySelect;
    [SerializeField] Slider sfxVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] TextMeshProUGUI killedByText;
    [SerializeField] int menuSceneIndex = 0;
    [SerializeField] int mainSceneIndex = 1;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == menuSceneIndex)
        {
            difficultySelect.value = GameManager.Instance.difficulty;
            sfxVolumeSlider.value = GameManager.Instance.sfxVolume;
            musicVolumeSlider.value = GameManager.Instance.musicVolume;
        }
        else
        {
            string killedBy = GameManager.Instance.killedBy;
            if (killedBy != "")
            {
                killedByText.text = "Unfortunately, the hero was killed by a" + ("AEIOUaeiou".Contains(killedBy[0]) ? "n " : " ") + killedBy;
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(mainSceneIndex);
    }

    public void ChangeDifficulty()
    {
        GameManager.Instance.difficulty = difficultySelect.value;
    }

    public void ChangeSFXVolume()
    {
        GameManager.Instance.sfxVolume = sfxVolumeSlider.value;
    }

    public void ChangeMusicVolume()
    {
        GameManager.Instance.musicVolume = musicVolumeSlider.value;
    }

    public void QuitGame()
    {
# if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
