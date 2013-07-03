using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SFENFileCleaner {
    class Program {
        static int Main(string[] args) {
            try {
                foreach (string path in args) {
                    DoFile(path);
                }
                return 0;
            } catch (Exception e) {
                Console.Error.WriteLine(e);
                return 1;
            }
        }

        private static void DoFile(string path) {
            Console.WriteLine(path + " 処理開始");
            var lines = File.ReadAllLines(path, Encoding.Default)
                .Select(x => x.Trim())
                .Where(x => 0 < x.Length)
                .OrderBy(x => x).Distinct().ToArray();
            List<string> result = new List<string>();
            for (int i = 0; i < lines.Length; i++) {
                if (i + 1 < lines.Length && lines[i + 1].StartsWith(lines[i])) {
                    Console.WriteLine("前方一致している棋譜のため除外: " + lines[i]);
                } else {
                    result.Add(lines[i]);
                }
            }
            File.WriteAllLines(path, result.ToArray(), Encoding.Default);
            Console.WriteLine(path + " 処理完了 出力行数=" + result.Count);
        }
    }
}
