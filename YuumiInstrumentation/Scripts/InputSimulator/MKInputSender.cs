using System;
using System.Collections.Generic;

namespace MouseKeyboard.Network
{
    public class MKInputSender
    {

        public delegate void MKInput(MKPacketContent content);

        private Dictionary<Commands, MKInput> dict = new Dictionary<Commands, MKInput>();

        public MKInputSender(MKInput a)
        {

        }
    }
}