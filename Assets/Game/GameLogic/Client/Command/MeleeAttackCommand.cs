using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class MeleeAttackCommand : IClientCommand
{
    public ClientPackets Command => throw new NotImplementedException();

    public void Execute(GameDirector director)
    {
        throw new NotImplementedException();
    }

    public Packet ToPacket()
    {
        throw new NotImplementedException();
    }
}
