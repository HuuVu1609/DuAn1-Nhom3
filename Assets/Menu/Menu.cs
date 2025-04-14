using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Menu: MonoBehaviour
{
    public GameObject titleObject; // GameObject chứa hình ảnh hoặc chữ tên game "Poor Bunny"
    public Button playButton;
    public Button exitButton;
    public Button soundToggleButton;
    private bool isSoundOn = true;
    void Start()
    {
        // Hiệu ứng di chuyển GameObject title
        titleObject.transform.DOMoveY(titleObject.transform.position.y + 20, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);

        // Gán sự kiện cho các nút
        playButton.onClick.AddListener(PlayGame);
        exitButton.onClick.AddListener(ExitGame);
        soundToggleButton.onClick.AddListener(ToggleSound);
    }
    void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    void ExitGame()
    {
        Debug.Log("Thoát game!");
        Application.Quit(); // 🆕 Thoát game
    }
    void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        AudioListener.volume = isSoundOn ? 1f : 0f;
        Debug.Log("Âm thanh hiện tại: " + (isSoundOn ? "BẬT" : "TẮT"));
    }
}