using System;
using Blackboard.Actors;

namespace Blackboard.Events
{
    public class DogEventChannel : EventChannel
    {
	 	public Action<ActorSO> onDogPet;
    }
}
