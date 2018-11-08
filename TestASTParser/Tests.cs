using System;
using NUnit.Framework;
using SimpleScanner;
using SimpleParser;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestASTParser
{
    public class ASTParserTests
    {
        public static JObject Parse(string text)
        {
            Scanner scanner = new Scanner();
            scanner.SetSource(text, 0);

            Parser parser = new Parser(scanner);

            var b = parser.Parse();
            if (!b)
                Assert.Fail("программа не распознана");
            else
            {
                JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
                jsonSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                jsonSettings.TypeNameHandling = TypeNameHandling.All;
                string output = JsonConvert.SerializeObject(parser.root, jsonSettings);
                return JObject.Parse(output);
            }

            return null;

        }
    }
    
    [TestFixture]
    public class WhileTests
    {
        
        [Test]
        public void TestWhile()
        {
            var tree = ASTParserTests.Parse("begin while 2 do a:=2 end");
            Assert.AreEqual("ProgramTree.WhileNode, SimpleLang", (string)tree["StList"]["$values"][0]["$type"]);   
            Assert.AreEqual("ProgramTree.IntNumNode, SimpleLang", (string)tree["StList"]["$values"][0]["Expr"]["$type"]);
            Assert.AreEqual("2", ((string)tree["StList"]["$values"][0]["Expr"]["Num"]).Trim());
            Assert.AreEqual("ProgramTree.AssignNode, SimpleLang", (string)tree["StList"]["$values"][0]["Stat"]["$type"]);
        }
    }
    
    [TestFixture]
    public class RepeatTests
    {
        
        [Test]
        public void TestRepeat()
        {
            var tree = ASTParserTests.Parse("begin repeat a:=2 until 2 end");
            Assert.AreEqual("ProgramTree.RepeatNode, SimpleLang", (string)tree["StList"]["$values"][0]["$type"]);
            Assert.AreEqual("ProgramTree.RepeatNode, SimpleLang", (string)tree["StList"]["$values"][0]["$type"]);
            Assert.AreEqual("ProgramTree.BlockNode, SimpleLang", (string)tree["StList"]["$values"][0]["StList"]["$values"][0]["$type"]);
            Assert.AreEqual("ProgramTree.IntNumNode, SimpleLang", (string)tree["StList"]["$values"][0]["Expr"]["$type"]);
            Assert.AreEqual("2", ((string)tree["StList"]["$values"][0]["Expr"]["Num"]).Trim());
        }
    }
    
    [TestFixture]
    public class ForTests
    {
        
        [Test]
        public void TestFor()
        {
            var tree = ASTParserTests.Parse("begin for i:=2 to 10 do a:=(2+b)/(c-a*2); end");
            Assert.AreEqual("ProgramTree.ForNode, SimpleLang", (string)tree["StList"]["$values"][0]["$type"]);
            Assert.AreEqual("ProgramTree.AssignNode, SimpleLang", (string)tree["StList"]["$values"][0]["Asgn"]["$type"]);
            Assert.AreEqual("i", ((string)tree["StList"]["$values"][0]["Asgn"]["Id"]["Name"]).Trim());
            Assert.AreEqual("2", ((string)tree["StList"]["$values"][0]["Asgn"]["Expr"]["Num"]).Trim());
            Assert.AreEqual("ProgramTree.IntNumNode, SimpleLang", (string)tree["StList"]["$values"][0]["Expr"]["$type"]);
            Assert.AreEqual("10", ((string)tree["StList"]["$values"][0]["Expr"]["Num"]).Trim());
            Assert.AreEqual("ProgramTree.AssignNode, SimpleLang", (string)tree["StList"]["$values"][0]["Stat"]["$type"]);
        }
    }
    
    [TestFixture]
    public class WriteTests
    {
        
        [Test]
        public void TestWrite()
        {
            var tree = ASTParserTests.Parse("begin write(2) end");
            Assert.AreEqual("ProgramTree.WriteNode, SimpleLang", (string)tree["StList"]["$values"][0]["$type"]);
            Assert.AreEqual("ProgramTree.IntNumNode, SimpleLang", (string)tree["StList"]["$values"][0]["Expr"]["$type"]);
            Assert.AreEqual("2", ((string)tree["StList"]["$values"][0]["Expr"]["Num"]).Trim());
        }
    }

    [TestFixture]
    public class IfTests
    {

        [Test]
        public void TestIfShort()
        {
            var tree = ASTParserTests.Parse("begin if a then b := 2 end");
            Assert.AreEqual("ProgramTree.IfNode, SimpleLang", (string)tree["StList"]["$values"][0]["$type"]);
            Assert.AreEqual("ProgramTree.IdNode, SimpleLang", (string)tree["StList"]["$values"][0]["Expr"]["$type"]);
            Assert.AreEqual("a", ((string)tree["StList"]["$values"][0]["Expr"]["Name"]).Trim());
            Assert.AreEqual("ProgramTree.AssignNode, SimpleLang", (string)tree["StList"]["$values"][0]["St1"]["$type"]);
        }

        [Test]
        public void TestIfFull()
        {
            var tree = ASTParserTests.Parse("begin if b then c := 3 else b := 4 end");
            Assert.AreEqual("ProgramTree.IfNode, SimpleLang", (string)tree["StList"]["$values"][0]["$type"]);
            Assert.AreEqual("ProgramTree.IdNode, SimpleLang", (string)tree["StList"]["$values"][0]["Expr"]["$type"]);
            Assert.AreEqual("b", ((string)tree["StList"]["$values"][0]["Expr"]["Name"]).Trim());
            Assert.AreEqual("ProgramTree.AssignNode, SimpleLang", (string)tree["StList"]["$values"][0]["St1"]["$type"]);
            Assert.AreEqual("ProgramTree.AssignNode, SimpleLang", (string)tree["StList"]["$values"][0]["St2"]["$type"]);
        }
    }

    [TestFixture]
    public class VarTests
    {

        [Test]
        public void TestVar()
        {
            var tree = ASTParserTests.Parse("begin var a, b end");
            Assert.AreEqual("ProgramTree.VarDefNode, SimpleLang", (string)tree["StList"]["$values"][0]["$type"]);
            Assert.AreEqual("ProgramTree.IdNode, SimpleLang", (string)tree["StList"]["$values"][0]["IdL"]["$values"][0]["$type"]);
            Assert.AreEqual("a", ((string)tree["StList"]["$values"][0]["IdL"]["$values"][0]["Name"]).Trim());
            Assert.AreEqual("ProgramTree.IdNode, SimpleLang", (string)tree["StList"]["$values"][0]["IdL"]["$values"][1]["$type"]);
            Assert.AreEqual("b", ((string)tree["StList"]["$values"][0]["IdL"]["$values"][1]["Name"]).Trim());
        }
    }

    [TestFixture]
    public class BinaryTests
    {

        [Test]
        public void TestBinaryAssign()
        {
            var tree = ASTParserTests.Parse("begin a := (3 + 4) * 7 end");
            Assert.AreEqual("ProgramTree.BinaryNode, SimpleLang", (string)tree["StList"]["$values"][0]["Expr"]["$type"]);
            Assert.AreEqual("ProgramTree.BinaryNode, SimpleLang", (string)tree["StList"]["$values"][0]["Expr"]["Left"]["$type"]);
            Assert.AreEqual("ProgramTree.IntNumNode, SimpleLang", (string)tree["StList"]["$values"][0]["Expr"]["Left"]["Left"]["$type"]);
            Assert.AreEqual("3", ((string)tree["StList"]["$values"][0]["Expr"]["Left"]["Left"]["Num"]).Trim());
            Assert.AreEqual("ProgramTree.IntNumNode, SimpleLang", (string)tree["StList"]["$values"][0]["Expr"]["Left"]["Right"]["$type"]);
            Assert.AreEqual("4", ((string)tree["StList"]["$values"][0]["Expr"]["Left"]["Right"]["Num"]).Trim());
            Assert.AreEqual("+", ((string)tree["StList"]["$values"][0]["Expr"]["Left"]["Operation"]).Trim());
            Assert.AreEqual("ProgramTree.IntNumNode, SimpleLang", (string)tree["StList"]["$values"][0]["Expr"]["Right"]["$type"]);
            Assert.AreEqual("7", ((string)tree["StList"]["$values"][0]["Expr"]["Right"]["Num"]).Trim());
            Assert.AreEqual("*", ((string)tree["StList"]["$values"][0]["Expr"]["Operation"]).Trim());
        }

        [Test]
        public void TestBinaryWrite()
        {
            var tree = ASTParserTests.Parse("begin write(a / 10) end");
            Assert.AreEqual("ProgramTree.BinaryNode, SimpleLang", (string)tree["StList"]["$values"][0]["Expr"]["$type"]);
            Assert.AreEqual("ProgramTree.IdNode, SimpleLang", (string)tree["StList"]["$values"][0]["Expr"]["Left"]["$type"]);
            Assert.AreEqual("ProgramTree.IntNumNode, SimpleLang", (string)tree["StList"]["$values"][0]["Expr"]["Right"]["$type"]);
            Assert.AreEqual("a", ((string)tree["StList"]["$values"][0]["Expr"]["Left"]["Name"]).Trim());
            Assert.AreEqual("10", ((string)tree["StList"]["$values"][0]["Expr"]["Right"]["Num"]).Trim());
            Assert.AreEqual("/", ((string)tree["StList"]["$values"][0]["Expr"]["Operation"]).Trim());
        }
    }
}