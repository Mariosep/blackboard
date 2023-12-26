using System.Collections;

public class CoroutineManager : Singleton<CoroutineManager>
{
    public void StartCoroutineFromManager(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}