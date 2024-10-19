using OpenModular.Common.Utils.DependencyInjection;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Runtime.CompilerServices;
using OpenModular.Module.UAP.Core.Domain.Accounts;

namespace OpenModular.Module.UAP.Core.Infrastructure;

// Copy from https://github.com/aspnet/Identity/blob/rel/2.0.0/src/Microsoft.Extensions.Identity.Core/PasswordHasher.cs

public class PasswordHasher : IPasswordHasher, ISingletonDependency
{
    private readonly int _iterationCount = 10000;
    private readonly int _saltSize = 128 / 8;
    private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();

    public string HashPassword(Account user, string password)
    {
        var salt = new byte[_saltSize];
        Rng.GetBytes(salt);
        var subkey = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, _iterationCount, 256 / 8);

        var outputBytes = new byte[13 + salt.Length + subkey.Length];
        outputBytes[0] = 0x01;
        WriteNetworkByteOrder(outputBytes, 1, (uint)KeyDerivationPrf.HMACSHA256);
        WriteNetworkByteOrder(outputBytes, 5, (uint)_iterationCount);
        WriteNetworkByteOrder(outputBytes, 9, (uint)_saltSize);
        Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
        Buffer.BlockCopy(subkey, 0, outputBytes, 29, subkey.Length);
        return Convert.ToBase64String(outputBytes);
    }

    public bool VerifyHashedPassword(Account user, string hashedPassword, string providedPassword)
    {
        var decodedHashedPassword = Convert.FromBase64String(hashedPassword);
        try
        {
            var salt = new byte[_saltSize];
            Buffer.BlockCopy(decodedHashedPassword, 13, salt, 0, salt.Length);

            // Read the subkey (the rest of the payload): must be >= 128 bits
            int subkeyLength = decodedHashedPassword.Length - 13 - salt.Length;
            if (subkeyLength < 128 / 8)
            {
                return false;
            }
            var expectedSubkey = new byte[subkeyLength];
            Buffer.BlockCopy(decodedHashedPassword, 13 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

            // Hash the incoming password and verify it
            var actualSubkey = KeyDerivation.Pbkdf2(providedPassword, salt, KeyDerivationPrf.HMACSHA256, _iterationCount, subkeyLength);
            return CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey);
        }
        catch
        {
            // This should never occur except in the case of a malformed payload, where
            // we might go off the end of the array. Regardless, a malformed payload
            // implies verification failed.
            return false;
        }
    }

    private void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
    {
        buffer[offset + 0] = (byte)(value >> 24);
        buffer[offset + 1] = (byte)(value >> 16);
        buffer[offset + 2] = (byte)(value >> 8);
        buffer[offset + 3] = (byte)(value >> 0);
    }


    private uint ReadNetworkByteOrder(byte[] buffer, int offset)
    {
        return ((uint)(buffer[offset + 0]) << 24)
               | ((uint)(buffer[offset + 1]) << 16)
               | ((uint)(buffer[offset + 2]) << 8)
               | ((uint)(buffer[offset + 3]));
    }

    // Compares two byte arrays for equality. The method is specifically written so that the loop is not optimized.
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    private bool ByteArraysEqual(byte[] a, byte[] b)
    {
        if (a == null && b == null)
        {
            return true;
        }
        if (a == null || b == null || a.Length != b.Length)
        {
            return false;
        }
        var areSame = true;
        for (var i = 0; i < a.Length; i++)
        {
            areSame &= (a[i] == b[i]);
        }
        return areSame;
    }
}