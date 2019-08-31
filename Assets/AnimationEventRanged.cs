using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventRanged : MonoBehaviour
{
    public void FootStep()
    {
        GetComponent<AudioSource>().Play();
    }
}
