using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DroneSensor))]
public class DroneSensorEditor : Editor
{
    private void OnSceneGUI()
    {
        DroneSensor sense = (DroneSensor)target;

        Handles.color = Color.white;
        Handles.DrawWireArc(sense.transform.position, Vector3.forward, Vector3.up, 360, sense.radius);

        Vector3 viewAngle1 = DirectionFromAngle(sense.transform.eulerAngles.z, -sense.angle / 2);
        Vector3 viewAngle2 = DirectionFromAngle(sense.transform.eulerAngles.z, sense.angle / 2);

        Handles.color = Color.green;
        Handles.DrawLine(sense.transform.position, sense.transform.position + viewAngle1 * sense.radius);
        Handles.DrawLine(sense.transform.position, sense.transform.position + viewAngle2 * sense.radius);

        if (sense.targetSensed && sense.targetGO != null)
        {
            Handles.color = Color.red;
            Handles.DrawLine(sense.transform.position, sense.targetGO.position);
        }
        else if (sense.soundSensed && sense.soundGO != null)
        {
            Handles.color = Color.yellow;
            Handles.DrawLine(sense.transform.position, sense.soundGO.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerZ, float angleInDegrees)
    {
        angleInDegrees += eulerZ;
        return new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad) * -1, Mathf.Sin(angleInDegrees * Mathf.Deg2Rad) * -1, 0);
    }
}