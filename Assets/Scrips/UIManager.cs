using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Timer & Carrot Count")]
    [SerializeField] private Text timerText;
    [SerializeField] private Text carrotCountText;

    private float timer;
    private int carrotCount;

    [Header("UI Components")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject playUI;
    [SerializeField] private GameObject ResultUI;
    
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Khoi;

    [Header("Buttons")]
    [SerializeField] private Button[] buttons; 

    [Header("Results UI")]
    [SerializeField] private Text currentCarrotText;  // Hiển thị Carrot mới nhất
    [SerializeField] private Text currentTimeText;    // Hiển thị thời gian mới nhất
    [SerializeField] private Text previousCarrotText; // Hiển thị Carrot trước đó
    [SerializeField] private Text previousTimeText;   // Hiển thị thời gian trước đó
    
    //Quan li am thanh
    private bool isBackgroundMusicOn ; 
    private bool isGameSoundOn = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        // Cac su kien 
        buttons[0].onClick.AddListener(PauseGame); // Pause
        buttons[1].onClick.AddListener(ResumeGame); // Play
        buttons[2].onClick.AddListener(() => SceneManager.LoadScene("MenuScene")); // Out
        buttons[3].onClick.AddListener(() => 
        {
            Time.timeScale = 1; 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }); // Again
        buttons[4].onClick.AddListener(ConvertMusicBackground); // chuyen doi sang StopMusicBackground
        buttons[5].onClick.AddListener(ConvertMusicGame); // chuyen doi sang StopMusicGame
        buttons[6].onClick.AddListener(ToggleBackgroundMusic); // StopMusicBackground
        buttons[7].onClick.AddListener(ToggleGameSound); // StopMusicGame
        buttons[8].onClick.AddListener(() => SceneManager.LoadScene("MenuScene")); // Out
        buttons[9].onClick.AddListener(() => 
        {
            Time.timeScale = 1; 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }); // Again
        Player.SetActive(false);
        Khoi.SetActive(false);
       XuatHien();
    }

    private void Update()
    {
        UpdateTimer();
    }

    private void XuatHien()
    {
        StartCoroutine(XuatHienPlayer());
    }

    private IEnumerator XuatHienPlayer()
    {
        Khoi.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Khoi.SetActive(false);
        Player.SetActive(true);
    }
    private void UpdateTimer()
    {
        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        int milliseconds = Mathf.FloorToInt((timer % 1) * 100);
        timerText.text = $"{minutes:0}:{seconds:00}:{milliseconds:00}";
    }

    public void AddCarrot()
    {
        carrotCount++;
        carrotCountText.text = carrotCount.ToString();
    }
    public void AddGoldCarrot()
    {
        carrotCount += 3;
        carrotCountText.text = carrotCount.ToString();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        AudioManager.instance.audioBackGround.Pause();
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        playUI.SetActive(true);
        AudioManager.instance.audioBackGround.UnPause();
    }

    private void ConvertMusicBackground()
    {
        buttons[4].gameObject.SetActive(false);
        buttons[6].gameObject.SetActive(true);
        isBackgroundMusicOn = false;
        AudioManager.instance.ToggleAudio(AudioManager.instance.audioBackGround, false);
    }
    private void ConvertMusicGame()
    {
        buttons[5].gameObject.SetActive(false);
        buttons[7].gameObject.SetActive(true);
        isGameSoundOn = false;
        AudioManager.instance.ToggleAudio(AudioManager.instance.audioClip, false);
        
    }
    private void ToggleBackgroundMusic()
    {
        buttons[4].gameObject.SetActive(true);
        buttons[6].gameObject.SetActive(false);
        isBackgroundMusicOn = true;
        AudioManager.instance.ToggleAudio(AudioManager.instance.audioBackGround, true);
    }

    private void ToggleGameSound()
    {
        buttons[5].gameObject.SetActive(true);
        buttons[7].gameObject.SetActive(false);
        isGameSoundOn = true;
        AudioManager.instance.ToggleAudio(AudioManager.instance.audioClip, true);
    }
    public void SaveGameResult()
    {
        GameResult result = new GameResult();
    
        // Kiểm tra nếu đã có dữ liệu trước đó
        if (File.Exists(Application.persistentDataPath + "/GameResult.json"))
        {
            string jsonData = File.ReadAllText(Application.persistentDataPath + "/GameResult.json");
            GameResult previousResult = JsonUtility.FromJson<GameResult>(jsonData);
        
            result.previousCarrotCount = previousResult.currentCarrotCount;
            result.previousTime = previousResult.currentTime;
        }
    
        // Cập nhật kết quả hiện tại
        result.currentCarrotCount = carrotCount;
        result.currentTime = timer;

        // Ghi vào file JSON
        string json = JsonUtility.ToJson(result, true);
        File.WriteAllText(Application.persistentDataPath + "/GameResult.json", json);
        
        
    }

    public void ShowResults()
    {
        if (File.Exists(Application.persistentDataPath + "/GameResult.json"))
        {
            string jsonData = File.ReadAllText(Application.persistentDataPath + "/GameResult.json");
            GameResult result = JsonUtility.FromJson<GameResult>(jsonData);
        
            previousCarrotText.text = result.previousCarrotCount.ToString();
            previousTimeText.text = $"{result.previousTime:0:00.00}s";
            currentCarrotText.text = result.currentCarrotCount.ToString();
            currentTimeText.text = $"{result.currentTime:0:00.00}s";
        }
        ResultUI.SetActive(true);
        playUI.SetActive(false);
        AudioManager.instance.PlayResultBackgroundAudio();
        AudioManager.instance.ToggleAudio(AudioManager.instance.audioClip, false);
    }
    
}
