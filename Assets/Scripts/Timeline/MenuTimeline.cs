using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class MenuTimeline : MonoBehaviour
{
    private PlayableDirector playableDirector;
    private void Awake()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }
    private void Start()
    {
        GameManager.Instance.OnClickStartButton += Instance_OnClickStartButton;
    }
    private void Instance_OnClickStartButton()
    {
        StopTimeline();
    }
    private void StopTimeline()
    {
        if (playableDirector != null)
            playableDirector.Stop();

    }
}
