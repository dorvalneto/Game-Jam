using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBird : MonoBehaviour
{
    public float velocity = 1;
    private Rigidbody2D rb;
    public FlappyBirdManager gameManager;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            rb.velocity = Vector2.up * velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        gameManager.GameOver();
    }

}
