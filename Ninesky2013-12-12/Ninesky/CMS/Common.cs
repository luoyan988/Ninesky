using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Ninesky
{
    /// <summary>
    /// 通用类
    /// </summary>
    public class Common
    {
        /// <summary>
        /// 获取验证码【字符串】
        /// </summary>
        /// <param name="Length">验证码长度【必须大于0】</param>
        /// <returns></returns>
        public static string VerificationText(int Length)
        {
            char[] _verification = new Char[Length];
            Random _random = new Random();
            char[] _dictionary = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            for (int i = 0; i < Length; i++)
            {
                _verification[i] = _dictionary[_random.Next(_dictionary.Length - 1)];
            }
            return new string(_verification);
        }
        /// <summary>
        /// Sha256加密
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Sha256(string plainText)
        {
            SHA256Managed _sha256 = new SHA256Managed();
            byte[] _cipherText = _sha256.ComputeHash(Encoding.Default.GetBytes(plainText));
            return Convert.ToBase64String(_cipherText);
        }

        public static List<string> GetModelError(System.Web.Mvc.ModelStateDictionary modelState)
        {
            return null;
        }
    }
}