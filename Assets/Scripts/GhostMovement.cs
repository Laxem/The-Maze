using UnityEngine;
using System.Collections;

namespace Game
{
    public class GhostMovement : MonoBehaviour
    {

        private float moveSpeed = 3f;
        private float angularMoveSpeed = 80f;
        private float lastAngle;
        private float minDistance = 0.5f;
        private Transform player;
        private GameHandler gameHandler;

        // Use this for initialization
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            gameHandler = GameObject.FindGameObjectWithTag("MazeCamera").GetComponent<GameHandler>();
            moveSpeed = gameHandler.ghostSpeed;
            angularMoveSpeed = gameHandler.ghostAngularSpeed;

            Vector3 dir = player.position - transform.position;
            Quaternion rotat = Quaternion.LookRotation(dir, Vector3.down);
            float angle = 0;
            if (dir.x > 0) angle = 180 - rotat.eulerAngles.x; //an other method can surely be found
            else angle = rotat.eulerAngles.x;
            lastAngle = angle;
            rotat = Quaternion.Euler(0f, 0f, angle);
            transform.rotation = rotat;

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            Move();
        }

        void Move()
        {
            // calculate target rotation with x and y lock
            Vector3 dir = player.position - transform.position;
            Quaternion rotat = Quaternion.LookRotation(dir, Vector3.down);
            float angle = 0;
            if (dir.x > 0) angle = 180 - rotat.eulerAngles.x; //an other method can surely be found
            else angle = rotat.eulerAngles.x;

            // limite angular acceleration
            float diffAngle = Mathf.Abs(Mathf.DeltaAngle(lastAngle , angle));
            if (diffAngle > (angularMoveSpeed * Time.fixedDeltaTime))
                angle = lastAngle + Mathf.Sign(Mathf.DeltaAngle(lastAngle, angle)) * angularMoveSpeed * Time.fixedDeltaTime; 

            // set the final angle
            rotat = Quaternion.Euler(0f, 0f, angle);
            transform.rotation = rotat;
        
            // stop moving forward if near the target
            if (Vector3.Distance(player.position , transform.position)>minDistance)
                transform.position -= transform.right*moveSpeed*Time.deltaTime;

            lastAngle = angle; // used in the next angular acceleration calcul
        }
        

    }
}


