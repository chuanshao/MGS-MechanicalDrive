/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  Synchronizer.cs
 *  Description  :  Define Synchronizer component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/27/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.MechanicalDrive
{
    [AddComponentMenu("Developer/MechanicalDrive/Synchronizer")]
    public class Synchronizer : Mechanism
    {
        #region Property and Field
        /// <summary>
        /// Mechanisms drive by this synchronizer.
        /// </summary>
        public Mechanism[] mechanisms;
        #endregion

        #region Public Method
        /// <summary>
        /// Drive synchronizer's mechanisms.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        public override void Drive(float velocity)
        {
            foreach (var mechanism in mechanisms)
            {
                mechanism.Drive(velocity);
            }
        }
        #endregion
    }
}