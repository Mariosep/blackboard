using UnityEngine;

public class TriggerSoundFact : MonoBehaviour
{
    public AudioClip clip;
    
    public BoolFactSO boolFact;

    private void Start()
    {
        if (boolFact != null)
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