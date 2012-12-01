using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BehaviorLibrary.Components.Decorators
{
    public class RandomDecorator : BehaviorComponent
    {

        private float r_Probability;

        private Func<float> r_RandomFunction;

        private BehaviorComponent r_Behavior;

        /// <summary>
        /// randomly executes the behavior
        /// </summary>
        /// <param name="probability">probability of execution</param>
        /// <param name="randomFunction">function that determines probability to execute</param>
        /// <param name="behavior">behavior to execute</param>
        public RandomDecorator(float probability, Func<float> randomFunction, BehaviorComponent behavior)
        {
            r_Probability = probability;
            r_RandomFunction = randomFunction;
            r_Behavior = behavior;
        }


        public override BehaviorReturnCode Behave()
        {
            try
            {
                if (r_RandomFunction.Invoke() <= r_Probability)
                {
                    ReturnCode = r_Behavior.Behave();
                    return ReturnCode;
                }
                else
                {
                    ReturnCode = BehaviorReturnCode.Running;
                    return BehaviorReturnCode.Running;
                }
            }
            catch (Exception e)
            {
#if DEBUG
                Console.Error.WriteLine(e.ToString());
#endif
                ReturnCode = BehaviorReturnCode.Failure;
                return BehaviorReturnCode.Failure;
            }
        }
    }
}
