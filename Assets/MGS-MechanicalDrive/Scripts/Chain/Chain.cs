/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  Chain.cs
 *  Description  :  Define Chain component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/21/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using Developer.AnimationCurveExtension;
using UnityEngine;

namespace Developer.MechanicalDrive
{
    [AddComponentMenu("Developer/MechanicalDrive/Chain")]
    public class Chain : Mechanism
    {
        #region Property and Field
        /// <summary>
        /// Root of chain track anchors.
        /// </summary>
        public Transform anchorRoot;

        /// <summary>
        /// Root of chain nodes.
        /// </summary>
        public Transform nodeRoot;

        /// <summary>
        /// Prefab of nodes.
        /// </summary>
        public GameObject nodePrefab;

        /// <summary>
        /// Count of chain nodes.
        /// </summary>
        public int count = 50;

        /// <summary>
        /// Space of nodes.
        /// </summary>
        public float space = 0.1f;

        /// <summary>
        /// Nodes of chain.
        /// </summary>
        [HideInInspector]
        public Node[] nodes;

        /// <summary>
        /// VectorAnimationCurve of nodes.
        /// </summary>
        public VectorAnimationCurve curve { protected set; get; }

        /// <summary>
        /// Timer for VectorAnimationCurve.
        /// </summary>
        protected float timer = 0;

        /// <summary>
        /// Delta time for VectorAnimationCurve
        /// </summary>
        protected const float delta = 0.01f;
        #endregion

        #region Private Method
        protected virtual void Awake()
        {
            CreateCurve();
        }

        /// <summary>
        /// Tow node move and rotate base on VectorAnimationCurve.
        /// </summary>
        /// <param name="node">Target node to tow.</param>
        /// <param name="time">Time of current in curve.</param>
        protected void TowNodeBaseOnCurve(Transform node, float time)
        {
            //Calculate position and direction.
            var nodePos = curve.Evaluate(time);
            var deltaPos = curve.Evaluate(time + delta);
            var up = Vector3.Cross(deltaPos - nodePos, transform.forward);

            //Update position and direction.
            node.localPosition = nodePos;
            node.LookAt(nodeRoot.TransformPoint(deltaPos), up);
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Create the curve base on anchors.
        /// </summary>
        public virtual void CreateCurve()
        {
            curve = new VectorAnimationCurve();
            curve.preWrapMode = curve.postWrapMode = WrapMode.Loop;

            //Add frame keys to curve.
            float time = 0;
            for (int i = 0; i < anchorRoot.childCount - 1; i++)
            {
                curve.AddKey(time, anchorRoot.GetChild(i).localPosition);
                time += Vector3.Distance(anchorRoot.GetChild(i).position, anchorRoot.GetChild(i + 1).position);
            }

            //Add last key and loop key(the first key).
            curve.AddKey(time, anchorRoot.GetChild(anchorRoot.childCount - 1).localPosition);
            time += Vector3.Distance(anchorRoot.GetChild(anchorRoot.childCount - 1).position, anchorRoot.GetChild(0).position);
            curve.AddKey(time, anchorRoot.GetChild(0).localPosition);

            //Smooth the in and out tangents of curve keyframes.
            curve.SmoothTangents(0);
        }

        /// <summary>
        /// Create chain nodes.
        /// </summary>
        public virtual void CreateNodes()
        {
            nodes = new Node[count];
            for (int i = 0; i < count; i++)
            {
                //Create node.
                var nodeClone = Instantiate(nodePrefab);
                nodeClone.transform.SetParent(nodeRoot);

                //Tow node.
                TowNodeBaseOnCurve(nodeClone.transform, i * space);

                //Set node ID.
                nodes[i] = nodeClone.GetComponent<Node>();
                nodes[i].ID = i;
            }
        }

        /// <summary>
        /// Drive chain.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        public override void Drive(float velocity)
        {
            timer += velocity * Mathf.Deg2Rad * Time.deltaTime;
            foreach (var node in nodes)
            {
                TowNodeBaseOnCurve(node.transform, node.ID * space + timer);
            }
        }
        #endregion
    }
}