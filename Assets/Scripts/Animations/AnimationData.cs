using UnityEngine;

public abstract class AnimationData
{
    public string AnimationName { get; }
    public GameObject ObjectToAnimate { get; }

    public AnimationData(string animationName, GameObject objectToAnimate)
    {
        AnimationName = animationName;
        ObjectToAnimate = objectToAnimate;
    }
}
