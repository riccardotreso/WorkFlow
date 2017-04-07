using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NTTWorkFlow;
using System.Linq;

namespace NTTWorkFlow.Test
{
    [TestClass]
    public class main
    {
        [TestMethod]
        public void loadXML()
        {
            WorkFlow myWF = new WorkFlow("C:\\Progetti\\NTTWorkFlow\\NTTWorkFlow.Test\\WF.xml");

            var step = myWF.Complete(Step.Load(1), true);
            step = myWF.Complete(step.First(), true);
            step = myWF.Complete(step.First(), true, 1);
            step = myWF.Complete(step.First(), true);
            step = myWF.Complete(step.First(), true);

        }
    }
}
