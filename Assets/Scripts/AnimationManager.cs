using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class AnimationManager : MonoBehaviour
{
    [System.Serializable]
    public class AnimState
    {
        public bool isActive;
        public bool isLoco;
        public bool isPlaying;
        public string name;
        public AnimationClip sClip;
        public float fadeLength = 0.25f;
        public FadeMode fadeMode;
        public Easing.Function easeFunct;
        [Header("If Locomotion:")]
        public int locoIndex;
        public AnimationClip[] locoSeq;
        public float[] locoFadeLength;
        public Easing.Function[] locoEaseFunct;
        
    }
    
    public AnimancerComponent animancerState;
    public AnimState[] fStates;
    private bool clearCurrentState;
    [SerializeField]
    private AnimState activeState;
    [SerializeField]
    private AnimState pastState;

    void Update()
    {
        if(!clearCurrentState)
        {
            for (int i = 0; i < fStates.Length; i++)
            {
                if (fStates[i].isActive)
                {
                    activeState = fStates[i];
                    if (activeState.isLoco)
                    {
                        break;
                    }
                    else
                    {
                        if (!activeState.isPlaying)
                        {
                            
                            var state = animancerState.Play(activeState.sClip, activeState.fadeLength, activeState.fadeMode);
                            fStates[i].isPlaying = true;
                            CustomFade.Apply(state, activeState.easeFunct);
                        }
                    }
                }
            }
        }
    }

    public void PlayState(int i, bool isLoco, bool clearFlags, int activeSpoke)
    {
        PurgeStates();
        clearCurrentState = clearFlags;
        fStates[i].isActive = true;
        if (isLoco)
        {
            var state = animancerState.Play(fStates[i].locoSeq[fStates[i].locoIndex], fStates[i].locoFadeLength[fStates[i].locoIndex], fStates[i].fadeMode);
            CustomFade.Apply(state, fStates[i].locoEaseFunct[fStates[i].locoIndex]);
            fStates[i].locoIndex = activeSpoke;
            
        }


    }

    public void PurgeStates()
    {
        for(int i = 0; i<fStates.Length; i++)
        {
            fStates[i].isActive = false;
            fStates[i].isPlaying = false;
        }
    }


}
