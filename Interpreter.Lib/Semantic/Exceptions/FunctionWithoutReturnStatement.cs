using Interpreter.Lib.RBNF.Analysis.Lexical;

namespace Interpreter.Lib.Semantic.Exceptions
{
    public class FunctionWithoutReturnStatement : SemanticException
    {
        public FunctionWithoutReturnStatement(Segment segment) :
            base(
                $"{segment} function with non-void return type must have a return statement"
            )
        {
        }
    }
}