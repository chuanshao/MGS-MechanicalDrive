/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  LinearVibratorEditor.cs
 *  Description  :  Custom editor for LinearVibrator.
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
    [CustomEditor(typeof(LinearVibrator), true)]
    [CanEditMultipleObjects]
    public class LinearVibratorEditor : MechanismEditor
    {
        #region Property and Field
        protected LinearVibrator script { get { return target as LinearVibrator; } }

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

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = blue;

            DrawSphereCap(startPosition, Quaternion.identity, nodeSize);
            DrawSphereCap(script.transform.position, Quaternion.identity, nodeSize);

            DrawArrow(startPosition, script.transform.forward, arrowLength, nodeSize, "Axis", blue);
            DrawArrow(startPosition, script.transform.forward, -script.amplitudeRadius, nodeSize, string.Empty, blue);
            DrawArrow(startPosition, script.transform.forward, script.amplitudeRadius, nodeSize, string.Empty, blue);
        }
        #endregion
    }
}