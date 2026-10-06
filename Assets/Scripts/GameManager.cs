using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Rigidbody ball;
    public Transform startPoint;
    public PlatformTilt platform;
    public TextMeshProUGUI counterText;
    public GameObject winText;
    public float fallY = -10f;

    private int _collected = 0;
    private const int Total = 3;

    private GameObject[] _collectibles;
    private Transform[] _pillars;
    private Vector3[] _pillarPos;
    private Quaternion[] _pillarRot;

    void Start()
    {
        _collectibles = GameObject.FindGameObjectsWithTag("Collectible");

        GameObject[] p = GameObject.FindGameObjectsWithTag("Pillar");
        _pillars = new Transform[p.Length];
        _pillarPos = new Vector3[p.Length];
        _pillarRot = new Quaternion[p.Length];
        for (int i = 0; i < p.Length; i++)
        {
            _pillars[i] = p[i].transform;
            _pillarPos[i] = p[i].transform.localPosition;
            _pillarRot[i] = p[i].transform.localRotation;
        }

        UpdateUI();
        winText.SetActive(false);
    }

    void Update()
    {
        if (ball.position.y < fallY) ResetBall();
    }

    public void Collect(GameObject item)
    {
        item.SetActive(false);
        _collected++;
        UpdateUI();
    }

    public void ReachGoal()
    {
        if (_collected >= Total) winText.SetActive(true);
    }

    void UpdateUI()
    {
        counterText.text = "Objetos: " + _collected + "/" + Total;
    }

    void ResetBall()
    {
        ball.linearVelocity = Vector3.zero;  
        ball.angularVelocity = Vector3.zero;
        ball.position = startPoint.position;
    }

    public void RestartGame()
    {
        platform.ResetPlatform();
        ResetBall();

        foreach (var c in _collectibles) c.SetActive(true);
        _collected = 0;

        for (int i = 0; i < _pillars.Length; i++)
        {
            Rigidbody rb = _pillars[i].GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            _pillars[i].localPosition = _pillarPos[i];
            _pillars[i].localRotation = _pillarRot[i];
        }

        winText.SetActive(false);
        UpdateUI();
    }
}