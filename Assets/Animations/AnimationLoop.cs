using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationLoop : MonoBehaviour
{
    public float speed;
    public float pause = 0f;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("time",(speed* Time.time) % (1f + pause));
    }
}
