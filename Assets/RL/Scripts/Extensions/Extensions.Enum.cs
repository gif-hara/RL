using RL.GameSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL.Extensions
{
    /// <summary>
    /// <see cref="Enum"/>に関する拡張関数群
    /// </summary>
    public static partial class Extensions
    {
        public static Point ToPoint(this Direction self)
        {
            var result = Point.Zero;
            if(self == Direction.Left || self == Direction.LeftTop || self == Direction.LeftBottom)
            {
                result.x = -1;
            }
            else if(self == Direction.Right || self == Direction.RightTop || self == Direction.RightBottom)
            {
                result.x = 1;
            }

            if(self == Direction.LeftTop || self == Direction.Top || self == Direction.RightTop)
            {
                result.y = -1;
            }
            else if(self == Direction.LeftBottom || self == Direction.Bottom || self == Direction.RightBottom)
            {
                result.y = 1;
            }

            return result;
        }

        public static Direction ToDirection(this Point self)
        {
            if(self.x == 0 && self.y == 0)
            {
                return Direction.None;
            }
            if (self.x < 0 && self.y == 0)
            {
                return Direction.Left;
            }
            if (self.x < 0 && self.y < 0)
            {
                return Direction.LeftTop;
            }
            if (self.x == 0 && self.y < 0)
            {
                return Direction.Top;
            }
            if (self.x > 0 && self.y < 0)
            {
                return Direction.RightTop;
            }
            if (self.x > 0 && self.y == 0)
            {
                return Direction.Right;
            }
            if (self.x > 0 && self.y > 0)
            {
                return Direction.RightBottom;
            }
            if (self.x == 0 && self.y > 0)
            {
                return Direction.Bottom;
            }
            if (self.x < 0 && self.y > 0)
            {
                return Direction.LeftBottom;
            }

            Assert.IsTrue(false, $"未対応の値です self = {self}");
            return Direction.None;
        }

        /// <summary>
        /// 4方向へ変換する
        /// </summary>
        /// <remarks>
        /// 斜め方向の場合はランダムで4方向に変換する
        /// </remarks>
        public static Direction ToCross(this Direction self)
        {
            if(self.IsCross())
            {
                return self;
            }

            var random = Random.value < 0.5f ? -1 : 1;
            return (Direction)((int)self + random);
        }

        /// <summary>
        /// <paramref name="self"/>が4方向であるか返す
        /// </summary>
        public static bool IsCross(this Direction self)
        {
            return (int)self % 2 == 1;
        }

        /// <summary>
        /// 水平方向であるか返す
        /// </summary>
        public static bool IsHorizontal(this Direction self)
        {
            return self == Direction.Left || self == Direction.Right;
        }

        /// <summary>
        /// 垂直方向であるか返す
        /// </summary>
        public static bool IsVertical(this Direction self)
        {
            return self == Direction.Top || self == Direction.Bottom;
        }

        /// <summary>
        /// 反転した<see cref="Direction"/>を返す
        /// </summary>
        /// <remarks>
        /// <see cref="Direction.Left"/>の場合は<see cref="Direction.Right"/>を返す
        /// <see cref="Direction.LeftTop"/>の場合は<see cref="Direction.RightBottom"/>を返す
        /// </remarks>
        public static Direction Invert(this Direction self)
        {
            Assert.AreNotEqual(self, Direction.None);
            var id = (int)self;
            if(id < 4)
            {
                return (Direction)(id + 4);
            }
            else
            {
                return (Direction)(id - 4);
            }
        }
    }
}
