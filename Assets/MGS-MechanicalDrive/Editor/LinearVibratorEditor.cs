/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: LinearVibratorEditor.cs
 *  Author: Mogoson   Version: 1.0   Date: 6/24/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.      LinearVibratorEditor        Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     6/24/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.MechanicalDrive
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(LinearVibrator), true)]
    [CanEditMultipleObjects]
    public class LinearVibratorEditor : MeEditor
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
            Handles.SphereCap(0, startPosition, Quaternion.identity, nodeSize);
            Handles.SphereCap(0, script.transform.position, Quaternion.identity, nodeSize);

            DrawArrow(startPosition, script.transform.forward, arrowLength, nodeSize, "Axis", blue);
            DrawArrow(startPosition, script.transform.forward, -script.amplitudeRadius, nodeSize, string.Empty, blue);
            DrawArrow(startPosition, script.transform.forward, script.amplitudeRadius, nodeSize, string.Empty, blue);
        }
        #endregion
    }//class_end
}//namespace_end