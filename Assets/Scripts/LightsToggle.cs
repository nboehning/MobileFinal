using UnityEngine;
using System.Collections;

public class LightsToggle : MonoBehaviour {

    public bool IsOn { get; private set; }

    Animator animator;

    void Start()
    {
        IsOn = false;
        animator = GetComponent<Animator>();
        enabled = false;
    }

	void LateUpdate ()
    {
        IsOn = !IsOn;
        animator.SetBool("IsOn", IsOn);
        enabled = false;
	}
}
