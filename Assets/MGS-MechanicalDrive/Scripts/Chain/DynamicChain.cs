/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  DynamicChain.cs
 *  Description  :  Define DynamicChain component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/27/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.MechanicalDrive
{
    [AddComponentMenu("Developer/MechanicalDrive/DynamicChain")]
    public class DynamicChain : Chain
    {
        #region Public Method
        /// <summary>
        /// Drive chain.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        public override void Drive(float velocity)
        {
            CreateCurve();

            var maxTime = curve[curve.length - 1].time;
            if (Mathf.Abs(timer) >= maxTime)
                timer -= maxTime;

            base.Drive(velocity);
        }
        #endregion
    }
}