using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour {
    private GameObject filter;
    private Button buttonPlay;
    private Button buttonQuit;
    //private Button mainMenuButton;

    public void Initialize() {
        filter = GameObject.Find("Filter");
        buttonPlay = GameObject.Find("Button_play").GetComponent<Button>();
        buttonQuit = GameObject.Find("Button_quit").GetComponent<Button>();
    }

    public void RunPlayUI() {
        buttonPlay.gameObject.SetActive(false);
        buttonQuit.gameObject.SetActive(false);
        filter.SetActive(false);
    }

    public void RunMainMenu() {
        buttonPlay.gameObject.SetActive(true);
        buttonQuit.gameObject.SetActive(true);
        filter.SetActive(true);
    }
}
