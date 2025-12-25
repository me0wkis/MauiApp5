// Services/DatabaseService.cs
using SQLite;
using MauiApp5.Models;

namespace MauiApp5.Services;

public class DatabaseService
{
    private static readonly Lazy<DatabaseService> _instance =
        new(() => new DatabaseService());
    public static DatabaseService Instance => _instance.Value;

    private SQLiteAsyncConnection _connection;
    private bool _initialized = false;

    private DatabaseService()
    {
    }

    private async Task InitAsync()
    {
        if (_initialized)
            return;

        var dbPath = Path.Combine(
            FileSystem.AppDataDirectory,
            "scores.db3");

        _connection = new SQLiteAsyncConnection(dbPath);
        await _connection.CreateTableAsync<ScoreRecord>();

        _initialized = true;
    }

    public async Task AddScoreAsync(ScoreRecord record)
    {
        await InitAsync();
        await _connection.InsertAsync(record);
    }

    public async Task<List<ScoreRecord>> GetScoresAsync(string scope)
    {
        await InitAsync();
        return await _connection.Table<ScoreRecord>()
                                .Where(s => s.Scope == scope)
                                .OrderByDescending(s => s.CreatedAt)
                                .ToListAsync();
    }

    /// <summary>
    /// Имитация "глобального обмена":
    /// просто копируем все локальные рекорды в глобальные (Scope = "Global").
    /// В отчёте можно написать, что вместо этого должно быть HTTP-взаимодействие
    /// с удалённым сервером.
    /// </summary>
    public async Task SyncToGlobalAsync()
    {
        await InitAsync();
        var locals = await GetScoresAsync("Local");

        foreach (var record in locals)
        {
            var globalCopy = new ScoreRecord
            {
                Player = record.Player,
                Result = record.Result,
                CreatedAt = record.CreatedAt,
                Scope = "Global"
            };
            await _connection.InsertAsync(globalCopy);
        }
    }
}
