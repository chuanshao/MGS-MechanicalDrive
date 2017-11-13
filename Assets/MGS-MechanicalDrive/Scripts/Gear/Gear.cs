/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  Gear.cs
 *  Description  :  Define Gear component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/22/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.MechanicalDrive
{
    [AddComponentMenu("Developer/MechanicalDrive/Gear")]
    public class Gear : Mechanism
    {
        #region Property and Field
        /// <summary>
        /// Radius of gear.
        /// </summary>
        public float radius = 0.5f;
        #endregion

        #region Public Method
        /// <summary>
        /// Drive gear.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        public override void Drive(float velocity)
        {
            transform.Rotate(Vector3.forward, velocity / radius * Time.deltaTime, Space.Self);
        }
        #endregion
    }
}