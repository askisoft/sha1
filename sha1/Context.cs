using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

namespace Sha1
{
    public class Context
    {
        private static readonly string _currentProcessName = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);

        public string ExeName { get { return _currentProcessName; } }
        public string HashName { get; private set; }
        public HashAlgorithm HashAlgorithm { get; private set; }
        private Context()
        {
        }

        public static Context FromCurrentProcess()
        {
            if (_currentProcessName.ToLower() == "md5.exe")
            {
                return new Context
                {
                    HashName = "MD5",
                    HashAlgorithm = new MD5CryptoServiceProvider()
                };
            }
            else if (_currentProcessName.ToLower() == "sha512.exe")
            {
                return new Context
                {
                    HashName = "SHA-512",
                    HashAlgorithm = new SHA512CryptoServiceProvider()
                };
            }
            else if (_currentProcessName.ToLower() == "sha256.exe")
            {
                return new Context
                {
                    HashName = "SHA-256",
                    HashAlgorithm = new SHA256CryptoServiceProvider()
                };
            }
            else if (_currentProcessName.ToLower() == "sha1.exe")
            {
                return new Context
                {
                    HashName = "SHA-1",
                    HashAlgorithm = new SHA1CryptoServiceProvider()
                };
            }
            else
            {
                throw new InvalidOperationException("Executable name must be named one of the following: \"md5.exe\", \"sha512.exe\", \"sha256.exe\" or \"sha1.exe\".");
            }
        }
    }
}
