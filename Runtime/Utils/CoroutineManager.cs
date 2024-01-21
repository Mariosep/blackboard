using System.Collections;

namespace Blackboard.Utils
{
    public class CoroutineManager : Singleton<CoroutineManager>
    {
        public void StartCoroutineFromManager(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
    }
}