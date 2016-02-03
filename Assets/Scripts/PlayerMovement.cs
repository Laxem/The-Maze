using UnityEngine;
using System.Collections;

//Player inherits from MovingObject, our base class for objects that can move, Enemy also inherits from this.
namespace Game
{
    public class PlayerMovement : MonoBehaviour
    {
        private float moveSpeed = 5f;
        private LayerMask blockingLayer;
        //public GameHandler gameHandler;
        private BoxCollider2D boxCollider;
        private Rigidbody2D rigidB;
        [HideInInspector] public static bool pause = false;

        private string horizontalAxeName;
        private string verticalAxeName;

        //Start overrides the Start function of MovingObject
        protected void Start()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            rigidB = GetComponent<Rigidbody2D>();

            horizontalAxeName = "Horizontal";
            verticalAxeName = "Vertical";

            blockingLayer = LayerMask.NameToLayer("Blocking Layer");
        }

        private void Update()
        {
            if (pause) return;

            float horizontal = Input.GetAxisRaw(horizontalAxeName);
            float vertical = Input.GetAxisRaw(verticalAxeName);


            //Check if we have a non-zero value for horizontal or vertical
            if (horizontal != 0 || vertical != 0)
            {
                //Debug.Log(horizontal + " " + vertical);

                //InputToDir(ref horizontal, ref vertical);              
                horizontal = horizontal * moveSpeed * Time.deltaTime;
                vertical = vertical * moveSpeed * Time.deltaTime;

                Move(horizontal, vertical);
            }

        }

        private void InputToDir(ref float horizontal, ref float vertical)
        {
            if (horizontal != 0 && vertical != 0)
            {
                horizontal = horizontal * moveSpeed * Time.deltaTime / 1.415f;  // 1.415 correspond to an approximation of square root of 2
                vertical = vertical * moveSpeed * Time.deltaTime / 1.415f;
            }
            else
            {
                horizontal = horizontal * moveSpeed * Time.deltaTime;
                vertical = vertical * moveSpeed * Time.deltaTime;
            }
        }

        protected bool Move(float xDir, float yDir)
        {
            RaycastHit2D hit;

            Vector2 start = transform.position;

            Vector2 end = start + new Vector2(xDir, yDir);

            boxCollider.enabled = false;

            hit = Physics2D.Linecast(start, end, blockingLayer);

            boxCollider.enabled = true;

            //Debug.Log(hit.distance);
            if (hit.collider == null)
            {
                rigidB.MovePosition(end);

                return true;
            }
            else
            {
                Debug.Log("collide blocking layer");
            }

            return false;
        }

        
        //OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
        private void OnTriggerEnter2D(Collider2D other)
        {
            //Check if the tag of the trigger collided with is Exit.
            if (other.tag == "Exit")
            {
                
                //pause = true;
                //Disable the player object since level is over.
                if(GameHandler.instance.CanExit()) enabled = false;
            }
            else if(other.tag == "Key")
            {
                Key key = other.gameObject.GetComponent("Key") as Key;
                key.TouchPlayer();       
            }
            else if(other.tag == "foe")
            {
                GameHandler.instance.GameOver();
            }
        }

    }
}
