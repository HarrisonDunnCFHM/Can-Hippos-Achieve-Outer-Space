using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HippoProfile : MonoBehaviour
{
    Animator myAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFear(bool fear)
    {
        myAnimator.SetBool("scared", fear); 
    }

    public void SetLaugh(bool happy)
    {
        myAnimator.SetBool("happy", happy);

    }

    public void SetTalk(bool talk)
    {
        myAnimator.SetBool("talking", talk);
    }

}
