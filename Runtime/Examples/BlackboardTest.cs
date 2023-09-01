using UnityEngine;

public class BlackboardTest : MonoBehaviour
{
    public BoolFact boolFact;
    public IntFact intFact;
    public FloatFact floatFact;
    public StringFact stringFact;

    public EventActor eventActor;
    public EventItem eventItem;
    
    public Actor actor;
    public Item item;

    private void Start()
    {
        boolFact.onValueChanged += OnBoolFactValueChanged;
        intFact.onValueChanged += OnCoinCollected;
        
        if(eventActor.HasEvent)
            eventActor.AddListener(OnActorEventInvoked);
    }

    [ContextMenu("ToggleFactValue")]
    private void ToggleFactValue()
    {
        if (boolFact.HasValue)
            boolFact.Value = !boolFact.Value;
    }
    
    [ContextMenu("IncrementFactValue")]
    private void IncrementFactValue()
    {
        if (intFact.HasValue)
            intFact.Value += 1;
    }

    private void OnBoolFactValueChanged(bool value)
    {
        Debug.Log($"Value changed to: {value}");
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