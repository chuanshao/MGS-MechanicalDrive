/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  Damper.cs
 *  Description  :  Define Damper component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/23/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.MechanicalDrive
{
    public enum DamperState
    {
        Accelerating = 0,
        Decelerating = 1,
        Stop = 2
    }

    [AddComponentMenu("Developer/MechanicalDrive/Damper")]
    [RequireComponent(typeof(Engine))]
    public class Damper : MonoBehaviour
    {
        #region Property and Field
        /// <summary>
        /// AnimationCurve of damper acceleration.
        /// </summary>
        public AnimationCurve accelerationCurve;

        /// <summary>
        /// AnimationCurve of damper deceleration.
        /// </summary>
        public AnimationCurve decelerationCurve;

        /// <summary>
        /// State of this damper.
        /// </summary>
        protected DamperState state = DamperState.Accelerating;

        /// <summary>
        /// Time of AnimationCurve.
        /// </summary>
        protected float timer = 0;

        /// <summary>
        /// Damper attach engine.
        /// </summary>
        protected Engine engine;
        #endregion

        #region Protected Method
        protected virtual void Reset()
        {
            accelerationCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0), new Keyframe(2, 100) });
            decelerationCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 100), new Keyframe(3, 0) });
        }

        protected virtual void Awake()
        {
            engine = GetComponent<Engine>();
        }

        protected virtual void Update()
        {
            if (state == DamperState.Accelerating)
            {
                timer += Time.deltaTime;
                engine.power = accelerationCurve.Evaluate(timer);
                if (timer >= accelerationCurve[accelerationCurve.length - 1].time)
                {
                    timer = 0;
                    state = DamperState.Stop;
                    enabled = false;
                }
            }
            else if (state == DamperState.Decelerating)
            {
                timer += Time.deltaTime;
                engine.power = decelerationCurve.Evaluate(timer);
                if (timer >= decelerationCurve[decelerationCurve.length - 1].time)
                {
                    engine.enabled = false;
                    engine.power = timer = 0;
                    state = DamperState.Stop;
                    enabled = false;
                }
            }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Begin accelerate engine's power.
        /// </summary>
        public virtual void BeginAccelerate()
        {
            if (engine.power == 0)
            {
                state = DamperState.Accelerating;
                enabled = true;
            }
        }

        /// <summary>
        /// Begin decelerate engine's power.
        /// </summary>
        public virtual void BeginDecelerate()
        {
            if (engine.power != 0 && state == DamperState.Stop)
            {
                state = DamperState.Decelerating;
                enabled = true;
            }
        }
        #endregion
    }
}