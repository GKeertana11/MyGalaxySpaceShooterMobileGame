using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{

    #region PUBLIC VARIABLES
    public Transform launcher;
    public float rotatiopnSpeed=10f;//The rotation of ship in degrees per second.
    public float movementSpeed=2f;//The movement of ship by force applied Force applied in second.
    #endregion

    #region PRIVATE VARIABLES
    private bool isRotating = false;
    private GameManager gameManager;

    private const string TURN_COUROUTINE_FUNCTION = "Turn_RotateTowardsTap";

    #endregion

    #region MONO BEHAVIOUR METHODS
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()//When game object is active then we are subscribing it.
    {
        MyMobileGalaxyShooter.UserInputHandler.onTouchAction += TowardsTouch;
    }
    private void OnDisable()//When game object is inactive then we are desubscribing it.
    {
        MyMobileGalaxyShooter.UserInputHandler.onTouchAction -= TowardsTouch;
    }
    private void Shoot()
    {
        Bullet bullet = PoolManager.Instance.Spawn(Constants.BULLET_PREFAB_NAME).GetComponent<Bullet>();
        bullet.SetPosition(launcher.position);
        bullet.SetTrajectory(bullet.transform.position + transform.forward);
    }
    #endregion

    #region PUBLIC METHODS
    public void TowardsTouch(Touch touch)
    {
       Vector3 worldTouchPosition= Camera.main.ScreenToWorldPoint(touch.position);//It converts screen point to world point.
        StopCoroutine(TURN_COUROUTINE_FUNCTION);
        StartCoroutine(TURN_COUROUTINE_FUNCTION,worldTouchPosition);

    }
    #endregion

    #region PRIVATE METHODS
    #endregion

    #region COUROUTINE FUNCTIONS
   /* IEnumerator Turn_Rotate_MoveTowardsTap(Vector3 tempPoint)
    {
        isRotating = true;
        //tempPoint -= this.transform.position;//to find distance difference btw touch position and current position.
       // tempPoint.z = transform.position.z;//Assigning z value of ship position to touch position.
        transform.position = tempPoint;
        Quaternion startRotation = this.transform.rotation;//the rotation start point.
        Quaternion endRotation = Quaternion.LookRotation(tempPoint,Vector3.up);//this rotation will look at  touch point in an upward direction.
        
        float time = Quaternion.Angle(startRotation, endRotation)/rotatiopnSpeed;//Angle between start and end rotations.
       
        for (float i = 0; i < time; i+=Time.deltaTime)
        {
            transform.rotation=Quaternion.Slerp(startRotation, endRotation, i);

        }

        transform.rotation = endRotation;//We need to put shooting functionality here.
        isRotating = false;

        yield return (null);

    }*/
   IEnumerator Turn_RotateTowardsTap(Vector3 tempPoint)
    {
        isRotating = true;

        tempPoint -= this.transform.position;//to find distance difference btw touch position and current position.
        tempPoint.z = transform.position.z;//Assigning z value of touch position to ship position.
        Quaternion startRotation = this.transform.rotation;//took the value of ships rotation.
        Quaternion endRotation = Quaternion.LookRotation(tempPoint, Vector3.forward);//this rotation will look towards touch position in forward direction.
        for (float i = 0; i < 1f; i+=Time.deltaTime)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, i);
            yield return (null);
        }                                                                            // transform.rotation = (endRotation);
     
        transform.rotation = endRotation;
        Shoot();
        isRotating = false;
        

       
      
    }
    #endregion

   

}
