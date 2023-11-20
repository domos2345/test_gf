using System;
using UnityEngine;
using System.Collections;
using Infima_Games.Low_Poly_Shooter_Pack___Free_Sample.Code.Upgrades;
using Random = UnityEngine.Random;

public class TargetScript : MonoBehaviour
{
    float randomTime;
    bool routineStarted = false;

    //Used to check if the target has been hit
    public bool isHit = false;

    [Header("Customizable Options")]
    //Decides if the target is stationary or moving
    public bool isStationary = true;

    //Minimum time before the target goes back up
    public float minTime;

    //Maximum time before the target goes back up
    public float maxTime;

    [Header("Audio")] public AudioClip upSound;
    public AudioClip downSound;

    [Header("Animations")] public AnimationClip targetUp;
    public AnimationClip targetDown;

    public AudioSource audioSource;

    // collider for sides of the object to decide, where (not) to move.
    [SerializeField] private BoxCollider leftSide;
    [SerializeField] private BoxCollider rightSide;

    // speed of target movement
    [SerializeField] private float speed;

    private float timeToDecideDirection;
    private bool isMovingRight;
    private Transform _transform;
    private System.Random randomGenerator = new System.Random();

    // loot drop
    private UpgradeOrbHolder upgradeOrbHolder;


    private void Awake()
    {
        _transform = GetComponent<Transform>();
        upgradeOrbHolder = FindObjectOfType<UpgradeOrbHolder>();
    }


    private void Update()
    {
        //Generate random time based on min and max time values
        randomTime = Random.Range(minTime, maxTime);

        //If the target is hit
        if (isHit)
        {
            if (routineStarted == false)
            {
                //Animate the target "down"
                gameObject.GetComponent<Animation>().clip = targetDown;
                gameObject.GetComponent<Animation>().Play();
                upgradeOrbHolder.DropRandomOrb(_transform.position);

                //Set the downSound as current sound, and play it
                audioSource.GetComponent<AudioSource>().clip = downSound;
                audioSource.Play();

                //Start the timer
                StartCoroutine(DelayTimer());
                routineStarted = true;
            }
        }

        // Every 1 to 3 seconds decide randomly direction to move to
        if (timeToDecideDirection <= 0)
        {
            isMovingRight = randomGenerator.Next(0, 2) == 0;
            timeToDecideDirection = 1 + (float) randomGenerator.NextDouble() * 2;
        }

        timeToDecideDirection -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (isStationary || isHit)
        {
            return;
        }

        _transform.Translate(isMovingRight
            ? Vector3.right * Time.deltaTime * speed
            : Vector3.left * Time.deltaTime * speed);
    }

    //Time before the target pops back up
    private IEnumerator DelayTimer()
    {
        //Wait for random amount of time
        yield return new WaitForSeconds(randomTime);
        //Animate the target "up" 
        gameObject.GetComponent<Animation>().clip = targetUp;
        gameObject.GetComponent<Animation>().Play();

        //Set the upSound as current sound, and play it
        audioSource.GetComponent<AudioSource>().clip = upSound;
        audioSource.Play();

        //Target is no longer hit
        isHit = false;
        routineStarted = false;
    }


    // Move to the opposite direction of collision
    public void HitSide(bool isRightSide)
    {
        isMovingRight = !isRightSide;
    }
}