using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;

namespace Elmah.Net
{
	public class CryptographyHelper
	{
        public enum HashType : int { md5, sha1, sha256, sha384, sha512, ripemd160 }
        
        // 16 byte salt
        public static string GenerateSalt()
        {
            byte[] buf = new byte[16];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }
                
        public static bool CheckPassword(string password, string encodedpassword, string currentSalt, MembershipPasswordFormat currentFormat)
        {
            switch (currentFormat)
            {
                case MembershipPasswordFormat.Clear:    // 0
                    return encodedpassword.Equals(password);
                case MembershipPasswordFormat.Hashed:   // 1
                    if (currentSalt != null)
                    {
                        // Membership Provider version
                        password = Encode(password, currentFormat, currentSalt);
                        return encodedpassword.Equals(password);
                    }
                    else
                    {
                        // Aspnet Identity Version using embedded salt
                        return VerifyHashedPassword(encodedpassword, password);
                    }
                case MembershipPasswordFormat.Encrypted:    // 2
                default:
                    // Custom - TODO: Remove Custom format when possible
                    return CryptographyHelper.VerifyHash(password, encodedpassword); //TODO: Remove Custom format when possible
            }
        }

        public static string ComputeHash(string content)
		{
			return ComputeHash(content, HashType.sha256, null); 
		}

		protected static bool VerifyHash(string plaintext, string hash)
		{
			return VerifyHash(plaintext, HashType.sha256, hash);
		}



        public static string Encode(string pass, MembershipPasswordFormat passwordFormat, string salt)
        {
            if (passwordFormat == MembershipPasswordFormat.Clear) // MembershipPasswordFormat.Clear
                return pass;

            byte[] bIn = Encoding.Unicode.GetBytes(pass);
            byte[] bSalt = Convert.FromBase64String(salt);
            byte[] bAll = new byte[bSalt.Length + bIn.Length];
            byte[] bRet = null;

            Buffer.BlockCopy(bSalt, 0, bAll, 0, bSalt.Length);
            Buffer.BlockCopy(bIn, 0, bAll, bSalt.Length, bIn.Length);
            if (passwordFormat == MembershipPasswordFormat.Hashed)
            { // MembershipPasswordFormat.Hashed
                HashAlgorithm s = HashAlgorithm.Create(Membership.HashAlgorithmType);
                bRet = s.ComputeHash(bAll);
            }
            else
            {
                bRet = EncryptPassword(bAll);
            }

            return Convert.ToBase64String(bRet);
        }


		protected static string UnEncodePassword(string pass, int passwordFormat)
        {
            switch (passwordFormat)
            {
                case 0: // MembershipPasswordFormat.Clear:
                    return pass;
                case 2: // MembershipPasswordFormat.Encrypted
                    byte[] bIn = Convert.FromBase64String(pass);
                    byte[] bRet = DecryptPassword(bIn);
                    if (bRet == null)
                    {
                        return null;
                    }
                    else
                    {
                        // return decoded value 16 bytes of salt
                        return Encoding.Unicode.GetString(bRet, 16, bRet.Length - 16);
                    }
                default: // MembershipPasswordFormat.Hashed:
                    throw new ProviderException("Cannot Decode Hashed Password");
            }
        }
        

        //return MachineKeySection.EncryptOrDecryptData(true, password, null, 0, password.Length, IVType.None);
        public static byte[] EncryptPassword(byte[] password)
        {
            Type machineKeySection = typeof(MachineKeySection);
            Type[] paramTypes = new Type[] { typeof(bool), typeof(byte[]), typeof(byte[]), typeof(int), typeof(int) };
            MethodInfo encryptOrDecryptData = machineKeySection.GetMethod("EncryptOrDecryptData", BindingFlags.Static | BindingFlags.NonPublic, null, paramTypes, null);

            byte[] decryptedData = (byte[])encryptOrDecryptData.Invoke(null, new object[] { true, password, null, 0, password.Length });

            return decryptedData;
        }

        // return MachineKeySection.EncryptOrDecryptData(false, encodedPassword, null, 0, encodedPassword.Length, IVType.None);
        public static byte[] DecryptPassword(byte[] encodedPassword)
        {
            Type machineKeySection = typeof(MachineKeySection);
            Type[] paramTypes = new Type[] { typeof(bool), typeof(byte[]), typeof(byte[]), typeof(int), typeof(int) };
            MethodInfo encryptOrDecryptData = machineKeySection.GetMethod("EncryptOrDecryptData", BindingFlags.Static | BindingFlags.NonPublic, null, paramTypes, null);

            byte[] decryptedData = (byte[])encryptOrDecryptData.Invoke(null, new object[] { false, encodedPassword, null, 0, encodedPassword.Length });

            return decryptedData;
        }

        
        //public static string GenerateFriendlyPassword(int intPasswordLength)
        //{
        //    string strChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!";

        //    Random r = new Random();
        //    string strNewPassword = "";

        //    for (int i = 0; i < intPasswordLength; i++)
        //    {
        //        int intRandom = r.Next(0, strChars.Length);
        //        strNewPassword += strChars[intRandom];
        //    }

        //    return strNewPassword;
        ////}
        
        ///// <summary>
        ///// Generate Random Password
        ///// </summary>
        ///// <returns></returns>
        //public static string CreateRandomPassword()
        //{
        //    string password;
        //    Random Rnd = new Random();
        //    password = "" + (char)Rnd.Next(65, 91) + Rnd.Next(2, 10) + (char)Rnd.Next(65, 91) + Rnd.Next(2, 10) + (char)Rnd.Next(65, 91) + Rnd.Next(2, 10) + (char)Rnd.Next(65, 91) + Rnd.Next(2, 10);
        //    password = password.Replace("I", "J").Replace("O", "P");
        //    return password;
        //}



        // ASPNetIdentity Default Implementation
        //public static string HashPassword(string password)
        //{
        //    byte[] salt;
        //    byte[] buffer2;
        //    if (password == null)
        //    {
        //        throw new ArgumentNullException("password");
        //    }
        //    using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
        //    {
        //        salt = bytes.Salt;
        //        buffer2 = bytes.GetBytes(0x20);
        //    }
        //    byte[] dst = new byte[0x31];
        //    Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
        //    Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
        //    return Convert.ToBase64String(dst);
        //}

        // ASPNetIdentity Default Implementation
        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }


        /// <summary>Compares two byte arrays for equality.</summary>
        /// <param name="b0">First byte array.</param><param name="b1">Second byte array.</param>
        /// <returns>true if the arrays are equal; false otherwise.</returns>
        private static bool ByteArraysEqual(byte[] b0, byte[] b1)
        {
            if (b0 == b1)
            {
                return true;
            }

            if (b0 == null || b1 == null)
            {
                return false;
            }

            if (b0.Length != b1.Length)
            {
                return false;
            }

            for (int i = 0; i < b0.Length; i++)
            {
                if (b0[i] != b1[i])
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// Generates a hash for the given plain text value and returns a
        /// base64-encoded result. Before the hash is computed, a random salt
        /// is generated and appended to the plain text. This salt is stored at
        /// the end of the hash value, so it can be used later for hash
        /// verification.
        /// </summary>
        /// <param name="plainText">
        /// Plaintext value to be hashed. The function does not check whether
        /// this parameter is null.
        /// </param>
        /// <param name="hashAlgorithm">
        /// Name of the hash algorithm. Allowed values are: "MD5", "SHA1",
        /// "SHA256", "SHA384", and "SHA512" 
        /// </param>
        /// <param name="saltBytes">
        /// Salt bytes. This parameter can be null, in which case a random salt
        /// value will be generated.
        /// </param>
        /// <returns>
        /// Hash value formatted as a base64-encoded string.
        /// </returns>
        public static string ComputeHash(string plainText, HashType algorithm, byte[] saltBytes)
        {
            // If salt is not specified, generate it on the fly.
            if (saltBytes == null)
            {
                // Define min and max salt sizes.
                int minSaltSize = 4;
                int maxSaltSize = 8;

                // Generate a random number for the size of the salt.
                Random random = new Random();
                int saltSize = random.Next(minSaltSize, maxSaltSize);

                // Allocate a byte array, which will hold the salt.
                saltBytes = new byte[saltSize];

                // Initialize a random number generator.
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                // Fill the salt with cryptographically strong byte values.
                rng.GetNonZeroBytes(saltBytes);
            }

            // Convert plain text into a byte array.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Allocate array, which will hold plain text and salt.
            byte[] plainTextWithSaltBytes =
                new byte[plainTextBytes.Length + saltBytes.Length];

            // Copy plain text bytes into resulting array.
            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            // Append salt bytes to the resulting array.
            for (int i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

            // Because we support multiple hashing algorithms, we must define
            // hash object as a common (abstract) base class. We will specify the
            // actual hashing algorithm class later during object creation.
            HashAlgorithm hash;

            // Initialize appropriate hashing algorithm class.
            switch (algorithm)
            {
                case HashType.md5:
                    hash = new MD5CryptoServiceProvider();
                    break;
                case HashType.sha1:
                    hash = new SHA1Managed();
                    break;
                case HashType.sha256:
                    hash = new SHA256Managed();
                    break;
                case HashType.sha384:
                    hash = new SHA384Managed();
                    break;
                case HashType.sha512:
                    hash = new SHA512Managed();
                    break;
                default:
                    hash = new SHA1Managed();
                    break;
            }

            // Compute hash value of our plain text with appended salt.
            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

            // Create array which will hold hash and original salt bytes.
            byte[] hashWithSaltBytes = new byte[hashBytes.Length +
                saltBytes.Length];

            // Copy hash bytes into resulting array.
            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            // Append salt bytes to the result.
            for (int i = 0; i < saltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

            // Convert result into a base64-encoded string.
            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

            // Return the result.
            return hashValue;
        }

        /// <summary>
        /// Compares a hash of the specified plain text value to a given hash
        /// value. Plain text is hashed with the same salt value as the original
        /// hash.
        /// </summary>
        /// <param name="plainText">
        /// Plain text to be verified against the specified hash. The function
        /// does not check whether this parameter is null.
        /// </param>
        /// <param name="hashAlgorithm">
        /// Name of the hash algorithm. Allowed values are: "MD5", "SHA1", 
        /// "SHA256", "SHA384", and "SHA512" (if any other value is specified,
        /// MD5 hashing algorithm will be used). This value is case-insensitive.
        /// </param>
        /// <param name="hashValue">
        /// Base64-encoded hash value produced by ComputeHash function. This value
        /// includes the original salt appended to it.
        /// </param>
        /// <returns>
        /// If computed hash mathes the specified hash the function the return
        /// value is true; otherwise, the function returns false.
        /// </returns>
        public static bool VerifyHash(string plainText, HashType algorithm, string hashValue)
        {
            // Convert base64-encoded hash value into a byte array.
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

            // We must know size of hash (without salt).
            int hashSizeInBits, hashSizeInBytes;


            // Size of hash is based on the specified algorithm.
            switch (algorithm)
            {
                case HashType.md5:
                    hashSizeInBits = 128;
                    break;
                case HashType.sha1:
                    hashSizeInBits = 160;
                    break;
                case HashType.sha256:
                    hashSizeInBits = 256;
                    break;
                case HashType.sha384:
                    hashSizeInBits = 384;
                    break;
                case HashType.sha512:
                    hashSizeInBits = 512;
                    break;
                default: // sha1
                    hashSizeInBits = 160;
                    break;
            }

            // Convert size of hash from bits to bytes.
            hashSizeInBytes = hashSizeInBits / 8;

            // Make sure that the specified hash value is long enough.
            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;

            // Allocate array to hold original salt bytes retrieved from hash.
            byte[] saltBytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];

            // Copy salt from the end of the hash to the new array.
            for (int i = 0; i < saltBytes.Length; i++)
            {
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];
            }

            // Compute a new hash string using same salt
            string expectedHashString = ComputeHash(plainText, algorithm, saltBytes);

            // If the computed hash matches the specified hash the plain text value must be correct.
            return (hashValue == expectedHashString);
        }
	}
}