using UnityEngine;


/// <summary>
/// Bot inherits InteractiveObject and acts as the "Bot" entity.
/// Hops in place and changes color when activated.
/// </summary>
public class Bot : InteractiveObject
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _activationJumpForce = 1000f;

    /// <summary>
    /// Called by the base class when this object is activated.
    /// </summary>
    protected override void DidActivate()
    {
        base.DidActivate();
        if  (_rigidBody != null)
        {
            _rigidBody.velocity = Vector3.zero;
            if (Physics.Raycast(transform.position, Vector3.down, 0.5f))
            {
                _rigidBody.AddForce(new Vector3(0f, _activationJumpForce, 0f));
            }
        }
    }
}
