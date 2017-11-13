/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  Node.cs
 *  Description  :  Define Node component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/21/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.MechanicalDrive
{
    [AddComponentMenu("Developer/MechanicalDrive/Node")]
    public class Node : MonoBehaviour
    {
        #region Property and Field
        /// <summary>
        /// ID of node in the chain.
        /// </summary>
        public int ID;
        #endregion
    }
}