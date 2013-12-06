// Michael Kepple
// November 24th, 2011
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Needed references
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;

namespace csWIC
{
    class Interpreter
    {
        [STAThread]
        /// <summary>Initial main function</summary>
        /// <remarks>Static main function that calls the Interpreter constructor.</remarks>
        static void Main(string[] args)
        {
            new Interpreter();
        }

        /// <summary>Interpreter Constructor</summary>
        /// <remarks> Macro-level function that initializes instruction lists, tables, etc. Then begins execution. </remarks>
        public Interpreter()
        {
            // Call helper readfile function to allow the user to select input file.
            string [] lines = readFile();
            // Send resulting string array off to be converted into the instruction array.
            Instruction[] instructions = convertToInstructions(lines);
            // Construct the jump and (initial) symbol tables. Declare the empty stack.
            Dictionary<string, int> jumpTable = buildJumpTable(instructions);
            Dictionary<string, int> symbolTable = buildSymbolTable(instructions);
            Stack<int> stack = new Stack<int>();
            // Set PC initially to zero.
            int pc = 0;
            Stopwatch time = new Stopwatch();
            time.Start();
            // Main program execution loop
            while (pc < instructions.Length)
            {
                instructions[pc].execute(ref pc, ref stack, ref symbolTable, jumpTable);
                pc++;
            }
            time.Stop();
            Console.WriteLine("Time Elapsed: " + time.Elapsed);
        }

        /// <summary>Builds the Symbol Table</summary>
        /// <returns>Dictionary object representing the Symbol table</returns>
        /// <remarks>If opcode = one of the instructions that can generate symbols, test and see if it is a symbol
        /// or a number. If it's a symbol and not already present, add it to the symbol table.</remarks>
        private Dictionary<string, int> buildSymbolTable(Instruction[] instructions)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();
            for (int i = 0; i < instructions.Length; i++)
            {
                string op = instructions[i].getOpcode();
                string oper = instructions[i].getOperand();
                if (op == "get" || op == "put" || op == "push" || op == "pop")
                {
                    int test = 0;
                    Boolean num = Int32.TryParse(oper, out test);
                    if (!num)
                    {
                        if (!temp.ContainsKey(oper))
                            temp.Add(oper, 0);
                    }
                }
            }
            return temp;
        }

        /// <summary>Builds the Jump Table</summary>
        /// <param name="instructions">All program instructions.</param>
        /// <returns>Dictionary object representing the Jump table</returns>
        /// <remarks>Line numbers start at zero</remarks>
        private Dictionary<string, int> buildJumpTable(Instruction[] instructions)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();
            for (int i = 0; i < instructions.Length; i++)
            {
                if (instructions[i].getOpcode() == "label")
                {
                    temp.Add(instructions[i].getOperand(), i);
                }
            }
            return temp;
        }

        /// <summary>Creates individual Instructions</summary>
        /// <param name="operation">String representation of Instruction code</param>
        /// <param name="operand">String representation of Instruction operand.</param>
        /// <param name="lineNum">Current line number of instruction (used for error message).</param>
        /// <returns>Specified instruction</returns>
        /// <remarks>Given an opcode and an operand, attempts to match and call appropriate constructor.</remarks>
        private Instruction createInstructionObject(string operation, string operand, int lineNum)
        {
            Instruction inst = null;
            switch (operation)
            {
                case "get":
                    inst = new Get(operation, operand);
                    break;
                case "put":
                    inst = new Put(operation, operand);
                    break;
                case "push":
                    inst = new Push(operation, operand);
                    break;
                case "pop":
                    inst = new Pop(operation, operand);
                    break;
                case "add":
                    inst = new Add(operation, operand);
                    break;
                case "sub":
                    inst = new Sub(operation, operand);
                    break;
                case "mult":
                    inst = new Mult(operation, operand);
                    break;
                case "div":
                    inst = new Div(operation, operand);
                    break;
                case "and":
                    inst = new And(operation, operand);
                    break;
                case "or":
                    inst = new Or(operation, operand);
                    break;
                case "not":
                    inst = new Not(operation, operand);
                    break;
                case "tstge":
                    inst = new Tstge(operation, operand);
                    break;
                case "tstgt":
                    inst = new Tstgt(operation, operand);
                    break;
                case "tstle":
                    inst = new Tstle(operation, operand);
                    break;
                case "tstlt":
                    inst = new Tstlt(operation, operand);
                    break;
                case "tsteq":
                    inst = new Tsteq(operation, operand);
                    break;
                case "tstne":
                    inst = new Tstne(operation, operand);
                    break;
                case "j":
                    inst = new Jump(operation, operand);
                    break;
                case "jf":
                    inst = new JumpF(operation, operand);
                    break;
                case "halt":
                    inst = new Halt(operation, operand);
                    break;
                case "nop":
                    inst = new NoOp(operation, operand);
                    break;
                case "mod":
                    inst = new Mod(operation, operand);
                    break;
                default:
                    if (operand == "label")
                        inst = new Label(operand, operation);
                    else
                    {
                        Console.WriteLine("Invalid instruction on Line: {0}", lineNum);
                        Console.WriteLine("***   Exiting Interpreter   ***");
                        Environment.Exit(1);
                    }
                    break;
            }
            return inst;
        }

        /// <summary>Creates instructions Instruction array.</summary>
        /// <param name="lines">String array of all lines of code.</param>
        /// <returns>Array of instructions representing entire program code.</returns>
        /// <remarks>Uses RegEx to allow for more WIC formatting options.</remarks>
        private Instruction[] convertToInstructions(string[] lines)
        {
            Instruction[] instructions = new Instruction[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                // Regular expression to capture the Opcode/Operand and test the formatting of the input line.
                Regex testInput = new Regex("\\|.*|[\t\x20]*([a-zA-Z0-9]+)[\t\x20]*([a-zA-Z0-9]*)\\s*[^\\S]*\\|?.*|\\s*");
                Match m = testInput.Match(lines[i]);
                if (m.Success)
                {
                    // Case the operation and operand to lowercase and trim any whitespace.
                    string op = m.Groups[1].Value.ToLower().Trim();
                    string oper = m.Groups[2].Value.ToLower().Trim();
                    if (op == "")
                        op = "nop";
                    // Attempt to create an instruction from the operation/operand.
                    instructions[i] = createInstructionObject(op, oper, i);
                }
                else
                {
                    // If invalid format, exit.
                    Console.WriteLine("Invalid instruction format entered!");
                    Console.WriteLine("***   Exiting Interpreter   ***");
                    Environment.Exit(1);
                }
            }
            return instructions;
        }

        /// <summary>Accepts user input, creates string array.</summary>
        /// <returns>Array of strings to be parsed for operations, operands.</returns>
        /// <remarks>Each string = line in the WIC source file.</remarks>
        private string[] readFile() 
        {
            while (true)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Title = "Select WIC source file...";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string[] lines = File.ReadAllLines(dialog.FileName);
                    return lines;
                }
            }
        }
    }
}
