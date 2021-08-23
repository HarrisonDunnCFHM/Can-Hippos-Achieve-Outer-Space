using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Braincloud : MonoBehaviour
{
    //config params
    [SerializeField] float moveSpeed;
    [SerializeField] float attackRange = 1f;

    //cached references
    HippoRocket hippoRocket;
    bool fireShot;

    // Start is called before the first frame update
    void Start()
    {
        hippoRocket = FindObjectOfType<HippoRocket>();
        fireShot = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        FacePlayer();
    }



    private void FacePlayer()
    {
        var distToRocket = transform.position.x - hippoRocket.transform.position.x;
        if (distToRocket > 0 && transform.localScale.x > 0)
        {
            var newFace = transform.localScale.x * -1;
            transform.localScale = new Vector2(newFace, transform.localScale.y);
        }
        if (distToRocket < 0 && transform.localScale.x < 0)
        {
            var newFace = transform.localScale.x * -1;
            transform.localScale = new Vector2(newFace, transform.localScale.y);
        }
    }

    private void Move()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - (Time.deltaTime * moveSpeed));
    }
}
