//Programmers: Faheem Syed, Helen lin
//Game: Bowser Vs Bowser



using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace TheGame
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        SoundEffect Round1;
        SoundEffect Round2;
        SoundEffect Round3;
        Song backgroundSong;
        SoundEffect punchSwoosh;
        SoundEffect punch;
        SoundEffect fireBall;
        enum playerState { STANDING, WALKING, RUNNING, JUMPING, BLOCKING, PUNCHING, FIRING, FIREBALL, HURT, DEAD };
        playerState Player1CurrentState;
        playerState Player1PreviousState;
        playerState Player2CurrentState;
        playerState Player2PreviousState;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameObject StartScreen;
        GameObject stage1;
        GameObject stage2;
        GameObject stage3;
        GameObject EndScreen;
        Texture2D Bowser1;
        Texture2D Bowser2;
        Texture2D Bowser1_FireBall;
        Texture2D Bowser2_FireBall;
        Texture2D Bowser1_HealthBar;
        Texture2D Bowser2_HealthBar;
        Texture2D WinMessageDisplay;
        Texture2D RoundNumber;
        Texture2D Player1_WinCountBox;
        Texture2D Player2_WinCountBox;
        KeyboardState previousKeyboardState = Keyboard.GetState();
        Rectangle SourceRectangle1;
        Rectangle DestinationRectangle1;
        Rectangle SourceRectangle2;
        Rectangle DestinationRectangle2;
        Rectangle SourceRectangle1_FireBall;
        Rectangle DestinationRectangle1_FireBall;
        Rectangle SourceRectangle2_FireBall;
        Rectangle DestinationRectangle2_FireBall;
        Rectangle SourceRectangle1_HealthBar;
        Rectangle DestinationRectangle1_HealthBar;
        Rectangle SourceRectangle2_HealthBar;
        Rectangle DestinationRectangle2_HealthBar;
        Rectangle SourceRectangle_WinMessage;
        Rectangle DestinationRectangle_WinMessage;
        Rectangle SourceRectangle_RoundNumber;
        Rectangle DestinationRectangle_RoundNumber;
        Rectangle SourceRectangle1_WinCountBox;
        Rectangle DestinationRectangle1_WinCountBox;
        Rectangle sourceRectangle2_WinCountBox;
        Rectangle DestinationRectangle2_WinCountBox;
        float elapsed1 = 0;
        float elapsed1_FireBall = 0;
        float elapsed2 = 0;
        float elapsed2_FireBall = 0;
        float elapsedForStartScreen = 0;
        float elapsedForEndScreen = 0;
        int frames1 = 0;
        int frames1_Fireball = 0;
        int frames2 = 0;
        int frames2_Fireball = 0;
        bool flip1 = false;
        bool flip1_FireBall = false;
        bool flip2 = true;
        bool flip2_FireBall = false;
        int playersStandingOnYCoordinate = 300;
        int player1X_ForPunching;
        int player2X_ForPunching;
        int DistanceBetweenPlayersX = 0;
        int DistanceBetweenPlayersY = 0;
        int DistanceBetweenPlayersXLimit = 110; //limit the distance when players are walking together
        int DistanceBetweenPlayer1AndFireBall2X = 0;
        int DistanceBetweenPlayer1AndFireBall2Y = 0;
        int DistanceBetweenPlayer2AndFireBall1X = 0;
        int DistanceBetweenPlayer2AndFireBall1Y = 0;
        int DistanceBetweenFireBall1AndFireBall2 = 0;
        bool player1_jumpingRight = false;
        bool player1_jumpingLeft = false;
        bool player2_jumpingRight = false;
        bool player2_jumpingLeft = false;
        int player1_Health = 100;
        int player2_Health = 100;
        bool FireBall1_CurrentState_Active = false;
        bool FireBall1_PreviousState_Active = false;
        bool FireBall2_CurrentState_Active = false;
        bool FireBall2_PreviousState_Active = false;
        bool WindowIsFullScreen = false;
        bool StartScreenActive = true;
        bool stage1Active = false;
        bool stage2Active = false;
        bool stage2PreviousActive = false;
        bool stage3Active = false;
        bool stage3PreviousActive = false;
        bool EndScreenActive = false;
        int player1_WinCount = 0;
        int player2_WinCount = 0;
        int WinMessageBoxCount = 0;
        bool WinMessageBoxActive = false;
        int RoundNumCount = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //graphics.IsFullScreen = true;
            //graphics.PreferredBackBufferWidth = 1280;
            //graphics.PreferredBackBufferHeight = 720;
            //WindowIsFullScreen = true;
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            backgroundSong = Content.Load<Song>("Bowser Background Song");
            MediaPlayer.Play(backgroundSong);
            MediaPlayer.IsRepeating = true;
            Round1 = Content.Load<SoundEffect>("Round 1");
            Round2 = Content.Load<SoundEffect>("Round 2");
            Round3 = Content.Load<SoundEffect>("Final Round");
            punch = Content.Load<SoundEffect>("Punch");
            punchSwoosh = Content.Load<SoundEffect>("PunchSwoosh");
            fireBall = Content.Load<SoundEffect>("FireBall");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            StartScreen = new GameObject(Content.Load<Texture2D>("StartScreen"), graphics);
            stage1 = new GameObject(Content.Load<Texture2D>("stage1"), graphics);
            stage2 = new GameObject(Content.Load<Texture2D>("stage2"), graphics);
            stage3 = new GameObject(Content.Load<Texture2D>("stage3"), graphics);
            EndScreen = new GameObject(Content.Load<Texture2D>("EndScreen"), graphics);
            Bowser1 = Content.Load<Texture2D>("BowserSpriteSheet");
            Bowser2 = Content.Load<Texture2D>("BowserSpriteSheet");
            Bowser1_FireBall = Content.Load<Texture2D>("BowserSpriteSheet");
            Bowser2_FireBall = Content.Load<Texture2D>("BowserSpriteSheet");
            Bowser1_HealthBar = Content.Load<Texture2D>("HealthBar");
            Bowser2_HealthBar = Content.Load<Texture2D>("HealthBar");
            WinMessageDisplay = Content.Load<Texture2D>("WinMessage");
            RoundNumber = Content.Load<Texture2D>("RoundNumber");
            Player1_WinCountBox = Content.Load<Texture2D>("PlayersWinCount");
            Player2_WinCountBox = Content.Load<Texture2D>("PlayersWinCount");


            
            if (WindowIsFullScreen == true)
            {

                playersStandingOnYCoordinate = 500;
                StartScreen.Scale = 1.0f;
                StartScreen.position = new Vector2(640, 360);
                stage1.Scale = 1.0f;
                stage1.position = new Vector2(640, 360);
                stage2.Scale = 1.0f;
                stage2.position = new Vector2(640, 360);
                stage3.Scale = 1.0f;
                stage3.position = new Vector2(640, 360);
                EndScreen.Scale = 1.0f;
                EndScreen.position = new Vector2(640, 360);
                DestinationRectangle1.X = 1100;
                DestinationRectangle2.X = 100;
                DestinationRectangle1.Y = 300;
                DestinationRectangle2.Y = 300;
                DestinationRectangle1_HealthBar = new Rectangle(925, 30, (int)(HealthBar.Bar.Width[0] * 1.2), (int)(HealthBar.Bar.Height[0] * .8));
                DestinationRectangle2_HealthBar = new Rectangle(0, 30, (int)(HealthBar.Bar.Width[0] * 1.2), (int)(HealthBar.Bar.Height[0] * .8));
                DestinationRectangle1_WinCountBox = new Rectangle(955, 0, Message.Player1_WinCountBox.Width[0], Message.Player1_WinCountBox.Height[0]);
                DestinationRectangle2_WinCountBox = new Rectangle(0, 0, Message.Player2_WinCountBox.Width[0], Message.Player2_WinCountBox.Height[0]);
                DestinationRectangle_RoundNumber = new Rectangle(600, 5, (int)(Message.RoundNumber.Width[0] * .4), (int)(Message.RoundNumber.Height[0] * .4));
            }

            else
            {
                StartScreen.Scale = .67f;
                StartScreen.position = new Vector2(420, 240);
                stage1.Scale = 1.0f;
                stage1.position = new Vector2(350, 200);
                stage2.Scale = 1.0f;
                stage2.position = new Vector2(350, 150);
                stage3.Scale = 1.0f;
                stage3.position = new Vector2(350, 150);
                EndScreen.Scale = .67f;
                EndScreen.position = new Vector2(370, 240);
                DestinationRectangle1.X = 450;
                DestinationRectangle2.X = 150;
                DestinationRectangle1.Y = 300;
                DestinationRectangle2.Y = 300;
                DestinationRectangle1_HealthBar = new Rectangle(450, 30, (int)(HealthBar.Bar.Width[0] * 1.2), (int)(HealthBar.Bar.Height[0] * .8));
                DestinationRectangle2_HealthBar = new Rectangle(0, 30, (int)(HealthBar.Bar.Width[0] * 1.2), (int)(HealthBar.Bar.Height[0] * .8));
                DestinationRectangle1_WinCountBox = new Rectangle(475, 0, Message.Player1_WinCountBox.Width[0], Message.Player1_WinCountBox.Height[0]);
                DestinationRectangle2_WinCountBox = new Rectangle(0, 0, Message.Player2_WinCountBox.Width[0], Message.Player2_WinCountBox.Height[0]);
                DestinationRectangle_RoundNumber = new Rectangle(345, 30, (int)(Message.RoundNumber.Width[0] * .4), (int)(Message.RoundNumber.Height[0] * .4));
            }


            Player1CurrentState = playerState.STANDING;
            Player1PreviousState = playerState.STANDING;
            Player2CurrentState = playerState.STANDING;
            Player2PreviousState = playerState.STANDING;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            RoundNumCount = 0;
            frames1 = 0;
            frames2 = 0;
            frames1_Fireball = 0;
            frames2_Fireball = 0;
            player1_WinCount = 0;
            player2_WinCount = 0;
        }
        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            #region keys for game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || GamePad.GetState(PlayerIndex.Two).Buttons.Back == ButtonState.Pressed)
                this.Exit();
#if!XBOX
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
#endif
            #endregion

            #region keys for switching StartScreen and EndScreen XBOX
            if (StartScreenActive)
            {
                elapsedForStartScreen += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (elapsedForStartScreen >= 500 && (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed) || (GamePad.GetState(PlayerIndex.Two).Buttons.Start == ButtonState.Pressed))
                {
                    StartScreenActive = false;
                    stage1Active = true;
                    Round1.Play();
                    elapsedForStartScreen = 0;
                }
            }
            if (EndScreenActive)
            {
                elapsedForEndScreen += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (elapsedForEndScreen >= 500 && (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed) || (GamePad.GetState(PlayerIndex.Two).Buttons.Start == ButtonState.Pressed))
                {
                    RoundNumCount = 0;
                    player1_WinCount = 0;
                    player2_WinCount = 0;
                    if (WindowIsFullScreen == true)
                    {
                        DestinationRectangle1.X = 1100;
                        DestinationRectangle2.X = 100;
                    }
                    else
                    {
                        DestinationRectangle1.X = 450;
                        DestinationRectangle2.X = 150;
                    }

                    StartScreenActive = true;
                    EndScreenActive = false;
                    elapsedForEndScreen = 0;
                }
            }
            #endregion
            #region keys for switching StartScreen and EndScreen !XBOX
#if!XBOX
            if (StartScreenActive)
            {
                elapsedForStartScreen += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (elapsedForStartScreen >= 500 && Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    Round1.Play();
                    StartScreenActive = false;
                    stage1Active = true;
                    elapsedForStartScreen = 0;
                }
            }
            if (EndScreenActive)
            {
                elapsedForEndScreen += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (elapsedForEndScreen >= 500 && Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    RoundNumCount = 0;
                    player1_WinCount = 0;
                    player2_WinCount = 0;
                    if (WindowIsFullScreen == true)
                    {
                        DestinationRectangle1.X = 1100;
                        DestinationRectangle2.X = 100;
                    }
                    else
                    {
                        DestinationRectangle1.X = 450;
                        DestinationRectangle2.X = 150;
                    }
                    StartScreenActive = true;
                    EndScreenActive = false;
                    elapsedForEndScreen = 0;
                }
            }
#endif
            #endregion

            if (stage1Active || stage2Active || stage3Active)
            {
                #region Keys for switching stages XBOX
                if (stage1Active && (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed) || (GamePad.GetState(PlayerIndex.Two).Buttons.Start == ButtonState.Pressed)
                    && (Player1CurrentState == playerState.DEAD || Player2CurrentState == playerState.DEAD))
                {
                    RoundNumCount += 1;
                    if (WindowIsFullScreen == true)
                    {
                        DestinationRectangle1.X = 1100;
                        DestinationRectangle2.X = 100;
                    }
                    else
                    {
                        DestinationRectangle1.X = 450;
                        DestinationRectangle2.X = 150;
                    }
                    stage1Active = false;
                    stage2Active = true;
                    WinMessageBoxActive = false;
                    player1_Health = 100;
                    player2_Health = 100;
                    Player1CurrentState = playerState.STANDING;
                    Player2CurrentState = playerState.STANDING;
                }
                if (stage2Active && (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed) || (GamePad.GetState(PlayerIndex.Two).Buttons.Start == ButtonState.Pressed)
                    && (Player1CurrentState == playerState.DEAD || Player2CurrentState == playerState.DEAD))
                {
                    RoundNumCount += 1;
                    if (WindowIsFullScreen == true)
                    {
                        DestinationRectangle1.X = 1100;
                        DestinationRectangle2.X = 100;
                    }
                    else
                    {
                        DestinationRectangle1.X = 450;
                        DestinationRectangle2.X = 150;
                    }
                    stage2Active = false;
                    stage3Active = true;
                    WinMessageBoxActive = false;
                    player1_Health = 100;
                    player2_Health = 100;
                    Player1CurrentState = playerState.STANDING;
                    Player2CurrentState = playerState.STANDING;
                }
                if (stage3Active && (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed) || (GamePad.GetState(PlayerIndex.Two).Buttons.Start == ButtonState.Pressed)
                    && (Player1CurrentState == playerState.DEAD || Player2CurrentState == playerState.DEAD))
                {
                    if (WindowIsFullScreen == true)
                    {
                        DestinationRectangle1.X = 1100;
                        DestinationRectangle2.X = 100;
                    }
                    else
                    {
                        DestinationRectangle1.X = 450;
                        DestinationRectangle2.X = 150;
                    }
                    stage3Active = false;
                    EndScreenActive = true;
                    WinMessageBoxActive = false;
                    player1_Health = 100;
                    player2_Health = 100;
                    Player1CurrentState = playerState.STANDING;
                    Player2CurrentState = playerState.STANDING;
                }
                #endregion
                #region Keys for switching stages !XBOX
#if!XBOX
                if (stage1Active && Keyboard.GetState().IsKeyDown(Keys.Space) && (Player1CurrentState == playerState.DEAD || Player2CurrentState == playerState.DEAD))
                {
                    RoundNumCount += 1;
                    if (WindowIsFullScreen == true)
                    {
                        DestinationRectangle1.X = 1100;
                        DestinationRectangle2.X = 100;
                    }
                    else
                    {
                        DestinationRectangle1.X = 450;
                        DestinationRectangle2.X = 150;
                    }
                    stage1Active = false;
                    stage2Active = true;
                    WinMessageBoxActive = false;
                    player1_Health = 100;
                    player2_Health = 100;
                    Player1CurrentState = playerState.STANDING;
                    Player2CurrentState = playerState.STANDING;
                }
                if (stage2Active && Keyboard.GetState().IsKeyDown(Keys.Space) && (Player1CurrentState == playerState.DEAD || Player2CurrentState == playerState.DEAD))
                {
                    RoundNumCount += 1;
                    if (WindowIsFullScreen == true)
                    {
                        DestinationRectangle1.X = 1100;
                        DestinationRectangle2.X = 100;
                    }
                    else
                    {
                        DestinationRectangle1.X = 450;
                        DestinationRectangle2.X = 150;
                    }
                    stage2Active = false;
                    stage3Active = true;
                    WinMessageBoxActive = false;
                    player1_Health = 100;
                    player2_Health = 100;
                    Player1CurrentState = playerState.STANDING;
                    Player2CurrentState = playerState.STANDING;
                }
                if (stage3Active && Keyboard.GetState().IsKeyDown(Keys.Space) && (Player1CurrentState == playerState.DEAD || Player2CurrentState == playerState.DEAD))
                {
                    if (WindowIsFullScreen == true)
                    {
                        DestinationRectangle1.X = 1100;
                        DestinationRectangle2.X = 100;
                    }
                    else
                    {
                        DestinationRectangle1.X = 450;
                        DestinationRectangle2.X = 150;
                    }
                    stage3Active = false;
                    EndScreenActive = true;
                    WinMessageBoxActive = false;
                    player1_Health = 100;
                    player2_Health = 100;
                    Player1CurrentState = playerState.STANDING;
                    Player2CurrentState = playerState.STANDING;
                }
#endif
                #endregion
                if (stage2PreviousActive != stage2Active && !stage3Active && !EndScreenActive && !StartScreenActive && !stage1Active)
                {
                    Round2.Play();
                    stage2PreviousActive = stage2Active;
                }

                if (stage3PreviousActive != stage3Active && !EndScreenActive && !StartScreenActive && !stage1Active)
                {
                    Round3.Play();
                    stage3PreviousActive = stage3Active;
                }

                #region keys for player 1 and player 2 for XBOX
                #region keys for player1
                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0 &&
                    Player1CurrentState != playerState.JUMPING &&
                    Player1CurrentState != playerState.PUNCHING &&
                    Player1CurrentState != playerState.FIRING &&
                    Player1CurrentState != playerState.HURT &&
                    Player1CurrentState != playerState.DEAD)
                {
                    if (!((DistanceBetweenPlayersX < DistanceBetweenPlayersXLimit) && (Math.Abs(DestinationRectangle1.X - DestinationRectangle2.X - Bowser.walking.speed) < DistanceBetweenPlayersX)))
                        DestinationRectangle1.X -= Bowser.walking.speed;   //WALKING to the left
                    Player1CurrentState = playerState.WALKING;
                }
                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0 &&
                    Player1CurrentState != playerState.JUMPING &&
                    Player1CurrentState != playerState.PUNCHING &&
                    Player1CurrentState != playerState.FIRING &&
                    Player1CurrentState != playerState.HURT &&
                    Player1CurrentState != playerState.DEAD)
                {
                    if (!((DistanceBetweenPlayersX < DistanceBetweenPlayersXLimit) && (Math.Abs(DestinationRectangle1.X - DestinationRectangle2.X + Bowser.walking.speed) < DistanceBetweenPlayersX)))
                        DestinationRectangle1.X += Bowser.walking.speed;     //WALKING to the right
                    Player1CurrentState = playerState.WALKING;
                }
                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0 &&
                    Player1CurrentState != playerState.PUNCHING &&
                    Player1CurrentState != playerState.FIRING &&
                    Player1CurrentState != playerState.HURT &&
                    Player1CurrentState != playerState.DEAD)
                {
                    if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0)
                        player1_jumpingRight = true;
                    if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0)
                        player1_jumpingLeft = true;
                    Player1CurrentState = playerState.JUMPING;
                }
                if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed &&
                    Player1CurrentState != playerState.JUMPING &&
                    Player1CurrentState != playerState.FIRING &&
                    Player1CurrentState != playerState.HURT &&
                    Player1CurrentState != playerState.DEAD &&
                    Player1PreviousState != playerState.PUNCHING)
                {
                    Player1CurrentState = playerState.PUNCHING;
                }
                if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed &&
                    Player1CurrentState != playerState.JUMPING &&
                    Player1CurrentState != playerState.PUNCHING &&
                    Player1CurrentState != playerState.HURT &&
                    Player1CurrentState != playerState.DEAD &&
                    Player1PreviousState != playerState.FIRING &&
                    !FireBall1_CurrentState_Active)
                {
                    Player1CurrentState = playerState.FIRING;
                }
                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X == 0 &&
                    Player1CurrentState != playerState.JUMPING &&
                    Player1CurrentState != playerState.PUNCHING &&
                    Player1CurrentState != playerState.FIRING &&
                    Player1CurrentState != playerState.HURT &&
                    Player1CurrentState != playerState.DEAD)
                {
                    Player1CurrentState = playerState.STANDING;
                }
                #endregion
                #region keys for player2
                if (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.X < 0 &&
                    Player2CurrentState != playerState.JUMPING &&
                    Player2CurrentState != playerState.PUNCHING &&
                    Player2CurrentState != playerState.FIRING &&
                    Player2CurrentState != playerState.HURT &&
                    Player2CurrentState != playerState.DEAD)
                {
                    if (!((DistanceBetweenPlayersX < DistanceBetweenPlayersXLimit) && (Math.Abs(DestinationRectangle1.X - DestinationRectangle2.X + Bowser.walking.speed) < DistanceBetweenPlayersX)))
                        DestinationRectangle2.X -= Bowser.walking.speed;   //WALKING to the left
                    Player2CurrentState = playerState.WALKING;
                }
                if (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.X > 0 &&
                    Player2CurrentState != playerState.JUMPING &&
                    Player2CurrentState != playerState.PUNCHING &&
                    Player2CurrentState != playerState.FIRING &&
                    Player2CurrentState != playerState.HURT &&
                    Player2CurrentState != playerState.DEAD)
                {
                    if (!((DistanceBetweenPlayersX < DistanceBetweenPlayersXLimit) && (Math.Abs(DestinationRectangle1.X - DestinationRectangle2.X - Bowser.walking.speed) < DistanceBetweenPlayersX)))
                        DestinationRectangle2.X += Bowser.walking.speed;     //WALKING to the right
                    Player2CurrentState = playerState.WALKING;
                }
                if (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.Y > 0 &&
                    Player2CurrentState != playerState.PUNCHING &&
                    Player2CurrentState != playerState.FIRING &&
                    Player2CurrentState != playerState.HURT &&
                    Player2CurrentState != playerState.DEAD)
                {
                    if (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.X > 0)
                        player2_jumpingRight = true;
                    if (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.X < 0)
                        player2_jumpingLeft = true;
                    Player2CurrentState = playerState.JUMPING;
                }
                if (GamePad.GetState(PlayerIndex.Two).Buttons.A == ButtonState.Pressed &&
                    Player2CurrentState != playerState.JUMPING &&
                    Player2CurrentState != playerState.FIRING &&
                    Player2CurrentState != playerState.HURT &&
                    Player2CurrentState != playerState.DEAD &&
                    Player2PreviousState != playerState.PUNCHING)
                {
                    Player2CurrentState = playerState.PUNCHING;
                }
                if (GamePad.GetState(PlayerIndex.Two).Buttons.B == ButtonState.Pressed &&
                    Player2CurrentState != playerState.JUMPING &&
                    Player2CurrentState != playerState.PUNCHING &&
                    Player2CurrentState != playerState.HURT &&
                    Player2CurrentState != playerState.DEAD &&
                    Player2PreviousState != playerState.FIRING &&
                    !FireBall2_CurrentState_Active)
                {
                    Player2CurrentState = playerState.FIRING;
                }
                if (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.X == 0 &&
                    Player2CurrentState != playerState.JUMPING &&
                    Player2CurrentState != playerState.PUNCHING &&
                    Player2CurrentState != playerState.FIRING &&
                    Player2CurrentState != playerState.HURT &&
                    Player2CurrentState != playerState.DEAD)
                {
                    Player2CurrentState = playerState.STANDING;
                }
                #endregion
                #endregion
                #region keys for player 1 and player 2 for !XBOX
#if!XBOX
                #region keys for player1
                if (Keyboard.GetState().IsKeyDown(Keys.Left) &&
                    Player1CurrentState != playerState.JUMPING &&
                    Player1CurrentState != playerState.PUNCHING &&
                    Player1CurrentState != playerState.FIRING &&
                    Player1CurrentState != playerState.HURT &&
                    Player1CurrentState != playerState.DEAD)
                {
                    if (!((DistanceBetweenPlayersX < DistanceBetweenPlayersXLimit) && (Math.Abs(DestinationRectangle1.X - DestinationRectangle2.X - Bowser.walking.speed) < DistanceBetweenPlayersX)))
                        DestinationRectangle1.X -= Bowser.walking.speed;   //WALKING to the left
                    Player1CurrentState = playerState.WALKING;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right) &&
                    Player1CurrentState != playerState.JUMPING &&
                    Player1CurrentState != playerState.PUNCHING &&
                    Player1CurrentState != playerState.FIRING &&
                    Player1CurrentState != playerState.HURT &&
                    Player1CurrentState != playerState.DEAD)
                {
                    if (!((DistanceBetweenPlayersX < DistanceBetweenPlayersXLimit) && (Math.Abs(DestinationRectangle1.X - DestinationRectangle2.X + Bowser.walking.speed) < DistanceBetweenPlayersX)))
                        DestinationRectangle1.X += Bowser.walking.speed;     //WALKING to the right
                    Player1CurrentState = playerState.WALKING;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up) &&
                    Player1CurrentState != playerState.PUNCHING &&
                    Player1CurrentState != playerState.FIRING &&
                    Player1CurrentState != playerState.HURT &&
                    Player1CurrentState != playerState.DEAD)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Right))
                        player1_jumpingRight = true;
                    if (Keyboard.GetState().IsKeyDown(Keys.Left))
                        player1_jumpingLeft = true;
                    Player1CurrentState = playerState.JUMPING;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.OemPeriod) &&
                    Player1CurrentState != playerState.JUMPING &&
                    Player1CurrentState != playerState.FIRING &&
                    Player1CurrentState != playerState.HURT &&
                    Player1CurrentState != playerState.DEAD &&
                    Player1PreviousState != playerState.PUNCHING)
                {
                    Player1CurrentState = playerState.PUNCHING;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.OemComma) &&
                    Player1CurrentState != playerState.JUMPING &&
                    Player1CurrentState != playerState.PUNCHING &&
                    Player1CurrentState != playerState.HURT &&
                    Player1CurrentState != playerState.DEAD &&
                    Player1PreviousState != playerState.FIRING &&
                    !FireBall1_CurrentState_Active)
                {
                    Player1CurrentState = playerState.FIRING;
                }
                if (!(Keyboard.GetState().IsKeyDown(Keys.Left)) && !(Keyboard.GetState().IsKeyDown(Keys.Right)) &&
                    Player1CurrentState != playerState.JUMPING &&
                    Player1CurrentState != playerState.PUNCHING &&
                    Player1CurrentState != playerState.FIRING &&
                    Player1CurrentState != playerState.HURT &&
                    Player1CurrentState != playerState.DEAD)
                {
                    Player1CurrentState = playerState.STANDING;
                }
                #endregion
                #region keys for player2
                if (Keyboard.GetState().IsKeyDown(Keys.A) &&
                    Player2CurrentState != playerState.JUMPING &&
                    Player2CurrentState != playerState.PUNCHING &&
                    Player2CurrentState != playerState.FIRING &&
                    Player2CurrentState != playerState.HURT &&
                    Player2CurrentState != playerState.DEAD)
                {
                    if (!((DistanceBetweenPlayersX < DistanceBetweenPlayersXLimit) && (Math.Abs(DestinationRectangle1.X - DestinationRectangle2.X + Bowser.walking.speed) < DistanceBetweenPlayersX)))
                        DestinationRectangle2.X -= Bowser.walking.speed;   //WALKING to the left
                    Player2CurrentState = playerState.WALKING;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D) &&
                    Player2CurrentState != playerState.JUMPING &&
                    Player2CurrentState != playerState.PUNCHING &&
                    Player2CurrentState != playerState.FIRING &&
                    Player2CurrentState != playerState.HURT &&
                    Player2CurrentState != playerState.DEAD)
                {
                    if (!((DistanceBetweenPlayersX < DistanceBetweenPlayersXLimit) && (Math.Abs(DestinationRectangle1.X - DestinationRectangle2.X - Bowser.walking.speed) < DistanceBetweenPlayersX)))
                        DestinationRectangle2.X += Bowser.walking.speed;     //WALKING to the right
                    Player2CurrentState = playerState.WALKING;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.W) &&
                    Player2CurrentState != playerState.PUNCHING &&
                    Player2CurrentState != playerState.FIRING &&
                    Player2CurrentState != playerState.HURT &&
                    Player2CurrentState != playerState.DEAD)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.D))
                        player2_jumpingRight = true;
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                        player2_jumpingLeft = true;
                    Player2CurrentState = playerState.JUMPING;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.E) &&
                    Player2CurrentState != playerState.JUMPING &&
                    Player2CurrentState != playerState.FIRING &&
                    Player2CurrentState != playerState.HURT &&
                    Player2CurrentState != playerState.DEAD &&
                    Player2PreviousState != playerState.PUNCHING)
                {
                    Player2CurrentState = playerState.PUNCHING;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Q) &&
                    Player2CurrentState != playerState.JUMPING &&
                    Player2CurrentState != playerState.PUNCHING &&
                    Player2CurrentState != playerState.HURT &&
                    Player2CurrentState != playerState.DEAD &&
                    Player2PreviousState != playerState.FIRING &&
                    !FireBall2_CurrentState_Active)
                {
                    Player2CurrentState = playerState.FIRING;
                }
                if (!(Keyboard.GetState().IsKeyDown(Keys.A)) && !(Keyboard.GetState().IsKeyDown(Keys.D)) &&
                    Player2CurrentState != playerState.JUMPING &&
                    Player2CurrentState != playerState.PUNCHING &&
                    Player2CurrentState != playerState.FIRING &&
                    Player2CurrentState != playerState.HURT &&
                    Player2CurrentState != playerState.DEAD)
                {
                    Player2CurrentState = playerState.STANDING;
                }
                #endregion
#endif
                #endregion

                #region changing (frames, delay, speed, health) for player 1 after elapse time
                switch (Player1CurrentState)
                {
                    #region case: STANDING
                    case playerState.STANDING:
                        if (Player1CurrentState != Player1PreviousState)
                        {
                            frames1 = 0;
                            elapsed1 = 0;
                            if (Player1PreviousState == playerState.PUNCHING && flip1)
                                DestinationRectangle1.X += 40;
                            Player1PreviousState = Player1CurrentState;
                        }
                        elapsed1 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (elapsed1 >= Bowser.standing.delay)
                        {
                            if (frames1 >= (Bowser.standing.numOfFrames - 1))
                                frames1 = 0;
                            else
                                frames1++;
                            elapsed1 = 0;
                        }
                        break;
                    #endregion
                    #region case: WALKING
                    case playerState.WALKING:
                        if (Player1CurrentState != Player1PreviousState)
                        {
                            frames1 = 0;
                            elapsed1 = 0;
                            Player1PreviousState = Player1CurrentState;
                        }
                        elapsed1 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (elapsed1 >= Bowser.walking.delay)
                        {
                            if (frames1 >= (Bowser.walking.numOfFrames - 1))
                                frames1 = 0;
                            else
                                frames1++;
                            elapsed1 = 0;
                        }
                        break;
                    #endregion
                    #region case: RUNNING
                    case playerState.RUNNING:
                        if (Player1CurrentState != Player1PreviousState)
                        {
                            frames1 = 0;
                            elapsed1 = 0;
                            Player1PreviousState = Player1CurrentState;
                        }
                        elapsed1 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (elapsed1 >= Bowser.running.delay)
                        {
                            if (frames1 >= (Bowser.running.numOfFrames - 1))
                                frames1 = 0;
                            else
                                frames1++;
                            elapsed1 = 0;
                        }
                        break;
                    #endregion
                    #region case: JUMPING
                    case playerState.JUMPING:
                        if (Player1CurrentState != Player1PreviousState)
                        {
                            frames1 = 0;
                            elapsed1 = 0;
                            Player1PreviousState = Player1CurrentState;
                        }
                        elapsed1 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (elapsed1 >= Bowser.jumping.delay)
                        {
                            if (frames1 != Bowser.jumping.numOfFrames - 1)
                            {
                                frames1++;
                                elapsed1 = 0;
                            }
                        }
                        if ((frames1 == Bowser.jumping.numOfFrames - 1) && (elapsed1 >= Bowser.jumping.delay))
                        {
                            frames1 = 0;
                            player1_jumpingRight = false;
                            player1_jumpingLeft = false;
                            Player1CurrentState = playerState.STANDING;
                        }
                        if (frames1 <= 5)
                            DestinationRectangle1.Y -= 8;
                        else if (frames1 == 6)
                            DestinationRectangle1.Y += 10;
                        else if (frames1 == 7)
                            DestinationRectangle1.Y += 10;
                        else if (frames1 == 8)
                            DestinationRectangle1.Y += 11;
                        else if (frames1 == 9)
                            DestinationRectangle1.Y += 12;
                        else if (frames1 == 10)
                        {
                            DestinationRectangle1.Y = playersStandingOnYCoordinate;
                        }
                        if (player1_jumpingRight)
                        {
                            DestinationRectangle1.X += 7;
                        }
                        if (player1_jumpingLeft)
                        {
                            DestinationRectangle1.X -= 7;
                        }
                        break;
                    #endregion
                    #region case: BLOCKING
                    case playerState.BLOCKING:
                        if (Player1CurrentState != Player1PreviousState)
                        {
                            frames1 = 0;
                            elapsed1 = 0;
                            Player1PreviousState = Player1CurrentState;
                        }
                        elapsed1 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (elapsed1 >= Bowser.blocking.delay)
                        {
                            if (frames1 != (Bowser.blocking.numOfFrames - 1))
                                frames1++;
                            elapsed1 = 0;
                        }
                        break;
                    #endregion
                    #region case: PUNCHING
                    case playerState.PUNCHING:
                        if (Player1CurrentState != Player1PreviousState)
                        {
                            punchSwoosh.Play();
                            frames1 = 0;
                            elapsed1 = 0;
                            player1X_ForPunching = DestinationRectangle1.X;
                            Player1PreviousState = Player1CurrentState;
                            if (DistanceBetweenPlayersX <= 160 && DistanceBetweenPlayersY <= 50 && Player2CurrentState != playerState.DEAD)
                            {
                                punch.Play();
                                player2_Health -= 10;
                                if (player2_Health <= 0)
                                    Player2CurrentState = playerState.DEAD;
                                if (player2_Health > 0)
                                    Player2CurrentState = playerState.HURT;
                            }
                        }
                        elapsed1 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (elapsed1 >= Bowser.punching.delay)
                        {
                            if (frames1 != Bowser.punching.numOfFrames - 1)
                            {
                                frames1++;
                                elapsed1 = 0;
                            }
                        }
                        if ((frames1 == Bowser.punching.numOfFrames - 1) && (elapsed1 >= Bowser.punching.delay + 200f))
                        {
                            frames1 = 0;
                            Player1CurrentState = playerState.STANDING;
                        }
                        break;
                    #endregion
                    #region case: FIRING
                    case playerState.FIRING:
                        if (Player1CurrentState != Player1PreviousState)
                        {
                            fireBall.Play();
                            FireBall1_CurrentState_Active = true;
                            frames1 = 0;
                            elapsed1 = 0;
                            Player1PreviousState = Player1CurrentState;
                        }
                        elapsed1 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (elapsed1 >= Bowser.firing.delay)
                        {
                            if (frames1 != Bowser.firing.numOfFrames - 1)
                            {
                                frames1++;
                                elapsed1 = 0;
                            }
                        }
                        if ((frames1 == Bowser.firing.numOfFrames - 1) && (elapsed1 >= Bowser.firing.delay + 200f))
                        {
                            frames1 = 0;
                            Player1CurrentState = playerState.STANDING;
                        }
                        break;
                    #endregion
                    #region case: HURT
                    case playerState.HURT:
                        if (Player1CurrentState != Player1PreviousState)
                        {
                            frames1 = 0;
                            elapsed1 = 0;
                            Player1PreviousState = Player1CurrentState;
                        }
                        elapsed1 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (elapsed1 >= Bowser.hurt.delay)
                        {
                            if (frames1 != Bowser.hurt.numOfFrames - 1)
                            {
                                if (!flip1)
                                    DestinationRectangle1.X -= 30;
                                else
                                    DestinationRectangle1.X += 30;
                                frames1++;
                                elapsed1 = 0;
                            }
                        }
                        if ((frames1 == Bowser.hurt.numOfFrames - 1) && (elapsed1 >= Bowser.hurt.delay + 200f))
                        {
                            frames1 = 0;
                            Player1CurrentState = playerState.STANDING;
                        }
                        break;
                    #endregion
                    #region case: DEAD
                    case playerState.DEAD:
                        if (Player1CurrentState != Player1PreviousState)
                        {
                            frames1 = 0;
                            elapsed1 = 0;
                            if (stage1Active)
                                WinMessageBoxCount = 1;
                            if (stage2Active)
                                WinMessageBoxCount = 3;
                            if (stage3Active)
                            {
                                if (player2_WinCount >= 1)
                                    WinMessageBoxCount = 5;
                                else
                                    WinMessageBoxCount = 4;
                            }
                            player2_WinCount += 1;
                            WinMessageBoxActive = true;
                            Player1PreviousState = Player1CurrentState;
                        }
                        elapsed1 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (elapsed1 >= Bowser.dead.delay)
                        {
                            if (frames1 < (Bowser.dead.numOfFrames - 1))
                            {
                                if (!flip1)
                                    DestinationRectangle1.X -= 50;
                                if (flip1)
                                    DestinationRectangle1.X += 50;
                                frames1++;
                            }
                            elapsed1 = 0;
                        }
                        break;
                    #endregion
                    default:
                        break;
                }
                #endregion
                #region changing (frames, delay, speed, health) for player 2 after elapse time
                switch (Player2CurrentState)
                {
                    #region case: STANDING
                    case playerState.STANDING:
                        if (Player2CurrentState != Player2PreviousState)
                        {
                            frames2 = 0;
                            elapsed2 = 0;
                            if (Player2PreviousState == playerState.PUNCHING && flip2)
                                DestinationRectangle2.X += 40;
                            Player2PreviousState = Player2CurrentState;
                        }
                        elapsed2 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (elapsed2 >= Bowser.standing.delay)
                        {
                            if (frames2 >= (Bowser.standing.numOfFrames - 1))
                                frames2 = 0;
                            else
                                frames2++;
                            elapsed2 = 0;
                        }
                        break;
                    #endregion
                    #region case: WALKING
                    case playerState.WALKING:
                        if (Player2CurrentState != Player2PreviousState)
                        {
                            frames2 = 0;
                            elapsed2 = 0;
                            Player2PreviousState = Player2CurrentState;
                        }
                        elapsed2 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (elapsed2 >= Bowser.walking.delay)
                        {
                            if (frames2 >= (Bowser.walking.numOfFrames - 1))
                                frames2 = 0;
                            else
                                frames2++;
                            elapsed2 = 0;
                        }
                        break;
                    #endregion
                    #region case: RUNNING
                    case playerState.RUNNING:
                        if (Player2CurrentState != Player2PreviousState)
                        {
                            frames2 = 0;
                            elapsed2 = 0;
                            Player2PreviousState = Player2CurrentState;
                        }
                        elapsed2 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (elapsed2 >= Bowser.running.delay)
                        {
                            if (frames2 >= (Bowser.running.numOfFrames - 1))
                                frames2 = 0;
                            else
                                frames2++;
                            elapsed2 = 0;
                        }
                        break;
                    #endregion
                    #region case: JUMPING
                    case playerState.JUMPING:
                        if (Player2CurrentState != Player2PreviousState)
                        {
                            frames2 = 0;
                            elapsed2 = 0;
                            Player2PreviousState = Player2CurrentState;
                        }
                        elapsed2 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (elapsed2 >= Bowser.jumping.delay)
                        {
                            if (frames2 != Bowser.jumping.numOfFrames - 1)
                            {
                                frames2++;
                                elapsed2 = 0;
                            }
                        }
                        if ((frames2 == Bowser.jumping.numOfFrames - 1) && (elapsed2 >= Bowser.jumping.delay))
                        {
                            frames2 = 0;
                            player2_jumpingRight = false;
                            player2_jumpingLeft = false;
                            Player2CurrentState = playerState.STANDING;
                        }
                        if (frames2 <= 5)
                            DestinationRectangle2.Y -= 8;
                        else if (frames2 == 6)
                            DestinationRectangle2.Y += 10;
                        else if (frames2 == 7)
                            DestinationRectangle2.Y += 10;
                        else if (frames2 == 8)
                            DestinationRectangle2.Y += 11;
                        else if (frames2 == 9)
                            DestinationRectangle2.Y += 12;
                        else if (frames2 == 10)
                        {
                            DestinationRectangle2.Y = playersStandingOnYCoordinate;
                        }
                        if (player2_jumpingRight)
                        {
                            DestinationRectangle2.X += 7;
                        }
                        if (player2_jumpingLeft)
                        {
                            DestinationRectangle2.X -= 7;
                        }
                        break;
                    #endregion
                    #region case: BLOCKING
                    case playerState.BLOCKING:
                        if (Player2CurrentState != Player2PreviousState)
                        {
                            frames2 = 0;
                            elapsed2 = 0;
                            Player2PreviousState = Player2CurrentState;
                        }
                        elapsed2 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (elapsed2 >= Bowser.blocking.delay)
                        {
                            if (frames2 != (Bowser.blocking.numOfFrames - 1))
                                frames2++;
                            elapsed2 = 0;
                        }
                        break;
                    #endregion
                    #region case: PUNCHING
                    case playerState.PUNCHING:
                        if (Player2CurrentState != Player2PreviousState)
                        {
                            punchSwoosh.Play();
                            frames2 = 0;
                            elapsed2 = 0;
                            player2X_ForPunching = DestinationRectangle2.X;
                            if (DistanceBetweenPlayersX <= 160 && DistanceBetweenPlayersY <= 50 && Player1CurrentState != playerState.DEAD)
                            {
                                punch.Play();
                                player1_Health -= 10;
                                frames1 = 0;
                                if (player1_Health <= 0)
                                    Player1CurrentState = playerState.DEAD;
                                if (player1_Health > 0)
                                    Player1CurrentState = playerState.HURT;
                            }
                            Player2PreviousState = Player2CurrentState;
                        }
                        elapsed2 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (elapsed2 >= Bowser.punching.delay)
                        {
                            if (frames2 != Bowser.punching.numOfFrames - 1)
                            {
                                frames2++;
                                elapsed2 = 0;
                            }
                        }
                        if ((frames2 == Bowser.punching.numOfFrames - 1) && (elapsed2 >= Bowser.punching.delay + 200f))
                        {
                            frames2 = 0;
                            Player2CurrentState = playerState.STANDING;
                        }
                        break;
                    #endregion
                    #region case: FIRING
                    case playerState.FIRING:
                        if (Player2CurrentState != Player2PreviousState)
                        {
                            fireBall.Play();
                            FireBall2_CurrentState_Active = true;
                            frames2 = 0;
                            elapsed2 = 0;
                            Player2PreviousState = Player2CurrentState;
                        }
                        elapsed2 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (elapsed2 >= Bowser.firing.delay)
                        {
                            if (frames2 != Bowser.firing.numOfFrames - 1)
                            {
                                frames2++;
                                elapsed2 = 0;
                            }
                        }
                        if ((frames2 == Bowser.firing.numOfFrames - 1) && (elapsed2 >= Bowser.firing.delay + 200f))
                        {
                            frames2 = 0;
                            Player2CurrentState = playerState.STANDING;
                        }
                        break;
                    #endregion
                    #region case: HURT
                    case playerState.HURT:
                        if (Player2CurrentState != Player2PreviousState)
                        {
                            if (player2_Health <= 0)
                                Player2CurrentState = playerState.DEAD;
                            frames2 = 0;
                            elapsed2 = 0;
                            Player2PreviousState = Player2CurrentState;
                        }
                        elapsed2 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (elapsed2 >= Bowser.hurt.delay)
                        {
                            if (frames2 != Bowser.hurt.numOfFrames - 1)
                            {
                                if (!flip2)
                                    DestinationRectangle2.X -= 30;
                                else
                                    DestinationRectangle2.X += 30;
                                frames2++;
                                elapsed2 = 0;
                            }
                        }
                        if ((frames2 == Bowser.hurt.numOfFrames - 1) && (elapsed2 >= Bowser.hurt.delay + 200f))
                        {
                            frames2 = 0;
                            Player2CurrentState = playerState.STANDING;
                        }
                        break;
                    #endregion
                    #region case: DEAD
                    case playerState.DEAD:
                        if (Player2CurrentState != Player2PreviousState)
                        {
                            frames2 = 0;
                            elapsed2 = 0;
                            if (stage1Active)
                                WinMessageBoxCount = 0;
                            if (stage2Active)
                                WinMessageBoxCount = 2;
                            if (stage3Active)
                            {
                                if (player1_WinCount >= 1)
                                    WinMessageBoxCount = 4;
                                else
                                    WinMessageBoxCount = 5;
                            }
                            player1_WinCount += 1;
                            WinMessageBoxActive = true;
                            Player2PreviousState = Player2CurrentState;
                        }
                        elapsed2 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (elapsed2 >= Bowser.dead.delay)
                        {
                            if (frames2 < (Bowser.dead.numOfFrames - 1))
                            {
                                if (!flip2)
                                    DestinationRectangle2.X -= 50;
                                if (flip2)
                                    DestinationRectangle2.X += 50;
                                frames2++;
                            }
                            elapsed2 = 0;
                        }
                        break;
                    #endregion
                    default:
                        break;
                }
                #endregion
                #region changing FireBall 1 after elapse time
                if (FireBall1_CurrentState_Active == true)
                {
                    if (FireBall1_CurrentState_Active != FireBall1_PreviousState_Active)
                    {
                        frames1_Fireball = 0;
                        elapsed1_FireBall = 0;
                        if (!flip1)
                        {
                            DestinationRectangle1_FireBall.X = DestinationRectangle1.X + 50;
                            flip1_FireBall = false;
                        }
                        if (flip1)
                        {
                            DestinationRectangle1_FireBall.X = DestinationRectangle1.X - 50;
                            flip1_FireBall = true;
                        }
                        FireBall1_PreviousState_Active = FireBall1_CurrentState_Active;

                    }
                    elapsed1_FireBall += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (elapsed1_FireBall >= Bowser.fireBall.delay)
                    {
                        if (frames1_Fireball >= (Bowser.fireBall.numOfFrames - 1))
                            frames1_Fireball = 0;
                        else
                            frames1_Fireball++;
                        elapsed1_FireBall = 0;
                        if (!flip1_FireBall)
                            DestinationRectangle1_FireBall.X += 20;
                        if (flip1_FireBall)
                            DestinationRectangle1_FireBall.X -= 20;
                    }
                    if (DistanceBetweenPlayer2AndFireBall1X <= 60 && DistanceBetweenPlayer2AndFireBall1Y <= 80 && elapsed1_FireBall > 20)
                    {
                        FireBall1_PreviousState_Active = false;
                        FireBall1_CurrentState_Active = false;
                        if (Player2CurrentState != playerState.DEAD)
                        {
                            player2_Health -= 10;
                            frames2 = 0;
                        }
                        if (player2_Health <= 0)
                            Player2CurrentState = playerState.DEAD;
                        if (player2_Health > 0)
                            Player2CurrentState = playerState.HURT;
                    }
                    if (((DestinationRectangle1_FireBall.X + 100) > graphics.GraphicsDevice.Viewport.Width && !flip1_FireBall) || (DestinationRectangle1_FireBall.X < 10))
                    {
                        DestinationRectangle1_FireBall.X = DestinationRectangle1.X;
                        FireBall1_PreviousState_Active = false;
                        FireBall1_CurrentState_Active = false;
                    }
                }
                #endregion
                #region changing FireBall 2 after elapse time
                if (FireBall2_CurrentState_Active == true)
                {
                    if (FireBall2_CurrentState_Active != FireBall2_PreviousState_Active)
                    {
                        frames2_Fireball = 0;
                        elapsed2_FireBall = 0;
                        if (!flip2)
                        {
                            DestinationRectangle2_FireBall.X = DestinationRectangle2.X + 50;
                            flip2_FireBall = false;
                        }
                        if (flip2)
                        {
                            DestinationRectangle2_FireBall.X = DestinationRectangle2.X - 50;
                            flip2_FireBall = true;
                        }
                        FireBall2_PreviousState_Active = true;
                    }
                    elapsed2_FireBall += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (elapsed2_FireBall >= Bowser.fireBall.delay)
                    {
                        if (frames2_Fireball >= (Bowser.fireBall.numOfFrames - 1))
                            frames2_Fireball = 0;
                        else
                            frames2_Fireball++;
                        elapsed2_FireBall = 0;
                        if (!flip2_FireBall)
                            DestinationRectangle2_FireBall.X += 20;
                        if (flip2_FireBall)
                            DestinationRectangle2_FireBall.X -= 20;
                    }
                    if (DistanceBetweenPlayer1AndFireBall2X <= 60 && DistanceBetweenPlayer1AndFireBall2Y <= 80 && elapsed2_FireBall > 20)
                    {
                        FireBall2_PreviousState_Active = false;
                        FireBall2_CurrentState_Active = false;
                        if (Player1CurrentState != playerState.DEAD)
                        {
                            player1_Health -= 10;
                            frames1 = 0;
                        }
                        if (player1_Health <= 0)
                            Player1CurrentState = playerState.DEAD;
                        if (player1_Health > 0)
                            Player1CurrentState = playerState.HURT;
                    }
                    if (((DestinationRectangle2_FireBall.X + 100) > graphics.GraphicsDevice.Viewport.Width && !flip2_FireBall) || (DestinationRectangle2_FireBall.X < 10))
                    {
                        DestinationRectangle2_FireBall.X = DestinationRectangle2.X;
                        FireBall2_PreviousState_Active = false;
                        FireBall2_CurrentState_Active = false;
                    }
                }
                #endregion

                #region defining when flip1 / flip2 is true
                if (DestinationRectangle1.X > DestinationRectangle2.X)
                {
                    if (player1_Health > 0)
                        flip1 = true;
                    if (player2_Health > 0)
                        flip2 = false;
                }
                else
                {
                    if (player1_Health > 0)
                        flip1 = false;
                    if (player2_Health > 0)
                        flip2 = true;
                }
                #endregion

                #region SourceRectangle and DestinationRectangle for player 1
                switch (Player1CurrentState)
                {
                    case playerState.STANDING:
                        SourceRectangle1 = new Rectangle(Bowser.standing.X[frames1], Bowser.standing.Y[frames1],
                            Bowser.standing.Width[frames1], Bowser.standing.Height[frames1]);
                        DestinationRectangle1 = new Rectangle(DestinationRectangle1.X, playersStandingOnYCoordinate + (Bowser.standing.Height[0] - Bowser.standing.Height[frames1]),
                            Bowser.standing.Width[frames1] * 2, Bowser.standing.Height[frames1] * 2);
                        break;
                    case playerState.WALKING:
                        SourceRectangle1 = new Rectangle(Bowser.walking.X[frames1], Bowser.walking.Y[frames1],
                            Bowser.walking.Width[frames1], Bowser.walking.Height[frames1]);
                        DestinationRectangle1 = new Rectangle(DestinationRectangle1.X, playersStandingOnYCoordinate + (Bowser.standing.Height[0] - Bowser.walking.Height[frames1]),
                            Bowser.walking.Width[frames1] * 2, Bowser.walking.Height[frames1] * 2);
                        break;
                    case playerState.RUNNING:
                        SourceRectangle1 = new Rectangle(Bowser.running.X[frames1], Bowser.running.Y[frames1],
                            Bowser.running.Width[frames1], Bowser.running.Height[frames1]);
                        DestinationRectangle1 = new Rectangle(DestinationRectangle1.X, playersStandingOnYCoordinate + (Bowser.standing.Height[0] - Bowser.running.Height[frames1]),
                            Bowser.running.Width[frames1] * 2, Bowser.running.Height[frames1] * 2);
                        break;
                    case playerState.JUMPING:
                        SourceRectangle1 = new Rectangle(Bowser.jumping.X[frames1], Bowser.jumping.Y[frames1],
                            Bowser.jumping.Width[frames1], Bowser.jumping.Height[frames1]);
                        DestinationRectangle1 = new Rectangle(DestinationRectangle1.X, DestinationRectangle1.Y,
                            Bowser.jumping.Width[frames1] * 2, Bowser.jumping.Height[frames1] * 2);
                        break;
                    case playerState.BLOCKING:
                        SourceRectangle1 = new Rectangle(Bowser.blocking.X[frames1], Bowser.blocking.Y[frames1],
                            Bowser.blocking.Width[frames1], Bowser.blocking.Height[frames1]);
                        DestinationRectangle1 = new Rectangle(DestinationRectangle1.X, playersStandingOnYCoordinate + (Bowser.standing.Height[0] - Bowser.blocking.Height[frames1]),
                            Bowser.blocking.Width[frames1] * 2, Bowser.blocking.Height[frames1] * 2);
                        break;
                    case playerState.PUNCHING:
                        SourceRectangle1 = new Rectangle(Bowser.punching.X[frames1], Bowser.punching.Y[frames1],
                            Bowser.punching.Width[frames1], Bowser.punching.Height[frames1]);
                        if (!flip1)
                            DestinationRectangle1 = new Rectangle(DestinationRectangle1.X, playersStandingOnYCoordinate + (Bowser.standing.Height[0] - Bowser.punching.Height[frames1]),
                                Bowser.punching.Width[frames1] * 2, Bowser.punching.Height[frames1] * 2);
                        else
                            DestinationRectangle1 = new Rectangle(player1X_ForPunching - Bowser.punching.Width[frames1] + Bowser.standing.Width[0] - 15, playersStandingOnYCoordinate + (Bowser.standing.Height[0] - Bowser.punching.Height[frames1]),
                                Bowser.punching.Width[frames1] * 2, Bowser.punching.Height[frames1] * 2);
                        break;
                    case playerState.FIRING:
                        SourceRectangle1 = new Rectangle(Bowser.firing.X[frames1], Bowser.firing.Y[frames1],
                            Bowser.firing.Width[frames1], Bowser.firing.Height[frames1]);
                        DestinationRectangle1 = new Rectangle(DestinationRectangle1.X, playersStandingOnYCoordinate + (Bowser.standing.Height[0] - Bowser.firing.Height[frames1]),
                            Bowser.firing.Width[frames1] * 2, Bowser.firing.Height[frames1] * 2);
                        break;
                    case playerState.HURT:
                        SourceRectangle1 = new Rectangle(Bowser.hurt.X[frames1], Bowser.hurt.Y[frames1],
                            Bowser.hurt.Width[frames1], Bowser.hurt.Height[frames1]);
                        DestinationRectangle1 = new Rectangle(DestinationRectangle1.X, playersStandingOnYCoordinate + (Bowser.standing.Height[0] - Bowser.hurt.Height[frames1]),
                            Bowser.hurt.Width[frames1] * 2, Bowser.hurt.Height[frames1] * 2);
                        break;
                    case playerState.DEAD:
                        SourceRectangle1 = new Rectangle(Bowser.dead.X[frames1], Bowser.dead.Y[frames1],
                            Bowser.dead.Width[frames1], Bowser.dead.Height[frames1]);
                        DestinationRectangle1 = new Rectangle(DestinationRectangle1.X, playersStandingOnYCoordinate + (Bowser.standing.Height[0] - Bowser.dead.Height[frames1]),
                            Bowser.dead.Width[frames1] * 2, Bowser.dead.Height[frames1] * 2);
                        break;
                    default:
                        break;
                }
                #endregion
                #region SourceRectangle and DestinationRectangle for player 2
                switch (Player2CurrentState)
                {
                    case playerState.STANDING:
                        SourceRectangle2 = new Rectangle(Bowser.standing.X[frames2], Bowser.standing.Y[frames2],
                            Bowser.standing.Width[frames2], Bowser.standing.Height[frames2]);
                        DestinationRectangle2 = new Rectangle(DestinationRectangle2.X, playersStandingOnYCoordinate + (Bowser.standing.Height[0] - Bowser.standing.Height[frames2]),
                            Bowser.standing.Width[frames2] * 2, Bowser.standing.Height[frames2] * 2);
                        break;
                    case playerState.WALKING:
                        SourceRectangle2 = new Rectangle(Bowser.walking.X[frames2], Bowser.walking.Y[frames2],
                            Bowser.walking.Width[frames2], Bowser.walking.Height[frames2]);
                        DestinationRectangle2 = new Rectangle(DestinationRectangle2.X, playersStandingOnYCoordinate + (Bowser.standing.Height[0] - Bowser.walking.Height[frames2]),
                            Bowser.walking.Width[frames2] * 2, Bowser.walking.Height[frames2] * 2);
                        break;
                    case playerState.RUNNING:
                        SourceRectangle2 = new Rectangle(Bowser.running.X[frames2], Bowser.running.Y[frames2],
                            Bowser.running.Width[frames2], Bowser.running.Height[frames2]);
                        DestinationRectangle2 = new Rectangle(DestinationRectangle2.X, playersStandingOnYCoordinate + (Bowser.standing.Height[0] - Bowser.running.Height[frames2]),
                            Bowser.running.Width[frames2] * 2, Bowser.running.Height[frames2] * 2);
                        break;
                    case playerState.JUMPING:
                        SourceRectangle2 = new Rectangle(Bowser.jumping.X[frames2], Bowser.jumping.Y[frames2],
                            Bowser.jumping.Width[frames2], Bowser.jumping.Height[frames2]);
                        DestinationRectangle2 = new Rectangle(DestinationRectangle2.X, DestinationRectangle2.Y,
                            Bowser.jumping.Width[frames2] * 2, Bowser.jumping.Height[frames2] * 2);
                        break;
                    case playerState.BLOCKING:
                        SourceRectangle2 = new Rectangle(Bowser.blocking.X[frames2], Bowser.blocking.Y[frames2],
                            Bowser.blocking.Width[frames2], Bowser.blocking.Height[frames2]);
                        DestinationRectangle2 = new Rectangle(DestinationRectangle2.X, playersStandingOnYCoordinate + (Bowser.standing.Height[0] - Bowser.blocking.Height[frames2]),
                            Bowser.blocking.Width[frames2] * 2, Bowser.blocking.Height[frames2] * 2);
                        break;
                    case playerState.PUNCHING:
                        SourceRectangle2 = new Rectangle(Bowser.punching.X[frames2], Bowser.punching.Y[frames2],
                            Bowser.punching.Width[frames2], Bowser.punching.Height[frames2]);
                        if (!flip2)
                            DestinationRectangle2 = new Rectangle(DestinationRectangle2.X, playersStandingOnYCoordinate + (Bowser.standing.Height[0] - Bowser.punching.Height[frames2]),
                                Bowser.punching.Width[frames2] * 2, Bowser.punching.Height[frames2] * 2);
                        else
                            DestinationRectangle2 = new Rectangle(player2X_ForPunching - Bowser.punching.Width[frames2] + Bowser.standing.Width[0] - 15, playersStandingOnYCoordinate + (Bowser.standing.Height[0] - Bowser.punching.Height[frames2]),
                                Bowser.punching.Width[frames2] * 2, Bowser.punching.Height[frames2] * 2);
                        break;
                    case playerState.FIRING:
                        SourceRectangle2 = new Rectangle(Bowser.firing.X[frames2], Bowser.firing.Y[frames2],
                            Bowser.firing.Width[frames2], Bowser.firing.Height[frames2]);
                        DestinationRectangle2 = new Rectangle(DestinationRectangle2.X, playersStandingOnYCoordinate + (Bowser.standing.Height[0] - Bowser.firing.Height[frames2]),
                            Bowser.firing.Width[frames2] * 2, Bowser.firing.Height[frames2] * 2);
                        break;
                    case playerState.HURT:
                        SourceRectangle2 = new Rectangle(Bowser.hurt.X[frames2], Bowser.hurt.Y[frames2],
                            Bowser.hurt.Width[frames2], Bowser.hurt.Height[frames2]);
                        DestinationRectangle2 = new Rectangle(DestinationRectangle2.X, playersStandingOnYCoordinate + (Bowser.standing.Height[0] - Bowser.hurt.Height[frames2]),
                            Bowser.hurt.Width[frames2] * 2, Bowser.hurt.Height[frames2] * 2);
                        break;
                    case playerState.DEAD:
                        SourceRectangle2 = new Rectangle(Bowser.dead.X[frames2], Bowser.dead.Y[frames2],
                            Bowser.dead.Width[frames2], Bowser.dead.Height[frames2]);
                        DestinationRectangle2 = new Rectangle(DestinationRectangle2.X, playersStandingOnYCoordinate + (Bowser.standing.Height[0] - Bowser.dead.Height[frames2]),
                            Bowser.dead.Width[frames2] * 2, Bowser.dead.Height[frames2] * 2);
                        break;
                    default:
                        break;
                }
                #endregion
                #region SourceRectangle and DestinationRectangle for player 1 FireBall
                if (FireBall1_CurrentState_Active)
                {
                    SourceRectangle1_FireBall = new Rectangle(Bowser.fireBall.X[frames1_Fireball], Bowser.fireBall.Y[frames1_Fireball],
                        Bowser.fireBall.Width[frames1_Fireball], Bowser.fireBall.Height[frames1_Fireball]);
                    DestinationRectangle1_FireBall = new Rectangle(DestinationRectangle1_FireBall.X, playersStandingOnYCoordinate + 30,
                        Bowser.fireBall.Width[frames1_Fireball] * 2, Bowser.fireBall.Height[frames1_Fireball] * 2);
                }
                if (!FireBall1_CurrentState_Active)
                {
                    SourceRectangle1_FireBall = new Rectangle(0, 0, 0, 0);
                    DestinationRectangle1_FireBall = new Rectangle(0, 0, 0, 0);
                }
                #endregion
                #region SourceRectangle and DestinationRectangle for player 2 FireBall
                if (FireBall2_CurrentState_Active)
                {
                    SourceRectangle2_FireBall = new Rectangle(Bowser.fireBall.X[frames2_Fireball], Bowser.fireBall.Y[frames2_Fireball],
                        Bowser.fireBall.Width[frames2_Fireball], Bowser.fireBall.Height[frames2_Fireball]);
                    DestinationRectangle2_FireBall = new Rectangle(DestinationRectangle2_FireBall.X, playersStandingOnYCoordinate + 30,
                        Bowser.fireBall.Width[frames2_Fireball] * 2, Bowser.fireBall.Height[frames2_Fireball] * 2);
                }
                if (!FireBall2_CurrentState_Active)
                {
                    SourceRectangle2_FireBall = new Rectangle(0, 0, 0, 0);
                    DestinationRectangle2_FireBall = new Rectangle(0, 0, 0, 0);
                }
                #endregion
                #region SourceRectangle for player 1 and player 2 HealthBar
                SourceRectangle1_HealthBar = new Rectangle(HealthBar.Bar.X[-(player1_Health / 10) + 10], HealthBar.Bar.Y[-(player1_Health / 10) + 10],
                    HealthBar.Bar.Width[-(player1_Health / 10) + 10], HealthBar.Bar.Height[-(player1_Health / 10) + 10]);
                SourceRectangle2_HealthBar = new Rectangle(HealthBar.Bar.X[-(player2_Health / 10) + 10], HealthBar.Bar.Y[-(player2_Health / 10) + 10],
                    HealthBar.Bar.Width[-(player2_Health / 10) + 10], HealthBar.Bar.Height[-(player2_Health / 10) + 10]);
                #endregion
                #region SourceRectangle and DestinationRectangle for WinMessageDisplay
                SourceRectangle_WinMessage = new Rectangle(Message.MessageBox.X[WinMessageBoxCount], Message.MessageBox.Y[WinMessageBoxCount],
                    Message.MessageBox.Width[WinMessageBoxCount], Message.MessageBox.Height[WinMessageBoxCount]);
                DestinationRectangle_WinMessage = new Rectangle(150, 120,
                    Message.MessageBox.Width[WinMessageBoxCount], Message.MessageBox.Height[WinMessageBoxCount]);
                #endregion
                #region SourceRectangle for player 1 and player 2 WinCountBox
                if (stage1Active || stage2Active || stage3Active)
                {
                    SourceRectangle1_WinCountBox = new Rectangle(Message.Player1_WinCountBox.X[player1_WinCount], Message.Player1_WinCountBox.Y[player1_WinCount],
                        Message.Player1_WinCountBox.Width[player1_WinCount], Message.Player1_WinCountBox.Height[player1_WinCount]);
                    sourceRectangle2_WinCountBox = new Rectangle(Message.Player2_WinCountBox.X[player2_WinCount], Message.Player2_WinCountBox.Y[player2_WinCount],
                        Message.Player2_WinCountBox.Width[player2_WinCount], Message.Player2_WinCountBox.Height[player2_WinCount]);
                }
                #endregion
                #region SourceRectangle for RoundNumber
                if (stage1Active || stage2Active || stage3Active)
                {
                    SourceRectangle_RoundNumber = new Rectangle(Message.RoundNumber.X[RoundNumCount], Message.RoundNumber.Y[RoundNumCount],
                        Message.RoundNumber.Width[RoundNumCount], Message.RoundNumber.Height[RoundNumCount]);
                }
                #endregion

                #region limit player1 from moving out of screen
                switch (Player1CurrentState)
                {
                    case playerState.STANDING:
                        DestinationRectangle1.X = (int)MathHelper.Clamp(DestinationRectangle1.X, 0, (graphics.GraphicsDevice.Viewport.Width - Bowser.standing.Width[frames1] * 2));
                        break;
                    case playerState.WALKING:
                        DestinationRectangle1.X = (int)MathHelper.Clamp(DestinationRectangle1.X, 0, (graphics.GraphicsDevice.Viewport.Width - Bowser.walking.Width[frames1] * 2));
                        break;
                    case playerState.RUNNING:
                        DestinationRectangle1.X = (int)MathHelper.Clamp(DestinationRectangle1.X, 0, (graphics.GraphicsDevice.Viewport.Width - Bowser.running.Width[frames1] * 2));
                        break;
                    case playerState.JUMPING:
                        DestinationRectangle1.X = (int)MathHelper.Clamp(DestinationRectangle1.X, 0, (graphics.GraphicsDevice.Viewport.Width - Bowser.jumping.Width[frames1] * 2));
                        break;
                    case playerState.BLOCKING:
                        DestinationRectangle1.X = (int)MathHelper.Clamp(DestinationRectangle1.X, 0, (graphics.GraphicsDevice.Viewport.Width - Bowser.blocking.Width[frames1] * 2));
                        break;
                    case playerState.PUNCHING:
                        DestinationRectangle1.X = (int)MathHelper.Clamp(DestinationRectangle1.X, 0, (graphics.GraphicsDevice.Viewport.Width - Bowser.punching.Width[frames1] * 2));
                        break;
                    case playerState.FIRING:
                        DestinationRectangle1.X = (int)MathHelper.Clamp(DestinationRectangle1.X, 0, (graphics.GraphicsDevice.Viewport.Width - Bowser.firing.Width[frames1] * 2));
                        break;
                    case playerState.HURT:
                        DestinationRectangle1.X = (int)MathHelper.Clamp(DestinationRectangle1.X, 0, (graphics.GraphicsDevice.Viewport.Width - Bowser.hurt.Width[frames1] * 2));
                        break;
                    case playerState.DEAD:
                        DestinationRectangle1.X = (int)MathHelper.Clamp(DestinationRectangle1.X, 0, (graphics.GraphicsDevice.Viewport.Width - Bowser.dead.Width[frames1] * 2));
                        break;
                    default:
                        break;
                }
                #endregion
                #region limit player2 from moving out of screen
                switch (Player2CurrentState)
                {
                    case playerState.STANDING:
                        DestinationRectangle2.X = (int)MathHelper.Clamp(DestinationRectangle2.X, 0, (graphics.GraphicsDevice.Viewport.Width - Bowser.standing.Width[frames2] * 2));
                        break;
                    case playerState.WALKING:
                        DestinationRectangle2.X = (int)MathHelper.Clamp(DestinationRectangle2.X, 0, (graphics.GraphicsDevice.Viewport.Width - Bowser.walking.Width[frames2] * 2));
                        break;
                    case playerState.RUNNING:
                        DestinationRectangle2.X = (int)MathHelper.Clamp(DestinationRectangle2.X, 0, (graphics.GraphicsDevice.Viewport.Width - Bowser.running.Width[frames2] * 2));
                        break;
                    case playerState.JUMPING:
                        DestinationRectangle2.X = (int)MathHelper.Clamp(DestinationRectangle2.X, 0, (graphics.GraphicsDevice.Viewport.Width - Bowser.jumping.Width[frames2] * 2));
                        break;
                    case playerState.BLOCKING:
                        DestinationRectangle2.X = (int)MathHelper.Clamp(DestinationRectangle2.X, 0, (graphics.GraphicsDevice.Viewport.Width - Bowser.blocking.Width[frames2] * 2));
                        break;
                    case playerState.PUNCHING:
                        DestinationRectangle2.X = (int)MathHelper.Clamp(DestinationRectangle2.X, 0, (graphics.GraphicsDevice.Viewport.Width - Bowser.punching.Width[frames2] * 2));
                        break;
                    case playerState.FIRING:
                        DestinationRectangle2.X = (int)MathHelper.Clamp(DestinationRectangle2.X, 0, (graphics.GraphicsDevice.Viewport.Width - Bowser.firing.Width[frames2] * 2));
                        break;
                    case playerState.HURT:
                        DestinationRectangle2.X = (int)MathHelper.Clamp(DestinationRectangle2.X, 0, (graphics.GraphicsDevice.Viewport.Width - Bowser.hurt.Width[frames2] * 2));
                        break;
                    case playerState.DEAD:
                        DestinationRectangle2.X = (int)MathHelper.Clamp(DestinationRectangle2.X, 0, (graphics.GraphicsDevice.Viewport.Width - Bowser.dead.Width[frames2] * 2));
                        break;
                    default:
                        break;
                }
                #endregion

                #region calculating DistanceBetweenPlayer and BetweenFireBall
                DistanceBetweenPlayersX = Math.Abs(DestinationRectangle1.X - DestinationRectangle2.X);  //to calculate the distance between 2 players in Horizontally
                DistanceBetweenPlayersY = Math.Abs(DestinationRectangle1.Y - DestinationRectangle2.Y);  //to calculate the distance between 2 players in Vertically
                DistanceBetweenPlayer1AndFireBall2X = Math.Abs(DestinationRectangle2_FireBall.X - DestinationRectangle1.X);
                DistanceBetweenPlayer2AndFireBall1X = Math.Abs(DestinationRectangle1_FireBall.X - DestinationRectangle2.X);
                DistanceBetweenPlayer1AndFireBall2Y = Math.Abs(DestinationRectangle2_FireBall.Y - DestinationRectangle1.Y);
                DistanceBetweenPlayer2AndFireBall1Y = Math.Abs(DestinationRectangle1_FireBall.Y - DestinationRectangle2.Y);
                DistanceBetweenFireBall1AndFireBall2 = Math.Abs(DestinationRectangle1_FireBall.X - DestinationRectangle2_FireBall.X);
                if (DistanceBetweenFireBall1AndFireBall2 <= 80 && FireBall1_CurrentState_Active && FireBall2_CurrentState_Active)
                {
                    FireBall2_PreviousState_Active = false;
                    FireBall2_CurrentState_Active = false;
                    FireBall1_PreviousState_Active = false;
                    FireBall1_CurrentState_Active = false;
                }
                #endregion


            }
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AliceBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            if (StartScreenActive)
                StartScreen.draw(spriteBatch);
            if (stage1Active)
                stage1.draw(spriteBatch);
            if (stage2Active)
                stage2.draw(spriteBatch);
            if (stage3Active)
                stage3.draw(spriteBatch);
            if (EndScreenActive)
                EndScreen.draw(spriteBatch);
            if (stage1Active || stage2Active || stage3Active)
            {
                if (flip1)
                    spriteBatch.Draw(Bowser1, DestinationRectangle1, SourceRectangle1, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0.8f);
                if (!flip1)
                    spriteBatch.Draw(Bowser1, DestinationRectangle1, SourceRectangle1, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.8f);
                if (flip2)
                    spriteBatch.Draw(Bowser2, DestinationRectangle2, SourceRectangle2, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0.8f);
                if (!flip2)
                    spriteBatch.Draw(Bowser2, DestinationRectangle2, SourceRectangle2, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.8f);
                if (flip1_FireBall)
                    spriteBatch.Draw(Bowser1_FireBall, DestinationRectangle1_FireBall, SourceRectangle1_FireBall, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0.7f);
                if (!flip1_FireBall)
                    spriteBatch.Draw(Bowser1_FireBall, DestinationRectangle1_FireBall, SourceRectangle1_FireBall, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.7f);
                if (flip2_FireBall)
                    spriteBatch.Draw(Bowser2_FireBall, DestinationRectangle2_FireBall, SourceRectangle2_FireBall, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0.7f);
                if (!flip2_FireBall)
                    spriteBatch.Draw(Bowser2_FireBall, DestinationRectangle2_FireBall, SourceRectangle2_FireBall, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.7f);
                spriteBatch.Draw(Bowser1_HealthBar, DestinationRectangle1_HealthBar, SourceRectangle1_HealthBar, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
                spriteBatch.Draw(Bowser2_HealthBar, DestinationRectangle2_HealthBar, SourceRectangle2_HealthBar, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
                spriteBatch.Draw(Player1_WinCountBox, DestinationRectangle1_WinCountBox, SourceRectangle1_WinCountBox, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
                spriteBatch.Draw(Player2_WinCountBox, DestinationRectangle2_WinCountBox, sourceRectangle2_WinCountBox, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
                spriteBatch.Draw(RoundNumber, DestinationRectangle_RoundNumber, SourceRectangle_RoundNumber, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
                if (WinMessageBoxActive)
                    spriteBatch.Draw(WinMessageDisplay, DestinationRectangle_WinMessage, SourceRectangle_WinMessage, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
            }


            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
