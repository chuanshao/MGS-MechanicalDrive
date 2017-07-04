/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: CentrifugalVibrator.cs
 *  Author: Mogoson   Version: 1.0   Date: 6/23/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.      CentrifugalVibrator         Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     6/23/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.MechanicalDrive
{
    using UnityEngine;

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
    }//class_end
}//namespace_end