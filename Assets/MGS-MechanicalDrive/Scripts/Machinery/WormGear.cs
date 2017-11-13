/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  WormGear.cs
 *  Description  :  Define WormGear component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/22/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.MechanicalDrive
{
    [AddComponentMenu("Developer/MechanicalDrive/WormGear")]
    public class WormGear : Mechanism
    {
        #region Property and Field
        /// <summary>
        /// Worm shaft.
        /// </summary>
        public Gear worm;

        /// <summary>
        /// Count of worm threads.
        /// </summary>
        public int threads = 1;

        /// <summary>
        /// Worm gear.
        /// </summary>
        public Gear gear;

        /// <summary>
        /// Count of gear Teeth.
        /// </summary>
        public int teeth = 36;
        #endregion

        #region Public Method
        /// <summary>
        /// Drive worm and gear.
        /// </summary>
        /// <param name="velocity">Worm linear velocity.</param>
        public override void Drive(float velocity)
        {
            var wormSpeed = velocity / worm.radius * Time.deltaTime;
            worm.transform.Rotate(Vector3.forward, wormSpeed, Space.Self);
            gear.transform.Rotate(Vector3.forward, wormSpeed * threads / teeth, Space.Self);
        }
        #endregion
    }
}