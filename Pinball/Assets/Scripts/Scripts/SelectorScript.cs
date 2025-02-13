﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorScript : MonoBehaviour
{

    private SignalHandlerScript signalHandler;
    private LinkedList<int> LLPosition = new LinkedList<int>(new int[3] {1, 2, 3});
    private LinkedListNode<int> currentPosition;

    private GameScript game;

    public SpriteRenderer fgarcadePreview;
    public SpriteRenderer pinballBetPreview;
    public static bool isFromMenu = false;

    public AudioSource audioSource;
    public AudioClip selectorMoveSound;
    public AudioClip selectorSelectSound;

    void Start()
    {
        signalHandler = Finder.GetSignalHandler();
        
        game = Finder.GetGameController();
        game.LoadRanking(false);
        currentPosition = LLPosition.First;
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (signalHandler.buttons.rightButton && !signalHandler.previousButtons.rightButton)
        {
            MoveUp();
            PlaySoundEffect(selectorMoveSound);
        }

        if (signalHandler.buttons.leftButton && !signalHandler.previousButtons.leftButton)
        {
            MoveDown();
            PlaySoundEffect(selectorMoveSound);
        }

        ShowInHover();

        if (signalHandler.buttons.select && !signalHandler.previousButtons.select)
        {
            PlaySoundEffect(selectorSelectSound);

            if (IsHoveringFGArcade)
            {
                game.LoadFGArcade();
            }
            else if (IsHoveringPinballBet)
            {
                game.LoadPinballBet();
            }
        }

        MoveSelector();
    }

    private void ShowInHover()
    {
        if (IsHoveringFGArcade)
        {
            fgarcadePreview.enabled = true;
        }
        else
        {
            fgarcadePreview.enabled = false;
        }

        if (IsHoveringRanking)
        {
            // This is necessary because the Ranking scene has it's own camera.
            GetMenuTableCamera().enabled = false;
            GetRankingTableCamera().enabled = true;
        }
        else
        {
            GetRankingTableCamera().enabled = false;
            GetMenuTableCamera().enabled = true;
        }

        if (IsHoveringPinballBet)
        {
            pinballBetPreview.enabled = true;
        }
        else
        {
            pinballBetPreview.enabled = false;
        }
    }

    private Camera GetMenuTableCamera()
    {
        return GameObject.FindGameObjectWithTag(Constants.MENU_TABLE_CAMERA).GetComponent<Camera>();
    }

    private Camera GetRankingTableCamera()
    {
        return GameObject.FindGameObjectWithTag(Constants.RANKING_TABLE_CAMERA).GetComponent<Camera>();
    }

    private bool IsHoveringRanking
    {
        get
        {
            if (currentPosition.Value == 1) return true;
            else return false;
        }
    }

    private bool IsHoveringFGArcade
    {
        get
        {
            if(currentPosition.Value == 2) return true;
            else return false;
        }
    }

    private bool IsHoveringPinballBet
    {
        get
        {
            if(currentPosition.Value == 3) return true;
            else return false;
        }
    }

    void MoveDown() => currentPosition = currentPosition.Previous ?? LLPosition.Last;

    void MoveUp() => currentPosition = currentPosition.Next ?? LLPosition.First;

    void MoveSelector()
    {
        switch(currentPosition.Value)
        {
            case 1:
                transform.position = new Vector3(0.32f, -0.3f, -29.75f);
                break;
            case 2:
                transform.position = new Vector3(0.32f, -1.3f, -29.75f);
                break;
            case 3:
                transform.position = new Vector3(0.32f, -2.05f, -29.75f);
                break;
            default:
                break;       
        }
    }

    void PlaySoundEffect(AudioClip audioToPlay)
    {
        audioSource.Stop();
        audioSource.clip = audioToPlay;
        audioSource.Play();
    }
}
