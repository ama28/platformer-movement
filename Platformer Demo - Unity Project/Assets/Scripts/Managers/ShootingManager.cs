using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingManager : MonoBehaviour
{
    [Header("Blast Attributes")]
    [Tooltip("equals blast power when held down for 1 second")]
    [SerializeField] private float blastPowerMultiplier;
    [SerializeField] private float maxBlastPower;
    private bool blastHeldDown = false;
    private float blastHeldDownTime = 0;
    [SerializeField] private float blastRadius;
    [Tooltip("Determines max time user input can be minimized after a blast")]
    [SerializeField] private float blastInputBufferLength;

    [Header("Precise Fire Attributes")]
    [SerializeField] private int preciseFireDmg = 1;

    private Rigidbody2D playerRB; 

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GameManager.Instance.player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (blastHeldDown) blastHeldDownTime += Time.deltaTime;
    }

    // Triggers explosion from mouse position to blast player using input system
    public void Blast(InputAction.CallbackContext context)
    {
        if (context.performed)
        {   
            blastHeldDown = true;
        }
        else if (context.canceled)
        {
            blastHeldDown = false;

            float adjustedPower; // for other factors that may affect blasting behavior mid game
            Vector3 mousePos = Mouse.current.position.ReadValue();
            Vector2 blastPos = Camera.main.ScreenToWorldPoint(mousePos);// implicit cast V3 -> V2

            // unfreeze player if cannon has frozen it at the cannon position
            // if (GameManager.Instance.statusManager.inCannon)
            // {
            //     playerRB.constraints = RigidbodyConstraints2D.None;
            //     adjustedPower *= player.currentCannon.cannonBlastPowerMultiplier; // set cannon blast power

            //     // set cannon blast position
            //     if (player.currentCannon.directionVector != Vector2.zero) // directionVector = Vector2.zero means the cannon is omnidirecitonal
            //         blastPos = new Vector2(player.transform.position.x, player.transform.position.y) + player.currentCannon.directionVector; 

            //     GameManager.Instance.statusManager.inCannon = false; 
            //     player.currentCannon = null;
            //     player.isFlyingFromCannon = true;
            // }

            // disable player input depending on blast power
            float distance = Vector2.Distance(blastPos, (Vector2) GameManager.Instance.player.transform.position);
            float timeToWait = Mathf.Clamp(1F/distance, 0, blastInputBufferLength);
            GameManager.Instance.playerMovement.curBlastInputBuffer = timeToWait;

            //TODO: put in if else with the cannon setting of adjusted power once that's back in
            adjustedPower = Mathf.Clamp(blastPowerMultiplier * blastHeldDownTime, 0, maxBlastPower);

            // blast and reset charge
            playerRB.AddExplosionForce(false, adjustedPower, blastPos, blastRadius, explosionDirModifier: new Vector2(1.0F, 1.4F), upwardsModifier: 0.0F);
            blastHeldDownTime = 0;

            // // trigger blast animation
            // GameManager.Instance.uiManager.crosshair.BlastAnimation();
        }
    }

    // Deals damage to a hit enemy or object when Fire (InputSystem) is triggered with mouse over target collider
    // public void PreciseFire(InputAction.CallbackContext context)
    // {
    //     if (context.performed)
    //     {
    //         RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
    //         if (hit.collider != null && hit.collider.gameObject.tag == "Shootable")
    //         {
    //             Shootable target = hit.collider.gameObject.GetComponent<Shootable>();
    //             target.Hit(preciseFireDmg);
    //             GameManager.Instance.IncrementTimeSaveMultiplier();
    //         }
    //         else 
    //         {
    //             GameManager.Instance.ResetTimeSaveMultiplier();
    //         }
    //     }
    // }
}
