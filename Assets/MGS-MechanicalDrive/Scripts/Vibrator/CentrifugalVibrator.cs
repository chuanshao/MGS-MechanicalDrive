/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  CentrifugalVibrator.cs
 *  Description  :  Define CentrifugalVibrator component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/23/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.MechanicalDrive
{
    [AddComponentMenu("Developer/MechanicalDrive/CentrifugalVibrator")]
    public class CentrifugalVibrator : Mechanism
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
        /// Current rotate angle of aixs.
        /// </summary>
        protected float currentAngle;
        #endregion

        #region Protected Method
        protected virtual void Awake()
        {
            startPosition = transform.localPosition;
        }

        /// <summary>
        /// Get local direction from wold direction.
        /// </summary>
        /// <param name="direction">Wold direction.</param>
        /// <returns>Local direction.</returns>
        protected Vector3 GetLocalDirection(Vector3 direction)
        {
            if (transform.parent)
                return transform.parent.InverseTransformVector(direction);
            else
                return direction;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Drive vibrator.
        /// </summary>
        /// <param name="velocity">Angular velocity [degrees].</param>
        public override void Drive(float velocity)
        {
            currentAngle += velocity * Time.deltaTime;
            var direction = Quaternion.AngleAxis(currentAngle, transform.forward) * transform.right;
            transform.localPosition = startPosition + GetLocalDirection(direction) * amplitudeRadius;
        }
        #endregion
    }
}