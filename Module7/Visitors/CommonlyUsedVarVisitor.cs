using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    public class CommonlyUsedVarVisitor : AutoVisitor
    {
        public Dictionary<string, int> vars = new Dictionary<string, int>();

        public string mostCommonlyUsedVar()
        {
            int max = 0;
            string name = "";

            foreach (var k in vars.Keys)
                if (vars[k] > max)
                {
                    max = vars[k];
                    name = k;
                }

            return name;
        }

        public override void VisitIdNode(IdNode id)
        {
            if (vars.ContainsKey(id.Name))
                ++vars[id.Name];
            else
                vars.Add(id.Name, 1);
        }
    }
}
