using UnityEngine;

[RequireComponent(typeof(Animation))]
public class DeathAnimation : MonoBehaviour
{
    private Animation anim;
    private readonly string animName = "Death";

    [SerializeField]
    protected AnimationClip deathAnimation;

    void Awake()
    {
        anim = GetComponent<Animation>();

        if (deathAnimation != null)
        {
            anim.AddClip(deathAnimation, animName);
        }
    }

    public void PlayDeathAnimation()
    {
        anim.Play();
    }
}
