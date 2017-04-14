using System;
using System.Collections.Generic;

namespace NTTWorkFlow.Models
{
    internal class WorkFlowStep : Step
    {
        public bool Condition { get; set; }
        public String EvalType { get; set; }
        public String EvalMethod { get; set; }
        public string ActionType { get; set; }
        public string ActionMethod { get; set; }
        public Step NextTrue { get; set; }
        public Step NextFalse { get; set; }
        public int Prev { get; set; }
        public IEnumerable<Step> SubStep { get; set; }
    }
}
