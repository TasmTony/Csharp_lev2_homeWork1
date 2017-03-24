using System;
using System.Drawing;

namespace MyGame
{
    /// <summary>
    /// Класс для отображения количества столкновений снаряда и астероида
    /// </summary>
    class Score:BaseObject
    {
        public Score(Point pos) : base(pos)
        {
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawString($"Score: "+Game.ScoreGame, SystemFonts.CaptionFont, Brushes.Red, pos.X, pos.Y);
        }

        public override void Update()
        {
            this.Draw();
        }
    }
}
