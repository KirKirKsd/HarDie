using System;
using System.Collections;
using Mirror;
using UnityEngine;

public class PlayerMotor : NetworkBehaviour {

    // Controls
    private PlayerControls controls;

    [Header("Movement")] 
    [SerializeField] private float speed;

    [Header("Bounce")] 
    [SerializeField] private float bounceStrength;
    [SerializeField] private float timeBounceAnimation;
    private bool isBounce;
    
    // Components
    private Rigidbody2D rb;
    private Camera mainCamera;
    private PlayerSync playerSync;
    
    private void Start() {
        if (!isLocalPlayer) {
            Destroy(this);
            return;
        }

        gameObject.tag = "I";
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        playerSync = GetComponent<PlayerSync>();
        
        controls = new PlayerControls();
        controls.Movement.Enable();
    }

    private void Update() {
        if (!isBounce) Movement();
        Look();
    }
    
    private void Movement() {
        var input = controls.Movement.Walk.ReadValue<Vector2>();
        var velocity = input * speed;
        rb.velocity = velocity;
    }
    
    private void Look() {
        Vector2 playerPosition = transform.position;
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var angle = Utility.AngleBetweenObjects(playerPosition, mousePosition);
        var rotation = Quaternion.Euler(0, 0, angle);
        playerSync.isPistolFlip = angle is < 275 and > 90;
        playerSync.pistolPivotRotation = rotation;
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            StartCoroutine(Bounce(other.gameObject));
        }
    }

    private IEnumerator Bounce(GameObject other) {
        isBounce = true;
        yield return new WaitForSeconds(0.1f);
        var angle = Utility.AngleBetweenObjects(transform.position, other.transform.position) + 180;
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        var forceVector = new Vector2(bounceStrength, 0);
        rb.velocity = rotation * forceVector;
        yield return new WaitForSeconds(timeBounceAnimation);
        isBounce = false;
    }

}