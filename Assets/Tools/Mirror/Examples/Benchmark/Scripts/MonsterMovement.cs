using UnityEngine;

namespace Mirror.Examples.Benchmark
{
    public class MonsterMovement : NetworkBehaviour
    {
        public float speed = 1;
        public float movementProbability = 0.5f;
        public float movementDistance = 20;

        private bool moving;
        private Vector3 start;
        private Vector3 destination;

        public override void OnStartServer()
        {
            start = transform.position;
        }

        [ServerCallback]
        private void Update()
        {
            if (moving)
            {
                if (Vector3.Distance(transform.position, destination) <= 0.01f)
                {
                    transform.position = destination;
                    moving = false;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
                }
            }
            else
            {
                var r = Random.value;
                if (r < movementProbability * Time.deltaTime)
                {
                    var circlePos = Random.insideUnitCircle;
                    var dir = new Vector3(circlePos.x, 0, circlePos.y);

                    // set destination on random pos in a circle around start.
                    // (don't want to wander off)
                    destination = start + dir * movementDistance;
                    moving = true;
                }
            }
        }
    }
}
