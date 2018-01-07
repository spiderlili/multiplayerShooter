using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//make a turret that rotate to mouse pointer: convert screen point to world space point
//a certain height above the ground and falls along that ray to the camera
//create a plane at the height of the turret
//find the intersection of the ray and the plane
//rotate the turret in y towards the point of intersection

public class screenPointUtility : MonoBehaviour
{
    public Transform testObject;
    public float testHeight = 1f;

    //create RayThroughCamera group containing an invisible plane under MainCamera

    //accessible at anytime without needting to create an instance, find horizontal plane height
    public static Vector3 GetWorldPointFromScreenPoint(Vector3 screenPoint, float height)
    {
        //return a worldspace coordinate as a vector3
        Ray ray = Camera.main.ScreenPointToRay(screenPoint); //draw a ray from the camera centre through the screen point
    //find a plane that's a certain height off the ground, pass in a normal + centre point
        Plane plane = new Plane(Vector3.up, new Vector3(0, height, 0));
        //ray struct and plane struct with ray function - skip adding the collider to the plane and adding a layer mask
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            return ray.GetPoint(distance);
        }
        return Vector3.zero; //if pass in a valid screen point and a valid height, its function should return a proper world space coordinate
    }
    //alternatively, do a physics raycast instead of a plane but it requires a plane with a collider to hit

    // Update is called once per frame
    void Update()
    {
        //set the test object's position to get world point from screen point and pass in input mouse position
        testObject.position = GetWorldPointFromScreenPoint(Input.mousePosition, testHeight);
    }
}
