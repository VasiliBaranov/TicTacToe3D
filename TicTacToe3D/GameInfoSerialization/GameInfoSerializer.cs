using System;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.IO;
using TicTacToe3D.GameInfoSerialization.Interfaces;
using TicTacToe3D.Properties;
using TicTacToe3D.GameInfo;

namespace TicTacToe3D.GameInfoSerialization
{
    /// <summary>
    /// Provides simplified and type safe interface for serialization and deserialization of game info,
    /// which is universal for the whole application
    /// </summary>
    public class GameInfoSerializer : IGameInfoSerializer
    {
        private readonly IFormatter _formatter;

        private readonly string _replayExtension;
        private readonly string _unfinishedGameExtension;

        private const string _serializerDllKey = "serializerDll";
        private const string _serializerTypeNameKey = "serializerTypeName";

        public GameInfoSerializer()
        {
            _formatter = GetFormatter();

            Settings settings = new Settings();
            _replayExtension = settings.ReplayExtension;
            _unfinishedGameExtension = settings.UnfinishedGameExtension;
        }

        #region IGameInfoSerializer Members

        /// <summary>
        /// Loads the unfinished game.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public GameInformation LoadUnfinishedGame(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException();
            }

            if (!GetExtension(path).Equals(_unfinishedGameExtension))
            {
                throw new InvalidOperationException();
            }

            GameInformation gameInfo = LoadGameInfo(path);

            if (gameInfo.GameStatus != GameStatus.Unfinished)
            {
                throw new InvalidOperationException();
            }

            return gameInfo;
        }

        /// <summary>
        /// Loads the replay.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public GameInformation LoadReplay(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException();
            }

            if (!GetExtension(path).Equals(_replayExtension))
            {
                throw new InvalidOperationException();
            }

            GameInformation gameInfo = LoadGameInfo(path);

            if (gameInfo.GameStatus == GameStatus.Unfinished)
            {
                throw new InvalidOperationException();
            }

            return gameInfo;
        }

        /// <summary>
        /// Saves the unfinished game.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="gameInfo">The game info.</param>
        public void SaveUnfinishedGame(string path, GameInformation gameInfo)
        {
            if (gameInfo == null || string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException();
            }

            if (gameInfo.GameStatus != GameStatus.Unfinished)
            {
                throw new InvalidOperationException();
            }

            if (!GetExtension(path).Equals(_unfinishedGameExtension))
            {
                throw new InvalidOperationException();
            }

            SaveGameInfo(path, gameInfo);
        }

        /// <summary>
        /// Saves the replay.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="gameInfo">The game info.</param>
        public void SaveReplay(string path, GameInformation gameInfo)
        {
            if (gameInfo == null || string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException();
            }

            if (gameInfo.GameStatus == GameStatus.Unfinished)
            {
                throw new InvalidOperationException();
            }

            if (!GetExtension(path).Equals(_replayExtension))
            {
                throw new InvalidOperationException();
            }

            SaveGameInfo(path, gameInfo);
        }

        /// <summary>
        /// Gets the replay extension.
        /// </summary>
        /// <value>The replay extension.</value>
        public string ReplayExtension
        {
            get 
            {
                return _replayExtension;
            }
        }

        /// <summary>
        /// Gets the unfinished game extension.
        /// </summary>
        /// <value>The unfinished game extension.</value>
        public string UnfinishedGameExtension
        {
            get 
            {
                return _unfinishedGameExtension;
            }
        }

        #endregion

        /// <summary>
        /// Saves the game info.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="gameInfo">The game info.</param>
        private void SaveGameInfo(string path, GameInformation gameInfo)
        {
            //if a file with such a name exists, we delete it
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                }
                catch
                {
                    throw new InvalidOperationException();
                }
            }

            //serialize game info using the serializer to the file
            try
            {
                using (FileStream fileStream = File.Create(path))
                {
                    _formatter.Serialize(fileStream, gameInfo);
                }
            }

            catch
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Loads the game info.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private GameInformation LoadGameInfo(string path)
        {
            if (!File.Exists(path))
            {
                throw new InvalidOperationException();
            }

            GameInformation gameInfo;

            //deserialize game info from the file, using the serializer 
            try
            {
                using (FileStream fileStream = File.OpenRead(path))
                {
                    gameInfo = _formatter.Deserialize(fileStream) as GameInformation;
                }
            }
            catch
            {
                throw new InvalidOperationException();
            }

            if (gameInfo == null)
            {
                throw new InvalidOperationException();
            }

            return gameInfo;
        }

        /// <summary>
        /// Gets the formatter.
        /// </summary>
        /// <returns></returns>
        private static IFormatter GetFormatter()
        {
            //read from app config standart serializer dll for game replays
            string serializerDll = System.Configuration.ConfigurationManager.AppSettings[_serializerDllKey];

            //read from app config standart serializer type name for game replays
            string serializerTypeName = System.Configuration.ConfigurationManager.AppSettings[_serializerTypeNameKey];

            if (serializerDll == null || serializerTypeName == null)
            {
                throw new InvalidOperationException();
            }

            IFormatter formatter;

            //initialize serializer/formatter
            try
            {
                ObjectHandle formatterObject = Activator.CreateInstanceFrom(serializerDll, serializerTypeName);
                if (formatterObject != null)
                {
                    formatter = (IFormatter) formatterObject.Unwrap();
                }
                else
                {
                    throw new InvalidOperationException();
                }
                //Assembly assembly = Assembly.LoadFrom(serializerDll);
                //Type serializerType = assembly.GetType(serializerTypeName, true);
                //formatter = (IFormatter)Activator.CreateInstance(serializerType);
            }
            catch
            {
                throw new InvalidOperationException();
            }

            return formatter;
        }

        /// <summary>
        /// Gets the extension.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private static string GetExtension(string path)
        {
            int lastDot = path.LastIndexOf('.');
            string extension = path.Substring(lastDot + 1);
            return extension;
        }
    }
}
