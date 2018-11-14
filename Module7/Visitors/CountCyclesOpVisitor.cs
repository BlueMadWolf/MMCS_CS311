using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    public class CountCyclesOpVisitor : AutoVisitor
    {
        public int CountOp = 0;
        public int CountCycle = 0;
        public int NowCycle = 0;

        public int MidCount()
        {
            if (CountCycle != 0)
                return CountOp / CountCycle;
            else
                return 0;
        }

        public override void VisitCycleNode(CycleNode c)
        {
            if (NowCycle > 0)
                CountOp += 1;
            CountCycle += 1;
            NowCycle += 1;
            c.Expr.Visit(this);
            c.Stat.Visit(this);
            NowCycle -= 1;
        }

        public override void VisitAssignNode(AssignNode a)
        {
            if (NowCycle > 0)
                CountOp += 1;
            a.Id.Visit(this);
            a.Expr.Visit(this);

        }

        public override void VisitWriteNode(WriteNode w)
        {
            if (NowCycle > 0)
                CountOp += 1;
            w.Expr.Visit(this);
        }

        public override void VisitVarDefNode(VarDefNode v)
        {
            if (NowCycle > 0)
                CountOp += 1;
            foreach (var v1 in v.vars)
                v1.Visit(this);
        }

        public override void VisitIfNode(IfNode i)
        {
            if (NowCycle > 0)
                CountOp += 1;
            CountCycle += 1;
            NowCycle += 1;
            i.Expr.Visit(this);
            i.St1.Visit(this);

            if (i.St2 != null)
                i.St2.Visit(this);
            NowCycle -= 1;
        }
    }
}
