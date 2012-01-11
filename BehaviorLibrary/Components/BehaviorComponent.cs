using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BehaviorLibrary.Components
{
    public abstract class  BehaviorComponent
    {
        protected BehaviorReturnCode ReturnCode;

        public BehaviorComponent() { }

        public abstract BehaviorReturnCode Behave();
    }
}
