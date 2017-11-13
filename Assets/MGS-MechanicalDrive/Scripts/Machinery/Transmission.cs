/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  Transmission.cs
 *  Description  :  Define Transmission component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/22/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.MechanicalDrive
{
    [AddComponentMenu("Developer/MechanicalDrive/Transmission")]
    public class Transmission : Mechanism
    {
        #region Property and Field
        /// <summary>
        /// Mechanism units to drive.
        /// </summary>
        public MechanismUnit[] mechanismUnits;
        #endregion

        #region Public Method
        /// <summary>
        /// Drive Transmission's mechanisms.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        public override void Drive(float velocity)
        {
            foreach (var unit in mechanismUnits)
            {
                unit.mechanism.Drive(velocity * unit.ratio);
            }
        }
        #endregion
    }
}