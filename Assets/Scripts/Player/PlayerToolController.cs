using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToolController : MonoBehaviour
{
    private PlayerStateController stateController;
    private PlayerCarryController carryController;
    private PlayerInput playerInput;

    private enum Tools
    {
        None,
        Axe,
        Pickaxe
    }

    private Tools currentTool;

    [Header("Tool Settings")]
    [SerializeField] private GameObject[] allTools;

    private bool isChopping = false;
    private bool isMining = false;
    private bool isCarrying = true;
    private void Awake()
    {
        stateController = GetComponent<PlayerStateController>();
        carryController = GetComponent<PlayerCarryController>();
        playerInput = GetComponent<PlayerInput>();
    }
    void Start()
    {
        InitializeTool();
    }

    private void InitializeTool()
    {
        currentTool = Tools.None;
        DeactiveAllTools();
    }

    private void DeactiveAllTools()
    {
        for (int i = 0; i < allTools.Length; i++)
        {
            allTools[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (IsToolSwitchable() && playerInput.PressedSwithKey())
            ChangeNextTool();
    }
    private void ChangeNextTool()
    {
        var axeIndex = 0;
        var pickaxeIndex = 1;

        switch (currentTool)
        {
            case Tools.None:
                currentTool = Tools.Axe;

                allTools[axeIndex].SetActive(true);
                allTools[pickaxeIndex].SetActive(false);

                isChopping = true;
                isMining = false;
                isCarrying = false;
                break;
            case Tools.Axe:
                currentTool = Tools.Pickaxe;

                allTools[axeIndex].SetActive(false);
                allTools[pickaxeIndex].SetActive(true);

                isChopping = false;
                isMining = true;
                isCarrying = false;
                break;
            case Tools.Pickaxe:
                currentTool = Tools.None;

                allTools[axeIndex].SetActive(false);
                allTools[pickaxeIndex].SetActive(false);

                isChopping = false;
                isMining = false;
                isCarrying = true;
                break;
        }
        
    }
    public BoxCollider GetAxeCollider()
    {
        var axeIndex = 0;
        var collider = allTools[axeIndex].GetComponent<BoxCollider>();

        return collider;
    }
    public BoxCollider GetPickaxeCollider()
    {
        var pickaxeIndex = 1;
        var collider = allTools[pickaxeIndex].GetComponent<BoxCollider>();

        return collider;
    }
    private bool IsToolSwitchable()
    {
        return stateController.currentState == PlayerStateController.States.Idle || stateController.currentState == PlayerStateController.States.Moving;
    }
    public bool IsChopping()
    {
        return isChopping;
    }
    public bool IsMining()
    {
        return isMining;
    }
    public bool IsCarrying()
    {
        return isCarrying;
    }
}
