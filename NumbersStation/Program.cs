using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech;
using System.Speech.Synthesis;

namespace NumbersStation
{
    class Program
    {
        static void Main(string[] args)
        {
            Random Seed = new Random();
            SpeechSynthesizer speechEngine = new SpeechSynthesizer();
            PromptBuilder promptBuilder = new PromptBuilder();
            PromptStyle promptStyle = new PromptStyle
            {
                Emphasis = PromptEmphasis.Strong,
                Rate = PromptRate.ExtraSlow,
                Volume = PromptVolume.Medium
            };
            int seedNum = Seed.Next(100,999);
            promptBuilder.StartStyle(promptStyle);
            promptBuilder.AppendText("Warning - Signature");
            promptBuilder.AppendBreak(new TimeSpan(20000000));
            promptBuilder.AppendTextWithHint(seedNum.ToString(), SayAs.SpellOut);
            promptBuilder.AppendText("Begin");
            promptBuilder.AppendBreak(new TimeSpan(20000000));
            string transmission = "Hello World";
            promptBuilder.AppendTextWithHint(Encode(transmission,seedNum), SayAs.SpellOut);
            promptBuilder.AppendBreak(new TimeSpan(20000000));
            promptBuilder.AppendText("End");
            promptBuilder.EndStyle();
            speechEngine.Speak(promptBuilder);
            Console.ReadKey();
        }

        static string Encode(string input, int seed)
        {
            Random encoder = new Random(seed);
            Hashtable cipher = new Hashtable();
            string encodedMessage = "";
            for (char c = 'A'; c <= 'Z'; c++)
            {
                cipher.Add(c, encoder.Next(1000,9999));
                Console.WriteLine(cipher[c]);
            }
            for (int letter = 0; letter < input.Length; letter++)
            {
                if (input[letter] == ' ')
                {
                    encodedMessage += "SPN";
                }
                else
                {
                    Console.Write(input[letter]);
                    encodedMessage += cipher[input.ToUpper()[letter]];
                    encodedMessage += " NC ";
                }
            }
            return encodedMessage;
        }
    }
}
