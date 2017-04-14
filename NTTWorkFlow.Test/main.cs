using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NTTWorkFlow;
using System.Linq;
using NTTWorkFlow.Models;
using NTTWorkFlow.Utility;

namespace NTTWorkFlow.Test
{
    [TestClass]
    public class main
    {
        WorkFlow myWF = new WorkFlow("C:\\Progetti\\NTTWorkFlow\\NTTWorkFlow.Test\\WF.xml");
        private static string refID = "927365375";
        [TestMethod]
        public void loadXML()
        {
            var step = myWF.Complete(Step.Load(1), refID, true);
            step = myWF.Complete(step.First(), refID, true);
            step = myWF.Complete(step.First(), refID, true, 1);
            step = myWF.Complete(step.First(), refID, true);
            step = myWF.Complete(step.First(), refID, true);

        }

        [TestMethod]
        public void LoadScript() {
            Initilize init = new Initilize();
            init.CreateDataStructure("Server=.;Database=master;Trusted_Connection=True;");

        }
    }
}
