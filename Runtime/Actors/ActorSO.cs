using UnityEngine;

[CreateAssetMenu(menuName = "Actor", fileName = "Actor")]
public class ActorSO : BlackboardElementSO
{
    public string shortName;
    public GameObject prefab;
    public Texture2D icon;
}