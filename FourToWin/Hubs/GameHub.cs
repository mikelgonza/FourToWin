using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourToWin.Hubs
{
    public class GameHub : Hub
    {
        //private static List<string> lobbies = new List<string>();
        //private static List<string> customLobbies = new List<string>();

        //private static Dictionary<string, int> lobbies = new Dictionary<string, int>();
        private static Dictionary<string, int> customLobbies = new Dictionary<string, int>();

        private static string player1Id, player1Nickname, player1Image;
        private static string player2Id, player2Nickname, player2Image;

        public async void CreateGame(string userId, string userNickname, string userImage)
        {
            int randomPlayer = GenerateRandomPlayer();

            if (randomPlayer == 1)
            {
                player1Id = userId;
                player1Nickname = userNickname;
                player1Image = userImage;
                string lobbyId = GenerateLobbyId();
                string player = "p1";
                customLobbies.Add(lobbyId, 1);
                await AddToGroup(lobbyId);
                await Clients.Caller.SendAsync("GetPlayerNum", player);
                await Clients.Caller.SendAsync("GetLobbyId", lobbyId);
                await Clients.Caller.SendAsync("GetUser1", player1Id, player1Nickname, player1Image);
            }
            else
            {
                player2Id = userId;
                player2Nickname = userNickname;
                player2Image = userImage;
                string lobbyId = GenerateLobbyId();
                string player = "p2";
                customLobbies.Add(lobbyId, 2);
                await AddToGroup(lobbyId);
                await Clients.Caller.SendAsync("GetPlayerNum", player);
                await Clients.Caller.SendAsync("GetLobbyId", lobbyId);
                await Clients.Caller.SendAsync("GetUser2", player2Id, player2Nickname, player2Image);
            }
        }

        public async void JoinGame(string lobbyId, string userId, string userNickname, string userImage)
        {
            if (customLobbies.ContainsKey(lobbyId))
            {
                int firstPlayer = customLobbies[lobbyId];
                if (firstPlayer == 1)
                {
                    player2Id = userId;
                    player2Nickname = userNickname;
                    player2Image = userImage;
                    customLobbies.Remove(lobbyId);
                    string player = "p2";
                    await AddToGroup(lobbyId);
                    await Clients.Caller.SendAsync("GetPlayerNum", player);
                    await Clients.Caller.SendAsync("GetLobbyId", lobbyId);
                    await Clients.Caller.SendAsync("GetUser1", player1Id, player1Nickname, player1Image);
                    await Clients.All.SendAsync("GetUser2", player2Id, player2Nickname, player2Image);
                    await Clients.Group(lobbyId).SendAsync("Ready");
                }
                else if (firstPlayer == 2)
                {
                    player1Id = userId;
                    player1Nickname = userNickname;
                    player1Image = userImage;
                    customLobbies.Remove(lobbyId);
                    string player = "p1";
                    await AddToGroup(lobbyId);
                    await Clients.Caller.SendAsync("GetPlayerNum", player);
                    await Clients.Caller.SendAsync("GetLobbyId", lobbyId);
                    await Clients.All.SendAsync("GetUser1", player1Id, player1Nickname, player1Image);
                    await Clients.Caller.SendAsync("GetUser2", player2Id, player2Nickname, player2Image);
                    await Clients.Group(lobbyId).SendAsync("Ready");
                }
            }
            else
            {
                await Clients.Caller.SendAsync("LobbyNotExist");
            }
        }

        //public async void QuickGame(string userId, string userNickname, string userImage)
        //{
        //    string lobbyId;
        //    string player;

        //    if (lobbies.Count == 0)
        //    {
        //        player1Id = userId;
        //        player1Nickname = userNickname;
        //        player1Image = userImage;
        //        player = "p1";
        //        lobbyId = GenerateLobbyId();
        //        lobbies.Add(lobbyId);
        //        await AddToGroup(lobbyId);
        //        await Clients.Caller.SendAsync("GetPlayerNum", player);
        //        await Clients.Caller.SendAsync("GetLobbyId", lobbyId);
        //        await Clients.Caller.SendAsync("GetUser1", player1Id, player1Nickname, player1Image);
        //    }
        //    else
        //    {
        //        player2Id = userId;
        //        player2Nickname = userNickname;
        //        player2Image = userImage;
        //        player = "p2";
        //        lobbyId = lobbies[0];
        //        lobbies.Remove(lobbyId);
        //        await AddToGroup(lobbyId);
        //        await Clients.Caller.SendAsync("GetPlayerNum", player);
        //        await Clients.Caller.SendAsync("GetLobbyId", lobbyId);
        //        await Clients.Caller.SendAsync("GetUser1", player1Id, player1Nickname, player1Image);
        //        await Clients.All.SendAsync("GetUser2", player2Id, player2Nickname, player2Image);
        //        await Clients.Group(lobbyId).SendAsync("Ready");
        //    }
        //}

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

        private int GenerateRandomPlayer()
        {
            int random = new Random().Next(1, 3);
            return random;
        }

    }
}
