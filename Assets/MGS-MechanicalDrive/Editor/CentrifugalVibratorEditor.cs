/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  CentrifugalVibratorEditor.cs
 *  Description  :  Custom editor for CentrifugalVibrator.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/24/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Developer.MechanicalDrive
{
    [CustomEditor(typeof(CentrifugalVibrator), true)]
    [CanEditMultipleObjects]
    public class CentrifugalVibratorEditor : MechanismEditor
    {
        #region Property and Field
        protected CentrifugalVibrator script { get { return target as CentrifugalVibrator; } }

        protected Vector3 startPosition
        {
            get
            {
                if (Application.isPlaying)
                {
                    if (script.transform.parent)
                        return script.transform.parent.TransformPoint(script.startPosition);
                    else
                        return script.startPosition;
                }
                else
                    return script.transform.position;
            }
        }
        #endregion

        protected virtual void OnSceneGUI()
        {
            Handles.color = blue;

            DrawSphereCap(startPosition, Quaternion.identity, nodeSize);
            DrawSphereCap(script.transform.position, Quaternion.identity, nodeSize);
            DrawCircleCap(startPosition, script.transform.rotation, script.amplitudeRadius);

            DrawArrow(startPosition, script.transform.position, nodeSize, string.Empty, blue);
            DrawArrow(startPosition, script.transform.forward, arrowLength, nodeSize, "Axis", blue);
        }
    }
}