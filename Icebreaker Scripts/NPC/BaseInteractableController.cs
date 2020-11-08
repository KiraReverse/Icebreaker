using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseInteractableController : MonoBehaviour
{
    protected GameObject player;

    [Tooltip("If this NPC can be interacted with multiple times, this checkbox should be ticked.")]
    public bool isMultiInteractable;

    [Tooltip("Attach Dialogue Container Here")]
    public BaseContainer dialogueContainer;

    public bool isZorkInteractable;

    protected bool isInteracted = false;

    protected bool isActive = true;
    public bool IsActive { get => isActive; }

    protected DialogueManager dialogueManager;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");

        dialogueManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<DialogueManager>();
    }


    public virtual void Interact()
    {
        if(!isZorkInteractable && player.GetComponent<PlayerController>().isZork)
        {
            return;
        }

        if (isInteracted && !isMultiInteractable)
        {
            return;
        }

        if(!isMultiInteractable)
        {
            isActive = false;
        }

        DisplayText();
        isInteracted = true;
            
    }

    protected void DisplayText()
    {
       dialogueManager.SortContainer(dialogueContainer);
    }

    public void SetActive(bool active)
    {
        isActive = active;
    }
}
