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
    class Player
    {
       
       
        public Vector2 playerPosition;

        const int speed = 6;

        Texture2D mSpriteTexture;
        Texture2D textureLeft;
        Texture2D textureRight;
        Texture2D textureDown;
        Texture2D textureUp;

        Texture2D textureLeftStill;
        Texture2D textureRightStill;
        Texture2D textureDownStill;
        Texture2D textureUpStill;
        Random random = new Random();
        KeyboardState keyboard = Keyboard.GetState();

        public Rectangle screenBounds = new Rectangle();
        bool directionRight = false;
        bool directionLeft = false;
        bool directionDown = false;
        bool directionUp = false;

        

        

        public bool playerHit = false;
        public bool playerAlive = true;
        

        public string assetName; 

        public void LoadContent (ContentManager theContentManager, string theAssetName)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>("player_sprite_down");
            textureDown = theContentManager.Load<Texture2D>("player_sprite_down");
            textureUp = theContentManager.Load<Texture2D>("player_sprite_up");
            textureRight = theContentManager.Load<Texture2D>("player_sprite_right");
            textureLeft = theContentManager.Load<Texture2D>("player_sprite_left");

            textureDownStill = theContentManager.Load<Texture2D>("player_still_down");
            textureUpStill = theContentManager.Load<Texture2D>("player_still_up");
            textureRightStill = theContentManager.Load<Texture2D>("player_still_right");
            textureLeftStill = theContentManager.Load<Texture2D>("player_still_left");

            assetName = theAssetName;

        }
        //For each of these move methods the directional sprites are assigned .. also calcualtes where the window edge is
        //keeping player within the boundary
        public void MoveUp()
        {
            playerPosition.X = MathHelper.Clamp(playerPosition.X, 0, screenBounds.Width - textureLeft.Width);
            playerPosition.Y = MathHelper.Clamp(playerPosition.Y, 0, screenBounds.Height - textureLeft.Height);
            
            if(playerPosition.Y < screenBounds.Height)
            {
                //These many bools are used to determine where the player is facing when still to load the still version of its directional sprite
                directionUp = true;
                directionDown = false;
                directionLeft = false;
                directionRight = false;

                playerPosition.Y -= speed;

                mSpriteTexture = textureUp;
            }
        }

        public void MoveDown()
        {
            playerPosition.X = MathHelper.Clamp(playerPosition.X, 0, screenBounds.Width - textureLeft.Width);
            playerPosition.Y = MathHelper.Clamp(playerPosition.Y, 0, screenBounds.Height - textureLeft.Height);
            
                directionDown = true;
                directionUp = false;
                directionLeft = false;
                directionRight = false;

                playerPosition.Y += speed;

                mSpriteTexture = textureDown;
            
           
        }

        public void MoveRight()
        {
            playerPosition.X = MathHelper.Clamp(playerPosition.X, 0, screenBounds.Width - textureLeft.Width);
            playerPosition.Y = MathHelper.Clamp(playerPosition.Y, 0, screenBounds.Height - textureLeft.Height);
            
                directionRight = true;
                directionDown = false;
                directionLeft = false;
                directionUp = false;

                playerPosition.X += speed;

                mSpriteTexture = textureRight;
            
           
        }

        public void MoveLeft()
        {
            playerPosition.X = MathHelper.Clamp(playerPosition.X, 0, screenBounds.Width - textureLeft.Width);
            playerPosition.Y = MathHelper.Clamp(playerPosition.Y, 0, screenBounds.Height - textureLeft.Height);

                directionLeft = true;
                directionRight = false;
                directionUp = false;
                directionDown = false;

                playerPosition.X -= speed;

                mSpriteTexture = textureLeft;
            
            
        }
        //Uses the bools used above to load the sprite when it stands still 
        public void LeftStill()
        {
            if( keyboard.IsKeyUp(Keys.Left) && directionLeft == true &&
            directionRight == false &&
            directionUp == false &&
            directionDown == false)
            {
                mSpriteTexture = textureLeftStill;
            }
        }

        public void RightStill()
        {
            if(keyboard.IsKeyUp(Keys.Right) && directionRight == true &&
            directionDown == false &&
            directionLeft == false &&
            directionUp == false)
            {
                mSpriteTexture = textureRightStill;
            }
        }

        public void UpStill()
        {
            if( keyboard.IsKeyUp(Keys.Up) && directionUp == true &&
            directionDown == false &&
            directionLeft == false &&
            directionRight == false)
            {
                mSpriteTexture = textureUpStill;
            }


        }

        public void DownStill()
        {
            if( keyboard.IsKeyUp(Keys.Down) &&directionDown == true &&
            directionUp == false &&
            directionLeft == false &&
            directionRight == false)
            {
                mSpriteTexture = textureDownStill;
            }
        }
        //Calculates player collision with the zombies calling its respawn method
        public void CollsionDetectionZomFollow(ZombieFollow zomFollow, int viewportWidth, int viewportHeight,  Bullet bullet)
        {
          Rectangle playerRectangle =
          new Rectangle((int)playerPosition.X, (int)playerPosition.Y,
          mSpriteTexture.Width, mSpriteTexture.Height);

          



            if (playerRectangle.Intersects(zomFollow.zomFollowRectangle) && zomFollow.zomFollowHit == false )
            {
                zomFollow.zomFollowHit = true;
                
                zomFollow.Respawn(random, viewportWidth, viewportHeight);

                playerAlive = false;
                Respawn(viewportWidth, viewportHeight);
                playerHit = true;
                bullet.score = 0;

              
            }
           
            
        }
        public void CollisionDetectionZomRandom(ZombieRandom zomRandom, int viewportWidth, int viewportHeight, Bullet bullet)
        {
         Rectangle playerRectangle =
         new Rectangle((int)playerPosition.X, (int)playerPosition.Y,
         mSpriteTexture.Width, mSpriteTexture.Height);

            if (playerRectangle.Intersects(zomRandom.zomRandomRectangle))
            {
                zomRandom.zomRandomHit = true;

                zomRandom.Respawn(random, viewportWidth, viewportHeight);
                playerAlive = false;
                Respawn(viewportWidth, viewportHeight);
                playerHit = true;


               bullet.score = 0;
                
            }
           
        }
        //Repawns the player at the middle of the screen
        public void Respawn(int viewportWidth, int viewportHeight)
        {
            if (playerAlive == false)
            {
                playerPosition.X = viewportWidth / 2 - (mSpriteTexture.Width / 2);
                playerPosition.Y = viewportHeight / 2 - (mSpriteTexture.Height / 2);

                playerAlive = true;

                
            }

           
           
        }


        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw( mSpriteTexture, playerPosition, Color.White);
        }
    }
}
