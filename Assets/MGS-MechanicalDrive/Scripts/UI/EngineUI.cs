/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  EngineUI.cs
 *  Description  :  Draw scene UI to control Engine.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/24/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.MechanicalDrive
{
    [AddComponentMenu("Developer/MechanicalDrive/EngineUI")]
    [RequireComponent(typeof(Engine))]
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
    }
}