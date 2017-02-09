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
    class Bullet
    {
        public Vector2 bulletPosition;

        const int speed = 8;
  
        Texture2D mSpriteTexture;
        Texture2D textureLeft;
        Texture2D textureRight;
        Texture2D textureDown;
        Texture2D textureUp;

        int bulletDirection; //tp determine the direction bullet will be fired and to load the correct sprite

        KeyboardState keyboard = Keyboard.GetState();
        public Rectangle screenBounds = new Rectangle();
        public Rectangle bulletRectangle = new Rectangle();
        Random random = new Random();

        public bool bulletAlive = false;
        

        public int score = 0;

        

        public string assetName; 

        

        public void LoadContent (ContentManager theContentManager, string theAssetName)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>("arrowLeft");
            textureDown = theContentManager.Load<Texture2D>("arrowDown");
            textureUp = theContentManager.Load<Texture2D>("arrowUp");
            textureRight = theContentManager.Load<Texture2D>("arrowRight");
            textureLeft = theContentManager.Load<Texture2D>("arrowLeft");
            
            assetName = theAssetName;
        }
        public void Shoot(int direction, Player player)
        {
            bulletDirection = direction;
            bulletAlive = true;

           

           
          
                bulletPosition.X = player.playerPosition.X;
                bulletPosition.Y = player.playerPosition.Y;
        }


        public void CollisionDetectionZomFollow(ZombieFollow zomFollow, int viewportWidth, int viewportHeight, Rectangle bulletRectangle)
        {
           
             if(bulletRectangle.Intersects(zomFollow.zomFollowRectangle) && bulletAlive == true)
           {
               
               zomFollow.zomFollowHit = true;
               zomFollow.Respawn(random, viewportWidth, viewportHeight);
               bulletAlive = false;
                 //Score increases when collision between zombies and bullet occurs
               score = score + 1;
               
           }
             
             
             
              
             
           
            
        }
        public void CollisionDetectionZomRandom(ZombieRandom zomRandom, int viewportWidth, int viewportHeight, Rectangle bulletRectangle)
        {

            if (bulletRectangle.Intersects(zomRandom.zomRandomRectangle) && bulletAlive == true)
            {

                zomRandom.zomRandomHit = true;
                zomRandom.Respawn(random, viewportWidth, viewportHeight);
                bulletAlive = false;

                score = score + 1;

            }







        }
               
        public void Update(Player player, int direction)
        {
          //Calculations for shooting bullet the correct direction

           if(bulletAlive == false)
           {
               return;
           }

            if (bulletDirection == 1)
             {
                
                 bulletPosition.X -= speed;

                 mSpriteTexture = textureLeft;
             }
            else if (bulletDirection == 2)
             {
                 

                 bulletPosition.X += speed;

                 mSpriteTexture = textureRight;

                 
             }
            else if (bulletDirection == 3)
             {

                 
                 bulletPosition.Y -= speed;

                 mSpriteTexture = textureUp;
             }
            else if (bulletDirection == 4)
             {
                
                 bulletPosition.Y += speed;

                 mSpriteTexture = textureDown;
             }
            //Kills the bullet if it goes off screen

            if (bulletPosition.Y < 0 || bulletPosition.X < 0)
            {
               bulletAlive = false;
            }
            if(bulletPosition.Y > player.screenBounds.Height || bulletPosition.X > player.screenBounds.Width)
            {
                bulletAlive = false;
            }
          bulletRectangle =
          new Rectangle((int)bulletPosition.X, (int)bulletPosition.Y,
          textureUp.Width, textureUp.Height);
        }
     
        public void Draw(SpriteBatch theSpriteBatch)
        {
            
                theSpriteBatch.Draw(mSpriteTexture, bulletPosition, Color.White);
            
            
        }
    }
}
