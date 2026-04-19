using MelonLoader;
using MelonLoader.Utils;
using mszbhop;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

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
        _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        _rb.interpolation = RigidbodyInterpolation.Interpolate;

        PhysicMaterial mat = new PhysicMaterial();
        mat.dynamicFriction = 0f;
        mat.staticFriction = 0f;
        mat.bounciness = 0f;
        mat.frictionCombine = PhysicMaterialCombine.Minimum;
        _col.material = mat;
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
            if (_rb.velocity.y < 0)
                velocity.y = -2f;
            MovementFunctions.GroundMove(ref velocity, wishDir, true);
        }
        else
        {
            MovementFunctions.AirMove(ref velocity, wishDir);
            velocity.y -= 20f * Time.fixedDeltaTime;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("Mouse ScrollWheel") != 0) && isGrounded)
        {
            velocity.y = ConfigManager.MovementConfig.JumpVelocityUnity;
        }

        _rb.velocity = velocity;
    }

    private bool IsGrounded()
    {
        _col.enabled = false;

        Vector3 origin = transform.position + _col.center;
        float rayLength = _col.height * 0.5f + 0.3f;
        bool hit = Physics.Raycast(origin, Vector3.down, rayLength);
        _col.enabled = true;
        return hit;
    }
}
