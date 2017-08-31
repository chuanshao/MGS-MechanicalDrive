/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: Gear.cs
 *  Author: Mogoson   Version: 1.0   Date: 6/22/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.             Gear                 Ignore.
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