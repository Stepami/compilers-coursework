using Interpreter.Lib.RBNF.Analysis.Lexical;
using Interpreter.Lib.Semantic.Types;

namespace Interpreter.Lib.Semantic.Exceptions
{
    public class WrongArrayLiteralDeclaration : SemanticException
    {
        public WrongArrayLiteralDeclaration(Segment segment, Type type) : 
            base($"{segment} Wrong array literal declaration: all array elements must be of type {type}")
        {
        }
    }
}