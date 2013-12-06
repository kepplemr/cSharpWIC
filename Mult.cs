// Michael Kepple
// November 24th, 2011
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csWIC
{
    class Mult : Instruction
    {
        public Mult(string operation, string operand)
            : base(operation, operand) { }

        /// <summary>MULT execute function</summary>
        /// <param name="pc">Current value of the program counter.</param>
        /// <param name="stack">The program stack.</param>
        /// <param name="symbolTable">The pre-loaded program symbol table.</param>
        /// <param name="jumpTable">The pre-loaded program jump table.</param>
        /// <remarks>Pops two values off of the stack, multiplies them, pushes result.</remarks>
        public override void execute(ref int pc, ref Stack<int> stack, ref Dictionary<string, int> symbolTable, Dictionary<string, int> jumpTable)
        {
            int rightOp = stack.Pop();
            int leftOp = stack.Pop();
            stack.Push(leftOp * rightOp);
        }
    }
}
