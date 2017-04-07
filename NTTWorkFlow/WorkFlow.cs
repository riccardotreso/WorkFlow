using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace NTTWorkFlow
{
    public class WorkFlow
    {
        private class WFStep : Step {
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

        private static readonly Step _begin = Step.Begin;
        private static readonly Step _end = Step.End;
        private IEnumerable<WFStep> _steps { get; set; }


        //TODO: caricare l'xml in memoria e processarlo
        public WorkFlow(string pathConfig)
        {
            _steps = new List<WFStep>();

            XElement doc = XElement.Load(pathConfig);
            List<WFStep> list =
                                 (from el in doc.Elements("Step")
                                  select new WFStep()
                                  {
                                      ID = int.Parse(el.Attribute("ID").Value),
                                      Name = el.Attribute("Name").Value,
                                      Condition = el.Attribute("Condition") != null ? bool.Parse(el.Attribute("Condition").Value ?? "false") : false,
                                      ActionType = el.Attribute("ActionType") != null ? el.Attribute("ActionType").Value : string.Empty,
                                      ActionMethod = el.Attribute("ActionMethod") != null ? el.Attribute("ActionMethod").Value : string.Empty,
                                      EvalType = el.Attribute("EvalType") != null ? el.Attribute("EvalType").Value : string.Empty,
                                      EvalMethod = el.Attribute("EvalMethod") != null ? el.Attribute("EvalMethod").Value : string.Empty
                                  }).ToList();

            foreach (var el in doc.Elements("Step"))
            {
                int id = int.Parse(el.Attribute("ID").Value);
                WFStep current = list.Where(x => x.ID == id).First();
                if (el.Attribute("Condition") == null || !bool.Parse(el.Attribute("Condition").Value ?? "false"))
                {
                    current.Next = list.Where(x => x.ID == int.Parse(el.Attribute("Next").Value)).FirstOrDefault();
                }
                else {
                    current.NextTrue = list.Where(x => x.ID == int.Parse(el.Attribute("NextTrue").Value)).FirstOrDefault();
                    current.NextFalse = list.Where(x => x.ID == int.Parse(el.Attribute("NextFalse").Value)).FirstOrDefault();
                }

                current.SubStep = (from sub in el.Elements("SubStep")
                                   select new Step()
                                   {
                                       ID = int.Parse(sub.Attribute("ID").Value),
                                       Parent = int.Parse(sub.Attribute("Parent").Value)
                                   });

            }

            list.Last().Next = _end;

            _steps = list;


        }

        public IEnumerable<Step> Complete(Step current, bool next = true, params object[] param) {
            if (current == null) throw new ArgumentNullException("current");
            WFStep sNext;

            if (next) {
                sNext = _steps.Where(x => x.ID == current.ID).FirstOrDefault();
            }
            else
            {
                sNext = _steps.Where(x => x.Next.ID == current.ID).FirstOrDefault();
            }

            if (sNext.Condition) {
                Assembly domainAssembly = Assembly.GetCallingAssembly();
                Type customerType = domainAssembly.GetType(sNext.EvalType);
                MethodInfo staticMethodInfo = customerType.GetMethod(sNext.EvalMethod);
                bool returnValue = Convert.ToBoolean(staticMethodInfo.Invoke(null, param));

                if (returnValue) {
                    return new List<Step>() { sNext.NextTrue };
                }
                else
                {
                    return new List<Step>() { sNext.NextFalse };
                }
            }

            if (sNext.SubStep==null || sNext.SubStep.Count() == 0) {
                return new List<Step>() { sNext.Next };
            }
            else
            {
                return sNext.SubStep;
            }
        }

    }

    public class Step
    {
        internal static readonly Step Begin = new Step { ID = 1, Name = "BEGIN" };
        internal static readonly Step End = new Step { ID = 9999, Name = "END" };

        public int ID { get; set; }
        public int Parent { get; set; }
        public string Name { get; set; }
        
        public Step Next { get; set; }

        public static Step Load(int pID) {
            return new Step()
            {
                ID = pID
            };
        }

    }
}
