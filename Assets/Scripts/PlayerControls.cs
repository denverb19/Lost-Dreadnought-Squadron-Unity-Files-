//using System.Numerics;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.75f;
    [SerializeField] float boostSpeed = 1.5f;
    [SerializeField] float evadeTime = 1f;
    [SerializeField] float leftPadding = 0.45f;
    [SerializeField] float rightPadding = 0.45f;
    [SerializeField] float topPadding = 0.95f;
    [SerializeField] float bottomPadding = 2.4f;
    const int EVASION = 3;
    const int BOOSTON = 4;
    const int BOOSTOFF = 5;
    private Vector2 localMoveVector;
    private Vector2 minBounds;
    private Vector2 maxBounds;
    private bool evadeInProgress = false;
    private bool rightTurnInProgress = false;
    private bool leftTurnInProgress = false;
    private bool lateralInProgress = false;
    private bool boostInProgress = false;
    private bool isFiringLasers = false;
    Animator thisAnimator;
    Shooter thisShooter;
    AudioScript thisAudioScript;
    void Start()
    {
        thisAnimator = transform.GetChild(0).GetComponent<Animator>();
        thisShooter = GetComponent<Shooter>();
        thisAudioScript = FindObjectOfType<AudioScript>();
        InitializeBounds();
    }
    void Update()
    {
        MoveShip();
    }
    void InitializeBounds()
    {
        Camera thisMainCam = Camera.main;
        minBounds = thisMainCam.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = thisMainCam.ViewportToWorldPoint(new Vector2(1, 1));
    }
    void OnMove(InputValue passedInputValue)
    {
        localMoveVector = passedInputValue.Get<Vector2>();
    }
    void OnEvade()
    {
        ToggleEvadeOn();
    }
    void OnBoost()
    {
        ToggleBoost();
    }
    void ToggleEvadeOn()
    {
        if (evadeInProgress)
        {
            return;
        }
        evadeInProgress = true;
        thisAnimator.SetBool("isEvading", true);
        thisAudioScript.PlaySound(EVASION);
        Invoke("ToggleEvadeOff", evadeTime);
    }
    void ToggleEvadeOff()
    {
        evadeInProgress = false;
        thisAnimator.SetBool("isEvading", false);
    }
    void ToggleBoost()
    {
        if (!thisAnimator.GetBool("isBoosting"))
        {
            thisAnimator.SetBool("isBoosting", true);
            thisAudioScript.PlaySound(BOOSTON);
        }
        else
        {
            thisAnimator.SetBool("isBoosting", false);
            thisAudioScript.PlaySound(BOOSTOFF);
        }
    }
    void MoveShip()
    {
        lateralInProgress = localMoveVector.y > Mathf.Epsilon || localMoveVector.y < (-Mathf.Epsilon);
        rightTurnInProgress = localMoveVector.x > Mathf.Epsilon;
        leftTurnInProgress = localMoveVector.x < -Mathf.Epsilon;
        if (rightTurnInProgress)
        {
            thisAnimator.SetBool("isTurningRight", true);
            ExecuteMove();
        }
        else if (leftTurnInProgress)
        {
            thisAnimator.SetBool("isTurningLeft", true);
            ExecuteMove();
        }
        else if (lateralInProgress)
        {
            thisAnimator.SetBool("isMoving", true);
            thisAnimator.SetBool("isTurningRight", false);
            thisAnimator.SetBool("isTurningLeft", false);
            ExecuteMove();
        }
        else
        {
            thisAnimator.SetBool("isMoving", false);
            thisAnimator.SetBool("isTurningRight", false);
            thisAnimator.SetBool("isTurningLeft", false);
        }
    }
    void ExecuteMove()
    {
        Vector2 dxMove = new Vector2();
        boostInProgress = thisAnimator.GetBool("isBoosting");
        Vector2 updatedPosition = new Vector2();
        if (boostInProgress)
        {
            dxMove = localMoveVector * boostSpeed;
        }
        else
        {
            dxMove = localMoveVector * moveSpeed;
        }
        updatedPosition.x = Mathf.Clamp(transform.position.x + (dxMove.x * Time.deltaTime), minBounds.x + leftPadding, maxBounds.x - rightPadding);
        updatedPosition.y = Mathf.Clamp(transform.position.y + (dxMove.y * Time.deltaTime), minBounds.y + bottomPadding, maxBounds.y - topPadding);
        transform.position = updatedPosition;
    }
    void OnFireLasers(InputValue thisValue)
    {
        if (thisShooter != null)
        {
            isFiringLasers = thisValue.isPressed;
            thisShooter.isShootingLasers = thisValue.isPressed;
        }
        if (isFiringLasers)
        {
            thisAnimator.SetBool("isFiringLasers", true);
        }
        else
        {
            thisAnimator.SetBool("isFiringLasers", false);
        }      
    }
}