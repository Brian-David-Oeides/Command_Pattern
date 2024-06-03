using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameObject actor; // calls the game object
    Animator anim;
    Command keySpace, keyK, keyP, upArrow, downArrow, turnLeft, turnRight;
    List<Command> oldCommands = new List<Command>();

    Coroutine replayCoroutine;
    bool shouldStartReplay;
    bool isReplaying;

    // Start is called before the first frame update
    void Start()
    {
        keySpace = new Jump(); // Keyword 'new' creates instance of original Command  
        keyK = new Kick();
        keyP = new Punch();
        upArrow = new Advance();
        downArrow = new Retreat();
        turnLeft = new TurnLeft(); // Initialize new commands
        turnRight = new TurnRight(); // Initialize new commands
        anim = actor.GetComponent<Animator>();
        Camera.main.GetComponent<CameraFollow360>().player = actor.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReplaying)
            HandleInput();
        StartReplay();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            keySpace.Execute(anim, true);
            oldCommands.Add(keySpace);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            keyK.Execute(anim, true);
            oldCommands.Add(keyK);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            keyP.Execute(anim, true);
            oldCommands.Add(keyP);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            upArrow.Execute(anim, true);
            oldCommands.Add(upArrow);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            downArrow.Execute(anim, true);
            oldCommands.Add(downArrow); // Record list of oldCommands
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) // Handle turn left
        {
            turnLeft.Execute(anim, true);
            oldCommands.Add(turnLeft); // Record list of oldCommands
        }
        else if (Input.GetKey(KeyCode.RightArrow)) // Handle turn right
        {
            turnRight.Execute(anim, true);
            oldCommands.Add(turnRight); // Record list of oldCommands
        }

        if (Input.GetKeyDown(KeyCode.R))
            shouldStartReplay = true;

        if (Input.GetKeyDown(KeyCode.Z))
            UndoLastCommand();

    }

    void UndoLastCommand()
    {
        if (oldCommands.Count > 0)
        {
            Command c = oldCommands[oldCommands.Count - 1];
            c.Execute(anim, false);
            oldCommands.RemoveAt(oldCommands.Count - 1); 
        }
    }

    void StartReplay()
    {
        if(shouldStartReplay && oldCommands.Count > 0)
        {
            shouldStartReplay = false;
            if(replayCoroutine != null)
            {
                StopCoroutine(replayCoroutine);
            }
            replayCoroutine = StartCoroutine(ReplayCommands());
        }
    }

    IEnumerator ReplayCommands()
    {
        isReplaying = true;

        for(int i = 0; i < oldCommands.Count; i++)
        {
            oldCommands[i].Execute(anim, true);
            yield return new WaitForSeconds(0f);
        }

        isReplaying = false;
    }
}
