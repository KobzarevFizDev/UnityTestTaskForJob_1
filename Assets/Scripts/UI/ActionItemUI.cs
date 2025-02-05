using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using TMPro;

public class ActionItemUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _lockImage;
    [SerializeField] private Image _unlockProgressBar;
    [SerializeField] private TextMeshProUGUI _unlockProgressText;

    private bool _isLock;

    public event Action<ActionType> Clicked;
    public ActionType Type => _actionData.Type;

    [SerializeField] private ActionData _actionData;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isLock)
            return;

        Clicked?.Invoke(_actionData.Type);
    }

    public void UpdateState(int leftToUnlock, int unlockCooldown)
    {
        if (leftToUnlock > 0)
        {
            _lockImage.gameObject.SetActive(true);
            _unlockProgressBar.gameObject.SetActive(true);
            _unlockProgressText.gameObject.SetActive(true);
            _isLock = true;
        }
        else
        {
            _lockImage.gameObject.SetActive(false);
            _unlockProgressBar.gameObject.SetActive(false);
            _unlockProgressText.gameObject.SetActive(false);
            _isLock = false;
        }
        _unlockProgressText.SetText(leftToUnlock.ToString());
        _unlockProgressBar.fillAmount = leftToUnlock / (float)unlockCooldown;
    }

}
