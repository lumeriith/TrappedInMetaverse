using UnityEngine;
using UnityEngine.Serialization;

public class Jump : MonoBehaviour
{
    Rigidbody rigidbody;
    public float gunJumpStrength = 2;
    public float marioJumpStrength = 3.5f;
    public float jumpStrength => ModeManager.instance.mode == GameMode.Gun ? gunJumpStrength : marioJumpStrength;

    public event System.Action Jumped;

    [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
    GroundCheck groundCheck;
    

    void Reset()
    {
        // Try to get groundCheck.
        groundCheck = GetComponentInChildren<GroundCheck>();
    }

    void Awake()
    {
        // Get rigidbody.
        rigidbody = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        // Jump when the Jump button is pressed and we are on the ground.
        if (Input.GetButtonDown("Jump") && (!groundCheck || groundCheck.isGrounded))
        {
            rigidbody.AddForce(Vector3.up * 100 * jumpStrength);
            Jumped?.Invoke();
        }
    }
}
