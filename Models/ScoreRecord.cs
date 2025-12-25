// Models/ScoreRecord.cs
using SQLite;

namespace MauiApp5.Models;

[Table("Scores")]
public class ScoreRecord
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Player { get; set; } = string.Empty;   // X, O, Both
    public string Result { get; set; } = string.Empty;   // Win / Draw
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// "Local" или "Global" — для лабораторной можно считать,
    /// что Local = рекорды на этом устройстве,
    /// Global = "синхронизированные" (логически глобальные).
    /// </summary>
    public string Scope { get; set; } = "Local";
}
