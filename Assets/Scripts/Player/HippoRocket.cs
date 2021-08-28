using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HippoRocket : MonoBehaviour
{
    //config parameters
    [SerializeField] float moveSpeedBase;
    [SerializeField] float xScreenPadding = 1f;
    [SerializeField] float yTopScreenPadding = 3f;
    [SerializeField] float yBotScreenPadding = 1f;
    [SerializeField] GameObject myEngines;
    [SerializeField] float gravity;
    [SerializeField] float gravityDelay = 1f;
    [SerializeField] TokenCollector tokenCollector;
    [SerializeField] AudioSource myAudioSource;
    [SerializeField] float speedMultiplier = 1.3f;
    [SerializeField] AudioClip[] myCrashClips;



    //cached references
    HealthManager myHealth;
    AudioManager audioManager;
    float moveSpeed;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float deltaX;
    float deltaY;
    float newXPos;
    float newYPos;
    float newZRot;
    float deltaXLast;
    bool ascending;
    bool blastOff;
    bool gravityOn;

    // Start is called before the first frame update
    void Start()
    {
        myHealth = GetComponent<HealthManager>();
        SetUpMoveBoundaries();
        moveSpeed = moveSpeedBase;
        ascending = true;
        myEngines.SetActive(false);
        gravityOn = false;
        audioManager = FindObjectOfType<AudioManager>();
        myAudioSource.volume = audioManager.effectVolume * audioManager.masterVolume;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.GetComponent<ObstacleDestroyer>()) { return; }
        if (!ascending) { return; }
        bool collectedToken = tokenCollector.CollectToken(collision.gameObject);
        if (collectedToken) { return; }
        int clipToPlay = UnityEngine.Random.Range(0, myCrashClips.Length);
        if (ascending)
        { AudioSource.PlayClipAtPoint(myCrashClips[clipToPlay], Camera.main.transform.position, audioManager.effectVolume * audioManager.masterVolume); }
        myHealth.TakeHit();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!ascending) { return; }
        bool collectedToken = tokenCollector.CollectToken(other.gameObject);
        if (collectedToken) { return; }
        int clipToPlay = UnityEngine.Random.Range(0, myCrashClips.Length);
        if (ascending)
        { AudioSource.PlayClipAtPoint(myCrashClips[clipToPlay], Camera.main.transform.position, audioManager.effectVolume * audioManager.masterVolume); }
        myHealth.TakeHit();
    }



    // Update is called once per frame
    void Update()
    {
        if (!blastOff) { return; }
        ControlShip();
        if (gravityOn) { transform.position = new Vector2(transform.position.x, transform.position.y - (gravity * Time.deltaTime)); }
        if (!ascending)
        {
            myAudioSource.volume -= Time.deltaTime;
        }
        if (Time.timeScale == 0)
        {
            myAudioSource.volume = 0f;
        }
        else
        {
            myAudioSource.volume = audioManager.effectVolume * 0.5f;
        }
    }

    private void ControlShip()
    {
        if (!ascending) return;
        deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        if (deltaY != 0 && myAudioSource.volume * 0.5f < audioManager.effectVolume * speedMultiplier * audioManager.masterVolume)
        { 
            gravityOn = true;
            myAudioSource.volume += Time.deltaTime;
        }
        else if (deltaX != 0 && myAudioSource.volume * 0.5f < audioManager.effectVolume * speedMultiplier * audioManager.masterVolume)
        {
            myAudioSource.volume += Time.deltaTime;
        }
        else if (myAudioSource.volume * 0.5f > audioManager.effectVolume * audioManager.masterVolume) 
        {
            myAudioSource.volume -= Time.deltaTime;
            if(myAudioSource.volume * 0.5f < audioManager.effectVolume * audioManager.masterVolume)
            {
                myAudioSource.volume = audioManager.effectVolume * audioManager.masterVolume * 0.5f;
            }
        }
        
        newXPos = Mathf.Clamp(transform.position.x, xMin, xMax) + deltaX;
        newZRot = Mathf.Clamp(transform.rotation.z - (deltaX / moveSpeed), -.25f,.25f);
        newYPos = Mathf.Clamp(transform.position.y, yMin, yMax) + deltaY;
        transform.position = new Vector2(newXPos, newYPos);
        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, newZRot, transform.rotation.w);
        if(deltaX > 0 && deltaX < deltaXLast)
        {
            if (transform.rotation.z < 0)
            {
                var resetZRot = transform.rotation.z + Time.deltaTime / moveSpeed;
                transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, resetZRot, transform.rotation.w);
            }
            if (Mathf.Approximately(transform.rotation.z, 0.0f))
            {
                transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0.0f, transform.rotation.w);
            }
        }
        else if (deltaX < 0 && deltaX > deltaXLast)
        {
            if (transform.rotation.z > 0)
            {
                var resetZRot = transform.rotation.z - Time.deltaTime / moveSpeed;
                transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, resetZRot, transform.rotation.w);
            }
            if (Mathf.Approximately(transform.rotation.z, 0.0f))
            {
                transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0.0f, transform.rotation.w);
            }
        }
        else if (deltaX == 0)
        {
            if(transform.rotation.z > 0)
            {
                var resetZRot = transform.rotation.z - Time.deltaTime/2;
                transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, resetZRot, transform.rotation.w);
            }
            if(transform.rotation.z < 0)
            {
                var resetZRot = transform.rotation.z + Time.deltaTime/2;
                transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, resetZRot, transform.rotation.w);
            }
            if (Mathf.Approximately(transform.rotation.z, 0.0f))
            {
                transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0.0f, transform.rotation.w);
            }
        }
        deltaXLast = deltaX;
    }

    public void BlastOff()
    {
        blastOff = true;
        myEngines.SetActive(true);
        StartCoroutine(StartGravity());
        myAudioSource.Play();
    }

    private IEnumerator StartGravity()
    {
        yield return new WaitForSeconds(gravityDelay);
        gravityOn = true;
    }

    public void StopEngines()
    {
        myEngines.SetActive(false);
        ascending = false;
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xScreenPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xScreenPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3 (0, 0, 0)).y + yBotScreenPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yTopScreenPadding;

    }
}
