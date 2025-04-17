using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button LoadGame;
    [SerializeField] private GameObject loadingImage ;
    [SerializeField] private Button ThongTin;
    [SerializeField] private Button TatThongTin;
    [SerializeField] private GameObject ThongTinGame;
    public GameObject titleObject;
    public Button playButton;
    public Button exitButton;
    public Button soundToggleButton;
    
    [SerializeField] private Animator animator;
    private bool isSoundOn = true;
    private float startY;
    private bool movingUp = true;
    public float moveSpeed = 5f; // Tốc độ di chuyển lên xuống

    void Start()
    {
        startY = titleObject.transform.position.y;

        // Gán sự kiện cho các nút
        playButton.onClick.AddListener(PlayGame);
        exitButton.onClick.AddListener(ExitGame);
        soundToggleButton.onClick.AddListener(ToggleSound);

        LoadGame.onClick.AddListener(PlayGame);
        loadingImage.SetActive(false);
        ThongTin.onClick.AddListener(ThongTinTroChoi);
        TatThongTin.onClick.AddListener(TatThongTinTroChoi);
    }

    private void Update()
    {
        // Hiệu ứng di chuyển lên xuống nhẹ nhàng
        float movement = movingUp ? moveSpeed * Time.deltaTime : -moveSpeed * Time.deltaTime;
        titleObject.transform.position += new Vector3(0, movement, 0);

        // Đảo hướng khi đạt đến giới hạn
        if (titleObject.transform.position.y >= startY + 0.3f) movingUp = false;
        if (titleObject.transform.position.y <= startY) movingUp = true;
    }

    public void PlayGame()
    {
        loadingImage.SetActive(true);
        StartCoroutine(LoadLevel());

    }

    void ExitGame()
    {
        Debug.Log("Thoát game!");
        Application.Quit();
    }

    void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        AudioListener.volume = isSoundOn ? 1f : 0f;
        Debug.Log("Âm thanh hiện tại: " + (isSoundOn ? "BẬT" : "TẮT"));
    }

    private IEnumerator LoadLevel()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GamePlay" );
        animator.SetTrigger("End");
    }

    private void ThongTinTroChoi()
    {
        ThongTinGame.SetActive(true);
    }

    private void TatThongTinTroChoi()
    {
        ThongTinGame.SetActive(false);
    }
}