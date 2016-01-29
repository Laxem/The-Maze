using UnityEngine;
using System.Collections;

namespace Completed
{
    public abstract class MovementHandler : MonoBehaviour
    {
        public float moveTime = 0.1f;          
        public LayerMask blockingLayer;         

        private BoxCollider2D boxCollider;     
        private Rigidbody2D rb2D;           
        private float inverseMoveTime;
        [HideInInspector]public static bool pause = false;      


        protected virtual void Start()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            
            rb2D = GetComponent<Rigidbody2D>();
            
            inverseMoveTime = 1f / moveTime;
        }

        
        protected bool Move(float xDir, float yDir, out RaycastHit2D hit)
        {
            Vector2 start = transform.position;
            
            Vector2 end = start + new Vector2(xDir, yDir);
            
            boxCollider.enabled = false;
            
            hit = Physics2D.Linecast(start, end, blockingLayer);
            
            boxCollider.enabled = true;
            
            if (hit.collider == null)
            {
                transform.position = end;
                
                return true;
            }
            
            return false;
        }

        
        protected IEnumerator SmoothMovement(Vector3 end)
        {
            float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            
            while (sqrRemainingDistance > float.Epsilon)
            {
                Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
                
                rb2D.MovePosition(newPostion);
                
                sqrRemainingDistance = (transform.position - end).sqrMagnitude;
                
                yield return null;
            }
        }


        //The virtual keyword means AttemptMove can be overridden by inheriting classes using the override keyword.
        //AttemptMove takes a generic parameter T to specify the type of component we expect our unit to interact with if blocked (Player for Enemies, Wall for Player).
        protected virtual void AttemptMove<T>(float xDir, float yDir)
            where T : Component
        {
            //Hit will store whatever our linecast hits when Move is called.
            RaycastHit2D hit;

            //Set canMove to true if Move was successful, false if failed.
            bool canMove = Move(xDir, yDir, out hit);

            //Check if nothing was hit by linecast
            if (hit.transform == null)
                //If nothing was hit, return and don't execute further code.
                return;

            //Get a component reference to the component of type T attached to the object that was hit
            T hitComponent = hit.transform.GetComponent<T>();

            //If canMove is false and hitComponent is not equal to null, meaning MovingObject is blocked and has hit something it can interact with.
            if (!canMove && hitComponent != null)

                //Call the OnCantMove function and pass it hitComponent as a parameter.
                OnCantMove(hitComponent);
        }


        //The abstract modifier indicates that the thing being modified has a missing or incomplete implementation.
        //OnCantMove will be overriden by functions in the inheriting classes.
        protected abstract void OnCantMove<T>(T component)
            where T : Component;
    }
}