using UnityEngine;

public class BlackboardTest : MonoBehaviour
{
    [ShowValue]
    public BoolFactSO boolFact;
    [ShowValue]
    public IntFactSO intFact;
    public FloatFactSO floatFact;
    
    public StringFactSO stringFact;

    public EventActorSO eventActorSo;
    public EventItemSO eventItemSo;
    public EventSO eventSo;

    public EventActor eventActor;
    public EventItem eventItem;
    
    public ActorSO actor;
    public ItemSO item;

    public EventType eventType;

    private void Start()
    {
        boolFact.onValueChanged += OnBoolFactValueChanged;
        //intFact.onValueChanged += OnCoinCollected;
        
        if(eventActor.HasEvent)
            eventActor.AddListener(OnActorEventInvoked);
    }

    [ContextMenu("ToggleFactValue")]
    private void ToggleFactValue()
    {
        if (boolFact != null)
            boolFact.Value = !boolFact.Value;
    }
    
    [ContextMenu("IncrementFactValue")]
    private void IncrementFactValue()
    {
        /*if (intFact.HasValue)
            intFact.Value += 1;*/
    }

    private void OnBoolFactValueChanged(bool value)
    {
        Debug.Log($"{boolFact.theName} changed to: {value}");
    }
    
    private void OnCoinCollected(int coinsCount)
    {
        if(coinsCount == 10)
            Debug.Log($"Quest completed.");
    }

    private void OnActorEventInvoked(ActorSO actor)
    {
        Debug.Log($"Actor event invoked with {actor.theName}");
    }
    
    private void OnDestroy()
    {
        boolFact.onValueChanged -= OnBoolFactValueChanged;
        
        if(eventActor.HasEvent)
            eventActor.RemoveListener(OnActorEventInvoked);
    }
}