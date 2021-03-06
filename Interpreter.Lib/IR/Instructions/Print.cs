using System;
using Interpreter.Lib.VM;
using Interpreter.Lib.VM.Values;

namespace Interpreter.Lib.IR.Instructions
{
    public class Print : Instruction
    {
        private readonly IValue _value;
        
        public Print(int number, IValue value) : base(number)
        {
            _value = value;
        }

        public override int Execute(VirtualMachine vm)
        {
            Console.WriteLine(_value.Get(vm.Frames.Peek()));
            return Number + 1;
        }

        protected override string ToStringRepresentation() => $"Print {_value}";
    }
}