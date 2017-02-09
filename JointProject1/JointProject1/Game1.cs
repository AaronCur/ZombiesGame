using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace JointProject1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        
        

        Bullet bullet1;
        Player aPlayer;
        ZombieFollow[]zomFollow = new ZombieFollow[2];
        

        ZombieRandom[] zomRandom = new ZombieRandom[2];
        

        Texture2D background;
        

        Vector2 backgroundPosition;
        
        


        public int viewportWidth;
        public int viewportHeight;
        
        public int rounds;

        


        

        public Random random = new Random();

        int direction;

        int highScore;
        int highRound;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            viewportHeight = graphics.GraphicsDevice.Viewport.Height;
            viewportWidth = graphics.GraphicsDevice.Viewport.Width;

           

            random = new Random();

            bullet1 = new Bullet();
            aPlayer = new Player();
            aPlayer.screenBounds = graphics.GraphicsDevice.Viewport.Bounds;

            zomFollow[0] = new ZombieFollow(random);
            zomFollow[1] = new ZombieFollow(random);
            
            zomRandom[0] = new ZombieRandom(random);
            zomRandom[1] = new ZombieRandom(random);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            aPlayer.LoadContent(this.Content, "player sprite left");
            zomFollow[0].LoadContent(this.Content, "ZombieDown");
            zomFollow[1].LoadContent(this.Content, "ZombieDown");
            
            zomRandom[0].LoadContent(this.Content, "ZombieDown");
            zomRandom[1].LoadContent(this.Content, "ZombieDown");
            bullet1.LoadContent(this.Content, "arrow");

            font = Content.Load<SpriteFont>("SpriteFont1");
            background = Content.Load<Texture2D>("Cemetary");

            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            //Get input
            KeyboardState keyboard = Keyboard.GetState();
            GamePadState gamePad = GamePad.GetState(PlayerIndex.One);

            //Allows game to exit
            if (gamePad.Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Move the player with the arrow keys or Dpad
            if (keyboard.IsKeyDown(Keys.Left) || gamePad.DPad.Left == ButtonState.Pressed )
            {

                if (aPlayer.playerPosition.X > 0 )
                aPlayer.MoveLeft();


                direction = 1;//Set direction so arrow shoots direction it is facing

            }
            else if (keyboard.IsKeyDown(Keys.Right) || gamePad.DPad.Right == ButtonState.Pressed)
            {
                

                aPlayer.MoveRight();

                direction = 2;

            }
            else if (keyboard.IsKeyDown(Keys.Up) || gamePad.DPad.Up == ButtonState.Pressed)
            {


                aPlayer.MoveUp();

                direction = 3;
            }
            else if (keyboard.IsKeyDown(Keys.Down) || gamePad.DPad.Down == ButtonState.Pressed)
            {


                aPlayer.MoveDown();

                direction = 4;

            }
            else if (keyboard.IsKeyDown(Keys.Space) == true && bullet1.bulletAlive == false)
            {

                bullet1.Shoot(direction, aPlayer);

               

            }
                //If the sprite isnt moving the still sprite is loaded for given direction
            else
            {
                
                aPlayer.LeftStill();

                aPlayer.RightStill();

                aPlayer.UpStill();

                aPlayer.DownStill();

            }
            
            zomFollow[0].Move(aPlayer);
            zomFollow[1].Move(aPlayer);

            zomRandom[0].Move(viewportWidth, viewportHeight);
            zomRandom[1].Move(viewportWidth, viewportHeight);
           
            bullet1.Update(aPlayer, direction);
            //Calcualte collision between the zombies and the bullet
            if (bullet1.bulletAlive == true)
            {
                bullet1.CollisionDetectionZomFollow(zomFollow[0], viewportWidth, viewportHeight,  bullet1.bulletRectangle);
                bullet1.CollisionDetectionZomFollow(zomFollow[1], viewportWidth, viewportHeight, bullet1.bulletRectangle);
                

                bullet1.CollisionDetectionZomRandom(zomRandom[0], viewportWidth, viewportHeight, bullet1.bulletRectangle);
                bullet1.CollisionDetectionZomRandom(zomRandom[1], viewportWidth, viewportHeight, bullet1.bulletRectangle);
                

            }
            
            //Calculate collision between player and the zombies
            aPlayer.CollsionDetectionZomFollow(zomFollow[0], viewportWidth, viewportHeight, bullet1);
            aPlayer.CollsionDetectionZomFollow(zomFollow[1], viewportWidth, viewportHeight, bullet1);
            

            aPlayer.CollisionDetectionZomRandom(zomRandom[0], viewportWidth, viewportHeight, bullet1);
            aPlayer.CollisionDetectionZomRandom(zomRandom[1], viewportWidth, viewportHeight, bullet1);

            //Logic for score needed per round
            if(bullet1.score == 0)
            {
                
                rounds = 0;
                
            }
            
            if(bullet1.score == 15)
            {
                rounds = 2;
            }
            if(bullet1.score == 25)
            {
                rounds = 3;
            }
            if(bullet1.score == 40)
            {
                rounds = 4;
            }
            if(bullet1.score == 55)
            {
                rounds = 5;
            }
            if(rounds == 0)
            {
                zomFollow[0].speed = 1;
                zomFollow[1].speed = 1;
                rounds = 1;
                

            }
            //Logic for what happens with increased round.... zombies get more aggressive!!!spooky

            if(rounds == 2)
            {
                
                zomFollow[0].speed = 2;
                zomFollow[1].speed = 2;
                
            }
            if (rounds == 3)
            {
                
                zomFollow[0].speed = 3;
                zomFollow[1].speed = 3;
                
            }
            if (rounds == 4)
            {
               
                zomFollow[0].speed = 4;
                zomFollow[1].speed = 4;
                

            }
            if (rounds == 5)
            {
                
                zomFollow[0].speed = 5;
                zomFollow[1].speed = 5;
               

            }
            //To set the highscore
            if(bullet1.score > highScore)
            {
                highScore = bullet1.score;
            }
            if(rounds > highRound)
            {
                highRound = rounds;
            }

           

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(background, backgroundPosition, Color.White);


            if(aPlayer.playerAlive == true)
            {
                aPlayer.Draw(this.spriteBatch);
                                                //Player object draws itself
            }
            //Red flash effect if u get hit by a zombie

            if(aPlayer.playerHit == true)
            {

                spriteBatch.Draw(background, backgroundPosition, Color.Red);
                    
                    aPlayer.playerHit = false;

                
            }
            spriteBatch.DrawString(font, rounds + ": ROUND ", new Vector2(650, 10), Color.Red);
            spriteBatch.DrawString(font, "SCORE :" + bullet1.score, new Vector2(25, 10), Color.Blue);
            spriteBatch.DrawString(font, ": RECORD :", new Vector2(335,5), Color.Green);
            spriteBatch.DrawString(font,"" +highScore, new Vector2(300, 5), Color.Blue);
            spriteBatch.DrawString(font, "" + highRound, new Vector2(500, 5), Color.Red);

          
            if (zomFollow[0].zomFollowHit == false)
            {
                zomFollow[0].Draw(this.spriteBatch);
                zomFollow[1].Draw(this.spriteBatch);
                
            }
            if (bullet1.bulletAlive == true)
            {

                bullet1.Draw(this.spriteBatch);
            }
            if(zomRandom[0].zomRandomHit == false)
            {
                zomRandom[0].Draw(this.spriteBatch);
                zomRandom[1].Draw(this.spriteBatch);
            }
            

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
