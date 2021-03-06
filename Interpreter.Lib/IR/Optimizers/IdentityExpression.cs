using Interpreter.Lib.IR.Instructions;

namespace Interpreter.Lib.IR.Optimizers
{
    public class IdentityExpression : IOptimizer<Simple>
    {
        public Simple Instruction { get; }

        public IdentityExpression(Simple instruction)
        {
            Instruction = instruction;
        }

        public bool Test()
        {
            var s = Instruction.ToString().Split('=')[1].Trim();
            return s.EndsWith("+ 0") || s.StartsWith("0 +") ||
                   s.EndsWith("* 1") || s.StartsWith("1 *") ||
                   s.EndsWith("- 0") || s.EndsWith("/ 1");
        }

        public void Optimize()
        {
            if (Test())
            {
                Instruction.ReduceToAssignment();
            }
        }
    }
}