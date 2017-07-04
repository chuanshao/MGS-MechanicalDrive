/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: Damper.cs
 *  Author: Mogoson   Version: 1.0   Date: 6/23/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.            Damper                Ignore.
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

    public enum DamperState
    {
        Accelerating, Decelerating, Stop
    }

    [RequireComponent(typeof(Engine))]
    [AddComponentMenu("Developer/MechanicalDrive/Damper")]
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
    }//class_end
}//namespace_end