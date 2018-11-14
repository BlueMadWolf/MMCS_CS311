using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    public class ExprComplexityVisitor : AutoVisitor
    {
        public int CurrCompl = 0;
        public int CurrNesting = 0;
        public int NestExpr = 0;
        public List<int> complexities = new List<int>();

        // список должен содержать сложность каждого выражения, встреченного при обычном порядке обхода AST
        public List<int> getComplexityList()
        {
            return complexities;
        }

        public override void VisitBinOpNode(BinOpNode binop)
        {
            CurrNesting += 1;
            CurrCompl += (binop.Op == '+') || (binop.Op == '-') ? 1 : 3;

            binop.Left.Visit(this);
            binop.Right.Visit(this);
            CurrNesting -= 1;

            if (CurrNesting == 0)
            {
                complexities.Add(CurrCompl);
                CurrCompl = 0;
            }
        }

        public override void VisitAssignNode(AssignNode a)
        {
            a.Id.Visit(this);
            NestExpr++;
            (a.Expr as ExprNode).Visit(this);
            NestExpr--;
        }

        public override void VisitCycleNode(CycleNode c)
        {
            NestExpr++;
            c.Expr.Visit(this);
            NestExpr--;
            c.Stat.Visit(this);
        }

      
        public override void VisitWriteNode(WriteNode w)
        {
            NestExpr++;
            w.Expr.Visit(this);
            NestExpr--;
        }

        public override void VisitIfNode(IfNode i)
        {
            NestExpr++;
            i.Expr.Visit(this);
            NestExpr--;
            i.St1.Visit(this);
            if (i.St2 != null)
                i.St2.Visit(this);
        }

    }
}
