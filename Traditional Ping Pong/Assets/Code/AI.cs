using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    public float maxSpeed = 2;
    public float agility;
    public float strength;

    private BorderMarkers fieldMarkers;
    private VectorConstraint aiConstraints;

    private Rigidbody2D racketRB;
    private Rigidbody2D ballRB;

    private bool initialized = false;
    private Vector2 targetPos;

    public void Initialize(Rigidbody2D ball, Transform fieldMarkersHolder, Transform aiConstraintsHolder) {
        initialized = true;
        racketRB = GetComponent<Rigidbody2D>();
        ballRB = ball;

        // Set up field markers
        fieldMarkers = new BorderMarkers(fieldMarkersHolder);

        // Set up constraint markers
        aiConstraints = new VectorConstraint(aiConstraintsHolder);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            ballRB.velocity = Vector2.zero;
        }
    }

    private void FixedUpdate() {
        if (initialized) {
            float moveSpeed = 0;
            if(ballRB.position.y < fieldMarkers.left.y) {
                moveSpeed = maxSpeed * Random.Range(.3f, .6f);
                targetPos = new Vector2(Mathf.Clamp(ballRB.position.x, aiConstraints.minX, aiConstraints.maxX), aiConstraints.maxY);
            }
            else {
                // if the ball is below the ai racket
                if(ballRB.position.y <= racketRB.transform.position.y) {
                    moveSpeed = maxSpeed * Random.Range(.5f, 1);
                    targetPos = new Vector2(Mathf.Clamp(ballRB.position.x, aiConstraints.minX, aiConstraints.maxX),
                                            Mathf.Clamp(ballRB.position.y, aiConstraints.minY, aiConstraints.maxY));
                }

                else {
                    moveSpeed = maxSpeed;
                    targetPos = fieldMarkers.top;
                }
            }

            racketRB.MovePosition(Vector2.MoveTowards(racketRB.position, targetPos, moveSpeed * Time.deltaTime));
        }        
    }


}
