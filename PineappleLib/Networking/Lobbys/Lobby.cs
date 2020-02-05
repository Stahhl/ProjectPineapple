using PineappleLib.Controllers;
using PineappleLib.Networking.Clients;
using PineappleLib.Networking.Servers;
using static PineappleLib.General.Values;
using PineappleLib.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PineappleLib.Networking.Lobbys
{
    public class Lobby
    {
        public Lobby(Server server, int id, string password)
        {
            Clients = new List<Client>();

            Server = server;
            Id = id;
            Password = password;

            MaxPlayers = lobbyMaxPlayers + 1;

            GameController = new GameController(Server);
            GameController.StartServer();
        }

        public int Id { get; private set; }
        public string Password { get; private set; }
        public bool IsOpen { get; private set; }

        public int CurrentPlayers { get; private set; }
        public int MaxPlayers { get; private set; }

        public Server Server { get; private set; }
        public GameController GameController { get; private set; }
        public List<Client> Clients { get; private set; }
        
        public void AddClientToLobby(int clientId)
        {
            if (CurrentPlayers >= MaxPlayers)
                throw new Exception("Lobby - AddClientToLobby()");

            Clients.Add(Server.Clients[clientId]);
            CurrentPlayers++;
        }

    }
}
