using UnityEngine;


/// <summary>
/// Player class controls the player's movement and actions.
/// </summary>
public class Player: MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _acceleration = 2f;
    [SerializeField] private float _maxSpeed = 4f;
    [SerializeField] private float _rotationSpeed = 90f;
    [SerializeField] private AudioClip _spawnObjectSound;
    [SerializeField] private GameObject _botPrefab;
    [SerializeField] private GameObject _itemPrefab;
    
    private float _speed;
    private float _timeUntilMovement = 0f;

    /// <summary>
    /// Updates every frame. Calls input methods.
    /// </summary>
    private void Update()
    {
        ControlMovement();
        ControlSpawning();
    }

    /// <summary>
    /// Controls the character's movement and handles user movement input.
    /// </summary>
    private void ControlMovement()
    {
        if (_characterController != null && Time.time >= _timeUntilMovement)
        {
            // get the forward input
            var forwardInput = Input.GetAxisRaw("Vertical");

            // if there is no input, slow the player down
            if (forwardInput == 0f)
            {
                _speed = Mathf.Lerp(_speed, 0f, 0.5f);
            }
            // otherwise, set the speed, clamping between backwards speed and maximum speed.
            else
            {
                _speed = Mathf.Clamp(_speed + forwardInput * _acceleration, -_maxSpeed/2f, _maxSpeed);
            }

            // move the character controller.
            _characterController.SimpleMove(transform.forward * _speed);

            // get horizontal input and rotate the character.
            var horizontalRotation = Input.GetAxis("Horizontal") * Time.deltaTime * _rotationSpeed;
            transform.Rotate(new Vector3(0f, horizontalRotation, 0f));

            // finally call the animation
            Animate(_speed, forwardInput);
        }
    }

    /// <summary>
    /// Animates the player's model.
    /// </summary>
    /// <param name="speed">Player's speed to determine animation.</param>
    /// <param name="forwardInput">Player's forward input to determine animation.</param>
    private void Animate(float speed, float forwardInput)
    {
        if (_animator != null)
        {
            _animator.SetBool("walk", speed < 0 && forwardInput != 0f);
            _animator.SetBool("run", speed > 0 && forwardInput != 0f);
        }
    }

    /// <summary>
    /// Controls spawning of Items and Bots.
    /// </summary>
    private void ControlSpawning()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SpawnItem();
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            SpawnBot();
        }
    }

    /// <summary>
    /// Spawns an Item.
    /// </summary>
    private void SpawnItem()
    {
        SpawnObject(_itemPrefab);
    }

    /// <summary>
    /// Spawns a Bot.
    /// </summary>
    private void SpawnBot()
    {
        SpawnObject(_botPrefab);
    }

    /// <summary>
    /// Spawns a prefab provided in the parameters.
    /// Detects if there is a clear space in front of the player using Physics Sphere Overlap before spawning.
    /// </summary>
    /// <param name="prefab"></param>
    private void SpawnObject(GameObject prefab)
    {
        if (prefab != null)
        {
            /// setup the desired spawn position.
            var spawnPosition = new Vector3(0f, 1, 0f) + transform.position + transform.forward * 2f;

            // use physics to find any possible collisions.
            var overlaps = Physics.OverlapSphere(spawnPosition, 1f);

            // if collisions were empty, the space is empty and ready to spawn at that location.
            if (overlaps.Length == 0)
            {
                // set the player's speed to zero
                _speed = 0f;
                // stop the player from controlling movement temporarily so they don't run right into their object.
                _timeUntilMovement = Time.time + 0.25f;
                Instantiate(prefab, spawnPosition, Quaternion.identity);
                // plays the spawn object sound effect.
                if (_spawnObjectSound != null)
                {
                    AudioSource.PlayClipAtPoint(_spawnObjectSound, transform.position);
                }
            }
        }
    }
}
