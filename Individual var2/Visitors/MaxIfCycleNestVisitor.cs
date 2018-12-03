using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;


namespace SimpleLang.Visitors
{
    public class MaxIfCycleNestVisitor : AutoVisitor
    {
        public int MaxNest = 0;
        int NowCycle = 0;

        public override void VisitIfNode(IfNode i)
        {
            NowCycle += 1;

            if (NowCycle > MaxNest)
                MaxNest = NowCycle;

            i.Expr.Visit(this);
            i.St1.Visit(this);
            if (i.St2 != null)
                i.St2.Visit(this);
            NowCycle -= 1;
        }
    }
}