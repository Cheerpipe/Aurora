﻿using Newtonsoft.Json.Linq;
using System;

namespace Aurora.Profiles.Skype
{
    public class State_SkypeOverlay : GameState<State_SkypeOverlay>
    {
        private Skype_Integration _integration_data;

        public Skype_Integration Data
        {
            get
            {
                if (_integration_data == null)
                {
                    _integration_data = new Skype_Integration(_ParsedData.ToString() ?? "");
                }

                return _integration_data;
            }
        }


        public State_SkypeOverlay()
        {
            json = "{}";
            _ParsedData = Newtonsoft.Json.Linq.JObject.Parse(json);
        }

        public State_SkypeOverlay(string JSONstring)
        {
            if (String.IsNullOrWhiteSpace(JSONstring))
                JSONstring = "{}";

            json = JSONstring;
            _ParsedData = JObject.Parse(JSONstring);
        }

        public State_SkypeOverlay(IGameState other_state) : base(other_state)
        {
        }
    }

    public class Skype_Integration : Node<Skype_Integration>
    {
        public int MissedMessagesCount;
        public bool IsCalled;

        internal Skype_Integration(string JSON) : base(JSON)
        {
            MissedMessagesCount = GetInt("MissedMessagesCount");
            IsCalled = GetBool("IsCalled");
        }
    }
}