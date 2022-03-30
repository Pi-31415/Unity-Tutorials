// Unity Snippet for point and click character controller using spine runtime
// For the 1 dimensional case
// By Pi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PointClickPlayerMovement_1D : MonoBehaviour{
    //Point and Click Controller Variables
    public float speed = 4f;
    Vector2 lastClickedPos;
    bool moving;

    //Rigidbody Controllers
    private Rigidbody2D rigidbody;

    //Animation Controller
    public SkeletonAnimation skeletonAnimation;
    //Animation Reference Assets - Export them
    public AnimationReferenceAsset idle,walking;
    public string currentState;
    //For flipping the character, scale accordingly to original prefab
    public float rotate_scale = 0.4f;
    public string currentAnimation;

    private void Start(){
        //Set up the character ready for animaiton and movement
        rigidbody = GetComponent<Rigidbody2D>();
        currentState="Idle";
        setCharacterState(currentState);
    }

    //Animation Controller Functions, Requires Spine Runtime (using Spine.Unity;)
    public void setAnimation(AnimationReferenceAsset animation,bool loop, float timeScale){
        if(animation.name.Equals(currentAnimation)){
            return;
        }
        skeletonAnimation.state.SetAnimation(0,animation,loop).TimeScale = timeScale;
        currentAnimation = animation.name;
    }

    public void setCharacterState(string state){
        //Update the following if statement to encapsulate all the possible animations
        if(state.Equals("Idle")){
            setAnimation(idle,true,1f);
        }else if(state.Equals("Walking")){
            setAnimation(walking,true,1f);
        }
    }

    private void Update(){
        //Detects the mouse input coordinates and translate to world
        if(Input.GetMouseButtonDown(0)){
            lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moving = true;
        }
        //Update player position
        if(moving && transform.position[0]!=lastClickedPos[0]){
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position,new Vector2(lastClickedPos[0],transform.position[1]),step);
            //Animation controller
            setCharacterState("Walking");
            if((lastClickedPos[0]-transform.position[0])>0.0){
                transform.localScale = new Vector2(rotate_scale,rotate_scale);
            }else{
                transform.localScale = new Vector2(-rotate_scale,rotate_scale);
            }
        }else{
            //Stop the object when it reaches the destination, by setting speed to 0
            rigidbody.velocity = new Vector2(0.0f,0.0f);
            moving = false;
            //Reset Animation State
            setCharacterState("Idle");
        }
    }


}
