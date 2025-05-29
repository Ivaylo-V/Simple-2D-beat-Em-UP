using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private bool canMove = true;
    [Tooltip(("If your character does not jump, ignore all below 'Jumping' Character"))]
    [SerializeField] private bool doesCharacterJump = false;

    [Header("Base / Root")]
    [SerializeField] private Rigidbody2D baseRB;
    [SerializeField] private float hSpeed = 10f;
    [SerializeField] private float vSpeed = 6f;
    [Range(0, 1.0f)]
    [SerializeField] float movementSmooth = 0.5f;

    [Header("'Jumping' Character")]
    [SerializeField] private Rigidbody2D charRB;
    [SerializeField] private float jumpVal = 10f;
    [SerializeField] private int possibleJumps = 1;
    [SerializeField] private int currentJumps = 0;
    [SerializeField] private bool onBase = false;
    [SerializeField] private Transform jumpDetector;
    [SerializeField] private float detectionDistance;
    [SerializeField] private LayerMask detectLayer;
    [SerializeField] private float jumpingGravityScale;
    [SerializeField] private float fallingGravityScale;
    private bool jump;



    private bool facingRight = true;
    private Vector3 velocity = Vector3.zero;

    PlayerInput input;
    Controls controls = new Controls();

    // 
    private Vector3 charDefaultRelPos, baseDefPos;

    // Start is called before the first frame update
    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        charDefaultRelPos = charRB.transform.localPosition;
    }

    private void Update()
    {
        controls = input.GetInput();
        if (controls.JumpState && currentJumps < possibleJumps)
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
       

        if (!onBase && doesCharacterJump && charRB.linearVelocity.y < 0)
        {
            detectBase();
        }

        if (canMove)
        {

            // Get screen bounds
            float minX = Camera.main.ViewportToWorldPoint(new Vector3(0.07f, 0, 0)).x;
            float maxX = Camera.main.ViewportToWorldPoint(new Vector3(0.93f, 0, 0)).x;
            float minY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
            float maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

            // Apply clamping to character position
            Vector3 clampedPosition = baseRB.transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);

            // Define raycast origins
            Vector2 rayOrigin = baseRB.position;

            // Define ray directions
            Vector2 rayDirectionHorizontal = new Vector2(controls.HorizontalMove, 0).normalized;
            Vector2 rayDirectionVertical = new Vector2(0, controls.VerticalMove).normalized;

            // Raycast distances
            float rayDistance = 0.6f;

            Vector3 targetVelocity = new Vector2(controls.HorizontalMove * hSpeed, controls.VerticalMove * vSpeed);

            // Perform horizontal wall detection
            RaycastHit2D hitHorizontal = Physics2D.Raycast(rayOrigin, rayDirectionHorizontal, rayDistance, LayerMask.GetMask("Wall"));

            if (hitHorizontal.collider != null)
            {
                Debug.Log("Horizontal wall detected, stopping movement!");
                targetVelocity.x = 0; // Stop horizontal movement
                movementSmooth = 0;
            }

            // Perform vertical wall detection
            RaycastHit2D hitVertical = Physics2D.Raycast(rayOrigin, rayDirectionVertical, rayDistance, LayerMask.GetMask("Wall"));

            if (hitVertical.collider != null)
            {
                Debug.Log("Vertical wall detected, stopping movement!");
                targetVelocity.y = 0; // Stop vertical movement
                movementSmooth = 0;
            }

            Vector2 _velocity = Vector3.SmoothDamp(baseRB.linearVelocity, targetVelocity, ref velocity, movementSmooth);

            // Set the clamped position
            baseRB.transform.position = clampedPosition;


            baseRB.linearVelocity = _velocity;

            //----- 
            if (doesCharacterJump)
            {
                if (onBase)
                {

                    // charRB.velocity = baseRB.velocity;
                    charRB.linearVelocity = Vector2.zero;

                    // vertical check
                    if (charRB.transform.localPosition != charDefaultRelPos)
                    {
                        var charTransform = charRB.transform;
                        charTransform.localPosition = new Vector2(charTransform.localPosition.x,
                            charDefaultRelPos.y);
                    }
                }
                else
                {
                    // falling
                    // if (charRB.velocity.y < 0)
                    // {
                    //     // charRB.gravityScale = fallingGravityScale;
                    // }
                    // else
                    // { // moving upward from jump
                    //     // charRB.gravityScale = jumpingGravityScale;
                    // }

                    charRB.linearVelocity = new Vector2(_velocity.x, charRB.linearVelocity.y);
                }

                if (jump)
                {
                    charRB.isKinematic = false;
                    charRB.AddForce(Vector2.up * jumpVal, ForceMode2D.Impulse);
                    charRB.gravityScale = jumpingGravityScale;
                    jump = false;
                    currentJumps++;
                    onBase = false;
                }

                // --- horizontal position check
                if (charRB.transform.localPosition != charDefaultRelPos)
                {
                    print("pos diff- local: " + charRB.transform.localPosition + "  --default: " + charDefaultRelPos);
                    var charTransform = charRB.transform;
                    charTransform.localPosition = new Vector2(charDefaultRelPos.x,
                        charTransform.localPosition.y);
                }
            }
            // --- 

            // rotate if we're facing the wrong way
            if (controls.HorizontalMove > 0 && !facingRight)
            {
                flip();
            }
            else if (controls.HorizontalMove < 0 && facingRight)
            {
                flip();
            }
        }
    }

    private void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void detectBase()
    {
        RaycastHit2D hit = Physics2D.Raycast(jumpDetector.position, -Vector2.up, detectionDistance, detectLayer);
        if (hit.collider != null)
        {
            onBase = true;
            charRB.isKinematic = true;
            currentJumps = 0;
            // charRB.velocity = Vector2.zero;
            // baseRB.velocity = Vector2.zero;
            Debug.Log("setting velocity to zero");
        }
    }

    private void OnDrawGizmos()
    {
        if (doesCharacterJump)
        {
            Gizmos.DrawRay(jumpDetector.transform.position, -Vector3.up * detectionDistance);
        }
    }
}
