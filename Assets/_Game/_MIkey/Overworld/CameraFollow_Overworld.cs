using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow_Overworld : MonoBehaviour
{
    [SerializeField] private Transform _playerfollow;
    private PlayerMovement_Overworld _Player;
    private bool isFacingRight;
    [SerializeField] private float flipRotationTime;
    private Coroutine turnCo;

    void Awake()
    {
        _Player = _playerfollow.gameObject.GetComponent<PlayerMovement_Overworld>();
        isFacingRight = _Player.isFacingRight;
    }
    void Start()
    {
        
    }
    public void CallTurn()
    {
        turnCo = StartCoroutine(FlipYLerp());
    }
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
