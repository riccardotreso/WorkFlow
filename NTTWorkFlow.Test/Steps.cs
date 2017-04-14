using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NTTWorkFlow.Models;
using System.Linq;

namespace NTTWorkFlow.Test
{
    [TestClass]
    public class Steps
    {
        private static WorkFlow myWF = new WorkFlow("C:\\Progetti\\NTTWorkFlow\\NTTWorkFlow.Test\\WF.xml");
        private static string refID = "927365375",
            userID = "tresor";

        [TestMethod]
        public void begin()
        {
            bool result = myWF.Begin(refID, userID);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void step1()
        {
            var step = myWF.GetCurrentStep(refID);
            step = myWF.Complete(step.First(), refID, true);
            if (step.Count() > 0)
                myWF.InsertNext(step.First(), refID, userID);
        }

        [TestMethod]
        public void step2()
        {
            var step = myWF.GetCurrentStep(refID);
            step = myWF.Complete(step.First(), refID, true);
            if (step.Count() > 0)
                myWF.InsertNext(step.First(), refID, userID);
        }

        [TestMethod]
        public void step3()
        {
            var step = myWF.GetCurrentStep(refID);
            step = myWF.Complete(step.First(), refID, true, 1);
            if (step.Count() > 0)
                myWF.InsertNext(step.First(), refID, userID);
        }
    }
}
