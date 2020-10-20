using System;
using System.Collections.Generic;

namespace MyGames.GameModel
{
    public enum GameModeEnum
    {
        UserVsAI,
        AIVsAI,
    }
    public enum GameEnum
    {
        RockPaperScissors,
        Chess,
        Fifa
    }
    public static class GameMovesRepo
    {
        private static Dictionary<GameEnum, string[]> gameMoves = new Dictionary<GameEnum, string[]>()
        {
            { GameEnum.RockPaperScissors,new string[]{"Rock","Paper","Scissors" }},
            { GameEnum.Fifa,new string[]{ "Penalty", "Corner","FreeKick" }},
            { GameEnum.Chess,new string[]{"e4","d4","King" }},

        };
        public static string[] GetGameMoves(GameEnum game) { return gameMoves[game]; }
        public static string[] GetGameMoves(string game) { return gameMoves[(GameEnum)Enum.Parse(typeof(GameEnum), game)]; }
    }
    public class Game
    {
        public virtual int Play(string firtPlayerMove, string secondPlayerMove) { return 0; }
        public string GameName { get; set; }
        public int Result { get; set; }
        public int ID { get; set; }
        public GameModeEnum Mode { get; set; }
        public string ModeName { get; set; }
        public int UserScore { get; set; }
        public int ComputerScore { get; set; }
        public int WhiteComputer { get; set; }
        public int BlackComputer { get; set; }
        public string[] UserMoves { get; set; }
    }
    public class Fifa : Game
    {
        public override int Play(string firstPlayerMove, string secondPlayerMove)
        {
            int result = 0;

            if (firstPlayerMove == "Penalty" && DateTime.Now.Second % 2 == 0)
            {
                result = 1;
                if (Mode == GameModeEnum.AIVsAI)
                    WhiteComputer++;
                else
                    UserScore++;
            }
            if (secondPlayerMove == "Penalty" && DateTime.Now.Second % 2 == 0)
            {
                result = 2;
                if (Mode == GameModeEnum.AIVsAI)
                    BlackComputer++;
                else
                    ComputerScore++;
            }
            return result;
        }
    }

    public class Chess : Game
    {
        public override int Play(string firstPlayerMove, string secondPlayerMove)
        {
            int result = 0;

            if (firstPlayerMove == "King" && DateTime.Now.Second % 2 == 0)
            {
                result = 1;
                if (Mode == GameModeEnum.AIVsAI)
                    WhiteComputer++;
                else
                    UserScore++;
            }
            if (secondPlayerMove == "King" && DateTime.Now.Second % 2 == 0)
            {
                result = 2;
                if (Mode == GameModeEnum.AIVsAI)
                    BlackComputer++;
                else
                    ComputerScore++;
            }
            return result;
        }
    }
    public class RPS : Game
    {
        public override int Play(string firstPlayerMove, string secondPlayerMove)
        {
            int result = 0;
            if (firstPlayerMove == "Rock" && secondPlayerMove == "Scissors")
            {
                if (Mode == GameModeEnum.AIVsAI)
                    WhiteComputer++;
                else
                    UserScore++;
                result = 1;
            }
            else if (firstPlayerMove == "Rock" && secondPlayerMove == "Paper")
            {
                if (Mode == GameModeEnum.AIVsAI)
                    BlackComputer++;
                else
                    ComputerScore++;

                result = 2;
            }
            else if (firstPlayerMove == "Paper" && secondPlayerMove == "Rock")
            {
                if (Mode == GameModeEnum.AIVsAI)
                    WhiteComputer++;
                else
                    UserScore++;
                result = 1;
            }
            else if (firstPlayerMove == "Paper" && secondPlayerMove == "Scissors")
            {
                if (Mode == GameModeEnum.AIVsAI)
                    BlackComputer++;
                else
                    ComputerScore++;
                result = 2;
            }
            else if (firstPlayerMove == "Scissors" && secondPlayerMove == "Rock")
            {
                if (Mode == GameModeEnum.AIVsAI)
                    BlackComputer++;
                else
                    ComputerScore++;
                result = 2;
            }
            else if (firstPlayerMove == "Scissors" && secondPlayerMove == "Paper")
            {
                if (Mode == GameModeEnum.AIVsAI)
                    WhiteComputer++;
                else
                    UserScore++;
                result = 1;
            }
            else
            {
                result = 0;
            }
            Result = result;
            return result;
        }
    }
}

