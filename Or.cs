// Michael Kepple
// November 24th, 2011
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csWIC
{
    class Or : Instruction
    {
        public Or(string operation, string operand)
            : base(operation, operand) { }

        /// <summary>OR execute function</summary>
        /// <param name="pc">Current value of the program counter.</param>
        /// <param name="stack">The program stack.</param>
        /// <param name="symbolTable">The pre-loaded program symbol table.</param>
        /// <param name="jumpTable">The pre-loaded program jump table.</param>
        /// <remarks>Pops two values off of the stack. If either (or both) is non-zero, push a '1' on the stack.</remarks>
        public override void execute(ref int pc, ref Stack<int> stack, ref Dictionary<string, int> symbolTable, Dictionary<string, int> jumpTable)
        {
            int x = stack.Pop();
            int y = stack.Pop();
            if (x != 0 || y != 0)
                stack.Push(1);
            else
                stack.Push(0);
        }
    }
}
