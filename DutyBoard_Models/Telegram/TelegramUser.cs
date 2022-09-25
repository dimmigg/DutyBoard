using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DutyBoard_Models.Telegram
{
    public class TelegramUser
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        public long ChatId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [NotMapped]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(3);

        public int RoleId { get; set; } = 0;

        [NotMapped]
        public bool IsAdmin => RoleId >= 3;

        [NotMapped]
        public bool IsActive => RoleId >= 1;

        public override string ToString() =>
            $"{(Username is null ? $"{FirstName}{LastName?.Insert(0, " ")}" : $"@{Username}")} ({ChatId})";
    }
}
