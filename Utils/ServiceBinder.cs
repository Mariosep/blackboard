/*using UnityEngine;

public static class ServiceBinder
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        // Initialize default service locator
        ServiceLocator.Initialize();
        
        // Register all services
        ServiceLocator.Register(new InputManager());
        ServiceLocator.Register(new QuestChannel());
        ServiceLocator.Register(new UIChannel());
        ServiceLocator.Register(new InteractionChannel());
    }
}*/