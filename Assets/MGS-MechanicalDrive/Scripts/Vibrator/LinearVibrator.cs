/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: LinearVibrator.cs
 *  Author: Mogoson   Version: 1.0   Date: 6/24/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.        LinearVibrator            Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     6/24/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.MechanicalDrive
{
    using UnityEngine;

    [AddComponentMenu("Developer/MechanicalDrive/LinearVibrator")]
    public class LinearVibrator : Mechanism
    {
        #region Property and Field
        /// <summary>
        /// Amplitude radius of vibrator.
        /// </summary>
        public float amplitudeRadius = 0.1f;

        /// <summary>
        /// Start loacal position.
        /// </summary>
        public Vector3 startPosition { protected set; get; }

        /// <summary>
        /// Vibration local axis.
        /// </summary>
        protected Vector3 localAxis
        {
            get
            {
                if (transform.parent)
                    return transform.parent.InverseTransformDirection(transform.forward);
                else
                    return transform.forward;
            }
        }

        /// <summary>
        /// Current offset base on start position.
        /// </summary>
        protected float currentOffset;

        /// <summary>
        /// Vibration direction.
        /// </summary>
        protected int direction = 1;
        #endregion

        #region Protected Method
        protected virtual void Awake()
        {
            startPosition = transform.localPosition;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Drive vibrator.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        public override void Drive(float velocity)
        {
            currentOffset += velocity * Mathf.Deg2Rad * direction * Time.deltaTime;
            if (currentOffset < -amplitudeRadius || currentOffset > amplitudeRadius)
            {
                direction *= -1;
                currentOffset = Mathf.Clamp(currentOffset, -amplitudeRadius, amplitudeRadius);
            }
            transform.localPosition = startPosition + localAxis * currentOffset;
        }
        #endregion
    }//class_end
}//namespace_end