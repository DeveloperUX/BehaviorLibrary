using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BehaviorLibrary.Components.Conditionals
{
    public class Conditional : BehaviorComponent
    {

        private Func<Boolean> c_Bool;

        /// <summary>
        /// Returns a return code equivalent to the test 
        /// -Returns Success if true
        /// -Returns Failure if false
        /// </summary>
        /// <param name="test">the value to be tested</param>
        public Conditional(Func<Boolean> test)
        {
            c_Bool = test;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode Behave()
        {

            try
            {
                switch (c_Bool.Invoke())
                {
                    case true:
                        ReturnCode = BehaviorReturnCode.Success;
                        return ReturnCode;
                    case false:
                        ReturnCode = BehaviorReturnCode.Failure;
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
