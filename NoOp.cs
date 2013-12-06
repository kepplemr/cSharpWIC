﻿// Michael Kepple
// November 24th, 2011
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csWIC
{
    class NoOp : Instruction
    {
        public NoOp(string operation, string operand)
            : base(operation, operand) { }

        /// <summary>NOP execute function</summary>
        /// <param name="pc">Current value of the program counter.</param>
        /// <param name="stack">The program stack.</param>
        /// <param name="symbolTable">The pre-loaded program symbol table.</param>
        /// <param name="jumpTable">The pre-loaded program jump table.</param>
        /// <remarks>Does a whole lot of nothing.</remarks>
        public override void execute(ref int pc, ref Stack<int> stack, ref Dictionary<string, int> symbolTable, Dictionary<string, int> jumpTable)
        {
            // Nothing to see here.
        }
    }
}
