
using System.Collections.Generic;
using System.Linq;

using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.InteropServices;
using System.IO.Compression;
using System.Threading.Tasks;
using DotNetty.Buffers;
using DotNetty.Codecs.Mqtt.Packets;
using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;
using System;

using System.Text;

namespace ProtocolGateway.Host.Common
{
    class DCMProvisionHandler: ChannelHandlerAdapter
    {
        short step = 0;
        int secretSize = 1024;

        public override void ChannelActive(IChannelHandlerContext context)
        {
          /*  IByteBuffer message =  Unpooled.Buffer(secretSize);
            byte[] msgbytes = new byte[secretSize];
            byte x = 0;

            for (int i=0;i< secretSize; i++)
             msgbytes[i]=(x++);

            message.WriteBytes(msgbytes); //Alice send the public key to Bob

            //Read the Public key from Bob

            //Other thing happen           

            //object message=new object();
            //message = "Holla";
            //"Hellow Alsharif";
            // var temp = message as IByteBuffer;

            Console.WriteLine("ChannelActive Sending dummy initilalization sequence");

           context.WriteAndFlushAsync(message);

            //  context.WriteAndFlushAsync("Hellow arduino");
            // context.WriteAsync(String.Format("Welcome to {0} !\r\n", "Alsharif"));
            //  context.WriteAndFlushAsync(String.Format("It is {0} now !\r\n", DateTime.Now));
            */
            ///// this statement should be enable or disabled based on the result of the key excahnge to 
            ///// enable the traffic to reach to the upper layers or not 
            context.FireChannelActive();
           // message.Release();

        }


        //===============================
        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            //    var packet = message as PublishPacket;
            //  var generalpacket = message as Packet;
            //  Console.WriteLine("ChannelRead");

            var buffer = message as IByteBuffer;

            switch (step)
               {
                   case 0:
                    Console.WriteLine("Case 0"); // Diffie hellman send 
                    Console.WriteLine("Received (Echo): " + buffer.ToString(Encoding.UTF8));
                    //===============send reply
                    IByteBuffer tempmessage = Unpooled.Buffer(secretSize);
                    byte[] msgbytes = new byte[secretSize];
                    byte x = 0;

                    for (int i = 0; i < secretSize; i++)
                        msgbytes[i] = (x++);

                    tempmessage.WriteBytes(msgbytes); //Alice send the public key to Bob

                    //Read the Public key from Bob

                    //Other thing happen           

                    //object message=new object();
                    //message = "Holla";
                    //"Hellow Alsharif";
                    // var temp = message as IByteBuffer;

                    Console.WriteLine("ChannelActive Sending dummy initilalization sequence");

                    context.WriteAndFlushAsync(tempmessage);
                    //=================end send

                    step = 1;
                   //context.FireChannelRead(message);
                       break;
                   case 1:
                    if (buffer != null)
                    {
                        //  Console.WriteLine("length " + buffer.ToArray().GetLength(0));

                        Console.WriteLine("Received (Enc): " + buffer.ToString(Encoding.UTF8));
                    }

                    //Do nothing to the message
                    //for (int i = 0; i < buffer.ToArray().GetLength(0); i++)
                    //{
                    //    byte temp;
                    //    temp = (byte)(buffer.GetByte(i) ^ 0x1);
                    //    buffer.SetByte(i, temp);

                    //}
                    //Console.WriteLine("Received (Clear) " + buffer.ToString(Encoding.UTF8));

                    step = 1;

                    //    Console.WriteLine("Case 1"); // Normal MQTT traffic
                    context.FireChannelRead(message);
                       break;

                   default:
                       Console.WriteLine("Default case");
                       break;
               }


            //   Console.WriteLine("packet type");
            //   Console.WriteLine(message.ToString());
            //   Console.WriteLine(packet.PacketType.ToString());

        ////    var buffer = message as IByteBuffer;
           //  if (buffer != null)
        //      {
              //  Console.WriteLine("length " + buffer.ToArray().GetLength(0));

     //           Console.WriteLine("Received (Enc): " + buffer.ToString(Encoding.UTF8));
     //         }
       //     for (int i = 0; i<buffer.ToArray().GetLength(0);i++)
     //       {
       //         byte temp;
        //        temp = (byte)(buffer.GetByte(i) ^ 0x1);
         //       buffer.SetByte(i,temp);

       //     }

         //   Console.WriteLine("length after decoding " + buffer.ToArray().GetLength(0));

        //    Console.WriteLine("Received (Clear) " + buffer.ToString(Encoding.UTF8));

            // context.WriteAsync(message);
            // object response = "hellow kara \n";

            //  context.WriteAsync(String.Format("Welcome to {0} !\r\n", "Alsharif"));
            //  context.WriteAndFlushAsync(String.Format("It is {0} now !\r\n", DateTime.Now));

            //var temp = response as IByteBuffer;
            //var wait_close = context.WriteAsync



            // context.WriteAsync(temp);
            // context.CloseAsync();


            //   

            ///// this statement should be enable or disabled based on the result of the key excahnge to 
         //   ///// enable the traffic to reach to the upper layers or not 
          //  context.FireChannelRead(message);
        }



      //  public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();


       public override Task WriteAsync(IChannelHandlerContext context, object message)
       {
            // Decoding process
            // var packet = message as PublishPacket;
            // var generalpacket = message as Packet;
            //  if (generalpacket.PacketType == PacketType.PUBLISH)

            //  packet.Payload.AdjustCapacity(32);
            //packet.Payload.SetBytes(0, encodedtext);
            //  packet.Payload.Clear();
            //  packet.Payload.WriteBytes(encodedtext);

            var buffer = message as IByteBuffer;
            if (buffer != null)
            {
                Console.WriteLine("Sending :(Clear) " + buffer.ToString(Encoding.UTF8));
            }
            for (int i = 0; i < buffer.ToArray().GetLength(0); i++)
            {
                byte temp;
                temp = (byte)(buffer.GetByte(i) ^ 0x1);
                buffer.SetByte(i, temp);

            }
            Console.WriteLine("sending :(ENC) " + buffer.ToString(Encoding.UTF8));

            //=================
            return context.WriteAsync(message);
       }



        //    Contract.Assert(outputStream.Length <= int.MaxValue);

        //   return Unpooled.WrappedBuffer(outputStream.GetBuffer(), 0, (int)outputStream.Length);

        // finally
        // {
        //   ReferenceCountUtil.Release(buffer);
        // }

        //public override  void ChannelActive(IChannelHandlerContext context)
     //   {
        //    Console.WriteLine("ChannelActive");

    //   }


    }
}