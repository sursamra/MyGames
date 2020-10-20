using MyGames.GameModel;
using System;
using System.Collections.Generic;
namespace MyGames.Service
{
    public class GameService
    {
        private static Random aiRndMove = new Random();
        private static DateTime startTime = DateTime.MinValue;
        public static DateTime GetDateTime()
        {
            if (startTime == DateTime.MinValue)
                startTime = DateTime.Now;
            return startTime;
        }
        public static List<Game> GetGames()
        {
            return GameFactory.GetGames();
        }
        public static string[] GetGameMoves(int ID)
        {
            GameEnum gameEnum = (GameEnum)ID;
            return GameMovesRepo.GetGameMoves(gameEnum);
        }

        private static string GetAIMove(GameEnum game)
        {
            return GetGameMoves((int)game)[aiRndMove.Next(0, 3)];
        }
        public static int PlayGame(int gameID, int modeID, string userMove = "")
        {

            GameEnum _game = (GameEnum)gameID;
            GameModeEnum gameMode = (GameModeEnum)modeID;
            Game game = GameFactory.GetGame(_game, gameMode);
            if (gameMode == GameModeEnum.UserVsAI)
                return game.Play(userMove, GetAIMove(_game));
            else
                return game.Play(GetAIMove(_game), GetAIMove(_game));
        }
    }
    public static class GameFactory
    {
        static List<Game> games = new List<Game>();
        public static List<Game> GetGames()
        {
            if (games.Count == 0)
                foreach (GameEnum game in Enum.GetValues(typeof(GameEnum)))
                {
                    foreach (GameModeEnum mode in Enum.GetValues(typeof(GameModeEnum)))
                    {
                        Game gameobj = CreateGame(game, mode);

                        gameobj.ID = (int)game;
                        gameobj.GameName = game.ToString();
                        gameobj.Mode = (GameModeEnum)mode;
                        gameobj.ModeName = mode.ToString();
                        gameobj.UserMoves = GameMovesRepo.GetGameMoves(game.ToString());
                        gameobj.Result = 0;

                        games.Add(gameobj);
                    }
                }
            return games;
        }
        public static Game GetGame(GameEnum game, GameModeEnum gameMode)
        {
            return games.Find(_ => _.ID == (int)game && _.Mode == gameMode);
        }
        public static Game CreateGame(GameEnum game, GameModeEnum gameMode)
        {
            switch (game)
            {
                case GameEnum.RockPaperScissors:
                    return new RPS();
                case GameEnum.Fifa:
                    return new Fifa();
                case GameEnum.Chess:
                    return new Chess();
            }
            return new Game();
        }
    }
}
