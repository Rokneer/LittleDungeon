using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHandler : MonoBehaviour
{
    public UnityEvent OnAnimationEventTriggered;

    public void TriggerAnimationEvent() => OnAnimationEventTriggered?.Invoke();
}
