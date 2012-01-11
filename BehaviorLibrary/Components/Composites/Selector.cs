using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BehaviorLibrary.Components.Composites
{
    public class Selector : BehaviorComponent
    {

        protected BehaviorComponent[] s_Behaviors;

        private short selections = 0;

        private short selLength = 0;

        /// <summary>
        /// Selects among the given behavior components
        /// Performs an OR-Like behavior and will "fail-over" to each successive component until Success is reached or Failure is certain
        /// -Returns Success if a behavior component returns Success
        /// -Returns Running if a behavior component returns Failure or Running
        /// -Returns Failure if all behavior components returned Failure or an error has occured
        /// </summary>
        /// <param name="behaviors">one to many behavior components</param>
        public Selector(params BehaviorComponent[] behaviors)
        {
            s_Behaviors = behaviors;
            selLength = (short)s_Behaviors.Length;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode Behave()
        {
            while (selections < selLength)
            {
                try
                {
                    switch (s_Behaviors[selections].Behave())
                    {
                        case BehaviorReturnCode.Failure:
                            selections++;
                            ReturnCode = BehaviorReturnCode.Running;
                            return ReturnCode;
                        case BehaviorReturnCode.Success:
                            selections = 0;
                            ReturnCode = BehaviorReturnCode.Success;
                            return ReturnCode;
                        case BehaviorReturnCode.Running:
                            ReturnCode = BehaviorReturnCode.Running;
                            return ReturnCode;
                        default:
                            selections++;
                            ReturnCode = BehaviorReturnCode.Failure;
                            return ReturnCode;
                    }
                }
                catch (Exception)
                {
                    selections++;
                    ReturnCode = BehaviorReturnCode.Failure;
                    return ReturnCode;
                }
            }

            selections = 0;
            ReturnCode = BehaviorReturnCode.Failure;
            return ReturnCode;
        }


    }
}
