/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: Engine.cs
 *  Author: Mogoson   Version: 1.0   Date: 6/22/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.            Engine                Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     6/22/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.MechanicalDrive
{
    using UnityEngine;

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