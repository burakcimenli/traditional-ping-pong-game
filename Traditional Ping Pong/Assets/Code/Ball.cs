using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public float maxTravelVelocity = 10;
    public float maxVelocity = 10;
    // The higher the number, the faster the speed is clamped
    public int ballVelocityClampFactor = 3;
    public Rigidbody2D rigidBody;

    private List<ParticleSystem> fxPool;
    private BorderMarkers fieldMarkers;
    private CornerMarkers cornerMarkers;
    private Gameplay gameplayScript;
    private AudioSource audioSource;
    private bool waitForScore = false;

    public void StartBall(List<ParticleSystem> fxPool, Transform fieldMarkerHolder, Transform goalCornerHolder, Gameplay gameplayScript) {
        if (maxVelocity < maxTravelVelocity)
            maxVelocity = maxTravelVelocity;

        this.fxPool = fxPool;
        audioSource = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody2D>();
        fieldMarkers = new BorderMarkers(fieldMarkerHolder);
        cornerMarkers = new CornerMarkers(goalCornerHolder);
        this.gameplayScript = gameplayScript;
    }


    private void FixedUpdate() {
        //Clamp ball velocity
        float clampRate = Mathf.Lerp(rigidBody.velocity.magnitude, maxTravelVelocity, Time.deltaTime * ballVelocityClampFactor);
        rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxVelocity);
        rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, clampRate);

        // SCORE CONDITIONS
        if (waitForScore == false) {
            if (transform.position.y > fieldMarkers.top.y) {
                StartCoroutine(ScoreTimer(true));
            }
            else if (transform.position.y < fieldMarkers.bot.y) {
                StartCoroutine(ScoreTimer(false));
            } 
        }
    }

    // Score
    private IEnumerator ScoreTimer(bool didPlayerScore, float duration = .35f) {
        waitForScore = true;
        yield return new WaitForSeconds(duration);
        gameplayScript.Score(didPlayerScore);
        transform.position = Vector2.zero;
        rigidBody.velocity = Vector2.zero;
        waitForScore = false;
    }

    private ParticleSystem GetFX() {
        foreach(ParticleSystem fx in fxPool) {
            if (!fx.gameObject.activeInHierarchy)
                return fx;
        }

        return null;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        audioSource.Play();
        ParticleSystem fx = GetFX();
        if (fx) {
            Color fxColor = Color.white;
            switch (collision.transform.name) {
                case "col-upperR":
                    fxColor = NColor.CreateColor(241, 88, 65, 255);
                    break;

                case "col-upperL":
                    fxColor = NColor.CreateColor(241, 243, 125, 255);
                    break;

                case "col-lowerL":
                    fxColor = NColor.CreateColor(150, 229, 96, 255);
                    break;

                case "col-lowerR":
                    fxColor = NColor.CreateColor(96, 138, 229, 255);
                    break;

                case "racket":
                    fxColor = NColor.CreateColor(241, 88, 65, 255);
                    break;

                case "racket_ai":
                    fxColor = NColor.CreateColor(96, 138, 229, 255);
                    break;
            }

            fx.startColor = fxColor;
            fx.transform.position = collision.GetContact(0).point;
            fx.transform.forward = collision.GetContact(0).normal;
            fx.gameObject.SetActive(true);
            fx.Play(); 
        }
    }
}
