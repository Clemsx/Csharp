using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Groups;
using Newtonsoft.Json;

namespace cardGame
{
    public class ChatServerHandler : SimpleChannelInboundHandler<string>
    {
        static volatile IChannelGroup group;
        private static Blackjack _bj = new Blackjack();

        public override void ChannelActive(IChannelHandlerContext contex)
        {
            IChannelGroup g = group;
            if (g == null)
            {
                lock (this)
                {
                    if (group == null)
                    {
                        g = group = new DefaultChannelGroup(contex.Executor);
                    }
                }
            }
            
            contex.WriteAndFlushAsync("Welcome to BLACKJACK !\r\n");

            g.Add(contex.Channel);
            _bj.AddPlayer(contex);
        }

        class EveryOneBut : IChannelMatcher
        {
            readonly IChannelId id;

            public EveryOneBut(IChannelId id)
            {
                this.id = id;
            }

            public bool Matches(IChannel channel) => channel.Id != this.id;
        }

        protected override void ChannelRead0(IChannelHandlerContext contex, string msg)
        {
            ProtocolJson pj = JsonConvert.DeserializeObject<ProtocolJson>(msg);
            Console.WriteLine("Server receive : " + pj.message + "\n at : " + pj.messageDate + "\n from : " + contex.Channel.RemoteAddress);
            
            //send message to all but this one
            //string broadcast = string.Format("[{0}] {1}\n", contex.Channel.RemoteAddress, pj.message);
            //string response = string.Format("[you] {0}\n", pj.message);

            //group.WriteAndFlushAsync(broadcast, new EveryOneBut(contex.Channel.Id));
            //contex.WriteAndFlushAsync(response);

            if (string.Equals("bye", pj.message, StringComparison.OrdinalIgnoreCase))
            {
                _bj.RemovePlayerByContex(contex);
                contex.CloseAsync();
            }
            else
            {
                _bj.BlackjackHandler(contex, pj.message);
            }
        }

        public override void ChannelReadComplete(IChannelHandlerContext ctx) => ctx.Flush();

        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception e)
        {
            Console.WriteLine("{0}", e.StackTrace);
            ctx.CloseAsync();
        }

        public override bool IsSharable => true;

        internal Program Program
        {
            get => default(Program);
            set
            {
            }
        }
    }
}
