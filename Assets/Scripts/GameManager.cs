using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int difficulty;
    public float musicVolume;
    public float sfxVolume;
    [SerializeField] int gameOverSceneIndex = 2;
    public string killedBy;

    public void GameOver()
    {
        SceneManager.LoadScene(gameOverSceneIndex);
    }
}
