using System;
using System.Collections.Generic;
using System.Text;

namespace Proquint
{
    /// <summary>
    /// Proquint helper to convert to/from Proquint strings.
    /// A Proquint is a PRO-nouncable QUINT-uplet of alternating unambiguous consonants and vowels, for example: "lusab".
    /// 
    /// A 32-bit implementation is used, giving Proquints strings of length 10 (not including the separator character).
    /// 
    /// Please see the article on proquints: http://arXiv.org/html/0901.4016
    /// Original C version: https://github.com/dsw/proquint
    /// </summary>
    public static class QuintHelper
    {
        #region Fields
        /// <summary>
        /// Unambiguos consonants
        /// </summary>
        private static readonly char[] Consonants =
        {
            'b', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'r', 's', 't', 'v', 'z'
        };
        /// <summary>
        /// Unambiguos vowels
        /// </summary>
        private static readonly char[] Vowels = { 'a', 'i', 'o', 'u' };
        /// <summary>
        /// Dictionary to obtain the consonant indexes in O(1)
        /// </summary>
        private readonly static Dictionary<char, uint> ConsonantIndex;
        /// <summary>
        /// Dictionary to obtain the vowel indexes in O(1)
        /// </summary>
        private readonly static Dictionary<char, uint> VowelIndex;
        /// <summary>
        /// Consonant mask
        /// </summary>
        private const uint MaskFirst4 = 0xF0000000;
        /// <summary>
        /// Vowel mask
        /// </summary>
        private const uint MaskFirst2 = 0xC0000000;
        /// <summary>
        /// Random
        /// </summary>
        private static readonly Random Rand = new Random();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes static members of the <see cref="QuintHelper"/> class.
        /// </summary>
        static QuintHelper()
        {
            ConsonantIndex = new Dictionary<char, uint>(Consonants.Length);
            for (uint i = 0; i < Consonants.Length; i++)
            {
                ConsonantIndex.Add(Consonants[i], i);
            }
            VowelIndex = new Dictionary<char, uint>(Vowels.Length);
            for (uint i = 0; i < Vowels.Length; i++)
            {
                VowelIndex.Add(Vowels[i], i);
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Gets a randomly generated proquint string
        /// </summary>
        /// <param name="sepChar">The separator character, or null.</param>
        public static string Random(char? sepChar = null)
        {
            return FromUint(RandomUint(), sepChar);
        }

        /// <summary>
        /// Converts an unsigned int to its corresponding Proquint string
        /// </summary>
        /// <param name="i">The unsigned int value to convert.</param>
        /// <param name="sepChar">The separator character, or null.</param>
        public static string FromUint(uint i, char? sepChar = null)
        {
            var sb = new StringBuilder();
            uint j;
            Action handleConsonant = () =>
            {
                j = i & MaskFirst4;
                i <<= 4;
                j >>= 28;
                sb.Append(Consonants[j]);
            };
            Action handleVowel = () =>
            {
                j = i & MaskFirst2;
                i <<= 2;
                j >>= 30;
                sb.Append(Vowels[j]);
            };
            handleConsonant();
            handleVowel();
            handleConsonant();
            handleVowel();
            handleConsonant();
            if (sepChar.HasValue)
            {
                sb.Append(sepChar.Value);
            }
            handleConsonant();
            handleVowel();
            handleConsonant();
            handleVowel();
            handleConsonant();
            return sb.ToString();
        }

        /// <summary>
        /// Converts a Proquint string to its corresponding unsigned int
        /// </summary>
        /// <param name="quint">The proquint string.</param>
        /// <param name="sepChar">The separator character, or null.</param>
        public static uint ToUint(string quint, char? sepChar = null)
        {
            if (quint.Length != 10 + (sepChar.HasValue ? 1 : 0))
            {
                throw new ArgumentOutOfRangeException("quint", quint, "The quint provided has an invalid format");
            }
            uint res = 0;
            quint = quint.ToLowerInvariant();
            for (int i = 0; i < quint.Length; i++)
            {
                var c = quint[i];
                if (ConsonantIndex.ContainsKey(c))
                {
                    res <<= 4;
                    res += ConsonantIndex[c];
                }
                else if (VowelIndex.ContainsKey(c))
                {
                    res <<= 2;
                    res += VowelIndex[c];
                }
                else if (quint[i] != sepChar)
                {
                    throw new ArgumentOutOfRangeException("quint", quint, "The quint provided has an invalid character");
                }
            }
            return res;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Get a true random uint
        /// </summary>
        /// <returns></returns>
        internal static uint RandomUint()
        {
            var bytes = new byte[4];
            Rand.NextBytes(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }
        #endregion
    }
}
