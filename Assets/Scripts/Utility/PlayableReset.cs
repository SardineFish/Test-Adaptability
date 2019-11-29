using UnityEngine;
using System.Collections;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class PlayableReset : MonoBehaviour
{
    PlayableDirector director;
    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }
    public void ResetPlayable()
    {
        director.Stop();
        director.time = 0;
        director.Evaluate();
    }
}
