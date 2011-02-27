﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Tftp.Net.Channel
{
    static class ChannelFactory
    {
        public static IChannel CreateServer(EndPoint localAddress)
        {
            if (localAddress is IPEndPoint)
                return CreateServerUdp((IPEndPoint)localAddress);

            throw new NotSupportedException("Unsupported endpoint type.");
        }

        public static IChannel CreateConnection(EndPoint remoteAddress)
        {
            if (remoteAddress is IPEndPoint)
                return CreateConnectionUdp((IPEndPoint)remoteAddress);

            throw new NotSupportedException("Unsupported endpoint type.");
        }

        #region UDP connections

        private static IChannel CreateServerUdp(IPEndPoint localAddress)
        {
            UdpClient socket = new UdpClient(localAddress);
            return new UdpChannel(socket);
        }

        private static IChannel CreateConnectionUdp(IPEndPoint remoteAddress)
        {
            IPEndPoint localAddress = new IPEndPoint(IPAddress.Any, 0);
            UdpChannel channel = new UdpChannel(new UdpClient(localAddress));
            channel.RemoteEndpoint = remoteAddress;
            return channel;
        }
        #endregion
    }
}