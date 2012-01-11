using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BehaviorLibrary.Components.Composites
{
    public class Sequence : BehaviorComponent
    {

        protected BehaviorComponent[] s_Behaviors;

        private short sequence = 0;

        private short seqLength = 0;
        
        /// <summary>
        /// Performs the given behavior components sequentially
        /// Performs an AND-Like behavior and will perform each successive component
        /// -Returns Success if all behavior components return Success
        /// -Returns Running if an individual behavior component returns Success or Running
        /// -Returns Failure if a behavior components returns Failure or an error is encountered
        /// </summary>
        /// <param name="behaviors">one to many behavior components</param>
        public Sequence(params BehaviorComponent[] behaviors)
        {
            s_Behaviors = behaviors;
            seqLength = (short) s_Behaviors.Length;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode Behave()
        {
            //while you can go through them, do so
            while (sequence < seqLength)
            {
                try
                {
                    switch (s_Behaviors[sequence].Behave())
                    {
                        case BehaviorReturnCode.Failure:
                            sequence = 0;
                            ReturnCode = BehaviorReturnCode.Failure;
                            return ReturnCode;
                        case BehaviorReturnCode.Success:
                            sequence++;
                            ReturnCode = BehaviorReturnCode.Running;
                            return ReturnCode;
                        case BehaviorReturnCode.Running:
                            ReturnCode = BehaviorReturnCode.Running;
                            return ReturnCode;
                    }
                }
                catch (Exception)
                {
                    sequence = 0;
                    ReturnCode = BehaviorReturnCode.Failure;
                    return ReturnCode;
                }

            }

            sequence = 0;
            ReturnCode = BehaviorReturnCode.Success;
            return ReturnCode;

        }

    }
}
