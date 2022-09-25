using System.IO;

namespace DutyBoard_Utility.TempFile
{
    public static class TempFileService
    {
        public static string GetSharedPath()
        {
            var dir = Path.Combine(WC.Path, "Shared");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return dir;
        }
    }
}
