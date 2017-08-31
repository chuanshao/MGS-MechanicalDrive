/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: GearEditor.cs
 *  Author: Mogoson   Version: 1.0   Date: 6/22/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.          GearEditor              Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     6/22/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.MechanicalDrive
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(Gear), true)]
    [CanEditMultipleObjects]
    public class GearEditor : MeEditor
    {
        #region Property and Field
        protected Gear script { get { return target as Gear; } }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = blue;
            Handles.SphereCap(0, script.transform.position, Quaternion.identity, nodeSize);
            Handles.CircleCap(0, script.transform.position, script.transform.rotation, script.radius);
            DrawArrow(script.transform.position, script.transform.forward, arrowLength, nodeSize, "Axis", blue);
        }
        #endregion
    }
}