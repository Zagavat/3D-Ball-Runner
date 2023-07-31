using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class Screen : MonoBehaviour
{
    [SerializeField] protected CanvasGroup CanvasGroup;
    [SerializeField] protected Button StartButton;
    [SerializeField] protected Button ExitButton;

    private void OnEnable()
    {
        StartButton.onClick.AddListener(OnStartButtonClick);
        ExitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        StartButton.onClick.RemoveListener(OnStartButtonClick);
        ExitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    protected abstract void OnStartButtonClick();

    protected abstract void OnExitButtonClick();

    public abstract void Open();

    public abstract void Close();
}
