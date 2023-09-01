using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DataSearchProvider<T> : ScriptableObject, ISearchWindowProvider
{
    private List<KeyValuePair<T, string>> items;
    private Action<T> onSetIndexCallback;

    protected string searchTreeTitle;
    
    public virtual void Init(List<KeyValuePair<T, string>> items, Action<T> callback)
    {
        this.items = items;
        onSetIndexCallback = callback;
    }
    
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        List<SearchTreeEntry> searchList = new List<SearchTreeEntry>();
        searchList.Add(new SearchTreeGroupEntry(new GUIContent(searchTreeTitle), 0));

        List<KeyValuePair<T, string>> sortedItems = items;
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
                    searchList.Add(new SearchTreeGroupEntry(new GUIContent(entryTitle[i]), i + 1));
                    groups.Add(groupName);
                }

                groupName += "/";
            }

            SearchTreeEntry entry = new SearchTreeEntry(new GUIContent(entryTitle.Last()));
            entry.level = entryTitle.Length;
            entry.userData = item.Key;
            searchList.Add(entry);
        }

        return searchList;
    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        onSetIndexCallback?.Invoke((T) SearchTreeEntry.userData);
        return true;
    }
}