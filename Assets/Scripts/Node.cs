using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Node
{
    public string nodeName;
    public List<Node> _childNodes;

    public Node(List<string> nodeNames, bool isCombineEnabled = false)
    {
        if (nodeNames.Count == 0) return;
        nodeName = nodeNames[0];
        nodeNames.RemoveAt(0);
        if (nodeNames.Count == 0) return;
        CreateChildNodes(nodeNames, isCombineEnabled);
    }

    public void CreateChildNodes(List<string> nodeNames, bool isCombineEnabled)
    {
        if(_childNodes == null) _childNodes = new List<Node>();
        var currentName = nodeNames[0];
        var nextNode = _childNodes?.Find(node => node.nodeName == currentName);
        if (nextNode != null)
        {
            nodeNames.RemoveAt(0);
            if (nodeNames.Count == 0) return;
            nextNode.CreateChildNodes(nodeNames, isCombineEnabled);
        }
        else
        {
            if (currentName.Contains("|"))
            {
                if (isCombineEnabled)
                {
                    InsertCombinatoryNodes(nodeNames, currentName);
                }
                else
                {
                    InsertMultipleNodes(nodeNames, currentName);
                }
            }
            else
            {
                InsertSingleNode(nodeNames);
            }
        }
    }

    private void InsertSingleNode(List<string> nodeNames)
    {
        var newNode = new Node(nodeNames);
        _childNodes?.Add(newNode);
    }

    private void InsertMultipleNodes(List<string> nodeNames, string currentName)
    {
        nodeNames.RemoveAt(0);
        var names = currentName.Split('|').ToList();
        names.ForEach(name =>
        {
            var newNameList = nodeNames.GetRange(0, nodeNames.Count);
            newNameList.Insert(0, name);
            CreateChildNodes(newNameList, false);
        });
    }

    private void InsertCombinatoryNodes(List<string> nodeNames, string currentName)
    {
        nodeNames.RemoveAt(0);
        var names = currentName.Split('|').ToList();
        var count = names.Count;
        for (var i = 0; i < count; i++)
        {
            for (var j = i + 1; j < count; j++)
            {
                names.Add($"{names[i]}-{names[j]}");
            }
        }
        names.ForEach(name =>
        {
            var newNameList = nodeNames.GetRange(0, nodeNames.Count);
            newNameList.Insert(0, name);
            CreateChildNodes(newNameList, true);
        });
    }

    public void PrintLeaf()
    {
        if (_childNodes == null)
        {
            Debug.Log($"{nodeName}/");
            return;
        }
        _childNodes.ForEach(node =>
        {
            node.PrintLeaf(nodeName);
        });
    }

    private void PrintLeaf(string path)
    {
        var currentPath = $"{path}/{nodeName}";
        if (_childNodes ==  null)
        {
            Debug.Log(currentPath);
        }
        else
        {
            _childNodes.ForEach(node =>
            {
                node.PrintLeaf(currentPath);
            });
        }
    }

    public void PrintTree(int level = 0)
    {
        var text = "";
        for (var i = 0; i < level; i++)
        {
            text += "-";
        }

        text += nodeName;
        Debug.Log(text);
        if (_childNodes == null) return;
        level++;
        _childNodes.ForEach(node => node.PrintTree(level));
    }
}
