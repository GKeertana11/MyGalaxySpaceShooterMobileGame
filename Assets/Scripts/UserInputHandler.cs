using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyMobileGalaxyShooter
{
    public class UserInputHandler : MonoBehaviour
    {
        #region EVENT METHODS
        public delegate void TapAction(Touch touch);
        public static event TapAction onTouchAction;//event called anywhere we go.
        #endregion
       
        #region PUBLIC VARIABLES
        public float maxTapMovement = 50f;//maximum pixel a tap can move.
        


        #endregion


        #region PRIVATE VARIABLES
        private Vector2 movement;//movement vector will track how far we move our finger on the screen.
        private bool tapGesturefailed = false;//Tap gesture will become true if touch moves too far.



        #endregion



        #region MONO BEHAVIOUR METHODS
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.touchCount > 0)//To find out no of touches greater than 0 or not. If no touches nothing is done.
            {
                Touch touch = Input.touches[0];//Need to find out no of touches on the screen. If there are more no of touches, need to call array.
                if (touch.phase == TouchPhase.Began)//We have several touch phases. Began enters the first frame of the touch.
                {
                    movement = Vector2.zero;//We made our movement to zero.

                }
                else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    movement += touch.deltaPosition;//The position delta since last change in pixel coordinates.
                    if (movement.magnitude > maxTapMovement)
                    {
                        tapGesturefailed = true;
                    }
                }
                else
                {
                    if (!tapGesturefailed)//if finger is removed, then we are calling tap.
                    {
                        if (onTouchAction != null)
                        {
                            onTouchAction(touch);
                        }
                       
                    }
                    tapGesturefailed = false; // Ready for the next tap.
                }
            }
            }
        
        #endregion


        #region PUBLIC METHODS


        #endregion


        #region PRIVATE METHODS

        #endregion


    }
}
