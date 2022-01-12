﻿

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;

public class Bird : MonoBehaviour {

    private const float JUMP_AMOUNT = 90f;

    private static Bird instance;

    public static Bird GetInstance() {
        return instance;
    }

    public event EventHandler OnDied;
    public event EventHandler OnStartedPlaying;

    private Rigidbody2D birdRigidbody2D;
    private State state;

    private enum State {
        WaitingToStart,
        Playing,
        Dead
    }

    private void Awake() {
        instance = this;
        birdRigidbody2D = GetComponent<Rigidbody2D>();
        birdRigidbody2D.bodyType = RigidbodyType2D.Static;
        state = State.WaitingToStart;
    }

    private void Update() {
        switch (state) {
            default:
            case State.WaitingToStart:
                if (TestInput()) {
                    // Start playing
                    state = State.Playing;
                    birdRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                    Jump();
                    if (OnStartedPlaying != null) OnStartedPlaying(this, EventArgs.Empty);
                }
                break;
            case State.Playing:
                if (TestInput()) {
                    Jump();
                }

                // Rotate bird as it jumps and falls
                transform.eulerAngles = new Vector3(0, 0, birdRigidbody2D.velocity.y * .15f);

                if (birdRigidbody2D.position.y > 50)
                {
                    birdRigidbody2D.bodyType = RigidbodyType2D.Static;
                    //        SoundManager.PlaySound(SoundManager.Sound.Lose);
                    SoundManager.PlaySound(SoundManager.Sound.AlternateLose);
                    if (OnDied != null) OnDied(this, EventArgs.Empty);
                    state = State.Dead;
                }
            break;
        case State.Dead:
            break;
        }
    }

    private bool TestInput() {
        return 
            Input.GetKeyDown(KeyCode.Space) || 
            Input.GetMouseButtonDown(0) ||
            Input.touchCount > 0;
    }

    private void Jump() {
        birdRigidbody2D.velocity = Vector2.up * JUMP_AMOUNT;
        SoundManager.PlaySound(SoundManager.Sound.BirdJump);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        birdRigidbody2D.bodyType = RigidbodyType2D.Static;
//        SoundManager.PlaySound(SoundManager.Sound.Lose);
        SoundManager.PlaySound(SoundManager.Sound.AlternateLose);
        if (OnDied != null) OnDied(this, EventArgs.Empty);
    }

}
