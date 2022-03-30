using Interpreter.Lib.Semantic.Types;
using Xunit;

namespace Interpreter.Tests.Unit
{
    public class TypeTests
    {
        [Fact]
        public void TypeEqualityTest()
        {
            var number = new Type("number");
            var arrayOfNumbers = new ArrayType(number);
            Assert.False(arrayOfNumbers.Equals(number));
            Assert.False(number.Equals(arrayOfNumbers));
        }

        [Fact]
        public void TypeStringRepresentationTest()
        {
            var matrix = new ArrayType(new ArrayType(new Type("number")));
            
            Assert.Equal("number[][]", matrix.ToString());
        }

        [Fact]
        public void ObjectTypeStringRepresentationTest()
        {
            var number = new Type("number");
            var point = new ObjectType(new PropertyType[] {new("x", number), new("y", number)});
            var line = new ObjectType(new PropertyType[] {new("start", point), new("end", point)});
            
            Assert.Equal("{start: {x: number;y: number;};end: {x: number;y: number;};}", line.ToString());
        }

        [Fact]
        public void ObjectTypeEqualityTest()
        {
            var number = new Type("number");
            var p2d1 = new ObjectType(
                new PropertyType[]
                {
                    new("x", number), 
                    new("y", number)
                }
            );
            var p2d2 = new ObjectType(
                new PropertyType[]
                {
                    new("y", number), 
                    new("x", number)
                }
            );
            Assert.Equal(p2d1, p2d2);

            var p3d1 = new ObjectType(
                new PropertyType[]
                {
                    new("a", number),
                    new("x", number),
                    new("y", number)
                }
            );
            var p3d2 = new ObjectType(
                new PropertyType[]
                {
                    new("y", number), 
                    new("x", number),
                    new("z", number)
                }
            );
            Assert.NotEqual(p3d1, p3d2);
            Assert.NotEqual(p3d2, p2d1);
        }
    }
}