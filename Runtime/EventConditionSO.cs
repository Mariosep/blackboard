using UnityEditor;

public class EventConditionSO : ConditionSO
{
    public EventSO eventSo;

    public EventType EventType => eventSo.type;
    
    public ActorSO actorArgRequired;
    public ItemSO itemArgRequired;

    public override void Init(string id)
    {
        base.Init(id);

        type = ConditionType.Event;
    }

    public override void SetElementRequired(BlackboardElementSO elementRequired)
    {
        if (elementRequired is EventSO eventRequirement)
        {
            eventSo = eventRequirement;
        }
    }

    public void SetArgRequired(BlackboardElementSO argRequired)
    {
        if (argRequired is ActorSO actor)
        {
            actorArgRequired = actor;
        }
        else if (argRequired is ItemSO item)
        {
            itemArgRequired = item; 
        }
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
#endif
    }

    public override BlackboardElementSO GetElementRequired()
    {
        return eventSo;
    }
}