using System;
using System.IO;
using System.Text;

namespace Sha1
{
    class Program
    {
        private static readonly Context _context = Context.FromCurrentProcess();

        static int Main(string[] args)
        {
            if (args.Length == 0) {
                Usage();
                return 1;
            }

            try
            {
                var hash = HashFile(args[0]);

                StdOut(hash);

                return 0;
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception.Message);
                return 2;
            }
        }

        private static byte[] HashFile(string path)
        {
            using (var stream = File.OpenRead(path))
            {
                return _context.HashAlgorithm.ComputeHash(stream);
            }
        }

        private static void StdOut(byte[] bytes)
        {
            var builder = new StringBuilder();

            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("X2"));
            }

            Console.WriteLine(builder.ToString().ToLower());
        }

        private static void Usage()
        {
            Console.WriteLine($"Usage: {_context.ExeName} file");
            Console.WriteLine();
            Console.WriteLine($"Compute {_context.HashName} hash of file and output to STDOUT its hex string representation.");
        }
    }
}
