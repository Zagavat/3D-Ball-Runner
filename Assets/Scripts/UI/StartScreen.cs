using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartScreen : Screen
{
    public event UnityAction StartButtonClick;
    public event UnityAction ExitButtonClick;

    public override void Close()
    {
        CanvasGroup.alpha = 0;
        StartButton.interactable = false;
        ExitButton.interactable = false;
    }

    public override void Open()
    {
        CanvasGroup.alpha = 1;
        StartButton.interactable = true;
        ExitButton.interactable = true;
    }

    protected override void OnExitButtonClick()
    {
        ExitButtonClick?.Invoke();
    }

    protected override void OnStartButtonClick()
    {
        StartButtonClick?.Invoke();
    }
}
