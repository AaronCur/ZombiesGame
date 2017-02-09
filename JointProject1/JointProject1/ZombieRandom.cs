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
    class ZombieRandom
    {
        public Vector2 zomRandomPosition;
        Texture2D aTexture;  //image of the sprite


        Texture2D textureLeft;
        Texture2D textureRight;
        Texture2D textureDown;
        Texture2D textureUp;
        public bool zomRandomHit= false;

        public Rectangle zomRandomRectangle = new Rectangle();

        
        int direction = 1; //Default travel direction


        
        public int speed = 1; //initial speed of the zombie at round 1
        

        public Rectangle zomFollowRectangle = new Rectangle();
        
        public ZombieRandom(Random aGen)
        {
          

            zomRandomPosition.X = aGen.Next(1, 500);
            zomRandomPosition.Y = aGen.Next(1, 500);

            zomRandomHit = false;
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
        public void Move(int viewportWidth, int viewportHeight)
        {
            
            //Logic for setting the random zombie on a set path(contradicts its name) and assigns the correct directional sprite
            
            if (direction == 0)
            {
               zomRandomPosition.X -= speed;
               aTexture = textureLeft;
              
            }
            if(direction == 1)
            {
                zomRandomPosition.X += speed;
                aTexture = textureRight;
            }
            if(direction == 3)
            {
                zomRandomPosition.Y += speed;
                aTexture = textureDown;
            }
            if (direction == 4)
            {
                zomRandomPosition.Y -= speed;
                aTexture = textureUp;
            }
            
           

           
           
           
           if(zomRandomPosition.X == viewportWidth - aTexture.Width)
           {
               direction = 4;
           }
           if(zomRandomPosition.Y > 0 && zomRandomPosition.Y < 10)
           {
               direction = 0;
           }

           if (zomRandomPosition.X == viewportWidth / 2 && zomRandomPosition.Y > 0 && zomRandomPosition.Y < 10)
           {
               direction = 3;
           }
          if(zomRandomPosition.X == viewportWidth / 2 && zomRandomPosition.Y == viewportHeight - aTexture.Height)
          {
              direction = 0;
          }
          if(zomRandomPosition.X == 0)
          {
              direction = 4;
          }
          if(zomRandomPosition.X == 0 && zomRandomPosition.Y == viewportHeight / 2)
          {
              direction = 1;
          }


         zomRandomRectangle =
         new Rectangle((int)zomRandomPosition.X, (int)zomRandomPosition.Y,
         textureUp.Width, textureUp.Height);

        
        }
        public void Respawn(Random aGen, int viewportWidth, int viewportHeight)
        {
            

            //When the zombie is hit by the bullet it respawned at the specific location and randomly goes left or right initially

            if (zomRandomHit == true)
            {
                direction = aGen.Next(2);


                zomRandomPosition.X = viewportWidth/2;
                zomRandomPosition.Y = viewportHeight - (2 * aTexture.Height);

                
            }
            zomRandomHit = false;    
         }
      
        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(aTexture, zomRandomPosition, Color.White);
        }

    }
}

