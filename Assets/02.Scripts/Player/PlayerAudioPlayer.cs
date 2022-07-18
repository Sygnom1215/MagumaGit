using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioPlayer : AudioPlayer
{
    [SerializeField]
    protected AudioClip _jumpClip;

    public void PlayJumpSound()
    {
        PlayClipWithVariablePitch(_jumpClip);
    }

}
