﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Menu: MonoBehaviour
{
    public GameObject titleObject; // GameObject chứa hình ảnh hoặc chữ tên game "Poor Bunny"
    public Button playButton;
    public Button settingsButton;
    public Button selectBunnyButton;
    public Button exitButton;
    void Start()
    {
        // Hiệu ứng di chuyển GameObject title
        titleObject.transform.DOMoveY(titleObject.transform.position.y + 20, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);

        // Gán sự kiện cho các nút
        playButton.onClick.AddListener(PlayGame);
        settingsButton.onClick.AddListener(OpenSettings);
        selectBunnyButton.onClick.AddListener(SelectBunny);
        exitButton.onClick.AddListener(ExitGame);
    }

    void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void OpenSettings()
    {
        Debug.Log("Mở cài đặt");
        // Thêm code để mở menu cài đặt nếu có
    }

    void SelectBunny()
    {
        Debug.Log("Chọn Thỏ");
        // Thêm code để mở giao diện chọn nhân vật
    }
    
    void ExitGame()
    {
        Debug.Log("Thoát game!");
        Application.Quit(); // 🆕 Thoát game
    }
}