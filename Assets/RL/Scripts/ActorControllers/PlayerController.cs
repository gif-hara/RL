﻿using RL.FieldSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL.ActorControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PlayerController : Actor
    {
        void Update()
        {
            int x = 0, y = 0;

            if (GetKeyDown(KeyCode.W) || GetKeyDown(KeyCode.Q) || GetKeyDown(KeyCode.E))
            {
                y = -1;
            }
            else if (GetKeyDown(KeyCode.X) || GetKeyDown(KeyCode.Z) || GetKeyDown(KeyCode.C))
            {
                y = 1;
            }
            if (GetKeyDown(KeyCode.A) || GetKeyDown(KeyCode.Q) || GetKeyDown(KeyCode.Z))
            {
                x = -1;
            }
            else if (GetKeyDown(KeyCode.D) || GetKeyDown(KeyCode.E) || GetKeyDown(KeyCode.C))
            {
                x = 1;
            }

            if (x != 0 || y != 0)
            {
                this.Move(this.Id + new Point(x, y), false);
            }
        }

        private static bool GetKeyDown(KeyCode keyCode)
        {
            return Input.GetKeyDown(keyCode);
        }
    }
}
