using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunc
{
    internal static class Static
    {
        public static string h_create_relative_path(string full_path, string root_path)
        {
            string normalizedFullPath = Path.GetFullPath(full_path);
            string normalizedRootPath = Path.GetFullPath(root_path);

            // root_path가 디렉터리로 끝나지 않으면 구분자를 붙여줌
            if (!normalizedRootPath.EndsWith(Path.DirectorySeparatorChar.ToString()) &&
                !normalizedRootPath.EndsWith(Path.AltDirectorySeparatorChar.ToString()))
            {
                normalizedRootPath += Path.DirectorySeparatorChar;
            }

            // full_path가 root_path 내부가 아니라면 예외 발생
            if (!normalizedFullPath.StartsWith(normalizedRootPath, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("full_path가 root_path 내부에 존재하지 않습니다.");
            }

            // 실제 상대 경로를 구함
            string relativePath = normalizedFullPath.Substring(normalizedRootPath.Length);

            // 앞쪽 구분자를 제거
            relativePath = relativePath.TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            return relativePath;
        }
        public static bool h_is_crlf(byte[] data)
        {
            if (data == null || data.Length < 2)
                return false; 
            int len = data.Length;
            // 마지막 두 바이트가 \r\n 또는 \n\r인지 확인
            return (data[len - 2] == 0x0D && data[len - 1] == 0x0A) || // "\r\n"
                   (data[len - 2] == 0x0A && data[len - 1] == 0x0D);
        }
        public static bool h_is_crlf(byte forward, byte backward)
        {
            return (forward == 0x0D && backward == 0x0A) || // "\r\n"
                    (forward == 0x0A && backward == 0x0D);
        }
        public static SETTING_TYPE h_string2setting_type(string str, out string type_data)
        {
            string[] split = str.Split(':');

            if (split.Length != 2) throw new Exception("setting type _error");
            type_data = split[1];
            if (Enum.TryParse(split[0], true, out SETTING_TYPE type))
                return type;
            else
                throw new Exception("Invalid setting type");
        }
    }
}
