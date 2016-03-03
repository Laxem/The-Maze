using UnityEngine;
using System.Collections;

namespace Game
{
    public class PlayerMovement : MonoBehaviour
    {
        private float moveSpeed = 5f;
        private float angle = 0;
        private LayerMask blockingLayer;
        private CircleCollider2D circleCollider;
        private Rigidbody2D rigidB;
        private GameHandler gameHandler;
        private Animator animator;
        [HideInInspector] public static bool pause = false;

        private string horizontalAxeName;
        private string verticalAxeName;
        
        void Start()
        {
            circleCollider = GetComponent<CircleCollider2D>();
            rigidB = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            horizontalAxeName = "Horizontal";
            verticalAxeName = "Vertical";

            blockingLayer = LayerMask.NameToLayer("Blocking Layer");
            gameHandler = GameObject.FindGameObjectWithTag("MazeCamera").GetComponent<GameHandler>();

        }

        private void FixedUpdate()
        {
            if (pause) return;

            float horizontal = Input.GetAxisRaw(horizontalAxeName);
            float vertical = Input.GetAxisRaw(verticalAxeName);


            //Check if we have a non-zero value for horizontal or vertical
            if (horizontal != 0 || vertical != 0)
            {
                animator.SetBool("move", true);           
                horizontal = horizontal * moveSpeed * Time.deltaTime;
                vertical = vertical * moveSpeed * Time.deltaTime;

                Move(horizontal, vertical);
            }
            else animator.SetBool("move", false);
            RotatePlayer(horizontal, vertical);

            CameraHandler camerahandler =  GameObject.FindGameObjectWithTag("MazeCamera").GetComponent<CameraHandler>();
            camerahandler.SetPlayerPosition(transform.position);
        }

        void RotatePlayer(float horizontal, float vertical)
        {
            if(horizontal > 0)
            {
                if (vertical > 0) angle = 135;
                else if (vertical < 0) angle = 45;
                else angle = 90;
            }
            else if(horizontal < 0)
            {
                if (vertical > 0) angle = 225;
                else if (vertical < 0) angle = 315;
                else angle = 270;
            }
            else
            {
                if (vertical > 0) angle = 180;
                else if (vertical < 0) angle = 0;
            }
            

            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        protected bool Move(float xDir, float yDir)
        {
            RaycastHit2D hit;

            Vector2 start = transform.position;

            Vector2 end = start + new Vector2(xDir, yDir);

            circleCollider.enabled = false;

            hit = Physics2D.Linecast(start, end, blockingLayer);

            circleCollider.enabled = true;

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
                gameHandler.Finish();
            }
            else if(other.tag == "Key")
            {
                Key key = other.gameObject.GetComponent("Key") as Key;
                key.TouchPlayer();       
            }
            else if(other.tag == "Foe")
            {
                gameHandler.GameOver();
            }
        }

    }
}
