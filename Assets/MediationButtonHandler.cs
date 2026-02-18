using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediationButtonHandler : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    [ContextMenu("PlayPing")]
    public void PlayPing()
    {
        anim.SetBool("isTriggered", true);
    }
}
