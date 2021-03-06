using Interpreter.Lib.RBNF.Analysis.Syntactic;
using Interpreter.Models;
using Interpreter.Services;
using Interpreter.Tests.TestData;
using Xunit;

namespace Interpreter.Tests.Unit
{
    public class ParserTests
    {
        private readonly TestContainer _container;
        private readonly LexerQueryModel _query;

        public ParserTests()
        {
            _container = new TestContainer();
            _query = new LexerQueryModel("tokenTypes.json");
        }

        private Parser GetParser(string text)
        {
            _query.Text = text;
            var lexerCreator = _container.Get<ILexerCreatorService>();
            var parserCreator = _container.Get<IParserCreatorService>();

            var lexer = lexerCreator.CreateLexer(_query);
            var parser = parserCreator.CreateParser(lexer);
            return parser;
        }

        [Theory]
        [ClassData(typeof(ParserSuccessTestData))]
        public void ParserDoesNotThrowTest(string text)
        {
            var parser = GetParser(text);
            
            var ex = Record.Exception(() =>
            {
                // ReSharper disable once UnusedVariable
                var ast = parser.TopDownParse();
            });
            Assert.Null(ex);
        }
    }
}