using System;
using UnityEngine;
using Mirror;

public class PlayerSync : NetworkBehaviour {

    [SyncVar] [HideInInspector] public Quaternion pistolPivotRotation;
    [SyncVar] [HideInInspector] public bool isPistolFlip;

    [SerializeField] private Transform pistolPivotT;
    [SerializeField] private SpriteRenderer pistolSR;
    
    private void Update() {
        pistolPivotT.rotation = pistolPivotRotation;
        pistolSR.flipY = isPistolFlip;
    }

}