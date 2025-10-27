using UnityEngine;

public class CarDriveAway : MonoBehaviour
{
    [Tooltip("Speed at which the car moves forward")]
    public float driveSpeed = 5f;

    [Tooltip("Delay before driving starts (seconds)")]
    public float delayBeforeDrive = 1.5f;

    [Tooltip("Sound or particle system for flair (optional)")]
    public AudioSource engineSound;

    private bool shouldDrive = false;
    private float timer = 0f;

    void Update()
    {
        if (shouldDrive)
        {
            // Move forward in the car's local forward direction
            transform.Translate(Vector3.forward * driveSpeed * Time.deltaTime, Space.Self);
        }
    }

    public void StartDrive()
    {
        Invoke(nameof(BeginDrive), delayBeforeDrive);
    }

    void BeginDrive()
    {
        shouldDrive = true;
        if (engineSound != null) engineSound.Play();
    }
}
