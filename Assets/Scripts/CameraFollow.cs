using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Follow Settings")]
    public float smoothSpeed = 0.2f;   // lower = smoother, higher = snappier
    public Vector3 offset = new Vector3(0, 0, -10);

    [Header("Look Ahead Settings")]
    public float lookAheadDistance = 2f;
    public float lookAheadSmooth = 0.1f;

    private Vector3 currentVelocity;
    private Vector3 lookAheadPos;
    private float lastTargetX;

    void Start()
    {
        if (target != null)
            lastTargetX = target.position.x;
    }

    void LateUpdate()
    {
        if (target == null) return;

        float deltaX = target.position.x - lastTargetX;
        bool movingHorizontally = Mathf.Abs(deltaX) > 0.01f;

        if (movingHorizontally)
        {
            lookAheadPos = Vector3.Lerp(
                lookAheadPos,
                new Vector3(Mathf.Sign(deltaX) * lookAheadDistance, 0, 0),
                lookAheadSmooth
            );
        }
        else
        {
            // reset look ahead when stopping
            lookAheadPos = Vector3.Lerp(lookAheadPos, Vector3.zero, lookAheadSmooth);
        }

        Vector3 targetPos = target.position + offset + lookAheadPos;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentVelocity, smoothSpeed);

        lastTargetX = target.position.x;
    }
}
