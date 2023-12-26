using Blackboard.Actions;
using UnityEngine;

public class BlackboardTrigger : MonoBehaviour
{
    public bool triggerOnce = true;
    public RequirementsSO requirements;
    
    public ActionList onTrigger;

    private Requirements req;

    private bool triggered;
    
    private void Awake()
    {
        req = new Requirements(requirements);
        //req.onGoalCompleted += OnTrigger;
        //triggerCondition.AddListener(OnTrigger);
    }

    private void Update()
    {
        if(triggerOnce && triggered)
            return;
        
        if (req.CheckRequirementsGoal())
        {
            OnTrigger();    
        }
    }

    private void OnTrigger()
    {
        onTrigger.Execute();
        
        if(triggerOnce)
        {
            //req.onGoalCompleted -= OnTrigger;
            triggered = true;
        }
    }
}
