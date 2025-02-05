using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EffectItemIcon : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _leftToCancelText;

    public void Show(int left)
    {
        gameObject.SetActive(true);
        _leftToCancelText.SetText(left.ToString());
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    //public void UpdateStatus(BaseAction action)
    //{
    //    if (action.LeftToCancel == 0)
    //        gameObject.SetActive(false);
    //    else
    //        gameObject.SetActive(true);

    //    _leftToCancelText.SetText(action.LeftToCancel.ToString());
    //}


}
