using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BehaviorLibrary.Components.Composites
{
    public class ParallelSequence : BehaviorComponent
    {

        private BehaviorComponent[] p_Behaviors;

        /// <summary>
        /// attempts to run the behaviors all in one cycle
        /// -Returns Success when all are successful
        /// -Returns Failure if one behavior fails or an error occurs
        /// -Does not Return Running
        /// </summary>
        /// <param name="behaviors"></param>
        public ParallelSequence(params BehaviorComponent[] behaviors)
        {
            p_Behaviors = behaviors;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode Behave()
        {

            for(int i = 0; i < p_Behaviors.Length;i++)
            {
                try
                {
                    switch (p_Behaviors[i].Behave())
                    {
                        case BehaviorReturnCode.Failure:
                            ReturnCode = BehaviorReturnCode.Failure;
                            return ReturnCode;
                        case BehaviorReturnCode.Success:
                            continue;
                        case BehaviorReturnCode.Running:
                            continue;
                        default:
                            ReturnCode = BehaviorReturnCode.Success;
                            return ReturnCode;
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.ToString());

                    ReturnCode = BehaviorReturnCode.Failure;
                    return ReturnCode;
                }
            }

            ReturnCode = BehaviorReturnCode.Success;
            return ReturnCode;
        }


    }
}
