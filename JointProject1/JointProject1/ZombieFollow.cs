using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace JointProject1
{
    class ZombieFollow
    {
        public Vector2 zomFollowPosition;
        Texture2D aTexture;  //image of the sprite


        Texture2D textureLeft;
        Texture2D textureRight;
        Texture2D textureDown;
        Texture2D textureUp;
        public bool zomFollowHit = false;
        


        
        public int speed = 1;
        

        public Rectangle zomFollowRectangle = new Rectangle();
        
        public ZombieFollow(Random aGen)
        {
          

            zomFollowPosition.X = aGen.Next(1, 500);
            zomFollowPosition.Y = aGen.Next(1, 500);

            zomFollowHit = false;
        }



        //Load image of the sprite
        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            aTexture = theContentManager.Load<Texture2D>("zombieDown");
            textureDown = theContentManager.Load<Texture2D>("zombieDown");
            textureUp = theContentManager.Load<Texture2D>("zombieUp");
            textureRight = theContentManager.Load<Texture2D>("zombieRight");
            textureLeft = theContentManager.Load<Texture2D>("zombieLeft");

        }
        public void Move(Player thePlayer)
        {
          
         
           //Logic to get this zombie to follow the players position finding the x co ordinate first to make the game slightyl easier

            if(zomFollowPosition.X <  thePlayer.playerPosition.X -10)
            {
                zomFollowPosition.X += speed;

                aTexture = textureRight;
                
            }
            else if (zomFollowPosition.X > thePlayer.playerPosition.X +10)
            {
                zomFollowPosition.X -= speed;

                aTexture = textureLeft;
                
            }
            else if (zomFollowPosition.Y < thePlayer.playerPosition.Y -10)
            {
                zomFollowPosition.Y += speed;

                aTexture = textureDown;
                
            }
            else if (zomFollowPosition.Y > thePlayer.playerPosition.Y +10)
            {
                zomFollowPosition.Y -= speed;

                aTexture = textureUp;
                
            }


           

            zomFollowRectangle =
            new Rectangle((int)zomFollowPosition.X, (int)zomFollowPosition.Y,
            textureUp.Width, textureUp.Height);
        }
        public void Respawn(Random aGen, int viewportWidth, int viewportHeight)
        {
            
            //Respawns the zombie when killed at one of the 4 screen edges slightly outside of the window so it walks into the screen area.... looks cool
            

            if (zomFollowHit == true)
            {
                
                
                 int positon = aGen.Next(0, 4);

                 if (positon == 1 && zomFollowHit == true)
                    {
                        zomFollowPosition.X = -aTexture.Width;
                        zomFollowPosition.Y = aGen.Next(0, viewportHeight);

                       
                    }
                    else if (positon == 2 && zomFollowHit == true)
                    {
                        zomFollowPosition.X = aGen.Next(0, viewportWidth + aTexture.Width);
                        zomFollowPosition.Y = -aTexture.Height - 5;

                    }
                    else if (positon == 3 && zomFollowHit == true)
                    {
                        zomFollowPosition.X = viewportWidth + aTexture.Width;
                        zomFollowPosition.Y = aGen.Next(viewportHeight + aTexture.Height);

                        
                    }
                    else if (positon == 4 && zomFollowHit == true )
                    {
                        zomFollowPosition.X = aGen.Next(viewportWidth + aTexture.Width);
                        zomFollowPosition.Y = viewportHeight + aTexture.Height;

                        

                    }
                
            }
            zomFollowHit = false;    
         }
      
        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(aTexture, zomFollowPosition, Color.White);
        }

    }
}
