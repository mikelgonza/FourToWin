using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourToWin.Hubs
{
    public class GameHub : Hub
    {
        public static List<string> lobbies = new List<string>();
        public static List<string> customLobbies = new List<string>();

        public async void CreateGame(string userId, string userNickname)
        {
            string lobbyId = GenerateLobbyId();
            string player = "p1";
            customLobbies.Add(lobbyId);
            await AddToGroup(lobbyId);
            await Clients.Caller.SendAsync("GetPlayerNum", player);
            await Clients.Caller.SendAsync("GetLobbyId", lobbyId);
            await Clients.All.SendAsync("GetUser1", userId, userNickname);
        }

        public async void JoinGame(string lobbyId, string userId, string userNickname)
        {
            if (customLobbies.Contains(lobbyId))
            {
                customLobbies.Remove(lobbyId);
                string player = "p2";
                await AddToGroup(lobbyId);
                await Clients.Caller.SendAsync("GetPlayerNum", player);
                await Clients.Caller.SendAsync("GetLobbyId", lobbyId);
                await Clients.All.SendAsync("GetUser2", userId, userNickname);
                await Clients.Group(lobbyId).SendAsync("Ready");
            }
            else
            {
                await Clients.Caller.SendAsync("LobbyNotExist");
            }
        }

        public async void QuickGame(string userId, string userNickname)
        {
            string lobbyId;
            string player;

            if (lobbies.Count == 0)
            {
                player = "p1";
                lobbyId = GenerateLobbyId();
                lobbies.Add(lobbyId);
                await AddToGroup(lobbyId);
                await Clients.Caller.SendAsync("GetPlayerNum", player);
                await Clients.Caller.SendAsync("GetLobbyId", lobbyId);
                await Clients.All.SendAsync("GetUser1", userId, userNickname);
            }
            else
            {
                player = "p2";
                lobbyId = lobbies[0];
                lobbies.Remove(lobbyId);
                await AddToGroup(lobbyId);
                await Clients.Caller.SendAsync("GetPlayerNum", player);
                await Clients.Caller.SendAsync("GetLobbyId", lobbyId);
                await Clients.All.SendAsync("GetUser2", userId, userNickname);
                await Clients.Group(lobbyId).SendAsync("Ready");
            }
        }

        public async Task SendMessageToGroup(string message, string group, string user)
        {
            await Clients.Group(group).SendAsync("ReceiveMessage", message, user);
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
