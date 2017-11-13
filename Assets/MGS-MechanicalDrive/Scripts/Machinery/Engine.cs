/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  Engine.cs
 *  Description  :  Define Engine component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/22/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.MechanicalDrive
{
    [AddComponentMenu("Developer/MechanicalDrive/Engine")]
    public class Engine : Synchronizer
    {
        #region Property and Field
        /// <summary>
        /// Engine output power.
        /// </summary>
        public float power = 100;

        /// <summary>
        /// Damper of engine power.
        /// </summary>
        protected Damper damper;
        #endregion

        #region Protected Method
        protected virtual void Awake()
        {
            damper = GetComponent<Damper>();
        }

        protected virtual void Update()
        {
            base.Drive(power);
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Start engine.
        /// </summary>
        public virtual void Starting()
        {
            if (damper)
                damper.BeginAccelerate();
            enabled = true;
        }

        /// <summary>
        /// Stop engine.
        /// </summary>
        public virtual void Stopping()
        {
            if (damper)
                damper.BeginDecelerate();
            else
                enabled = false;
        }
        #endregion
    }
}