using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Gameplay : MonoBehaviour {

    public GameObject fxPrefab;
    public GameObject racketPrefab;
    public GameObject aiRacketPrefab;
    public GameObject ball;

    // UI
    public Text[] scoreLabels;

    public Transform playerConstraintHolder;
    public Transform aiConstraintHolder;
    public Transform fieldMarkerHolder;
    public Transform goalCornerHolder;

    // 0=player racket, 1=cpu racket
    private int finishScore = 9;
    private GameObject[] rackets = new GameObject[2];
    private InputManager inputManager;
    private Ball ballScript;

    public void Init() {
        inputManager = GetComponent<InputManager>();

        rackets[0] = Instantiate(racketPrefab);
        rackets[1] = Instantiate(aiRacketPrefab);

        ball = Instantiate(ball);
        ballScript = ball.GetComponent<Ball>();

        rackets[0].transform.position = new Vector3(0, -5.5f, 0);
        rackets[0].tag = "Racket_Player";

        rackets[1].transform.position = new Vector3(0, +5.5f, 0);
        rackets[1].tag = "Racket_CPU";
        ball.transform.position = new Vector3(0, 0, 0);

        inputManager.Init(rackets[0], playerConstraintHolder);
        InitGame();
    }

    private void RunAI() {
        rackets[1].GetComponent<AI>().Initialize(ball.GetComponent<Rigidbody2D>(), fieldMarkerHolder, aiConstraintHolder);
    }

    private List<ParticleSystem> CreateFxPool() {
        List<ParticleSystem> fxPool = new List<ParticleSystem>();
        GameObject fxHolder = new GameObject("FX Holder");
        for(int i = 0; i < 15; i++) {
            fxPool.Add(Instantiate(fxPrefab, fxHolder.transform).GetComponent<ParticleSystem>());
            fxPool[i].gameObject.SetActive(false);
        }

        return fxPool;
    }

    public void InitGame() {
        ballScript.StartBall(CreateFxPool(), fieldMarkerHolder, goalCornerHolder, this);
        scoreLabels[0].gameObject.SetActive(true);
        scoreLabels[1].gameObject.SetActive(true);
        RunAI();
    }

    public void Score(bool didPlayerScore, bool reset = false) {
        if (!reset) {
            if (didPlayerScore) {
                int nScore = Int32.Parse(scoreLabels[0].text) + 1;
                scoreLabels[0].text = nScore.ToString();
            }

            else {
                int nScore = Int32.Parse(scoreLabels[1].text) + 1;
                scoreLabels[1].text = nScore.ToString();
            }

            CheckScoreProgress();
        }
        else {
            scoreLabels[0].text = "0";
            scoreLabels[1].text = "0";
        }
    }

    private void CheckScoreProgress() {
        if(Int32.Parse(scoreLabels[0].text) == finishScore) {
            // Player wins
            print("player wins");
            Score(true, true);
        }

        else if (Int32.Parse(scoreLabels[1].text) == finishScore) {
            print("ai wins");
            Score(false, true);
        }
    }
}
