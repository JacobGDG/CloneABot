using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class DeadPlayer : MonoBehaviour
{

    Vector3 velocity;

    public bool newVersion = true;


    Controller2D controller;

	// Use this for initialization
	void Start () {
        controller = GetComponent<Controller2D>();

    }
	
    public bool checkTopCollsiions()
    {
        return controller.collisions.above;
    }

	// Update is called once per frame
	void Update () {
        //if ((!controller.collisions.below && !gameObject.layer.Equals("Obstacle")) || newVersion)
        //{
            //newVersion = false;
           // gameObject.layer = 10;
            velocity.y += Player.gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            if (controller.collisions.above || controller.collisions.below)
                velocity.y = 0;
        //}
        //else
           // gameObject.layer = 9;

        //when hit ground stop calculating movement.
        //the layer is changed so when the movement is being caulated it can be something random as to not collid with self
        //once collisions no longer necassary bypass and change layer to "Obstacle" so new objects will collide with it
        //one problem with this is th eobject is fixed in place once floor is hit
    }
}
