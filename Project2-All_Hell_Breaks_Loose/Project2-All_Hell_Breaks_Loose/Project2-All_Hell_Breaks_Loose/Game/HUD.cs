using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project2_All_Hell_Breaks_Loose.Game
{
    class HUD
    {
        int score;
        int highScore;
        int previousScore;
        int waveNum;
        float playerHealth;
        string weaponName;
        int ammo;

        SpriteFont TNR;
        Vector2 HUDPosition;

        public HUD(Vector2 pos)
        {
            HUDPosition = pos;
        }

        public void loadFont(SpriteFont font)
        {
            TNR = font;
        }

        public void update(int score, int highScore, int prevScore, int wavenum, float health, string weapon, int ammo)
        {
            this.score = score;
            this.highScore = highScore;
            this.previousScore = prevScore;
            this.waveNum = wavenum;
            this.playerHealth = health;
            this.weaponName = weapon;
            this.ammo = ammo;
        }

        public void draw(SpriteBatch batch)
        {
            string HUDString = "Current Score: " + score
                + "\nHigh Score: " + highScore
                + "\nPrevious Score: " + previousScore
                + "\n" + "Wave: " + waveNum
                + "\n" + "Health: " + playerHealth
                + "\n" + "Current Weapon: " + weaponName + "   Ammo: " + ammo;
            batch.Begin();
            batch.DrawString(TNR, HUDString, HUDPosition, Color.White);
            batch.End();
        }
    }
}
