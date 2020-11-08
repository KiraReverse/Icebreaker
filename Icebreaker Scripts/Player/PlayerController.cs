using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Movement speed of the player.")]
    public float moveSpeed;
    [Tooltip("Tumble speed of the player.")]
    public float tumbleSpeed;
    public bool disableTumble;
    [Tooltip("The max speed that the player can go. (Including tumble)")]
    public float maxSpeed;


    KeyCode interactKey;
    KeyCode dodgeKey;
    KeyCode fireballKey;
    KeyCode swapPartnerKey;

    public bool isZork;

    public PlayerConfig playerConfig;
    public GameObject prompt;
    public Transform partnerWayPoint;
    public float partnerOffset;

    public GameObject fireballPrefab;

    public Directions startDir = Directions.down;
    public Directions partnerStartDir = Directions.down;
    
    
    Rigidbody2D rb;

    List<GameObject> interactableList = new List<GameObject>();

    GameManager gameManager;

    Animator playerAnim;

    public bool hasPartner = false;
    public Animator partnerAnim;

    Vector3 facingDir;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponentInChildren<Animator>();

        interactKey = playerConfig.interactKey;
        dodgeKey = playerConfig.dodgeKey;
        fireballKey = playerConfig.fireballKey;
        swapPartnerKey = playerConfig.swapPartner;
        SetFacingDir(startDir);

        if (partnerAnim != null)
        {
            SetPartnerFacingDir(partnerStartDir);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GameState == Enums.GameState.playing)
        {
            Movement();
            MoveAnim();
            Tumble();
            Interact();
            SwapPartner();
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
    }

    void SwapPartner()
    {
        if (Input.GetKeyDown(swapPartnerKey) && hasPartner)
        {
            RuntimeAnimatorController temp = playerAnim.runtimeAnimatorController;
            playerAnim.runtimeAnimatorController = partnerAnim.runtimeAnimatorController;
            partnerAnim.runtimeAnimatorController = temp;

            isZork = !isZork;
        }
    }

    public void SetFacingDir(Directions dir)
    {
        playerAnim.SetFloat("MovespeedX", 0f);
        playerAnim.SetFloat("MovespeedY", 0f);

        switch (dir)
        {
          
            case Directions.down:
            default:
                playerAnim.Play("PC_Idle_Front");
                break;

            case Directions.up:
                playerAnim.Play("PC_Idle_Back");
                break;
            case Directions.left:
                playerAnim.Play("PC_Idle_Left");
                break;
            case Directions.right:
                playerAnim.Play("PC_Idle_Right");
                break;
            
        }
    }

    public void SetPartnerFacingDir(Directions dir)
    {
        partnerAnim.SetFloat("MovespeedX", 0f);
        partnerAnim.SetFloat("MovespeedY", 0f);

        switch (dir)
        {

            case Directions.down:
            default:
                partnerAnim.Play("PC_Idle_Front");
                break;

            case Directions.up:
                partnerAnim.Play("PC_Idle_Back"); 
                break;
            case Directions.left:
                partnerAnim.Play("PC_Idle_Left");
                break;
            case Directions.right:
                partnerAnim.Play("PC_Idle_Right");
                break;

        }
    }

    void Tumble()
    {
        if(!disableTumble && Input.GetKeyDown(dodgeKey))
        {
            rb.AddForce(facingDir * tumbleSpeed, ForceMode2D.Impulse);
        }
    }
    void Movement()
    {

        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;

        

        if (moveDir != Vector3.zero)
        {
            facingDir = moveDir;
        }

        transform.position += moveDir * moveSpeed * Time.deltaTime;

        partnerWayPoint.position = transform.position + (facingDir * -partnerOffset);
    }

    void MoveAnim()
    {
        playerAnim.SetFloat("MovespeedX", Input.GetAxisRaw("Horizontal"));
        playerAnim.SetFloat("MovespeedY", Input.GetAxisRaw("Vertical"));

        if (hasPartner)
        {
            partnerAnim.SetFloat("MovespeedX", Input.GetAxisRaw("Horizontal"));
            partnerAnim.SetFloat("MovespeedY", Input.GetAxisRaw("Vertical"));
        }
    }

    public void IdleAnim()
    {
        playerAnim.SetFloat("MovespeedX", 0f);
        playerAnim.SetFloat("MovespeedY", 0f);

        if (hasPartner)
        {
            partnerAnim.SetFloat("MovespeedX", 0f);
            partnerAnim.SetFloat("MovespeedY", 0f);
        }
    }

    void Interact()
    {
        if (isZork && Input.GetKeyDown(fireballKey))
        {
            GameObject fireball = Instantiate(fireballPrefab, transform.position + (facingDir * 0.5f), Quaternion.identity);
            fireball.GetComponent<Rigidbody2D>().AddForce(facingDir * tumbleSpeed*2f);

        }

        GameObject interactable = CheckNearestInteractable();

        if (interactable != null && Input.GetKeyDown(interactKey) && interactableList.Contains(interactable))
        {
            if (interactable.activeSelf && interactable.GetComponent<BaseInteractableController>() != null)
            {
                interactable.GetComponent<BaseInteractableController>().Interact();
            }
            IdleAnim();
            prompt.SetActive(false);
            interactable = null;
            interactableList.Clear();
        }
        
    }
    
    GameObject CheckNearestInteractable()
    {
        float dist = Mathf.Infinity;
        GameObject nearest = null;
        foreach (GameObject obj in interactableList)
        {
            float objDist = Vector2.Distance(transform.position, obj.transform.position);

            if(objDist < dist)
            {
                nearest = obj;
                dist = objDist;
            }
        }

        return nearest;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.tag == "NPC")
        {
            if (collision.gameObject.GetComponent<BaseInteractableController>() != null && collision.gameObject.GetComponent<BaseInteractableController>().IsActive && !interactableList.Contains(collision.gameObject))
            {

                if(collision.gameObject.GetComponent<ExitController>() != null && collision.gameObject.GetComponent<ExitController>().walkover)
                {
                    return;
                }

                else if(!collision.gameObject.GetComponent<BaseInteractableController>().isZorkInteractable && isZork)
                {
                    return;
                }
                prompt.SetActive(true);
                interactableList.Add(collision.gameObject);
            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            if (collision.gameObject.GetComponent<BaseInteractableController>() != null && collision.gameObject.GetComponent<BaseInteractableController>().IsActive && !interactableList.Contains(collision.gameObject))
            {

                if (collision.gameObject.GetComponent<ExitController>() != null && collision.gameObject.GetComponent<ExitController>().walkover)
                {
                    return;
                }

                else if (!collision.gameObject.GetComponent<BaseInteractableController>().isZorkInteractable && isZork)
                {
                    return;
                }
                prompt.SetActive(true);
                interactableList.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC" && interactableList.Contains(collision.gameObject))
        {

            prompt.SetActive(false);
            interactableList.Remove(collision.gameObject);

        }

    }
}
