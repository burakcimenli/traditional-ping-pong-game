using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDisable : MonoBehaviour {

    ParticleSystem fx;

    private void Start() {
        fx = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update() {
        if (!fx.isPlaying)
            gameObject.SetActive(false);
    }
}
