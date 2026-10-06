using UnityEngine;

public class PlatformTilt : MonoBehaviour
{
    public float interval = 5f;
    public float maxAngle = 10f;
    public float smoothSpeed = 1f;

    private Quaternion _target;
    private Quaternion _initial;
    private float _timer;

    void Start()
    {
        _initial = transform.rotation;
        _target = _initial;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= interval)
        {
            _timer = 0f;
            float x = Random.Range(-maxAngle, maxAngle);
            float z = Random.Range(-maxAngle, maxAngle);
            _target = _initial * Quaternion.Euler(x, 0, z);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, _target, Time.deltaTime * smoothSpeed);
    }

    public void ResetPlatform()
    {
        transform.rotation = _initial;
        _target = _initial;
        _timer = 0f;
    }
}