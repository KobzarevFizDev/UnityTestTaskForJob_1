using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ActionFactory : MonoBehaviour
{
    [SerializeField] private ActionData _attackActionData;
    [SerializeField] private ActionData _shieldActionData;
    [SerializeField] private ActionData _cleanupActionData;
    [SerializeField] private ActionData _fireballActionData;
    [SerializeField] private ActionData _healthActionData;


    private BaseAction CreateAttackAction()
    {
        if (_attackActionData.Type != ActionType.Attack)
            throw new System.ArgumentException("Incorrect data for AttackAction");

        return new AttackAction(_attackActionData);

    }

    private BaseAction CreateShieldAction()
    {
        if (_shieldActionData.Type != ActionType.Shield)
            throw new System.ArgumentException("Incorrect data for ShieldAction");

        return new ShieldAction(_shieldActionData);
    }

    private BaseAction CreateFireballAction()
    {
        if (_fireballActionData.Type != ActionType.Fireball)
            throw new System.ArgumentException("Incorrect data for FireballAction");

        return new FireballAction(_fireballActionData);
    }

    private BaseAction CreateCleanupAction()
    {
        if (_cleanupActionData.Type != ActionType.Cleanup)
            throw new System.ArgumentException("Incorrect data for CleanupAction");

        return new CleanupAction(_cleanupActionData);
    }

    private BaseAction CreateHealthAction()
    {
        if (_healthActionData.Type != ActionType.Health)
            throw new System.ArgumentException("Incorrect data for HealthAction");
        
        return new HealthAction(_healthActionData);
    }

    public List<BaseAction> CreateActions()
    {
        List<BaseAction> actions = new List<BaseAction>();
        actions.Add(CreateAttackAction());
        actions.Add(CreateShieldAction());
        actions.Add(CreateFireballAction());
        actions.Add(CreateCleanupAction());
        actions.Add(CreateHealthAction());
        return actions;
    }
}
