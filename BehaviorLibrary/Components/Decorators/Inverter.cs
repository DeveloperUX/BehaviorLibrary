using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BehaviorLibrary.Components.Decorators
{
    public class Inverter : BehaviorComponent
    {

        private BehaviorComponent d_Behavior;

        /// <summary>
        /// inverts the given behavior
        /// -Returns Success on Failure or Error
        /// -Returns Failure on Success 
        /// -Returns Running on Running
        /// </summary>
        /// <param name="behavior"></param>
        public Inverter(BehaviorComponent behavior) 
        {
            d_Behavior = behavior;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode Behave()
        {
            try
            {
                switch (d_Behavior.Behave())
                {
                    case BehaviorReturnCode.Failure:
                        ReturnCode = BehaviorReturnCode.Success;
                        return ReturnCode;
                    case BehaviorReturnCode.Success:
                        ReturnCode = BehaviorReturnCode.Failure;
                        return ReturnCode;
                    case BehaviorReturnCode.Running:
                        ReturnCode = BehaviorReturnCode.Running;
                        return ReturnCode;
                }
            }
            catch (Exception)
            {
                ReturnCode = BehaviorReturnCode.Success;
                return ReturnCode;
            }

            ReturnCode = BehaviorReturnCode.Success;
            return ReturnCode;

        }

    }
}
