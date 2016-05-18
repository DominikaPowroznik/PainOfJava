using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {

        //MY: this was a Target; every refrence in code below was target.position, not target.transform.position
        public GameObject target;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

        //MY
        private float yRestriction;
        private PlatformerCharacter2D character;
        private bool playerFalling;
        private float searchingForPlayerDelay = 0f;

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;

        // Use this for initialization
        private void Start()
        {
            m_LastTargetPosition = target.transform.position;
            m_OffsetZ = (transform.position - target.transform.position).z;
            transform.parent = null;

            //MY
            character = target.GetComponent<PlatformerCharacter2D>();
        }


        // Update is called once per frame
        private void Update()
        {
            //MY: fixing errors when player is killed
            if(target == null)
            {
                FindPlayer();
                return;
            }

            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.transform.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.transform.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            //MY
            if(character.isGrounded())
            {
                playerFalling = false;
                yRestriction = -999999;
            }
            else if(!playerFalling)
            {
                playerFalling = true;
                yRestriction = target.transform.position.y - 2.5f;
            }
            newPos = new Vector3(newPos.x, Mathf.Clamp(newPos.y, yRestriction, Mathf.Infinity), newPos.z);

            transform.position = newPos;

            m_LastTargetPosition = target.transform.position;
        }

        //MY
        private void FindPlayer()
        {
            if (searchingForPlayerDelay <= Time.time)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if(player != null)
                {
                    target = player;
                }
                searchingForPlayerDelay = Time.time + 0.5f;
            }
        }
    }
}
