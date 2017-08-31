/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: Mechanism.cs
 *  Author: Mogoson   Version: 1.0   Date: 6/22/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.           Mechanism              Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     6/22/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.MechanicalDrive
{
    using System;
    using UnityEngine;

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