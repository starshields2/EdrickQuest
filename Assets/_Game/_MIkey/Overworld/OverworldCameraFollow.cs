using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _playerfollow;
    private OverworldPlayerMovement _Player;
    private bool isFacingRight;
    [SerializeField] private float flipRotationTime;
    private Coroutine turnCo;

    void Awake()
    {
        _Player = _playerfollow.gameObject.GetComponent<OverworldPlayerMovement>();
        isFacingRight = _Player.isFacingRight;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void CallTurn()
    {
        turnCo = StartCoroutine(FlipYLerp());
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = _playerfollow.position;
    }

    public IEnumerator FlipYLerp()
    {
        float startRot = transform.localEulerAngles.y;
        float endRotAmount = DetermineEndRot();
        float yRot = 0f;
        float elapsedTime = 0f;
        while(elapsedTime < flipRotationTime)
        {
            elapsedTime += Time.deltaTime;
            yRot = Mathf.Lerp(startRot, endRotAmount, (elapsedTime / flipRotationTime));
            transform.rotation = Quaternion.Euler(0f, yRot, 0f);
            yield return null;
        }
    }

    private float DetermineEndRot()
    {
        isFacingRight = !isFacingRight;
        if (isFacingRight)
        {
            return -180f;
        }
        else
        {
            return 0f;
        }
    }
}
