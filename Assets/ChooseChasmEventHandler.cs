using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseChasmEventHandler : MonoBehaviour
{
    public EventOverworldLinkHandler _linkHandler;
    public TextAsset _specialEventToLoad;
    public TextAsset[] _possibleStories;
    public MediationDialogue _eventToThread;
    public EventTrigger_Overworld _chasmEvent;

    public bool jasper;
    public bool yael;
    // Start is called before the first frame update
    void Start()
    {
        //_storyToLoad = null;
    }

    // Update is called once per frame
    void Update()
    {
        jasper = _linkHandler.JasperSolution;
        yael = _linkHandler.YaelSolution;
    }

    [ContextMenu("Load Variable Story")]
    public void ThreadEvent()
    {
        Debug.Log("Threading Event...");
        if (_linkHandler.JasperSolution)
        {
            _specialEventToLoad = _possibleStories[0];
        }
        if (_linkHandler.YaelSolution)
        {
           _specialEventToLoad = _possibleStories[1];
        }

        LoadStory();
    }

    private void LoadStory()
    {
        _eventToThread.inkJSONAsset = _specialEventToLoad;
        Debug.Log("Loaded: " + _specialEventToLoad);
        _chasmEvent._storyToLoad = _specialEventToLoad;
    }
}
