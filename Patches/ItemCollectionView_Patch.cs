using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using System;
using UnityEngine;
using System.Linq;
using Kitchen;
using KitchenData;

namespace EverythingAlways.Patches
{
    [HarmonyPatch(typeof(ItemCollectionView), nameof(ItemCollectionView.UpdateData))]
    static class ItemCollectionView_Patch
    {
        static readonly List<OpCode> OPCODES_TO_MATCH = new List<OpCode>()
        {
            OpCodes.Stloc_S,
            OpCodes.Ldloc_S,
            OpCodes.Conv_R4,
            OpCodes.Ldc_R4,
            OpCodes.Ldloc_S,
            OpCodes.Conv_R4,
            OpCodes.Mul,
            OpCodes.Sub,
            OpCodes.Ldc_R4,
        };

        static readonly List<object> OPERANDS_TO_MATCH = new List<object>() { };

        static readonly List<OpCode> MODIFIED_OPCODES = new List<OpCode>() { };

        static readonly List<object> MODIFIED_OPERANDS = new List<object>()
        {
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            0.45f
        };

        const int EXPECTED_MATCH_COUNT = 1;

        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> OriginalLambdaBody_Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            Main.LogInfo("ItemCollectionView Transpiler");
            Main.LogInfo("Attempt to space out Side & Main orders a bit more");
            List<CodeInstruction> list = instructions.ToList();

            int matches = 0;
            int windowSize = OPCODES_TO_MATCH.Count;
            for (int i = 0; i < list.Count - windowSize; i++)
            {
                for (int j = 0; j < windowSize; j++)
                {
                    if (OPCODES_TO_MATCH[j] == null)
                    {
                        return instructions;
                    }

                    int index = i + j;
                    OpCode opCode = list[index].opcode;
                    if (j < OPCODES_TO_MATCH.Count && opCode != OPCODES_TO_MATCH[j])
                    {
                        break;
                    }

                    if (j < OPERANDS_TO_MATCH.Count && OPERANDS_TO_MATCH[j] != null)
                    {
                        object operand = list[index].operand;
                        if (OPERANDS_TO_MATCH[j] != operand)
                        {
                            break;
                        }
                    }

                    if (j == OPCODES_TO_MATCH.Count - 1)
                    {
                        if (matches > EXPECTED_MATCH_COUNT)
                        {
                            Main.LogError("Number of matches found exceeded EXPECTED_MATCH_COUNT! Returning original IL.");
                            return instructions;
                        }

                        // Perform replacements
                        for (int k = 0; k < MODIFIED_OPCODES.Count; k++)
                        {
                            if (MODIFIED_OPCODES[k] != null)
                            {
                                int replacementIndex = i + k;
                                OpCode beforeChange = list[replacementIndex].opcode;
                                list[replacementIndex].opcode = MODIFIED_OPCODES[k];
                                Main.LogInfo($"Line {replacementIndex}: Replaced Opcode ({beforeChange} ==> {MODIFIED_OPCODES[k]})");
                            }
                        }

                        for (int k = 0; k < MODIFIED_OPERANDS.Count; k++)
                        {
                            if (MODIFIED_OPERANDS[k] != null)
                            {
                                int replacementIndex = i + k;
                                object beforeChange = list[replacementIndex].operand;
                                list[replacementIndex].operand = MODIFIED_OPERANDS[k];
                                Main.LogInfo($"Line {replacementIndex}: Replaced operand ({beforeChange ?? "null"} ==> {MODIFIED_OPERANDS[k] ?? "null"})");
                            }
                        }
                    }
                }
            }

            return list.AsEnumerable();
        }
    }
}
