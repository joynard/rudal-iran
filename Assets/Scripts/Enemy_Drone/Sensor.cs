using System.Collections;
using UnityEngine;

public class DroneSensor : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public Transform targetGO;
    public Transform soundGO;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public LayerMask soundMask;

    public bool targetSensed;
    public bool soundSensed;

    private void Start()
    {
        StartCoroutine(SensoryRoutine());
    }

    private IEnumerator SensoryRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            SensorCheck();
        }
    }

    private void SensorCheck()
    {
        Collider2D sightCheck = Physics2D.OverlapCircle(transform.position, radius, targetMask);
        if (sightCheck != null)
        {
            targetGO = sightCheck.transform;
            Vector2 directionToTarget = (targetGO.position - transform.position).normalized;

            if (Vector3.Angle(transform.right * -1, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, targetGO.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    targetSensed = true;
                }
                else
                {
                    targetSensed = false;
                }
            }
            else
            {
                targetSensed = false;
            }
        }
        else
        {
            targetSensed = false;
        }

        Collider2D hearingCheck = Physics2D.OverlapCircle(transform.position, radius, soundMask);
        if (hearingCheck != null)
        {
            soundGO = hearingCheck.transform;
            soundSensed = true;
        }
        else
        {
            soundSensed = false;
        }
    }
}