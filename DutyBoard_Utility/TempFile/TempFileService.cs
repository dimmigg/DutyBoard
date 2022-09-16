using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DutyBoard_Utility.TempFile
{
    public static class TempFileService
    {
        public static string GetSharedPath(string path)
        {
            var dir = Path.Combine(path, "Shared");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return dir;
        }
    }
}
