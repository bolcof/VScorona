#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("ej+Yofh6Z3mmKrV6hroz/Ib/x2nbWFZZadtYU1vbWFhZsfKYMwTqg2UKcoTMxQRoFGnMVRUa34WiqJnYadtYe2lUX1Bz3xHfrlRYWFhcWVoGkXgDiNFRkqWHB1z+0vjyxBEt0ygP4bjEUZlNWj9OPNPYPe8E9XQLvE1rgjj4NltCurHjSdyAnDOn+ChSBGNq/VL3LZYBdfzdU6RBKEwhhplYpYdbjIBUw45KKG55hR4XcY2vNTwCpa7QIuhMpjHr8jTsNu+GtOt/rMo6GxLKum3CU5Dp6tAXlA7l5aSKhoAY7EitH1VxK7Ursgdh4c2fi2bG54wmBPz6Kaxw/VG5vILQLtY9Q1966QY6MJVcEn2iyHV59TLjW6z+ECoBUEx8GFtaWFlY");
        private static int[] order = new int[] { 6,1,4,6,5,10,9,9,8,11,13,11,13,13,14 };
        private static int key = 89;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
