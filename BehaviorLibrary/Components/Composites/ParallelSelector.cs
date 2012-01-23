using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BehaviorLibrary.Components.Composites
{
    class ParallelSelector : BehaviorComponent
    {

        protected BehaviorComponent[] p_Behaviors;

        private short p_Selections = 0;

        private short p_SelLength = 0;

        /// <summary>
        /// Selects among the given behavior components
        /// Performs an OR-Like behavior and will "fail-over" to each successive component until Success is reached or Failure is certain
        /// -Returns Success if a behavior component returns Success
        /// -Returns Running if a behavior component returns Running
        /// -Returns Failure if all behavior components returned Failure
        /// </summary>
        /// <param name="behaviors">one to many behavior components</param>
        public ParallelSelector(params BehaviorComponent[] behaviors)
        {
            p_Behaviors = behaviors;
            p_SelLength = (short)p_Behaviors.Length;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode Behave()
        {
            while (p_Selections < p_SelLength)
            {
                try
                {
                    switch (p_Behaviors[p_Selections].Behave())
                    {
                        case BehaviorReturnCode.Failure:
                            p_Selections++;
                            continue;
                        case BehaviorReturnCode.Success:
                            p_Selections = 0;
                            ReturnCode = BehaviorReturnCode.Success;
                            return ReturnCode;
                        case BehaviorReturnCode.Running:
                            ReturnCode = BehaviorReturnCode.Running;
                            return ReturnCode;
                        default:
                            p_Selections++;
                            continue;
                    }
                }
                catch (Exception)
                {
                    p_Selections++;
                    continue;
                }
            }

            p_Selections = 0;
            ReturnCode = BehaviorReturnCode.Failure;
            return ReturnCode;
        }
    }
}
