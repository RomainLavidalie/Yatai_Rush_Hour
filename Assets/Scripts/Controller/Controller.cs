using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public Vector3 WantedDirectionMove { get; protected set; } = new Vector3(0, 0, 1);
    public Vector3 WantedDirectionLook { get; protected set; } = new Vector3(0, 0, 1);
    public float WantedSpeed { get; protected set; } = 0;

    public bool drawDebugDirections = false;

    protected Vector3 WantedDirectionLookTargetSmooth = new Vector3(0, 0, 1);
    public float smoothFactor = 0.2f;
    private const float SmoothFactorDivisor = 1e6f;
    public bool WantsToShoot { get; protected set; } = false;

    protected void SmoothWantedDirectionLook(float deltaTime)
    {
        float t = Mathf.Clamp(1 - Mathf.Pow(smoothFactor / SmoothFactorDivisor, Time.deltaTime),0,1);
        WantedDirectionLook = Vector3.Lerp(WantedDirectionLook, WantedDirectionLookTargetSmooth, t).normalized;
    }

    protected void DrawDebug()
    {
        if (drawDebugDirections)
        {
            Debug.DrawLine(transform.position, transform.position + WantedDirectionMove * 2.0f, Color.blue);
            Debug.DrawLine(transform.position, transform.position + WantedDirectionLookTargetSmooth * 2.0f, Color.red);
            Debug.DrawLine(transform.position, transform.position + WantedDirectionLook * 2.0f, Color.green);
        }
    }
    
    
}