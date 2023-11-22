using System;
using Mirror;
using UnityEngine;

public class PlayerMotor : NetworkBehaviour {

    // Controls
    private PlayerControls controls;

    [Header("Movement")] 
    [SerializeField] private float speed = 4f;
    
    // Components
    private Rigidbody2D rb;

    private void Awake() {
        controls = new PlayerControls();
        controls.Movement.Enable();
    }

    private void Start() {
        if (!isLocalPlayer) Destroy(this);
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        Movement();
    }

    private void Movement() {
        var input = controls.Movement.Walk.ReadValue<Vector2>();
        var velocity = input * speed;
        rb.velocity = velocity;
    }

}