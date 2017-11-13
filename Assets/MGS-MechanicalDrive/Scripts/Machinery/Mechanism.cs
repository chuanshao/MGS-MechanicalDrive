/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  Mechanism.cs
 *  Description  :  Define abstract mechanism.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/22/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;

namespace Developer.MechanicalDrive
{
    [Serializable]
    public struct MechanismUnit
    {
        //Mechanism to drive.
        public Mechanism mechanism;

        /// <summary>
        /// Velocity ratio.
        /// </summary>
        public float ratio;

        public MechanismUnit(Mechanism mechanism, float ratio)
        {
            this.mechanism = mechanism;
            this.ratio = ratio;
        }
    }

    public abstract class Mechanism : MonoBehaviour
    {
        #region Public Method
        /// <summary>
        /// Drive mechanism.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        public abstract void Drive(float velocity);
        #endregion
    }
}