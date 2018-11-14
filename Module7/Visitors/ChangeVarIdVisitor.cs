using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    public class ChangeVarIdVisitor : PrettyPrintVisitor
    {
        private string from, to;

        public ChangeVarIdVisitor(string _from, string _to)
        {
            from = _from;
            to = _to;
        }

        public override void VisitIdNode(IdNode id)
        {
            if (id.Name != from)
                Text += id.Name;
            else
                Text += to;
        }

        public override void VisitVarDefNode(VarDefNode w)
        {
            if (w.vars[0].Name != from)
                Text += IndentStr() + "var " + w.vars[0].Name;
            else
                Text += IndentStr() + "var " + to;

            for (int i = 1; i < w.vars.Count; i++)
                if (w.vars[i].Name != from)
                    Text += ',' + w.vars[i].Name;
                else
                    Text += ',' + to;
        }
    }
}
