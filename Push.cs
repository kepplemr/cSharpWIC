// Michael Kepple
// November 24th, 2011
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csWIC
{
    class Push : Instruction
    {
        public Push(string operation, string operand)
            : base(operation, operand) { }

        /// <summary>PUSH execute function</summary>
        /// <param name="pc">Current value of the program counter.</param>
        /// <param name="stack">The program stack.</param>
        /// <param name="symbolTable">The pre-loaded program symbol table.</param>
        /// <param name="jumpTable">The pre-loaded program jump table.</param>
        /// <remarks>Pushes the operand (immediate value or value retrieved from symbol table) onto the stack.</remarks>
        public override void execute(ref int pc, ref Stack<int> stack, ref Dictionary<string, int> symbolTable, Dictionary<string, int> jumpTable)
        {
            int number;
            Boolean num = Int32.TryParse(getOperand(), out number);
            if (num)
            {
                stack.Push(number);
            }
            else if (symbolTable.ContainsKey(getOperand()))
            {
                stack.Push(symbolTable[getOperand()]);
            }
            else
            {
                Console.WriteLine("Invalid PUSH on Line: {0}", pc);
                Console.WriteLine("***   Exiting Interpreter   ***");
                Environment.Exit(1);
            }
        }
    }
}
