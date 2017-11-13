/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  LinearVibrator.cs
 *  Description  :  Define LinearVibrator component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/24/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.MechanicalDrive
{
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
    }
}