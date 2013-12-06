// Michael Kepple
// November 24th, 2011
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csWIC
{
    class Put : Instruction
    {
        public  Put(string operation, string operand)
            : base(operation, operand) { }

        /// <summary>PUT execute function</summary>
        /// <param name="pc">Current value of the program counter.</param>
        /// <param name="stack">The program stack.</param>
        /// <param name="symbolTable">The pre-loaded program symbol table.</param>
        /// <param name="jumpTable">The pre-loaded program jump table.</param>
        /// <remarks>Looks the operand up in the symbol table, prints it's value to the screen.</remarks>
        public override void execute(ref int pc, ref Stack<int> stack, ref Dictionary<string, int> symbolTable, Dictionary<string, int> jumpTable)
        {
            Console.WriteLine(getOperand() + " = " + symbolTable[getOperand()]);
        }
    }
}
