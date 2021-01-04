using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {


    [Range(10, 25)]
    public float racketSpeed;
    private GameObject racket;
    private bool initialized = false;
    //private Rigidbody2D ball;
    private Rigidbody2D racketRB;
    private VectorConstraint racketConstraint;

    private Vector3 mouseStartPos;
    private Vector3 racketStartPos;

    public void Init(GameObject racket, Transform playerConstraintHolder) {
        //this.ball = ball;
        this.racket = racket;
        racketRB = racket.GetComponent<Rigidbody2D>();
        // Set up racket constraints
        racketConstraint = new VectorConstraint(playerConstraintHolder);
        initialized = true;
    }

    private void Update() {
        if (initialized) {
            // Input
            if (Input.GetMouseButtonDown(0)) {
                racketStartPos = racket.transform.position;
                mouseStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0)) {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 nPos = racketStartPos + (mousePos - mouseStartPos);
                nPos = new Vector3(Mathf.Clamp(nPos.x, racketConstraint.minX, racketConstraint.maxX),
                                   Mathf.Clamp(nPos.y, racketConstraint.minY, racketConstraint.maxY));

                racketRB.MovePosition(Vector3.Lerp(racket.transform.position, nPos, Time.deltaTime * racketSpeed));
            }
        }
    }
}


