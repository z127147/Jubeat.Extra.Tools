﻿using System;
using System.Linq;
using System.Text;
using static Jubeat.Extra.Models.Maps.Memo.MemoMap;

namespace Jubeat.Extra.Models.Maps.Memo
{
    public class MemoMeasurePart
    {
        public int[][] Buttons;

        public MemoMeasurePart()
        {
            Buttons = new int[4][];
        }

        public void AddButtons(string text)
        {
            for (var i = 0; i < 4; i++)
            {
                if (Buttons[i] == null)
                {
                    Buttons[i] = text.ToCharArray().Select(c =>
                    {
                        var index = NumberCharacters.IndexOf(value: c) + 1;
                        if (index > 0)
                        {
                            return index;
                        }
                        if (LeftCharacters.Contains(value: c))
                        {
                            return -1;
                        }
                        if (UpCharacters.Contains(value: c))
                        {
                            return -2;
                        }
                        if (RightCharacters.Contains(value: c))
                        {
                            return -3;
                        }
                        if (DownCharacters.Contains(value: c))
                        {
                            return -4;
                        }
                        if (EmptyCharacters.Contains(value: c))
                        {
                            return 0;
                        }
                        Console.WriteLine(value: $"[Warning] Unrecognized buttons part: {text}.");
                        return -255;
                    }).ToArray();
                    break;
                }
            }
            Console.WriteLine(value: "[Warning] Too many buttons, the excess will be ignored.");
        }

        public override string ToString()
        {
            var output = new char[4][];

            for (var i = 0; i < 4; i++)
            {
                output[i] = Buttons[i]?.Select(b =>
                {
                    if (b > 0)
                    {
                        return NumberCharacters[index: b - 1];
                    }
                    switch (b)
                    {
                        case 0:
                            return EmptyCharacters.First();

                        case -1:
                            return LeftCharacters.First();

                        case -2:
                            return UpCharacters.First();

                        case -3:
                            return RightCharacters.First();

                        case -4:
                            return DownCharacters.First();

                        default:
                            Console.WriteLine(value: $"[Warning] Detect an unrecognized data: {b}.");
                            return EmptyCharacters.First();
                    }
                }).ToArray();

                if (output[i] == null)
                {
                    Console.WriteLine(value: "[Warning] Detect a broken butoons data.");
                    return "";
                }
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    switch (Buttons[i][j])
                    {
                        case -1:
                            for (var k = j - 1; k >= 0; k--)
                            {
                                if (EmptyCharacters.Contains(value: output[i][k]))
                                {
                                    output[i][k] = HorizontalCharacters.First();
                                }
                                else
                                {
                                    break;
                                }
                            }
                            break;

                        case -2:
                            for (var k = i - 1; k >= 0; k--)
                            {
                                if (EmptyCharacters.Contains(value: output[k][j]))
                                {
                                    output[k][j] = VerticalCharacters.First();
                                }
                                else
                                {
                                    break;
                                }
                            }
                            break;

                        case -3:
                            for (var k = j + 1; k < 4; k++)
                            {
                                if (EmptyCharacters.Contains(value: output[i][k]))
                                {
                                    output[i][k] = VerticalCharacters.First();
                                }
                                else
                                {
                                    break;
                                }
                            }
                            break;

                        case -4:
                            for (var k = i + 1; k < 4; k++)
                            {
                                if (EmptyCharacters.Contains(value: output[k][j]))
                                {
                                    output[k][j] = VerticalCharacters.First();
                                }
                                else
                                {
                                    break;
                                }
                            }
                            break;
                    }
                }
            }

            return string.Join(separator: "\n", values: output.Select(l => string.Concat(values: l)));
        }
    }
}
