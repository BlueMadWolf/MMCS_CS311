using System.Collections.Generic;

namespace ProgramTree
{
    public enum AssignType { Assign, AssignPlus, AssignMinus, AssignMult, AssignDivide };

    public class Node // базовый класс для всех узлов    
    {
    }

    public class ExprNode : Node // базовый класс для всех выражений
    {
    }

    public class IdNode : ExprNode
    {
        public string Name { get; set; }
        public IdNode(string name) { Name = name; }
    }

    public class IntNumNode : ExprNode
    {
        public int Num { get; set; }
        public IntNumNode(int num) { Num = num; }
    }

    public class StatementNode : Node // базовый класс для всех операторов
    {
    }

    public class AssignNode : StatementNode
    {
        public IdNode Id { get; set; }
        public ExprNode Expr { get; set; }
        public AssignType AssOp { get; set; }
        public AssignNode(IdNode id, ExprNode expr, AssignType assop = AssignType.Assign)
        {
            Id = id;
            Expr = expr;
            AssOp = assop;
        }
    }

    public class CycleNode : StatementNode
    {
        public ExprNode Expr { get; set; }
        public StatementNode Stat { get; set; }
        public CycleNode(ExprNode expr, StatementNode stat)
        {
            Expr = expr;
            Stat = stat;
        }
    }

    public class ForNode : StatementNode
    {
        public AssignNode Asgn { get; set;  }
        public ExprNode Expr { get; set; }
        public StatementNode Stat { get; set; }
        public ForNode(AssignNode assign, ExprNode expr, StatementNode stat)
        {
            Asgn = assign;
            Expr = expr;
            Stat = stat;
        }
    }

    public class WhileNode : StatementNode
    {
        public ExprNode Expr { get; set; }
        public StatementNode Stat { get; set; }
        public WhileNode(ExprNode expr, StatementNode stat)
        {
            Expr = expr;
            Stat = stat;
        }
    }

    public class RepeatNode : StatementNode
    {
        public List<StatementNode> StList = new List<StatementNode>();
        public ExprNode Expr { get; set; }
        public RepeatNode(StatementNode st, ExprNode ex)
        {
            StList.Add(st);
            Expr = ex;
        }
    }

    public class WriteNode : StatementNode
    {
        public ExprNode Expr { get; set; }
        public WriteNode(ExprNode ex)
        {
            Expr = ex;
        }
    }

    public class BlockNode : StatementNode
    {
        public List<StatementNode> StList = new List<StatementNode>();
        public BlockNode(StatementNode stat)
        {
            Add(stat);
        }
        public void Add(StatementNode stat)
        {
            StList.Add(stat);
        }

    }

    public class VarDefNode : StatementNode
    {
        public List<IdNode> IdL = new List<IdNode>();
        public VarDefNode(IdNode idl)
        {
            Add(idl);
        }
        public void Add(IdNode stat)
        {
            IdL.Add(stat);
        }
    }

    public class BinaryNode : ExprNode
    {
        public ExprNode left { get; set; }
        public ExprNode right { get; set; }
        public char operation;
        public BinaryNode(ExprNode l, ExprNode r, char op)
        {
            left = l;
            right = r;
            operation = op;
        }
    }

    public class IfNode : StatementNode
    {
        public StatementNode St1 { get; set; }
        public StatementNode St2 { get; set; }
        public ExprNode Expr { get; set; }

        public IfNode(ExprNode ex, StatementNode st)
        {
            St1 = st;
            Expr = ex;
        }

        public IfNode(ExprNode ex, StatementNode st1, StatementNode st2)
        {
            St1 = st1;
            St2 = st2;
            Expr = ex;
        }
    }

}