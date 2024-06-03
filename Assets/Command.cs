using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public abstract void Execute(Animator anim, bool forward);
}

public class Advance : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        if(forward)
            anim.SetTrigger("isWalking");
        else
            anim.SetTrigger("isWalkingR");
    }
}

public class TurnLeft : Command
{
    public float turnSpeed = 0.08f; // Adjust the turn speed as necessary

    public override void Execute(Animator anim, bool forward)
    {
        if (forward)
        {
            RotateCharacter(anim, -6f, turnSpeed);
            anim.SetTrigger("isWalking");
        }
    }

    private void RotateCharacter(Animator anim, float angle, float speed)
    {
        Quaternion initialRotation = anim.transform.rotation;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0, angle, 0);
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * speed;
            anim.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime);
        }

        anim.transform.rotation = targetRotation;
    }
}

public class TurnRight : Command
{
    public float turnSpeed = 0.08f; // Adjust the turn speed as necessary, faster the turn speed the smaller degree of rotation

    public override void Execute(Animator anim, bool forward)
    {
        if (forward)
        {
            RotateCharacter(anim, 6f, turnSpeed);
            anim.SetTrigger("isWalking");
        }
    }

    private void RotateCharacter(Animator anim, float angle, float speed)
    {
        Quaternion initialRotation = anim.transform.rotation;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0, angle, 0);
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * speed;
            anim.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime);
        }

        anim.transform.rotation = targetRotation;
    }
}
public class Jump: Command 
{
    public override void Execute(Animator anim, bool forward)
    {
        if (forward)
            anim.SetTrigger("isJumping");
        else
            anim.SetTrigger("isJumpingR");
    }
}

public class Kick : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        if (forward)
            anim.SetTrigger("isKicking");
        else
            anim.SetTrigger("isKickingR");
    }
}

public class Punch : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        if (forward)
            anim.SetTrigger("isPunching");
        else
            anim.SetTrigger("isPunchingR");
    }
}

public class Retreat : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        if (forward)
            anim.SetTrigger("isWalkingR");
        else
            anim.SetTrigger("isWalking");
    }
}


public class DoNothing: Command 
{
    public override void Execute(Animator anim, bool forward)
    {
      
    }
}


