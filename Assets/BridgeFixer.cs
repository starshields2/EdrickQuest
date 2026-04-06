using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeFixer : MonoBehaviour
{
    public Transform _bridge1;
    public Transform _bridge2;
    public Transform _bridge3;

    public GameObject _bridgeMesh1;
    public GameObject _bridgeMesh2;
    public GameObject _bridgeMesh3;

    public EventOverworldLinkHandler _instructions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Awake()
    {
        FixBridge();
    }

    [ContextMenu("Fix the Bridge")]
    public void FixBridge()
    {
        _bridgeMesh1.transform.rotation = _bridge1.rotation;
        _bridgeMesh2.transform.rotation = _bridge2.rotation;
        _bridgeMesh3.transform.rotation = _bridge3.rotation;
        _bridgeMesh1.transform.position = _bridge1.position;
        _bridgeMesh2.transform.position = _bridge2.position;
        _bridgeMesh3.transform.position = _bridge3.position;
    }
}
