using System.Collections;
using System.Collections.Generic;
using UniTools.Reactive;
using UnityEngine;
public enum CharacterState
{
    Fly,
    Run
}
public abstract class CharacterBaseView : ConnectableMonoBehaviour
{
    [field: SerializeField] protected virtual Reactive<float> currentSpeed { get; set; } = new Reactive<float>();
    [field: SerializeField] public virtual float minSpeed { get; protected set; } = 5;
    [field: SerializeField] public virtual float maxSpeed { get; protected set; } = 20;
    [field: SerializeField] public virtual float startSpeed { get; protected set; } = 20;
    public virtual ReactiveList<AbilityInstance> currentAbilities { get; set; } = new ReactiveList<AbilityInstance>();
    public virtual Reactive<CharacterState> state { get; protected set; } = new Reactive<CharacterState>(CharacterState.Run);
    public IReactive<float> CurrentSpeed => currentSpeed;

    public virtual void SetSpeed(float speed)
    {
        currentSpeed.value -= speed;
        if (currentSpeed.value < minSpeed) currentSpeed.value = minSpeed;
        if (currentSpeed.value > maxSpeed) currentSpeed.value = maxSpeed;
    }
    public virtual void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
    public abstract void MoveForward();
    public abstract void Init(Vector3 moveDirection);
    public abstract void SetDirection(Vector3 moveDirection);
}
