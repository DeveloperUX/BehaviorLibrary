using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BehaviorLibrary.Components.Composites
{
    public class RootSelector : Selector
    {

        private BehaviorComponent[] rs_Behaviors;

        private Func<int> rs_Index;

        /// <summary>
        /// The selector for the root node of the behavior tree
        /// </summary>
        /// <param name="index">an index representing which of the behavior branches to perform</param>
        /// <param name="behaviors">the behavior branches to be selected from</param>
        public RootSelector(Func<int> index, params BehaviorComponent[] behaviors)
        {
            rs_Index = index;
            rs_Behaviors = behaviors;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode Behave()
        {
            try
            {
                switch (rs_Behaviors[rs_Index.Invoke()].Behave())
                {
                    case BehaviorReturnCode.Failure:
                        ReturnCode = BehaviorReturnCode.Failure;
                        return ReturnCode;
                    case BehaviorReturnCode.Success:
                        ReturnCode = BehaviorReturnCode.Success;
                        return ReturnCode;
                    case BehaviorReturnCode.Running:
                        ReturnCode = BehaviorReturnCode.Running;
                        return ReturnCode;
                    default:
                        ReturnCode = BehaviorReturnCode.Running;
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
