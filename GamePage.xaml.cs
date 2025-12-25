using System.Globalization;
using MauiApp5.Services;
using MauiApp5.Models;

namespace MauiApp5;

public partial class GamePage : ContentPage
{
    // ...

    private void ResetBoard()
    {
        _gameOver = false;
        _currentPlayer = 'X';
        UpdateStatusTurnText();

        _board = new char[3, 3];

        foreach (var button in new[] { B00, B01, B02, B10, B11, B12, B20, B21, B22 })
        {
            button.Text = string.Empty;
            button.IsEnabled = true;
        }
    }

    private void UpdateStatusTurnText()
    {
        var lang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        if (lang == "ru")
            StatusLabel.Text = $"Ход игрока {_currentPlayer}";
        else
            StatusLabel.Text = $"Player {_currentPlayer} turn";
    }

    private async void OnCellClicked(object sender, EventArgs e)
    {
        if (_gameOver) return;

        if (sender is Button btn)
        {
            if (!string.IsNullOrEmpty(btn.Text)) return;

            btn.Text = _currentPlayer.ToString();

            var row = Grid.GetRow(btn);
            var col = Grid.GetColumn(btn);
            _board[row, col] = _currentPlayer;

            var lang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

            if (CheckWin(_currentPlayer))
            {
                _gameOver = true;
                StatusLabel.Text = lang == "ru"
                    ? $"Игрок {_currentPlayer} победил!"
                    : $"Player {_currentPlayer} wins!";

                await _db.AddScoreAsync(new ScoreRecord
                {
                    Player = _currentPlayer.ToString(),
                    Result = "Win",
                    CreatedAt = DateTime.UtcNow,
                    Scope = "Local"
                });

                return;
            }

            if (IsDraw())
            {
                _gameOver = true;
                StatusLabel.Text = lang == "ru" ? "Ничья!" : "Draw!";

                await _db.AddScoreAsync(new ScoreRecord
                {
                    Player = "Both",
                    Result = "Draw",
                    CreatedAt = DateTime.UtcNow,
                    Scope = "Local"
                });

                return;
            }

            _currentPlayer = _currentPlayer == 'X' ? 'O' : 'X';
            UpdateStatusTurnText();
        }
    }

    // остальной код без изменений
}
