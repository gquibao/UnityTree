using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Node
{
    public string nodeName;
    public List<Node> _childNodes;

    public Node(List<string> nodeNames)
    {
        if (nodeNames.Count == 0) return;
        nodeName = nodeNames[0];
        nodeNames.RemoveAt(0);
        if (nodeNames.Count == 0) return;
        CreateChildNodes(nodeNames);
    }

    public void CreateChildNodes(List<string> nodeNames)
    {
        if(_childNodes == null) _childNodes = new List<Node>();
        var currentName = nodeNames[0];
        var nextNode = _childNodes?.Find(node => node.nodeName == currentName);
        if (nextNode != null)
        {
            nodeNames.RemoveAt(0);
            if (nodeNames.Count == 0) return;
            nextNode.CreateChildNodes(nodeNames);
        }
        else
        {
            Node newNode;
            if (currentName.Contains("|"))
            {
                nodeNames.RemoveAt(0);
                var names = currentName.Split('|').ToList();
                names.ForEach(name =>
                {
                    var newNameList = nodeNames;
                    newNameList.Insert(0, name);
                    newNode = new Node(newNameList);
                    _childNodes?.Add(newNode);
                });
            }
            else
            {
                newNode = new Node(nodeNames);
                _childNodes?.Add(newNode);
            }
        }
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
