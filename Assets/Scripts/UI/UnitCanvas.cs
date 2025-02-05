using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitCanvas : MonoBehaviour
{
    [SerializeField] private EffectItemIcon _healthEffectIcon;
    [SerializeField] private EffectItemIcon _shieldEffectIcon;
    [SerializeField] private EffectItemIcon _fireballEffectIcon;

    [SerializeField] private Slider _healthBar;
    [SerializeField] private TextMeshProUGUI _healthTextValue;


    public void UpdateHealth(int healthValue, int maxHealthValue)
    {
        _healthBar.value = healthValue / (float)maxHealthValue;
        _healthTextValue.SetText(healthValue.ToString());
    }

    public void UpdateIconStatusForEffect(BaseAction action)
    {
        EffectItemIcon icon = GetIconWithEffectByAction(action);

        if (icon == null)
            return;

        if (action.IsExpired)
            icon.Hide();
        else
        {
            icon.Show(action.LeftToCancel);
        }
    }


    private EffectItemIcon GetIconWithEffectByAction(BaseAction action)
    {
        switch (action.Type)
        {
            case ActionType.Shield:
                return _shieldEffectIcon;

            case ActionType.Health:
                return _healthEffectIcon;

            case ActionType.Fireball:
                return _fireballEffectIcon;

            default:
                return null;
        }
    }
}
