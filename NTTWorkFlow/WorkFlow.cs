using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using NTTWorkFlow.Models;
using NTTWorkFlow.Repository;

namespace NTTWorkFlow
{
    //TODO: Salvataggio dati DB
    //TODO: Caricamento Step da DB
    //TODO: Gestione deleghe
    public class WorkFlow
    {
       
        private static readonly Step _begin = Step.Begin;
        private static readonly Step _end = Step.End;
        private static WorkFlowDataAccess dataAccess;
        private IEnumerable<WorkFlowStep> _steps { get; set; }


        //TODO: caricare l'xml in memoria e processarlo
        public WorkFlow(string pathConfig)
        {
            //TODO: pass correct connection string
            dataAccess = new WorkFlowDataAccess("Server=.;Database=MyWorkflow;Trusted_Connection=True;");
            _steps = new List<WorkFlowStep>();

            XElement doc = XElement.Load(pathConfig);
            List<WorkFlowStep> list =
                                 (from el in doc.Elements("Step")
                                  select new WorkFlowStep()
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
                WorkFlowStep current = list.Where(x => x.ID == id).First();
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

        public bool Begin(String refID, String user) {
            return dataAccess.InsertTask(_begin, refID, user);
        }

        public IEnumerable<Step> GetCurrentStep(String refID) {
            return dataAccess.GetByRefID(refID);
        }

        public bool InsertNext(Step next, String refID, String user) {
            return dataAccess.InsertTask(next, refID, user);
        }


        public IEnumerable<Step> Complete(Step current, String refID, bool next = true, params object[] param) {
            if (current == null) throw new ArgumentNullException("current");
            WorkFlowStep sNext;

            if (!dataAccess.CompleteTask(current.ID, refID))
                return null;

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

    
}
