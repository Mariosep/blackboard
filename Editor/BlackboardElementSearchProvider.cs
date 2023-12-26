using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BlackboardElementSearchProvider : ScriptableObject, ISearchWindowProvider
{
    private List<KeyValuePair<FactSO, string>> factItems;
    private List<KeyValuePair<EventSO, string>> eventItems;
    private List<KeyValuePair<ActorSO, string>> actorItems;
    private List<KeyValuePair<ItemSO, string>> itemItems;
    
    private Action<BlackboardElementSO> onSetIndexCallback;
    
    private string searchTreeTitle;
    private Texture2D _indentationIcon;
    
    public void Init(Action<BlackboardElementSO> callback,
        List<KeyValuePair<FactSO, string>> factItems = null,
        List<KeyValuePair<EventSO, string>> eventItems = null,
        List<KeyValuePair<ActorSO, string>> actorItems = null,
        List<KeyValuePair<ItemSO, string>> itemItems = null)
    {
        this.factItems = factItems;
        this.eventItems = eventItems;
        this.actorItems = actorItems;
        this.itemItems = itemItems;
        
        onSetIndexCallback = callback;

        searchTreeTitle = "Blackboard elements";
        
        _indentationIcon = new Texture2D(1, 1);
        _indentationIcon.SetPixel(0,0, new Color(0,0,0,0));
        _indentationIcon.Apply();
    }
    
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        List<SearchTreeEntry> searchList = new List<SearchTreeEntry>();
        searchList.Add(new SearchTreeGroupEntry(new GUIContent(searchTreeTitle), 0));

        #region Facts
        if (factItems != null)
        {
            searchList.Add(new SearchTreeGroupEntry(new GUIContent("Facts"), 1));
            
            List<KeyValuePair<FactSO, string>> sortedItems = factItems;
            sortedItems.Sort((a, b) =>
            {
                string[] splitsA = a.Value.Split('/');
                string[] splitsB = b.Value.Split('/');

                for (int i = 0; i < splitsA.Length; i++)
                {
                    if (i >= splitsB.Length)
                        return 1;

                    int value = splitsA[i].CompareTo(splitsB[i]);

                    if (value != 0)
                    {
                        // Make sure that leaves go before nodes
                        if (splitsA.Length != splitsB.Length && (i == splitsA.Length - 1 || i == splitsB.Length - 1))
                            return splitsA.Length < splitsB.Length ? 1 : -1;

                        return value;
                    }
                }

                return 0;
            });

            List<string> groups = new List<string>();

            foreach (var item in sortedItems)
            {
                string[] entryTitle = item.Value.Split('/');
                string groupName = "";

                for (int i = 0; i < entryTitle.Length-1; i++)
                {
                    groupName += entryTitle[i];

                    if (!groups.Contains(groupName))
                    {
                        searchList.Add(new SearchTreeGroupEntry(new GUIContent(entryTitle[i]), i + 2));
                        groups.Add(groupName);
                    }

                    groupName += "/";
                }

                SearchTreeEntry entry = new SearchTreeEntry(new GUIContent(entryTitle.Last(), _indentationIcon));
                entry.level = entryTitle.Length + 1;
                entry.userData = item.Key;
                searchList.Add(entry);
            }
        }
        

        #endregion

        #region Events
        if (eventItems != null)
        {
            searchList.Add(new SearchTreeGroupEntry(new GUIContent("Events"), 1));
            
            List<KeyValuePair<EventSO, string>> sortedItems = eventItems;
            sortedItems.Sort((a, b) =>
            {
                string[] splitsA = a.Value.Split('/');
                string[] splitsB = b.Value.Split('/');

                for (int i = 0; i < splitsA.Length; i++)
                {
                    if (i >= splitsB.Length)
                        return 1;

                    int value = splitsA[i].CompareTo(splitsB[i]);

                    if (value != 0)
                    {
                        // Make sure that leaves go before nodes
                        if (splitsA.Length != splitsB.Length && (i == splitsA.Length - 1 || i == splitsB.Length - 1))
                            return splitsA.Length < splitsB.Length ? 1 : -1;

                        return value;
                    }
                }

                return 0;
            });

            List<string> groups = new List<string>();

            foreach (var item in sortedItems)
            {
                string[] entryTitle = item.Value.Split('/');
                string groupName = "";

                for (int i = 0; i < entryTitle.Length-1; i++)
                {
                    groupName += entryTitle[i];

                    if (!groups.Contains(groupName))
                    {
                        searchList.Add(new SearchTreeGroupEntry(new GUIContent(entryTitle[i]), i + 2));
                        groups.Add(groupName);
                    }

                    groupName += "/";
                }

                SearchTreeEntry entry = new SearchTreeEntry(new GUIContent(entryTitle.Last(), _indentationIcon));
                entry.level = entryTitle.Length + 1;
                entry.userData = item.Key;
                searchList.Add(entry);
            }
        }

        #endregion

        #region Actors
        if (actorItems != null)
        {
            searchList.Add(new SearchTreeGroupEntry(new GUIContent("Actors"), 1));
            
            List<KeyValuePair<ActorSO, string>> sortedItems = actorItems;
            sortedItems.Sort((a, b) =>
            {
                string[] splitsA = a.Value.Split('/');
                string[] splitsB = b.Value.Split('/');

                for (int i = 0; i < splitsA.Length; i++)
                {
                    if (i >= splitsB.Length)
                        return 1;

                    int value = splitsA[i].CompareTo(splitsB[i]);

                    if (value != 0)
                    {
                        // Make sure that leaves go before nodes
                        if (splitsA.Length != splitsB.Length && (i == splitsA.Length - 1 || i == splitsB.Length - 1))
                            return splitsA.Length < splitsB.Length ? 1 : -1;

                        return value;
                    }
                }

                return 0;
            });

            List<string> groups = new List<string>();

            foreach (var item in sortedItems)
            {
                string[] entryTitle = item.Value.Split('/');
                string groupName = "";

                for (int i = 0; i < entryTitle.Length-1; i++)
                {
                    groupName += entryTitle[i];

                    if (!groups.Contains(groupName))
                    {
                        searchList.Add(new SearchTreeGroupEntry(new GUIContent(entryTitle[i]), i + 2));
                        groups.Add(groupName);
                    }

                    groupName += "/";
                }

                SearchTreeEntry entry = new SearchTreeEntry(new GUIContent(entryTitle.Last(), _indentationIcon));
                entry.level = entryTitle.Length + 1;
                entry.userData = item.Key;
                searchList.Add(entry);
            }
        }
        

        #endregion

        #region items
        if (itemItems != null)
        {
            searchList.Add(new SearchTreeGroupEntry(new GUIContent("Items"), 1));
            
            List<KeyValuePair<ItemSO, string>> sortedItems = itemItems;
            sortedItems.Sort((a, b) =>
            {
                string[] splitsA = a.Value.Split('/');
                string[] splitsB = b.Value.Split('/');

                for (int i = 0; i < splitsA.Length; i++)
                {
                    if (i >= splitsB.Length)
                        return 1;

                    int value = splitsA[i].CompareTo(splitsB[i]);

                    if (value != 0)
                    {
                        // Make sure that leaves go before nodes
                        if (splitsA.Length != splitsB.Length && (i == splitsA.Length - 1 || i == splitsB.Length - 1))
                            return splitsA.Length < splitsB.Length ? 1 : -1;

                        return value;
                    }
                }

                return 0;
            });

            List<string> groups = new List<string>();

            foreach (var item in sortedItems)
            {
                string[] entryTitle = item.Value.Split('/');
                string groupName = "";

                for (int i = 0; i < entryTitle.Length-1; i++)
                {
                    groupName += entryTitle[i];

                    if (!groups.Contains(groupName))
                    {
                        searchList.Add(new SearchTreeGroupEntry(new GUIContent(entryTitle[i]), i + 2));
                        groups.Add(groupName);
                    }

                    groupName += "/";
                }

                SearchTreeEntry entry = new SearchTreeEntry(new GUIContent(entryTitle.Last(), _indentationIcon));
                entry.level = entryTitle.Length + 1;
                entry.userData = item.Key;
                searchList.Add(entry);
            }
        }
        #endregion

        
        
        return searchList;
    }
    
    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        onSetIndexCallback?.Invoke((BlackboardElementSO) SearchTreeEntry.userData);
        return true;
    }
}