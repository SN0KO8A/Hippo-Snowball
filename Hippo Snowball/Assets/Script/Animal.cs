using System;
using System.Collections;
using Spine.Unity;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SkeletonAnimation))]
public class Animal : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int lives;
    [SerializeField] private float speed;
    [SerializeField] private float strength;

    [Space] 
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform attackPoint;
    
    [Header("Animation")]
    [SpineAnimation,SerializeField] private string idleAnimation;
    [SpineAnimation,SerializeField] private string walkAnimation;
    [SpineAnimation,SerializeField] private string runAnimation;
    [SpineAnimation,SerializeField] private string getHitAnimation;
    
    //States
    private bool canMove = true;
    private bool deffeated = false;
    
    //Cache
    private SkeletonAnimation skeletonAnimation;
    private Spine.AnimationState animationState;
    private Spine.Skeleton skeleton;
    
    private string currentAnimation = "";
    private Rigidbody2D rigidbody2D;

    protected virtual void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        skeleton = skeletonAnimation.Skeleton;
        animationState = skeletonAnimation.AnimationState;

        rigidbody2D = GetComponent<Rigidbody2D>();
        
        SetAnimation(idleAnimation, true, skeleton.FlipX);
    }

    protected virtual void Move(float horizontalSpeed)
    {
        if (canMove)
        {
            float newSpeed = horizontalSpeed * speed;
            bool looksRight = horizontalSpeed < 0;

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
                SetAnimation(idleAnimation, true, skeleton.FlipX);
            }

            rigidbody2D.velocity = new Vector2(newSpeed, 0f);
        }
    }

    protected virtual void ThrowBall(float throwStrength)
    {
        //Spawning snowball
        GameObject snowball = ObjectPooler.GetObject(bullet);
        snowball.SetActive(true);
        snowball.transform.position = attackPoint.position;
        
        snowball.GetComponent<Rigidbody2D>().velocity += new Vector2(0f, throwStrength * strength);
    }

    protected void SetAnimation(string animation, bool loop, bool looksRight)
    {
        skeleton.FlipX = looksRight;

        if (currentAnimation.Equals(animation))
        {
            return;
        }

        animationState.SetAnimation(0, animation, loop);
        currentAnimation = animation;
    }

    //<summary> Animal gets hit, and it loose one life
    public virtual void ToGetHit()
    {
        lives--;
        StartCoroutine(StunEffect());
        
        if (lives <= 0)
        {
            Die();
        }
    }

    private IEnumerator StunEffect()
    {
        canMove = false;
        SetAnimation(getHitAnimation, false, skeleton.FlipX);
        yield return new WaitForSeconds(skeleton.Data.FindAnimation(getHitAnimation).Duration);
        canMove = true;
    }

    protected virtual void Die()
    {
        deffeated = true;
        Debug.Log("Animal: The animal was defeated");
    }
}
