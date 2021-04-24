using System;
using Spine.Unity;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SkeletonAnimation))]
public class Animal : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int lives;
    [SerializeField] private float speed;
    [SerializeField] private float strength;
    [Tooltip("In seconds")]
    [SerializeField] private float timeForThrow;

    [Space] 
    [SerializeField] private Transform attackPoint;
    
    [Header("Animation")]
    [SpineAnimation,SerializeField] private string idleAnimation;
    [SpineAnimation,SerializeField] private string walkAnimation;
    [SpineAnimation,SerializeField] private string runAnimation;
    [SpineAnimation,SerializeField] private string getHitAnimation;

    //Cache
    private SkeletonAnimation skeletonAnimation;
    private Spine.AnimationState animationState;
    private Spine.Skeleton skeleton;
    
    private string currentAnimation = "";

    private Rigidbody2D rigidbody2D;
    
    public int Lives
    {
        get => lives;
        set
        {
            lives = value;

            if (lives <= 0)
            {
                Die();
            }
        }
    }

    protected float TimeForThrow => timeForThrow;

    protected virtual void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        skeleton = skeletonAnimation.Skeleton;
        animationState = skeletonAnimation.AnimationState;

        rigidbody2D = GetComponent<Rigidbody2D>();
        
        SetAnimation(idleAnimation, true, true);
    }

    protected virtual void Move(float horizontalSpeed)
    {
        float newSpeed = horizontalSpeed * speed * Time.deltaTime;
        bool looksRight = horizontalSpeed > 0;

        if (horizontalSpeed >= 0.5f || horizontalSpeed <= -0.5f)
        {
            SetAnimation(runAnimation, true, looksRight);
        }

        else if (horizontalSpeed >= 0.05f || horizontalSpeed <= -0.05f)
        {
            SetAnimation(walkAnimation, true, looksRight);
        }

        else
        {
            SetAnimation(idleAnimation, true, looksRight);
        }

        rigidbody2D.velocity = new Vector2(newSpeed, 0f);
    }

    protected virtual void ThrowBall(float throwStrength)
    {
        //Spawning snowball
        GameObject snowball = ObjectPooler.GetObject();
        snowball.SetActive(true);
        snowball.transform.position = attackPoint.position;
        
        snowball.GetComponent<Rigidbody2D>().velocity += new Vector2(0f, throwStrength * strength);
    }

    protected void SetAnimation(string animation, bool loop, bool looksRight)
    {
        skeleton.FlipX = !looksRight;

        if (currentAnimation.Equals(animation))
        {
            return;
        }

        animationState.SetAnimation(0, animation, loop);
        currentAnimation = animation;
    }
    
    protected virtual void Die()
    {
        Debug.Log("Animal: The animal was defeated");
    }
}
