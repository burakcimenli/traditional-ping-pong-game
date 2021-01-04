using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeLights : MonoBehaviour {
    SpriteRenderer glowingEdge;
    bool currentlyTurningOn;
    bool willBeTurnedOff;

    void Start() {
        glowingEdge = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.transform.tag == "Ball") {
            StopAllCoroutines();
            StartCoroutine(SwitchLights(1));
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if(collision.transform.tag == "Ball") {
            if (!currentlyTurningOn)
                StartCoroutine(SwitchLights(0));
            else
                willBeTurnedOff = true;
        }
    }

    private IEnumerator SwitchLights(int value) {
        if(value == 1)
            currentlyTurningOn = true;

        Color from = glowingEdge.color;
        Color to = new Color(1, 1, 1, value);
        float t = 0;
        float duration = .1f;
        while(t < duration) {
            t += Time.deltaTime;
            glowingEdge.color = Color.Lerp(from, to, t / duration);
            yield return null;
        }

        if (willBeTurnedOff) {
            StartCoroutine(SwitchLights(0));
            willBeTurnedOff = false;
        }
        currentlyTurningOn = false;
    }
}
