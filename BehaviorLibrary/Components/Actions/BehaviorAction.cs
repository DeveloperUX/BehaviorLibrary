using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BehaviorLibrary.Components.Actions
{
    public class BehaviorAction : BehaviorComponent
    {

        private Func<BehaviorReturnCode> ba_Action;


        public BehaviorAction(Func<BehaviorReturnCode> action)
        {
            ba_Action = action;
        }

        public override BehaviorReturnCode Behave()
        {
            try
            {
                switch (ba_Action.Invoke())
                {
                    case BehaviorReturnCode.Success:
                        ReturnCode = BehaviorReturnCode.Success;
                        return ReturnCode;
                    case BehaviorReturnCode.Failure:
                        ReturnCode = BehaviorReturnCode.Failure;
                        return ReturnCode;
                    case BehaviorReturnCode.Running:
                        ReturnCode = BehaviorReturnCode.Running;
                        return ReturnCode;
                    default:
                        ReturnCode = BehaviorReturnCode.Failure;
                        return ReturnCode;
                }
            }
            catch (Exception)
            {
                ReturnCode = BehaviorReturnCode.Failure;
                return ReturnCode;
            }
        }

    }
}
