using System.Collections;
using System.Collections.Generic;
using TMPro;
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
            SceneManager.sceneLoaded += OnSceneLoad;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int difficulty;
    public float musicVolume;
    public float sfxVolume;
    [SerializeField] int mainSceneIndex = 1;
    [SerializeField] int gameOverSceneIndex = 2;
    [SerializeField] int winSceneIndex = 3;
    public string killedBy;
    public List<string> damageLogList = new List<string>();
    [SerializeField] TextMeshProUGUI damageLogDisplay;
    private AudioSource miscSoundPlayer { get => GetComponent<AudioSource>(); }
    [SerializeField] AudioClip winSFX;

    private void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == mainSceneIndex)
        {
            damageLogDisplay = GameObject.Find("Damage Log").GetComponent<TextMeshProUGUI>();
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(gameOverSceneIndex);
    }

    public void AddDamageLog(string attacker, string target, float damage)
    {
        AddLogMessage($"{attacker} hit {target} for {Mathf.RoundToInt(damage)} damage");
    }

    public void AddLogMessage(string message)
    {
        damageLogList.Add(message);
        StartCoroutine(DamageLogDisplayCountdown());
        UpdateDamageLogDisplay(); // ABSTRACTION
    }

    private IEnumerator DamageLogDisplayCountdown()
    {
        yield return new WaitForSeconds(6);
        damageLogList.RemoveAt(0);
        UpdateDamageLogDisplay();
    }

    private void UpdateDamageLogDisplay()
    {
        if (damageLogDisplay != null)
        {
            damageLogDisplay.text = string.Join('\n', damageLogList);
        }
    }

    public void WinGame()
    {
        miscSoundPlayer.clip = winSFX;
        miscSoundPlayer.Play();
        SceneManager.LoadScene(winSceneIndex);
    }
}
