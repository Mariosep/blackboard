using System;
using Blackboard.Actors;
using Blackboard.Events;
using Blackboard.Facts;
using Blackboard.Items;
using UnityEngine;

namespace Blackboard
{
    [CreateAssetMenu(fileName = "Blackboard", menuName = "Blackboard/Blackboard", order = 0)]
    public class BlackboardSO : ScriptableObject
    {
        public string id;
        public string blackboardName;
    
        public FactDataBaseSO factDataBase;
        private EventDataBase eventDataBase;
        public ActorDataBaseSO actorDataBase;
        public ItemDataBaseSO itemDataBase;

        public EventDataBase EventDataBase {
            get { return eventDataBase ??= new EventDataBase(); }
        }

        public void InitializeEventDataBase()
        {
            eventDataBase = new EventDataBase();
        }
        
        public bool BlackboardExists(string blackboardName)
        {
            return false;
        }

        public void SaveState()
        {
            factDataBase.SaveState();
            actorDataBase.SaveState();
            itemDataBase.SaveState();
        }
    
        public void RevertChanges()
        {
            factDataBase.RevertChanges();
            actorDataBase.RevertChanges();
            itemDataBase.RevertChanges();
        }
    
        #region Facts
        public FactGroupSO GetFactGroupByID(string id)
        {
            return (FactGroupSO) factDataBase.GetGroupById(id);
        }
    
        public FactSO GetFactByName(string factName)
        {
            foreach (FactGroupSO factGroup in factDataBase.groupsList)
            {
                FactSO fact = factGroup.GetElementByName(factName);

                if (fact != null)
                    return fact;
            }
        
            throw new Exception("The fact that you are trying to get is not registered");      
        }
    
        /*public FactSO GetFactByID(string id)
    {
        
    }*/
    
        public BoolFactSO GetBoolFactByName(string factName)
        {
            FactSO fact = GetFactByName(factName);

            if (fact != null && fact is BoolFactSO boolFact)
                return boolFact;
            else
                return null;
        }
    
        /*
    public BoolFact GetBoolFactByID(string id)
    {
        
    }
    */
    
        public IntFactSO GetIntFactByName(string factName)
        {
            FactSO fact = GetFactByName(factName);

            if (fact != null && fact is IntFactSO intFact)
                return intFact;
            else
                return null;
        }
    
        /*public IntFact GetIntFactByID(string id)
    {
        
    }*/
    
        public FloatFactSO GetFloatFactByName(string factName)
        {
            FactSO fact = GetFactByName(factName);

            if (fact != null && fact is FloatFactSO floatFact)
                return floatFact;
            else
                return null;
        }
    
        /*public FloatFact GetFloatFactByID(string id)
    {
        
    }*/
    
        public StringFactSO GetStringFactByName(string factName)
        {
            FactSO fact = GetFactByName(factName);

            if (fact != null && fact is StringFactSO stringFact)
                return stringFact;
            else
                return null;
        }
    
        /*public StringFact GetStringFactByID(string id)
    {
        
    }*/
        #endregion
    
        #region Actors
        public ActorGroupSO GetActorGroupByID(string id)
        {
            return (ActorGroupSO) actorDataBase.GetGroupById(id);
        }
    
        public ActorSO GetActorByName(string actorName)
        {
            foreach (ActorGroupSO actorGroup in actorDataBase.groupsList)
            {
                ActorSO actor = actorGroup.GetElementByName(actorName);

                if (actor != null)
                    return actor;
            }
        
            throw new Exception("The actor that you are trying to get is not registered");       
        }
    
        /*public ActorSO GetActorByID(string id)
    {
        
    }*/
        #endregion

        #region Items
        public ItemGroupSO GetItemGroupByID(string id)
        {
            return (ItemGroupSO) itemDataBase.GetGroupById(id);
        }
    
        public ItemSO GetItemByName(string itemName)
        {
            foreach (ItemGroupSO itemGroup in itemDataBase.groupsList)
            {
                ItemSO item = itemGroup.GetElementByName(itemName);

                if (item != null)
                    return item;
            }
        
            throw new Exception("The item that you are trying to get is not registered");
        }
    
        /*public ItemSO GetItemByID(string id)
    {
        
    }*/
        #endregion
    }
}