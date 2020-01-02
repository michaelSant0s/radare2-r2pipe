using System;
using r2pipe;
using System.Text.Json;

namespace LocalExampleCoreJson
{
    class Program
    {
        static void Main(string[] args)
        {
            var pipe = new R2Pipe("HelloWorld");
            pipe.RunCommand("ood");
            pipe.RunCommand("db sym.main; dc");
            string line = pipe.RunCommand("pdj 1");

            JsonDocument document = JsonDocument.Parse(line);
            var arrayEnumerator = document.RootElement.EnumerateArray();

            JsonElement opcode = default;
            while (arrayEnumerator.MoveNext() && opcode.ValueKind == JsonValueKind.Undefined)
            {
                JsonElement current = arrayEnumerator.Current;
                current.TryGetProperty("opcode", out opcode);
            }
            Console.WriteLine(opcode.GetRawText());

            System.Console.WriteLine();
            pipe.Dispose();
        }
    }
}
