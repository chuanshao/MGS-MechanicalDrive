/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: WormGear.cs
 *  Author: Mogoson   Version: 1.0   Date: 6/22/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.           WormGear               Ignore.
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
            var wormSpeed = velocity / worm.radius;
            worm.transform.Rotate(Vector3.forward, wormSpeed * Time.deltaTime, Space.Self);
            gear.transform.Rotate(Vector3.forward, wormSpeed * threads / teeth * Time.deltaTime, Space.Self);
        }
        #endregion
    }
}