using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : CharacterBaseView
{
    [SerializeField] private float rotationCoefficient = 10;
    [SerializeField] private Transform view;
    [SerializeField] CharacterController characterController;
    private Vector3 moveDirection;
    private readonly float flyHight = 10;
    private const float gravity = 1;
    private bool useGravity = true;


    public override void Init(Vector3 moveDirection)
    {
        currentSpeed.value = startSpeed;
        SetDirection(moveDirection);

        connections += state.Buffer((oldState, newState) =>
        {
            if (newState == CharacterState.Fly)
            {
                characterController.Move(Vector3.up * flyHight);
                useGravity = false;
            }

            if (newState == CharacterState.Run && oldState == CharacterState.Fly)
            {
                characterController.Move(Vector3.down * flyHight);
                useGravity = true;
            }
        });
    }

    public override void MoveForward()
    {
        characterController.Move(moveDirection * currentSpeed.value * Time.fixedDeltaTime);
        if (useGravity) characterController.Move(Vector3.down * gravity * Time.fixedDeltaTime);

        view?.Rotate(-currentSpeed.value * rotationCoefficient, 0, 0);
    }

    //Set route direction
    public override void SetDirection(Vector3 moveDirection)
    {
        this.moveDirection = moveDirection;
    }
    public override void SetPosition(Vector3 position)
    {
        characterController.enabled = false;
        base.SetPosition(position);
        characterController.enabled = true;
    }
    private void FixedUpdate()
    {
        MoveForward();
    }

}
