using LiteIM;

using Microsoft.AspNetCore.SignalR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApp.MessageHub
{
    public class ChatHub : Hub
    {
        readonly IImClient _imClient;

        public ChatHub(IImClient imClient)
        {
            _imClient = imClient;
        }

        /// <summary>
        /// 连接成功
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // 离开所有聊天室
            _imClient.LeaveChan(this.Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// 加入聊天室
        /// </summary>
        /// <param name="chans"></param>
        /// <returns></returns>
        public async Task JoinChan(string[] chans)
        {
            await Task.Yield();

            foreach (var chan in chans)
            {
                _imClient.JoinChan(this.Context.ConnectionId, chan);
            }
        }

        public async Task SendData(string chan)
        {
            var clientList = _imClient.GetChanClientList(chan)
                .Where(o => o != this.Context.ConnectionId);

            await Clients.Clients(clientList.ToList())
                .SendAsync("", "");
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
