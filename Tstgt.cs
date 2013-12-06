// Michael Kepple
// November 24th, 2011
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csWIC
{
    class Tstgt : Instruction
    {
        public Tstgt(string operation, string operand)
            : base(operation, operand) { }

        /// <summary>TSTGT execute function</summary>
        /// <param name="pc">Current value of the program counter.</param>
        /// <param name="stack">The program stack.</param>
        /// <param name="symbolTable">The pre-loaded program symbol table.</param>
        /// <param name="jumpTable">The pre-loaded program jump table.</param>
        /// <remarks>Pops top value off the stack, compares to zero. Pushes '1' if greater than zero, '0' otherwise.</remarks>
        public override void execute(ref int pc, ref Stack<int> stack, ref Dictionary<string, int> symbolTable, Dictionary<string, int> jumpTable)
        {
            int tst = stack.Pop();
            if (tst > 0)
                stack.Push(1);
            else
                stack.Push(0);
        }
    }
}
