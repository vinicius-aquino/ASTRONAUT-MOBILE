using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    public List<AnimatorSetup> animatorSetups;

    public enum AnimationType
    {
        IDLE,
        RUN,
        DEAD
    }

    public void play(AnimationType type, float currentSpeedFactor = 1f)
    {
        //animatorSetups.ForEach(i => { if (i.type == type) { animator.SetTrigger(i.trigger); } });

        foreach (var animation in animatorSetups)
        {
            if(animation.type == type)
            {
                animator.SetTrigger(animation.trigger);
                animator.speed = animation.speed * currentSpeedFactor;
                break;
            }
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            play(AnimationType.RUN);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            play(AnimationType.DEAD);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            play(AnimationType.IDLE);
        }
    }

}

[System.Serializable]
public class AnimatorSetup
{
    public AnimatorManager.AnimationType type;
    public string trigger;
    public float speed = 1f;
}
