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
    [SerializeField] private Toggle combinatorialToggle;
    [SerializeField] private Button clearButton;
    private List<Node> _trees;
    private bool _isCombinatorialEnabled;

    private void Start()
    {
        _trees = new List<Node>();
        inputField.onSubmit.AddListener(InsertNodes);
        clearButton.onClick.AddListener(ClearTreeList);
        combinatorialToggle.onValueChanged.AddListener(EnableCombine);
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

    private void EnableCombine(bool isEnabled)
    {
        _isCombinatorialEnabled = isEnabled;
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
            nextNode.CreateChildNodes(nodeNames, _isCombinatorialEnabled);
        }
        else
        {
            var newNode = new Node(nodeNames, _isCombinatorialEnabled);
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