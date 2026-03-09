using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventOverworldLinkHandler : MonoBehaviour
{
    public GameObject _OverworldObject;
    public MediationDialogue _EventToRead;
    public bool _startResolve;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_EventToRead.pass)
        {
            if (!_startResolve)
            {
                _startResolve = true;
                ResolveEvent();
            }
        }
    }

    public void ResolveEvent()
    {
        _OverworldObject.SetActive(true);
    }
}
