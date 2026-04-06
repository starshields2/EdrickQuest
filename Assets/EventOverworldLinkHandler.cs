using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class EventOverworldLinkHandler : MonoBehaviour
{
    public GameObject _OverworldObjectYael;
    public GameObject _OverworldObjectJasper;
    public MediationDialogue _EventToRead;
    public bool _startResolve;
    public TextAsset inkJSONAsset = null;
    public Story story;
    public string _eventToHandle;
    public bool JasperSolution;
    public bool YaelSolution;

    // Start is called before the first frame update
    void Start()
    {

        
    }

     void InitalizeVariables()
    {

        JasperSolution = _EventToRead.jasperWins;
        YaelSolution = _EventToRead.yaelWins;
    }

    // Update is called once per frame
    void Update()
    {
        inkJSONAsset = _EventToRead.inkJSONAsset;
        if (_EventToRead.pass)
        {
         
            if(inkJSONAsset.name == "Bridge")
            {
                Debug.Log("Passed Event: " + _EventToRead);
                if (!_startResolve)
                {
                    InitalizeVariables();
                    _startResolve = true;
                    ResolveEvent();
                }

            }

        }
    }

    public void ResolveEvent()
    {

        if (JasperSolution)
        {
         _OverworldObjectJasper.SetActive(true);
          this.gameObject.SetActive(false);
        }
        if (YaelSolution)
        {
         _OverworldObjectYael.SetActive(true);
          this.gameObject.SetActive(false);
        }

    }
}
