using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    // 0 -> play, 1-> quit
    private GameObject bgFilter;
    private GameObject[] buttons = new GameObject[2];
    private Gameplay gameplay;
    private UIManager uiManager;

    // Start is called before the first frame update
    void Start() {
        uiManager = GetComponent<UIManager>();
        gameplay = GetComponent<Gameplay>();

        uiManager.Initialize();
    }

    public void Play() {
        uiManager.RunPlayUI();
        gameplay.Init();
    }

    public void GameOver(bool victory) {

    }

    public void Quit() {
        Application.Quit();
    }
}
