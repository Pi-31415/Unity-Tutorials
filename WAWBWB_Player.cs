//Point and click movement with lagging

using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Point and Click Controller Variables
    public float speed = 4f;

    Vector2 lastClickedPos;

    bool moving;

    //Footstep Sounds
    public AudioSource footstep_wood;

    //Rigidbody Controllers
    private Rigidbody2D rigidbody;

    //Animation Controller
    public SkeletonAnimation skeletonAnimation;

    //Animation Reference Assets - Export them
    public AnimationReferenceAsset

            idle,
            walking;

    public string currentState;

    //For flipping the character, scale accordingly to original prefab
    public float rotate_scale = 0.4f;

    public string currentAnimation;

    //Facing Right or left
    bool facing_right = true;

    //Cursor
    public GameObject handCursor;

    private void Start()
    {
        //Set up the character ready for animaiton and movement
        rigidbody = GetComponent<Rigidbody2D>();
        currentState = "Idle";
        setCharacterState (currentState);

        //For Dog Movement
        PlayerPrefs.SetFloat("playerSpeed", 0.0f);
        PlayerPrefs.SetFloat("playerPosition", transform.position[0]);
        PlayerPrefs.SetInt("hittingWall", 0);
        PlayerPrefs.SetInt("dogWallHitTrigger", 0);

        //For Controller
        PlayerPrefs.SetInt("playerAllowedToMove", 1);

        //Sound Configurations
        footstep_wood.loop = true;
    }

    //Animation Controller Functions, Requires Spine Runtime (using Spine.Unity;)
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
        //Update the following if statement to encapsulate all the possible animations
        if (state.Equals("Idle"))
        {
            setAnimation(idle, true, 1f);
        }
        else if (state.Equals("Walking"))
        {
            setAnimation(walking, true, 1f);
        }
    }

    public void Stop()
    {
        //Stop the object when it reaches the destination, by setting speed to 0
        rigidbody.velocity = new Vector2(0.0f, 0.0f);

        //Stop Footstep Sound
        footstep_wood.Pause();
        moving = false;

        //Reset Animation State
        setCharacterState("Idle");

        //Movement available again
        PlayerPrefs.SetInt("hittingWall", 0);
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

    private void FixedUpdate()
    {
        //Detects the mouse input coordinates and translate to world
        if (Input.GetMouseButtonDown(0))
        {
            lastClickedPos =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Destroy all previous cursors
            var cursors = GameObject.FindGameObjectsWithTag("cursor");
            foreach (var cursor in cursors)
            {
                Destroy (cursor);
            }

            //Show Cursor
            Instantiate(handCursor,
            new Vector3(lastClickedPos[0], lastClickedPos[1], -9),
            Quaternion.identity);
            moving = true;
        }
    }

    private void Update()
    {
        if (
            PlayerPrefs.GetInt("hittingWall") == 0 &&
            PlayerPrefs.GetInt("playerAllowedToMove") == 1
        )
        {
            //Update player position
            if (moving && transform.position[0] != lastClickedPos[0])
            {
                float step = speed * Time.deltaTime;
                transform.position =
                    Vector2
                        .MoveTowards(transform.position,
                        new Vector2(lastClickedPos[0], transform.position[1]),
                        step);

                //Animation controller
                setCharacterState("Walking");

                //Footstep Sound
                footstep_wood.UnPause();

                //Set speed data globally for dog movement
                PlayerPrefs
                    .SetFloat("playerSpeed",
                    (lastClickedPos[0] - transform.position[0]));
                PlayerPrefs.SetFloat("playerPosition", (transform.position[0]));
                if ((lastClickedPos[0] - transform.position[0]) > 0.0)
                {
                    transform.localScale =
                        new Vector2(rotate_scale, rotate_scale);
                }
                else if ((lastClickedPos[0] - transform.position[0]) < 0.0)
                {
                    transform.localScale =
                        new Vector2(-rotate_scale, rotate_scale);
                }
            }
            else
            {
                //Stop when not moving
                Stop();
            }
        }
        else
        {
            //Stop when hitting the wall
            Stop();
        }
    }
}
