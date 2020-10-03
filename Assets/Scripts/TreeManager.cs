using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TreeManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Toggle isCombineEnabledToggle;
    [SerializeField] private Button clearButton;
    private List<Node> _trees;

    private void Start()
    {
        _trees = new List<Node>();
        inputField.onSubmit.AddListener(InsertNodes);
        clearButton.onClick.AddListener(ClearTreeList);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _trees.ForEach(node => node.PrintLeaf());
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _trees.ForEach(node => node.PrintTree());
        }
    }

    private void ClearTreeList()
    {
        _trees.Clear();
    }

    private void InsertNodes(string inputText)
    {
        var nodeNames = FormatString(inputText);
            
        var nextNode = _trees.Find(node => node.nodeName == nodeNames[0]);
        if (nextNode != null)
        {
            nodeNames.RemoveAt(0);
            nextNode.CreateChildNodes(nodeNames, isCombineEnabledToggle.isOn);
        }
        else
        {
            var newNode = new Node(nodeNames, isCombineEnabledToggle.isOn);
            _trees.Add(newNode);
        }
    }

    private List<string> FormatString(string textToFormat)
    {
        if (textToFormat[0] == '/')
        {
            textToFormat = textToFormat.Remove(0, 1);
        }
        var split = textToFormat.Split('/').ToList();
        return split;
    }
}