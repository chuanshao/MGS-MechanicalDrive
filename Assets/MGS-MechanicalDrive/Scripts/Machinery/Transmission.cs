/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: Transmission.cs
 *  Author: Mogoson   Version: 1.0   Date: 6/22/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.         Transmission             Ignore.
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