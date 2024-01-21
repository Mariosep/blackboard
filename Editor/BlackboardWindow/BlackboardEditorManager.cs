using System.Collections.Generic;
using Blackboard.Actors;
using Blackboard.Events;
using Blackboard.Facts;
using Blackboard.Items;
using UnityEditor;
using UnityEditorForks;

namespace Blackboard.Editor
{
    public class BlackboardEditorManager : ScriptableSingleton<BlackboardEditorManager>
    {
        private BlackboardSO _blackboard;

        public BlackboardSO Blackboard
        {
            get
            {
                if (_blackboard == null)
                {
                    List<BlackboardSO> assetsList = typeof(BlackboardSO).FindAssetsByType<BlackboardSO>();
                    if (assetsList.Count > 0)
                        _blackboard = assetsList[0];
                }
                
                return _blackboard;
            }
            set => _blackboard = value;
        }

        public FactDataBaseSO FactDataBase => Blackboard.factDataBase;
        public EventDataBase EventDataBase => Blackboard.EventDataBase;
        public ActorDataBaseSO ActorDataBase => Blackboard.actorDataBase;
        public ItemDataBaseSO ItemDataBase => Blackboard.itemDataBase;
        
        public string GetElementPath(BlackboardElementSO element)
        {
            ElementGroupSO group = element.group;
            
            if(element is BaseEventSO eventSo)
                return eventSo.GetName();
            else
                return $"{group.groupName}: {element.Name}";
        }
    
        public FactSO GetFact(string groupId, string factId)
        {
            if (FactDataBase.TryGetGroup(groupId, out FactGroupSO factGroup))
            {
                if (factGroup.TryGetElement(factId, out FactSO fact))
                {
                    return fact;                                    
                }
            }

            return null;
        }

        public string GetFactPath(string groupId, string factId)
        {
            if (FactDataBase.TryGetGroup(groupId, out FactGroupSO factGroup))
            {
                if (factGroup.TryGetElement(factId, out FactSO fact))
                {
                    return $"{factGroup.groupName}: {fact.Name}";                                    
                }
            }

            return "";
        }
    
        public ActorSO GetActor(string groupId, string id)
        {
            if (ActorDataBase.TryGetGroup(groupId, out ActorGroupSO actorGroup))
            {
                if (actorGroup.TryGetElement(id, out ActorSO actor))
                {
                    return actor;                                    
                }
            }

            return null;
        }

        public string GetActorPath(string groupId, string id)
        {
            if (ActorDataBase.TryGetGroup(groupId, out ActorGroupSO actorGroup))
            {
                if (actorGroup.TryGetElement(id, out ActorSO actor))
                {
                    return $"{actorGroup.groupName}: {actor.Name}";                                    
                }
            }

            return "";
        }
    
        public ItemSO GetItem(string groupId, string id)
        {
            if (ItemDataBase.TryGetGroup(groupId, out ItemGroupSO itemGroup))
            {
                if (itemGroup.TryGetElement(id, out ItemSO item))
                {
                    return item;                                    
                }
            }

            return null;
        }

        public string GetItemPath(string groupId, string id)
        {
            if (ItemDataBase.TryGetGroup(groupId, out ItemGroupSO itemGroup))
            {
                if (itemGroup.TryGetElement(id, out ItemSO item))
                {
                    return $"{itemGroup.groupName}: {item.Name}";                                    
                }
            }

            return "";
        }
    }
}