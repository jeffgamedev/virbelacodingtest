using System.Collections;
using UnityEngine;


/// <summary>
/// InteractiveObject represents objects that are tracked by the MoveListener.
/// Changes color when directed by the MoveListener to do so.
/// </summary>
public class InteractiveObject : MonoBehaviour
{
    [SerializeField] private Color _activatedColored;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private AudioClip _activatedSound;

    private Color _targetColor;
    private Color _currentColor;
    private bool _registered;

    /// <summary>
    /// Called when the object is enabled. Sets default values and calls the RegisterObject coroutine.
    /// </summary>
    private void OnEnable()
    {
        // check that there is a valid renderer and shared material.
        if (_renderer != null && _renderer.sharedMaterial != null)
        {
            // re-create the shared material for independent material color changing.
            _renderer.sharedMaterial = new Material(_renderer.sharedMaterial);
        }
        // set the target color to the default color
        _targetColor = _defaultColor;
        SetColor(_defaultColor);
        StartCoroutine(RegisterObject());
    }

    /// <summary>
    /// Register this interactive object with the MoveListener.
    /// Utilizes coroutine to avoid race conditions if the MoveListener instance has not been set.
    /// Will attempt to register until successful.
    /// </summary>
    /// <returns></returns>
    private IEnumerator RegisterObject()
    {
        // while the object is not registered with the MoveListener, try to register it.
        while (!_registered)
        {
            // Register with the MoveListener. Upon success, stop attempting to register.
            _registered = MoveListener.RegisterObject(this);
            yield return null;
        }
    }

    /// <summary>
    /// Called when the object is disabled. Unregisters the object from the MoveListener.
    /// </summary>
    private void OnDisable()
    {
        // Unregister 
        _registered = false;
        MoveListener.UnregisterObject(this);
    }

    /// <summary>
    /// Sets the color of the object on the renderer's shared material.
    /// </summary>
    /// <param name="color">Color to set the renderer's shared material to.</param>
    private void SetColor(Color color)
    {
        if (_renderer != null && _renderer.sharedMaterial != null)
        {
            _renderer.sharedMaterial.color = color;
        }
    }

    /// <summary>
    /// Receives and sets the state of the object.
    /// </summary>
    /// <param name="isActivated"></param>
    public void SendState(bool isActivated)
    {
        if (isActivated && _targetColor != _activatedColored)
        {
            _targetColor = _activatedColored;
            DidActivate();
        }
        else if (!isActivated && _targetColor != _defaultColor)
        {
            _targetColor = _defaultColor;
        }
    }

    /// <summary>
    /// Virtual method that is called when an object is activated. Can be overriden for additional functionality.
    /// Base functionality plays the activation sound effect.
    /// </summary>
    protected virtual void DidActivate()
    {
        PlayActivationSound();
    }

    /// <summary>
    /// Plays the activation sound effect.
    /// </summary>
    private void PlayActivationSound()
    {
        if (_activatedSound != null && Time.time > 0f)
        {
            AudioSource.PlayClipAtPoint(_activatedSound, transform.position);
        }
    }

    /// <summary>
    /// Updates every frame, even in edit mode.
    /// Can be overridden in extended classes to expand functionality.
    /// </summary>
    protected virtual void Update()
    {
        TweenColor();
    }

    /// <summary>
    /// Tweens the current color of the object to the target color.
    /// </summary>
    private void TweenColor()
    {
        _currentColor = Color.Lerp(_currentColor, _targetColor, .015f);
        SetColor(_currentColor);
    }
}
