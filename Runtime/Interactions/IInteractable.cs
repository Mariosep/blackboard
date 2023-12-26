public interface IInteractable
{
    public string Name { get; }
    public string InteractionName { get; }
    public bool InteractionEnabled { get; }
    public bool CanInteract();
    public void Interact();
}