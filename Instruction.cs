// Michael Kepple
// November 24th, 2011
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csWIC
{
    abstract class Instruction
    {
        private String opcode;
        private String operand;

        public String getOpcode()
        {
            return opcode;
        }

        public String getOperand()
        {
            return operand;
        }

        private void setOpcode(String op)
        {
            opcode = op;
        }

        private void setOperand(String oper)
        {
            operand = oper;
        }

        public Instruction(String op, String oper)
        {
            setOpcode(op);
            setOperand(oper);
        }
        
        // Abstract class that all Instruction types will implement.
        public abstract void execute(ref int pc, ref Stack<int> stack, ref Dictionary<string, int> symbolTable, Dictionary<string, int> jumpTable);
    }
}
