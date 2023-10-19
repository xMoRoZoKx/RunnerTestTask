using System.Collections;
using System.Collections.Generic;
using UniTools.Reactive;
using UnityEngine;

public abstract class CharacterBaseView : MonoBehaviour
{
    [field: SerializeField] protected virtual Reactive<float> currentSpeed { get; set; } = new Reactive<float>(20);
    public IReactive<float> CurrentSpeed => currentSpeed;
    [field: SerializeField] public virtual float minSpeed { get; protected set; } = 5;
    public virtual bool isFly { get; protected set; } = false;
    public virtual float AddSpeedAndGetFactualOffset(float speed)
    {
        var oldSpeed = currentSpeed.value;

        currentSpeed.value -= speed;
        if (currentSpeed.value < minSpeed) currentSpeed.value = minSpeed;

        return currentSpeed.value - oldSpeed;
    }
    public virtual void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
    public abstract void MoveForward();
    public abstract void Init(Vector3 moveDirection);
    public abstract void SetDirection(Vector3 moveDirection);
    public abstract void Fly(float flyDuration);
    public abstract void Fall();
}
