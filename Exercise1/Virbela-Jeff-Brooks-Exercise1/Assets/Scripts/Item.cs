using UnityEngine;


/// <summary>
/// Item inherits InteractiveObject and acts as the "Item" entity.
/// Rotates in place and changes color when activated.
/// </summary>
public class Item : InteractiveObject
{
    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private float _activatedRotationSpeed = 1f;
    [SerializeField] private Transform _rotationTransform;

    private float _currentRotationSpeed = 0f;

    /// <summary>
    /// Updates every frame, and calls the base update.
    /// Rotates the _rotationTransform when application is playing.
    /// </summary>
    protected override void Update()
    {
        base.Update();
        _currentRotationSpeed = Mathf.Lerp(_currentRotationSpeed, _rotationSpeed, .001f);
        if (_rotationTransform != null)
        {
            _rotationTransform.Rotate(0, 0f, _currentRotationSpeed);
        }
    }

    /// <summary>
    /// Called by the base class when this object is activated.
    /// Calls the base method and sets a new rotation speed.
    /// </summary>
    protected override void DidActivate()
    {
        base.DidActivate();
        _currentRotationSpeed = _activatedRotationSpeed;
    }
}
