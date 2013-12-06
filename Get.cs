// Michael Kepple
// November 24th, 2011
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csWIC
{
    class Get: Instruction
    {
        public Get(string operation, string operand)
            : base(operation, operand) { }

        /// <summary>GET execute function</summary>
        /// <param name="pc">Current value of the program counter.</param>
        /// <param name="stack">The program stack.</param>
        /// <param name="symbolTable">The pre-loaded program symbol table.</param>
        /// <param name="jumpTable">The pre-loaded program jump table.</param>
        /// <remarks>Accepts and stores valid user input into thge symbol table.</remarks>
        public override void execute(ref int pc, ref Stack<int> stack, ref Dictionary<string, int> symbolTable, Dictionary<string, int> jumpTable)
        {
            Console.Write("Enter " + getOperand() + " > ");
            int number;
            string input = Console.ReadLine();
            Boolean num = Int32.TryParse(input, out number);
            if (num)
                symbolTable[getOperand()] = number;
            else
            {
                Console.WriteLine("*** Input must be a valid integer ***");
                execute(ref pc, ref stack, ref symbolTable, jumpTable);
            }
        }
    }
}
