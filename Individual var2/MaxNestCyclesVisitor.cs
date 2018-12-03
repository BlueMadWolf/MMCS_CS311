using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    public class MaxNestCyclesVisitor : AutoVisitor
    {
        public int MaxNest = 0;
        int NowCycle = 0;

        public override void VisitCycleNode(CycleNode c)
        {
            NowCycle += 1;

            if (NowCycle > MaxNest)
                MaxNest = NowCycle;

            c.Expr.Visit(this);
            c.Stat.Visit(this);
            NowCycle -= 1;
        }
    }
}
