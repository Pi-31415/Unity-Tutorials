using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle,walking;
    public string currentState;
    public float speed; 
    public float movement; 
    private Rigidbody2D rigidbody;
    public string currentAnimation;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        currentState="Idle";
        setCharacterState(currentState);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void setAnimation(AnimationReferenceAsset animation,bool loop, float timeScale){
        if(animation.name.Equals(currentAnimation)){
            return;
        }
        skeletonAnimation.state.SetAnimation(0,animation,loop).TimeScale = timeScale;
        currentAnimation = animation.name;
    }

    public void setCharacterState(string state){
        if(state.Equals("Idle")){
            setAnimation(idle,true,1f);
        }else if(state.Equals("Walking")){
            setAnimation(walking,true,1f);
        }
    }

    public void Move(){
        movement = Input.GetAxis("Horizontal");
        rigidbody.velocity = new Vector2(movement*speed,rigidbody.velocity.y);
        if(movement!=0){
            setCharacterState("Walking");
            if(movement>0){
                transform.localScale = new Vector2(0.4f,0.4f);
            }else{
                transform.localScale = new Vector2(-0.4f,0.4f);
            }
        }else{
            setCharacterState("Idle");
        }
    }
}
