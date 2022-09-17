using System.IO;

namespace DutyBoard_Telegram.Services
{
    public static class TempFileService
    {
        public static string GetSharedPath(string env)
        {
            var dir = Path.Combine(env, "Shared");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return dir;
        }
    }
}
