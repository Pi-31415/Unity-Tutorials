//Old dog controller for player point and click
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class DogController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;

    public AnimationReferenceAsset

            idle,
            walking;

    public string currentState;

    public float speed;

    public float rotate_scale;

    public float movement;

    public float player_position;

    private Rigidbody2D rigidbody;

    public string currentAnimation;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        currentState = "Idle";
        setCharacterState (currentState);
        PlayerPrefs.SetInt("dogWallHitTrigger", 0);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void setAnimation(
        AnimationReferenceAsset animation,
        bool loop,
        float timeScale
    )
    {
        if (animation.name.Equals(currentAnimation))
        {
            return;
        }
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale =
            timeScale;
        currentAnimation = animation.name;
    }

    public void setCharacterState(string state)
    {
        if (state.Equals("Idle"))
        {
            setAnimation(idle, true, 1f);
        }
        else if (state.Equals("Walking"))
        {
            setAnimation(walking, true, 1f);
        }
    }

    //Collision Detection
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            PlayerPrefs.SetInt("hittingWall", 1);
        }
    }

    //Collision Detection - Dog Only
    void OnCollisionEnter2D(Collision2D col)
    {
        //This is a separate animation controller for wall hitting for dog
        if (col.gameObject.CompareTag("Wall"))
        {
            PlayerPrefs.SetInt("dogWallHitTrigger", 1);
        }
    }

    //Collision Detection - Dog Only
    void OnCollisionExit2D(Collision2D col)
    {
        //This is a separate animation controller for wall hitting for dog
        if (col.gameObject.CompareTag("Wall"))
        {
            PlayerPrefs.SetInt("dogWallHitTrigger", 0);
        }
    }

    public void Move()
    {
        movement = PlayerPrefs.GetFloat("playerSpeed");
        player_position = PlayerPrefs.GetFloat("playerPosition");

        //Make dog go near player if too far
        if ((player_position - transform.position[0]) > 2.5)
        {
            movement = player_position - transform.position[0];
        }

        //Set Dog Speed according to player
        if (movement > 0)
        {
            speed = 3.0f;
            movement = 1;
        }
        else if (movement < 0)
        {
            speed = 3.5f;
            movement = (-1);
        }

        //Stop the Dog when the wall collision occurs on either dog or girl
        if (PlayerPrefs.GetInt("dogWallHitTrigger") == 0)
        {
            rigidbody.velocity =
                new Vector2(movement * speed, rigidbody.velocity.y);
        }
        else
        {
            rigidbody.velocity = new Vector2(0.0f, 0.0f);
        }

        //Set animation state accordingly
        if (movement != 0)
        {
            if (PlayerPrefs.GetInt("dogWallHitTrigger") == 0)
            {
                setCharacterState("Walking");
            }
            else
            {
                setCharacterState("Idle");
            }
            if (movement > 0)
            {
                transform.localScale = new Vector2(rotate_scale, rotate_scale);
            }
            else
            {
                transform.localScale = new Vector2(-rotate_scale, rotate_scale);
            }
        }
        else
        {
            setCharacterState("Idle");
        }
    }
}
