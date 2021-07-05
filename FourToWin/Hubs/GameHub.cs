using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourToWin.Hubs
{
    public class GameHub : Hub
    {
        string lobby = string.Empty;

        public async void CreateGame()
        {
            string lobbyId = GenerateLobbyId();
            string player = "p1";
            await AddToGroup(lobbyId);
            await Clients.Caller.SendAsync("GetPlayerNum", player);
            await Clients.Caller.SendAsync("GetLobbyId", lobbyId);
        }

        public async void JoinGame(string lobbyId)
        {
            string player = "p2";
            await AddToGroup(lobbyId);
            await Clients.Caller.SendAsync("GetPlayerNum", player);
            await Clients.Caller.SendAsync("GetLobbyId", lobbyId);
            await Clients.Group(lobbyId).SendAsync("Ready");
        }

        public async void QuickGame()
        {
            string lobbyId;
            string player;

            if (lobby == string.Empty)
            {
                player = "p1";
                lobbyId = GenerateLobbyId();
                await AddToGroup(lobbyId);
                await Clients.Caller.SendAsync("GetPlayerNum", player);
                await Clients.Caller.SendAsync("GetRandomLobbyId", lobbyId);
                lobby = lobbyId;
            }
            else
            {
                player = "p2";
                lobbyId = lobby;
                lobby = string.Empty;
                await AddToGroup(lobbyId);
                await Clients.Caller.SendAsync("GetPlayerNum", player);
                await Clients.Caller.SendAsync("GetRandomLobbyId", lobbyId);
                await Clients.Group(lobbyId).SendAsync("Ready");
            }
        }

        public async Task SendMessageToGroup(string message, string group)
        {
            await Clients.Group(group).SendAsync("ReceiveMessage", message);
        }

        public async Task SendColumn(int column, string group)
        {
            await Clients.Group(group).SendAsync("ReceiveColumn", column);
        }

        public async Task AddToGroup(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public async Task RemoveFromGroup(string group)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
        }

        private string GenerateLobbyId()
        {
            int lobbyId = new Random().Next(10000, 99999);
            return lobbyId.ToString();
        }

    }
}
