﻿using System;
using UnityEngine;

namespace RainMeadow
{
    public class SlugcatCustomization : AvatarData
    {
        public Color bodyColor;
        public Color eyeColor;
        public SlugcatStats.Name playingAs;

        public SlugcatCustomization() { }

        internal override void ModifyBodyColor(ref Color originalBodyColor)
        {
            originalBodyColor = new Color(Mathf.Clamp(bodyColor.r, 0.004f, 0.996f), Mathf.Clamp(bodyColor.g, 0.004f, 0.996f), Mathf.Clamp(bodyColor.b, 0.004f, 0.996f));
        }

        internal override void ModifyEyeColor(ref Color originalEyeColor)
        {
            originalEyeColor = new Color(Mathf.Clamp(eyeColor.r, 0.004f, 0.996f), Mathf.Clamp(eyeColor.g, 0.004f, 0.996f), Mathf.Clamp(eyeColor.b, 0.004f, 0.996f));
        }

        internal override Color GetBodyColor()
        {
            return bodyColor;
        }

        internal Color SlugcatColor()
        {
            return bodyColor;
        }

        public override EntityDataState MakeState(OnlineEntity onlineEntity, OnlineResource inResource)
        {
            if (inResource is Lobby || inResource is WorldSession)
            {
                return new State(this);
            }
            return null;
        }

        public class State : EntityDataState
        {
            [OnlineFieldColorRgb]
            public Color bodyColor;
            [OnlineFieldColorRgb]
            public Color eyeColor;
            [OnlineField(nullable = true)]
            public SlugcatStats.Name playingAs;

            public State() { }
            public State(SlugcatCustomization slugcatCustomization) : base()
            {
                bodyColor = slugcatCustomization.bodyColor;
                eyeColor = slugcatCustomization.eyeColor;
                playingAs = slugcatCustomization.playingAs;
            }

            public override void ReadTo(OnlineEntity.EntityData entityData, OnlineEntity onlineEntity)
            {
                var slugcatCustomization = onlineEntity.GetData<SlugcatCustomization>();
                slugcatCustomization.bodyColor = bodyColor;
                slugcatCustomization.eyeColor = eyeColor;
                slugcatCustomization.playingAs = playingAs;
            }

            public override Type GetDataType() => typeof(SlugcatCustomization);
        }
    }
}
