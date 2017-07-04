/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: Synchronizer.cs
 *  Author: Mogoson   Version: 1.0   Date: 6/27/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.         Synchronizer             Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     6/27/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.MechanicalDrive
{
    using UnityEngine;

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
    }//class_end
}//namespace_end