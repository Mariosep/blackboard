using UnityEngine;

public class TriggerSoundFact : MonoBehaviour
{
    public AudioClip clip;
    
    public BoolFact boolFact;

    private void Start()
    {
        if (boolFact.HasValue)
            boolFact.onValueChanged += TriggerSound;
    }

    private void TriggerSound(bool value)
    {
        if(clip != null)
            AudioSource.PlayClipAtPoint(clip, Vector3.zero);
    }

    private void OnDestroy()
    {
        boolFact.onValueChanged -= TriggerSound;
    }
}