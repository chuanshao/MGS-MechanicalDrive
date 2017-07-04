/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: Belt.cs
 *  Author: Mogoson   Version: 1.0   Date: 6/22/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.             Belt                 Ignore.
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

    [RequireComponent(typeof(Renderer))]
    [AddComponentMenu("Developer/MechanicalDrive/Belt")]
    public class Belt : Mechanism
    {
        #region Property and Field
        /// <summary>
        /// Renderer of belt.
        /// </summary>
        protected Renderer mRenderer;
        #endregion

        #region Private Method
        protected virtual void Awake()
        {
            mRenderer = GetComponent<Renderer>();
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Drive belt.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        public override void Drive(float velocity)
        {
            mRenderer.material.mainTextureOffset += new Vector2(velocity * Mathf.Deg2Rad * Time.deltaTime, 0);
        }
        #endregion
    }//class_end
}//namespace_end