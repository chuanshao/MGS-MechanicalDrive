/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: DynamicChain.cs
 *  Author: Mogoson   Version: 1.0   Date: 6/27/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.         DynamicChain             Ignore.
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
    }//class_end
}//namespace_end