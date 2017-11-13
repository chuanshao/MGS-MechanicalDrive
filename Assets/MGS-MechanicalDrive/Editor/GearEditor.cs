/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  GearEditor.cs
 *  Description  :  Custom editor for Gear.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/22/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Developer.MechanicalDrive
{
    [CustomEditor(typeof(Gear), true)]
    [CanEditMultipleObjects]
    public class GearEditor : MechanismEditor
    {
        #region Property and Field
        protected Gear script { get { return target as Gear; } }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = blue;
            DrawSphereCap(script.transform.position, Quaternion.identity, nodeSize);
            DrawCircleCap(script.transform.position, script.transform.rotation, script.radius);
            DrawArrow(script.transform.position, script.transform.forward, arrowLength, nodeSize, "Axis", blue);
        }
        #endregion
    }
}