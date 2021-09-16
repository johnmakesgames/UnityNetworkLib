using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;


public enum PacketType
{ 
    UNINITIALISED = 0,
    DEFAULT = 1,
}


public class Packet
{
    public PacketType packetType = PacketType.UNINITIALISED;

    public Packet()
    {
        packetType = PacketType.UNINITIALISED;
    }

    public virtual byte[] GetAsBytes()
    {

        MemoryStream memoryStream = new MemoryStream();
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        binaryFormatter.Serialize(memoryStream, this);
        return memoryStream.GetBuffer();
    }

    static Packet CreatePacketFromBytes(byte[] bytes)
    {
        try
        {
            MemoryStream memoryStream = new MemoryStream(bytes);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            Packet receivedPacket = binaryFormatter.Deserialize(memoryStream) as Packet;
            return receivedPacket;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);

            return new Packet();
        }
    }
}