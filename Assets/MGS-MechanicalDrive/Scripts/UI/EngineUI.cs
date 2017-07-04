/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: EngineUI.cs
 *  Author: Mogoson   Version: 1.0   Date: 6/24/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.           EngineUI               Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     6/24/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.MechanicalDrive
{
    using UnityEngine;

    [RequireComponent(typeof(Engine))]
    [AddComponentMenu("Developer/MechanicalDrive/EngineUI")]
    public class EngineUI : MonoBehaviour
    {
        #region Property and Field
        public float xOfset = 10;
        public float yOfset = 10;
        protected Engine engine;
        #endregion

        #region Protected Method
        protected virtual void Start()
        {
            engine = GetComponent<Engine>();
        }

        protected virtual void OnGUI()
        {
            GUILayout.Space(yOfset);
            GUILayout.BeginHorizontal();
            GUILayout.Space(xOfset);
            if (GUILayout.Button("Start Engine"))
                engine.Starting();
            if (GUILayout.Button("Stop Engine"))
                engine.Stopping();
            GUILayout.EndHorizontal();
        }
        #endregion
    }//class_end
}//namespace_end