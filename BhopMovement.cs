using MelonLoader;
using mszbhop;
using UnityEngine;

[RegisterTypeInIl2Cpp]
public class BhopMovement : MonoBehaviour
{
    private Rigidbody _rb;
    private CapsuleCollider _col;

    void Start()
    {
        _rb = Mod.Kiri.GetComponent<Rigidbody>();
        _col = Mod.Kiri.GetComponent<CapsuleCollider>();

        _rb.useGravity = false;
        _rb.drag = 0f;
        _rb.angularDrag = 0f;
        _rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        Vector2 _moveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        bool isGrounded = IsGrounded();

        Vector3 velocity = _rb.velocity;

        Vector3 wishDir = transform.TransformDirection(
            new Vector3(_moveInput.x, 0f, _moveInput.y)
        ).normalized;

        if (isGrounded)
        {
            velocity.y = 0f;
            MovementFunctions.GroundMove(ref velocity, wishDir, true);
        }
        else
        {
            MovementFunctions.AirMove(ref velocity, wishDir);
            velocity.y -= 20f * Time.fixedDeltaTime;
        }

        if ((Input.GetKeyDown(KeyCode.Space) && isGrounded))
        {
            velocity.y = MovementFunctions.JumpVelocity;
        }

        _rb.velocity = velocity;
    }

    private bool IsGrounded()
    {
        _col.enabled = false;

        Vector3 origin = transform.position + _col.center;
        float rayLength = _col.height * 0.5f;
        bool hit = Physics.Raycast(origin, Vector3.down, rayLength);
        _col.enabled = true;
        return hit;
    }
}
