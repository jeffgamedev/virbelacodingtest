using UnityEngine;


/// <summary>
/// Detects change on this GameObject's transform.
/// Reports to the MoveListener when a change is detected.
/// </summary>
public class MoveDetector : MonoBehaviour
{
    /// <summary>
    /// Updates this behaviour every frame, even when in edit mode.
    /// If a transform has been changed, it will report it to the MoveListener.
    /// </summary>
    private void Update()
    {
        if (transform.hasChanged)
        {
            transform.hasChanged = false;
            MoveListener.ReportMoveDetected();
        }
    }
}
