/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: CentrifugalVibratorEditor.cs
 *  Author: Mogoson   Version: 1.0   Date: 6/24/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.   CentrifugalVibratorEditor      Ignore.
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

    [CustomEditor(typeof(CentrifugalVibrator), true)]
    [CanEditMultipleObjects]
    public class CentrifugalVibratorEditor : MeEditor
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
            Handles.SphereCap(0, startPosition, Quaternion.identity, nodeSize);
            Handles.SphereCap(0, script.transform.position, Quaternion.identity, nodeSize);
            Handles.CircleCap(0, startPosition, script.transform.rotation, script.amplitudeRadius);

            DrawArrow(startPosition, script.transform.position, nodeSize, string.Empty, blue);
            DrawArrow(startPosition, script.transform.forward, arrowLength, nodeSize, "Axis", blue);
        }
    }//class_end
}//namespace_end