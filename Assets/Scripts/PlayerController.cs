using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    
    private Rigidbody body;
    private float movementX;
    private float movementY;
    private int score;

    public float speed = 0;
    public TextMeshProUGUI scoreText;
    public GameObject winTextObject, greenIn, redIn;
    public Vector3 originalPosition, greenInVector, redInVector;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        score = 0;
        SetScoreText();
        winTextObject.SetActive(false);
        originalPosition = transform.position;
        greenInVector = greenIn.transform.position;
        redInVector = redIn.transform.position;
    }

    private void FixedUpdate() 
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        body.AddForce(movement * speed);
    }

    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }

    void OnTriggerEnter(Collider other) 
   {
        
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            score += 1;
            SetScoreText();
            if(GameObject.FindGameObjectsWithTag("PickUp").Length < 1) {
                winTextObject.SetActive(true);
            }
        }

        else if (other.gameObject.CompareTag("GreenOut")){
            transform.position = greenInVector;
        }

        else if (other.gameObject.CompareTag("RedOut")){
            transform.position = redInVector;
        }

        else if (other.gameObject.CompareTag("Border")){
            transform.position = originalPosition;
        }
   }

   void SetScoreText() 
   {
       scoreText.text =  "Score: " + score.ToString();
   }

}
