using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QTESys : MonoBehaviour
{
    public GameObject DBox;
    public GameObject PassBox;
    public Transform visual;

    public int QTEGen;

    public bool CorrectKeyPressed;
    public bool countdownPassed;
    public bool CountingDown;
    public bool startQTE;
    public bool startKeyWait;
    public bool anyKeyPressed;

    public TensionCounter _tensionHandler;
    public float timeToPress;

    public enum QTEState
    {
        Start,
        WaitingForKey,
        Pass,
        Fail,
    }

    public QTEState _state;

    void Update()
    {
        // Adjust time based on tension
        if (_tensionHandler.highTension)
        {
            timeToPress = 1.5f;
            Debug.Log("Time to press: " + timeToPress);
        }
        else if (_tensionHandler.lowTension)
        {
            timeToPress = 4f;
            Debug.Log("Time to press: " + timeToPress);
        }

        switch (_state)
        {
            case QTEState.Start:
                Debug.Log("QTE: START");
                if (startQTE)
                {
                    startQTE = false;
                    QTEStartLogic();
                }
                break;

            case QTEState.WaitingForKey:
                Debug.Log("QTE: WAIT FOR KEY");
                if (startKeyWait)
                {
                    StartKeyWaitLogic();
                }
                if (countdownPassed)
                {
                    _state = QTEState.Fail;
                }
                
                break;

            case QTEState.Pass:
                Debug.Log("QTE: PASS");
                StartCoroutine(DelayedReset());
                break;

            case QTEState.Fail:
                Debug.Log("QTE: FAIL");
                StartCoroutine(DelayedReset());
                break;
        }
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(timeToPress);
        if (Input.anyKeyDown)
        {
            Debug.Log("Keydown");
            if (QTEGen == 1)
                CorrectKeyPressed = Input.GetKeyDown(KeyCode.H);
            else if (QTEGen == 2)
                CorrectKeyPressed = Input.GetKeyDown(KeyCode.J);
            else if (QTEGen == 3)
                CorrectKeyPressed = Input.GetKeyDown(KeyCode.K);

            _state = CorrectKeyPressed ? QTEState.Pass : QTEState.Fail;
            if (_state == QTEState.Pass)
            {
                PassBox.GetComponent<TextMeshProUGUI>().text = "PASS";
                yield return new WaitForSeconds(0.5f);
            }
            else if (_state == QTEState.Fail)
            {
                PassBox.GetComponent<TextMeshProUGUI>().text = "FAIL";
                yield return new WaitForSeconds(0.5f);
            }

            PassBox.GetComponent<TextMeshProUGUI>().text = "";
            DBox.GetComponent<TextMeshProUGUI>().text = "";
            CorrectKeyPressed = false;

            yield return new WaitForSeconds(0.5f);
            StartCoroutine(DelayedReset());
        }
        else
        {
            countdownPassed = true;
        }

        

    }

    void StartKeyWaitLogic()
    {
        startKeyWait = false;
        if (!CountingDown)
        {
            CountingDown = true; 
            StartCoroutine(Countdown());
        }

        
    }

    IEnumerator KeyPressing()
    {
        yield return null;
    }

    IEnumerator DelayedReset()
    {
        yield return new WaitForSeconds(0.5f);
        ResetQTE();
    }

    void ResetQTE()
    {
        visual.localScale = new Vector3(3f, 3f, 0f);
        DBox.GetComponent<TextMeshProUGUI>().text = "";
        PassBox.GetComponent<TextMeshProUGUI>().text = "";
        CorrectKeyPressed = false;
        CountingDown = false;
        countdownPassed = false;

        StartCoroutine(EnableStartQTE());
    }

    IEnumerator EnableStartQTE()
    {
        yield return new WaitForSeconds(0.1f);
        startQTE = true;
        _state = QTEState.Start;
    }

    public void ScaleVisual()
    {
        float elapsed = 0f;
        Vector3 startScale = new Vector3(3f, 3f, 0f);
        Vector3 endScale = new Vector3(1.2f, 1.2f, 0f);

        float t = timeToPress;
        visual.localScale = Vector3.Lerp(startScale, endScale, t);
        elapsed += Time.deltaTime;
       // yield return null;
        visual.localScale = endScale;
    }

    public void QTEStartLogic()
    {
       
        QTEGen = Random.Range(1, 4); // Randomize key
        if (QTEGen == 1)
            DBox.GetComponent<TextMeshProUGUI>().text = "[H]";
        else if (QTEGen == 2)
            DBox.GetComponent<TextMeshProUGUI>().text = "[J]";
        else if (QTEGen == 3)
            DBox.GetComponent<TextMeshProUGUI>().text = "[K]";
        _state = QTEState.WaitingForKey;
        startKeyWait = true;
    }
}
