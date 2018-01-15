﻿using Newtonsoft.Json.Linq;

namespace LeagueSandbox.GameServer.Logic.GameObjects
{
    public class Placeable : AttackableUnit
    {
        public string Name { get; private set; }
        public AttackableUnit Owner { get; private set; } // We'll probably want to change this in the future

        public Placeable(
            AttackableUnit owner,
            float x,
            float y,
            string model,
            string name,
            uint netId = 0
        ) : base(model, new Stats(), 40, x, y, 0, netId)
        {
            SetTeam(owner.Team);

            Owner = owner;

            SetVisibleByTeam(Team, true);

            MoveOrder = MoveOrder.MOVE_ORDER_MOVE;

            Name = name;
        }

        public override void OnAdded()
        {
            base.OnAdded();
            _game.PacketNotifier.NotifySpawn(this);
        }

        public override bool IsInDistress()
        {
            return DistressCause != null;
        }
    }
}
