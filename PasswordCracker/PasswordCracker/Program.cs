using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace PasswordCracker
{
    /// <summary>
    /// A list of md5 hashed passwords is contained within the passwords_hashed.txt file.  Your task
    /// is to crack each of the passwords.  Your input will be an array of strings obtained by reading
    /// in each line of the text file and your output will be validated by passing an array of the
    /// cracked passwords to the Validator.ValidateResults() method.  This method will compute a SHA256
    /// hash of each of your solved passwords and compare it against a list of known hashes for each
    /// password.  If they match, it means that you correctly cracked the password.  Be warned that the
    /// test is ALL or NOTHING.. so one wrong password means the test fails.
    /// </summary>
    class Program
    {
        public static string md5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }


        public static string convertArrayToString(string[] input)
        {
            string output = string.Join(",", input);
            output = output.Replace(",", "");
            return output;
        }
        
        static void Main(string[] args)
        {
            string[] hashedPasswords = File.ReadAllLines("passwords_hashed.txt");
            string[] crackedPasswords = new string[hashedPasswords.Length];
            Dictionary<string, string> generatedPasswords = new Dictionary<string, string>();
            string[] alphabet = new string[]
            {
                "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"
            };
            Console.WriteLine("MD5 Password Cracker v1.0");
            // Thread t1 = new Thread(new ThreadStart());

            for (int a = 0; a < alphabet.Length; a++)
            {
                for (int b = 0; b < alphabet.Length; b++)
                {
                    for (int c = 0; c < alphabet.Length; c++)
                    {
                        for (int d = 0; d < alphabet.Length; d++)
                        {
                            for (int e = 0; e < alphabet.Length; e++)
                            {
                                var pass = alphabet[a] + alphabet[b] + alphabet[c] + alphabet[d] + alphabet[e];
                                generatedPasswords.Add(md5(pass), pass);
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < hashedPasswords.Length; i++)
            {
                if (generatedPasswords.ContainsKey(hashedPasswords[i]))
                {
                    crackedPasswords[i] = generatedPasswords[hashedPasswords[i]];
                }
            }
            // Use this method to test if you managed to correctly crack all the passwords
            // Note that hashedPasswords will need to be swapped out with an array the exact
            // same length that contains all the cracked passwords
            bool passwordsValidated = Validator.ValidateResults(crackedPasswords);
            
            Console.WriteLine($"\nPasswords successfully cracked: {passwordsValidated}");
        }
    }
}