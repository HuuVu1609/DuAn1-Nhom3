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
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI carrotCountText;

    private float timer;
    private int carrotCount;

    [Header("UI Components")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject playUI;
    [SerializeField] private GameObject ResultUI;
    [SerializeField] private GameObject TrapUI;

    [Header("Buttons")]
    [SerializeField] private Button[] buttons; 

    [Header("Results UI")]
    [SerializeField] private TextMeshProUGUI currentCarrotText;  // Hiển thị Carrot mới nhất
    [SerializeField] private TextMeshProUGUI currentTimeText;    // Hiển thị thời gian mới nhất
    [SerializeField] private TextMeshProUGUI previousCarrotText; // Hiển thị Carrot trước đó
    [SerializeField] private TextMeshProUGUI previousTimeText;   // Hiển thị thời gian trước đó
    
    //Quan li am thanh
    private bool isBackgroundMusicOn = true; 
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
        buttons[2].onClick.AddListener(() => SceneManager.LoadScene("Menu")); // Out
        buttons[3].onClick.AddListener(() => 
        {
            Time.timeScale = 1; 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }); // Again
        buttons[4].onClick.AddListener(ConvertMusicBackground); // chuyen doi sang StopMusicBackground
        buttons[5].onClick.AddListener(ConvertMusicGame); // chuyen doi sang StopMusicGame
        buttons[6].onClick.AddListener(ToggleBackgroundMusic); // StopMusicBackground
        buttons[7].onClick.AddListener(ToggleGameSound); // StopMusicGame
        buttons[8].onClick.AddListener(() => SceneManager.LoadScene("Menu")); // Out
        buttons[9].onClick.AddListener(() => 
        {
            Time.timeScale = 1; 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }); // Again
        LoadResultsFromFile(); // Tai ket qua 
    }

    private void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        int milliseconds = Mathf.FloorToInt((timer % 1) * 100);
        timerText.text = $"{minutes:00}:{seconds:00}:{milliseconds:00}";
    }

    public void AddCarrot()
    {
        carrotCount++;
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
    public void SaveResultsToFile()
    {
        string path = Application.dataPath + "/results.txt";

        // Kiểm tra nếu đã có thành tích trước đó
        string previousCarrot = "0";
        string previousTime = "0:00:00";
        if (File.Exists(path))
        {
            string[] results = File.ReadAllLines(path);
            previousCarrot = results.Length > 0 ? results[0].Replace("CarrotCount:", "").Trim() : "0";

            // Định dạng thời gian trước đó
            previousTime = results.Length > 1 ? results[1].Replace("TimePlayed:", "").Trim() : "0:00:00";
        }

        // Định dạng thời gian chơi mới nhất thành 0:00:00
        int hours = Mathf.FloorToInt(timer / 3600);
        int minutes = Mathf.FloorToInt((timer % 3600) / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        string currentTimeFormatted = $"{hours:0}:{minutes:00}:{seconds:00}";

        // Ghi thành tích mới
        string currentCarrot = $"CarrotCount: {carrotCount}";
        string currentTime = $"TimePlayed: {currentTimeFormatted}";
        File.WriteAllText(path, currentCarrot + "\n" + currentTime);

        // Hiển thị thành tích mới nhất
        currentCarrotText.text = $"{carrotCount}";
        currentTimeText.text = $"{currentTimeFormatted}";

        // Hiển thị thành tích trước đó
        previousCarrotText.text = $"{previousCarrot}";
        previousTimeText.text = $"{previousTime}";

        Debug.Log("Đã lưu kết quả vào results.txt");
    }
    public void LoadResultsFromFile()
    {
        string path = Application.dataPath + "/results.txt";
        if (File.Exists(path))
        {
            string[] results = File.ReadAllLines(path);
            string previousCarrot = results.Length > 0 ? results[0].Replace("CarrotCount:", "").Trim() : "0";
            string previousTime = results.Length > 1 ? results[1].Replace("TimePlayed:", "").Trim() : "0:00:00";

            // Hiển thị thành tích trước đó trên ResultUI
            previousCarrotText.text = $"{previousCarrot}";
            previousTimeText.text = $"{previousTime}";
        }
        else
        {
            previousCarrotText.text = "0";
            previousTimeText.text = "0:00:00";
        }
    }

    public void ShowResults()
    {
        ResultUI.SetActive(true);
        playUI.SetActive(false);
        TrapUI.SetActive(false);
        AudioManager.instance.PlayResultBackgroundAudio();
    }
}
