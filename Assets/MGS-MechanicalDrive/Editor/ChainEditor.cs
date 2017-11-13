/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  ChainEditor.cs
 *  Description  :  Custom editor for Chain.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/21/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEditor;
using UnityEngine;

namespace Developer.MechanicalDrive
{
    [CustomEditor(typeof(Chain), true)]
    [CanEditMultipleObjects]
    public class ChainEditor : MechanismEditor
    {
        #region Property and Field
        protected Chain script { get { return target as Chain; } }

        protected const float delta = 0.1f;
        #endregion

        #region Protected Method
        protected virtual void OnEnable()
        {
            if (script.anchorRoot)
            {
                script.anchorRoot.localPosition = Vector3.zero;
                script.anchorRoot.localRotation = Quaternion.identity;
                if (script.anchorRoot.childCount >= 2)
                    script.CreateCurve();
            }
            if (script.nodeRoot)
            {
                script.nodeRoot.localPosition = Vector3.zero;
                script.nodeRoot.localRotation = Quaternion.identity;
            }
        }

        protected virtual void OnSceneGUI()
        {
            #region Coordinate System
            Handles.color = blue;

            var horizontal = script.transform.right * lineLength;
            var vertical = script.transform.up * lineLength;
            DrawSphereCap(script.transform.position, Quaternion.identity, nodeSize);

            Handles.DrawLine(script.transform.position - horizontal, script.transform.position + horizontal);
            Handles.DrawLine(script.transform.position - vertical, script.transform.position + vertical);
            #endregion

            #region Anchors And Curve
            if (script.anchorRoot)
            {
                foreach (Transform anchor in script.anchorRoot)
                {
                    DrawSphereCap(anchor.position, Quaternion.identity, nodeSize);
                }

                if (script.anchorRoot.childCount >= 2)
                {
                    var maxTime = script.curve[script.curve.length - 1].time;
                    for (float timer = 0; timer < maxTime; timer += delta)
                    {
                        var timerPoint = script.anchorRoot.TransformPoint(script.curve.Evaluate(timer));
                        var deltaPoint = script.anchorRoot.TransformPoint(script.curve.Evaluate(Mathf.Clamp(timer + delta, 0, maxTime)));
                        Handles.DrawLine(timerPoint, deltaPoint);
                    }
                }
            }
            #endregion

            if (AnchorEditor.isOpen)
            {
                #region Circular Settings
                if (AnchorEditor.isCircularSettingsReasonable)
                {
                    var from = Quaternion.AngleAxis(AnchorEditor.from, script.transform.forward) * Vector3.up;
                    var to = Quaternion.AngleAxis(AnchorEditor.to, script.transform.forward) * Vector3.up;
                    var angle = AnchorEditor.to - AnchorEditor.from;

                    Handles.color = green;
                    Handles.DrawWireArc(AnchorEditor.center.position, script.transform.forward, from, angle, AnchorEditor.radius);

                    DrawArrow(AnchorEditor.center.position, from, AnchorEditor.radius, nodeSize, string.Empty, green);
                    DrawArrow(AnchorEditor.center.position, to, AnchorEditor.radius, nodeSize, string.Empty, green);

                    if (AnchorEditor.countC > 2)
                    {
                        var space = angle / (AnchorEditor.countC - 1);
                        for (int i = 0; i < AnchorEditor.countC - 2; i++)
                        {
                            var direction = Quaternion.AngleAxis(AnchorEditor.from + space * (i + 1), script.transform.forward) * Vector3.up;
                            DrawArrow(AnchorEditor.center.position, direction.normalized, AnchorEditor.radius, nodeSize, string.Empty, green);
                        }
                    }
                }
                #endregion

                #region Linear Settings
                if (AnchorEditor.isLinearSettingsReasonable)
                {
                    var direction = (AnchorEditor.end.position - AnchorEditor.start.position).normalized;
                    var space = Vector3.Distance(AnchorEditor.start.position, AnchorEditor.end.position) / (AnchorEditor.countL + 1);

                    Handles.color = green;
                    Handles.DrawLine(AnchorEditor.start.position, AnchorEditor.end.position);
                    for (int i = 0; i < AnchorEditor.countL; i++)
                    {
                        DrawSphereCap(AnchorEditor.start.position + direction * space * (i + 1), Quaternion.identity, nodeSize);
                    }
                }
                #endregion
            }
        }

        protected void EstimateCount()
        {
            var estimate = script.curve[script.curve.length - 1].time / script.space;
            script.count = (int)Math.Round(estimate, MidpointRounding.AwayFromZero);
            MarkSceneDirty();
        }

        protected void DeleteNodes()
        {
            while (script.nodeRoot.childCount > 0)
            {
                DestroyImmediate(script.nodeRoot.GetChild(0).gameObject);
            }
            MarkSceneDirty();
        }
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (script.anchorRoot == null)
                return;

            script.anchorRoot.localPosition = Vector3.zero;
            script.anchorRoot.localRotation = Quaternion.identity;

            if (GUILayout.Button("Anchor Editor"))
                AnchorEditor.ShowEditor(script);

            if (script.anchorRoot.childCount < 2)
                return;

            if (script.curve == null)
                script.CreateCurve();

            if (script.nodeRoot == null || script.nodePrefab == null)
                return;

            GUILayout.BeginHorizontal("Node Editor", "Window", GUILayout.Height(45));
            if (GUILayout.Button("Estimate"))
                EstimateCount();

            if (GUILayout.Button("Create"))
            {
                DeleteNodes();
                script.CreateNodes();
            }

            if (GUILayout.Button("Delete"))
                DeleteNodes();
            GUILayout.EndHorizontal();
        }
        #endregion
    }
}