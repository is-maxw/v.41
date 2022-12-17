using Raylib_cs;
using System.Numerics;



Raylib.InitWindow(1024, 768, "Topdown game");
Raylib.SetTargetFPS(60);

Rectangle character = new Rectangle(48, 300, 80, 80);
Rectangle enemyRect = new Rectangle(900, 310, 64, 64);




//DOORS
Rectangle doorS1 = new Rectangle(1000, 300, 80, 150);
Rectangle door1S = new Rectangle(0, 300, 30, 150);
Rectangle door21 = new Rectangle(0, 300, 30, 150);
Rectangle door23 = new Rectangle(1000, 300, 80, 150);
Rectangle door32 = new Rectangle(0, 300, 30, 150);
Rectangle door3a = new Rectangle(500, 0, 100, 30);
Rectangle doora3 = new Rectangle(500, 738, 100, 100);
Rectangle door3b = new Rectangle(500, 738, 100, 100);
Rectangle doorb3 = new Rectangle(500, 0, 100, 30);

Rectangle button3a = new Rectangle(512, 80, 40, 40);

Rectangle realKey = new Rectangle(950, 600, 40, 40);

List<Rectangle> fakeKeys = new();

fakeKeys.Add(new Rectangle(100, 100, 40, 40));
fakeKeys.Add(new Rectangle(259, 380, 40, 40));
fakeKeys.Add(new Rectangle(550, 500, 40, 40));
fakeKeys.Add(new Rectangle(900, 460, 40, 40));
fakeKeys.Add(new Rectangle(600, 600, 40, 40));
fakeKeys.Add(new Rectangle(435, 240, 40, 40));

//MAZE
Rectangle mazeButton = new Rectangle(200, 350, 40, 40);
Rectangle mazeButton2 = new Rectangle(900, 355, 40, 40);

List<Rectangle> mazeBlocks = new();

mazeBlocks.Add(new Rectangle(350, 0, 150, 500));
mazeBlocks.Add(new Rectangle(350, 620, 1000, 1000));
mazeBlocks.Add(new Rectangle(620, 200, 150, 1000));
mazeBlocks.Add(new Rectangle(360, 0, 1000, 80));
mazeBlocks.Add(new Rectangle(890, 0, 1000, 300));
mazeBlocks.Add(new Rectangle(750, 450, 1000, 1000));


bool playerHasKey = false;
bool playerHasRealKey = false;
bool impossibleMode = false;

float speed = 8.5f;

Color myColor = new Color(200, 30, 50, 0);

Texture2D avatarImage = Raylib.LoadTexture("pixil-frame-0 (1).png");
Texture2D startBackground = Raylib.LoadTexture("start_background.png.png");
// Texture2D gameBackground = Raylib.LoadTexture("grass_game_rs.png");
// Texture2D gameApple = Raylib.LoadTexture("moms_apple.png");

string currentScene = "start"; // start, game, win, gameover


Vector2 enemyMovement = new Vector2(1, 0);
float enemySpeed = 2;


// WHILE/IF --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
while (Raylib.WindowShouldClose() == false)
{
    Vector2 movement = new Vector2(0, 0);

    if (currentScene == "game")
    {

        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            movement.X = 1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            movement.X = -1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            movement.Y = 1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            movement.Y = -1;
        }

        if (impossibleMode == false)
        {
            Raylib.DrawText("Impossible Mode", 60, 40, 24, Color.RED);
            Rectangle impossible = new Rectangle(120, 70, 40, 40);
            Raylib.DrawRectangleRec(impossible, Color.GOLD);
            
            if (Raylib.CheckCollisionRecs(character, impossible))
            {
                impossibleMode = true;
            }
        }
        if (impossibleMode == true)
        {
            enemySpeed = 10;
        }

        Vector2 playerPos = new Vector2(character.x, character.y);
        Vector2 enemyPos = new Vector2(enemyRect.x, enemyRect.y);

        Vector2 diff = playerPos - enemyPos;

        Vector2 enemyDirection = Vector2.Normalize(diff);



        enemyMovement = enemyDirection * enemySpeed;

        enemyRect.x += enemyMovement.X;
        enemyRect.y += enemyMovement.Y;

        if (Raylib.CheckCollisionRecs(character, enemyRect))
        {
            currentScene = "gameover";
        }

        if (Raylib.CheckCollisionRecs(character, doorS1))
        {
            currentScene = "room1";
            character.x = 60;
            character.y = 310;
        }


    }

    if (currentScene == "room1")
    {

        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            movement.X = 1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            movement.X = -1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            movement.Y = 1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            movement.Y = -1;
        }

        Vector2 playerPos = new Vector2(character.x, character.y);

        if (playerHasKey == false)
        {
            Rectangle key1 = new Rectangle(700, 550, 40, 40);
            Raylib.DrawRectangleRec(key1, Color.YELLOW);

            if (Raylib.CheckCollisionRecs(character, key1))
            {
                playerHasKey = true;
            }
        }

        else if (playerHasKey == true)
        {
            Rectangle door12 = new Rectangle(1000, 300, 80, 150);
            Raylib.DrawRectangleRec(door12, Color.LIME);

            if (Raylib.CheckCollisionRecs(character, door12))
            {
                currentScene = "room2";
                character.x = 60;
                character.y = 310;
            }
        }



        if (Raylib.CheckCollisionRecs(character, door1S))
        {
            currentScene = "game";
            enemyRect.x = 45;
            enemyRect.y = 300;

            character.x = 900;
            character.y = 310;
        }


        if (character.x >= 945)
        {
            currentScene = "gameover";
        }
        else if (character.x <= 0)
        {
            currentScene = "gameover";
        }
        else if (character.y >= 690)
        {
            currentScene = "gameover";
        }
        else if (character.y <= 0)
        {
            currentScene = "gameover";
        }

    }

    else if (currentScene == "room2")
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            movement.X = 1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            movement.X = -1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            movement.Y = 1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            movement.Y = -1;
        }

        Vector2 playerPos = new Vector2(character.x, character.y);

        if (Raylib.CheckCollisionRecs(character, door21))
        {
            currentScene = "room1";
            character.x = 900;
            character.y = 310;
        }
        if (Raylib.CheckCollisionRecs(character, door23))
        {
            currentScene = "room3";
            character.x = 45;
            character.y = 310;
        }

        if (Raylib.CheckCollisionRecs(character, mazeButton))
        {
            foreach (Rectangle block in mazeBlocks)
            {
                Raylib.DrawRectangleRec(block, Color.RED);
            }
        }
        else if (Raylib.CheckCollisionRecs(character, mazeButton2))
        {
            foreach (Rectangle block in mazeBlocks)
            {
                Raylib.DrawRectangleRec(block, Color.RED);
            }
        }

        bool hasCollided = false;
        foreach (Rectangle block in mazeBlocks)
        {
            if (Raylib.CheckCollisionRecs(character, block))
            {
                hasCollided = true;
            }
        }

        if (hasCollided == true)
        {
            currentScene = "gameover2";
        }

        if (character.x >= 945)
        {
            currentScene = "gameover";
        }
        else if (character.x <= 0)
        {
            currentScene = "gameover";
        }
        else if (character.y >= 690)
        {
            currentScene = "gameover";
        }
        else if (character.y <= 0)
        {
            currentScene = "gameover";
        }
    }

    if (currentScene == "gameover2")
    {
         if (Raylib.IsKeyDown(KeyboardKey.KEY_R))
        {
            currentScene = "room2";
            character.x = 48;
            character.y = 300;

        }
    }

    else if (currentScene == "room3")
    {
        
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            movement.X = 1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            movement.X = -1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            movement.Y = 1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            movement.Y = -1;
        }

        Vector2 playerPos = new Vector2(character.x, character.y);

        if (Raylib.CheckCollisionRecs(character, door32))
        {
            currentScene = "room2";
            character.x = 900;
            character.y = 310;
        }

        if (Raylib.CheckCollisionRecs(character, door3a))
        {
            currentScene = "room3a";
            character.x = 510;
            character.y = 643;
        }
        else if (Raylib.CheckCollisionRecs(character, door3b))
        {
            currentScene = "room3b";
            character.x = 510;
            character.y = 45;
        }

           if (character.x >= 945)
        {
            currentScene = "gameover";
        }
        else if (character.x <= 0)
        {
            currentScene = "gameover";
        }
        else if (character.y >= 690)
        {
            currentScene = "gameover";
        }
        else if (character.y <= 0)
        {
            currentScene = "gameover";
        }
    }

    if (currentScene == "room3a")
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            movement.X = 1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            movement.X = -1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            movement.Y = 1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            movement.Y = -1;
        }

        if (Raylib.CheckCollisionRecs(character, doora3))
        {
            currentScene = "room3";
            character.x = 510;
            character.y = 45;
        }

        if (Raylib.CheckCollisionRecs(character, button3a))
        {
            
        }
       

        Vector2 playerPos = new Vector2(character.x, character.y);

        if (character.x >= 945)
        {
            currentScene = "gameover";
        }
        else if (character.x <= 0)
        {
            currentScene = "gameover";
        }
        else if (character.y >= 690)
        {
            currentScene = "gameover";
        }
        else if (character.y <= 0)
        {
            currentScene = "gameover";
        }
    }
    if (currentScene == "room3b")
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            movement.X = 1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            movement.X = -1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            movement.Y = 1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            movement.Y = -1;
        }

         if (Raylib.CheckCollisionRecs(character, doorb3))
        {
            currentScene = "room3";
            character.x = 510;
            character.y = 643;
        }

        Vector2 playerPos = new Vector2(character.x, character.y);

        if (character.x >= 945)
        {
            currentScene = "gameover";
        }
        else if (character.x <= 0)
        {
            currentScene = "gameover";
        }
        else if (character.y >= 690)
        {
            currentScene = "gameover";
        }
        else if (character.y <= 0)
        {
            currentScene = "gameover";
        }
    }

    else if (currentScene == "start")
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
        {
            currentScene = "game";
            character.x = 48;
            character.y = 300;

            enemyRect.x = 900;
            enemyRect.y = 310;

        }
    }
    if (currentScene == "gameover")
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_R))
        {
            currentScene = "game";
            character.x = 48;
            character.y = 300;

            enemyRect.x = 900;
            enemyRect.y = 310;
        }
    }

    if (currentScene == "game")
    {
        if (character.x >= 945)
        {
            currentScene = "gameover";
        }
        else if (character.x <= 0)
        {
            currentScene = "gameover";
        }
        else if (character.y >= 690)
        {
            currentScene = "gameover";
        }
        else if (character.y <= 0)
        {
            currentScene = "gameover";
        }

    }

    if (movement.Length() > 0)
    {
        movement = Vector2.Normalize(movement) * speed;
    }

    character.x += movement.X;
    character.y += movement.Y;



    // DRAW --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---

    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.WHITE);

    if (currentScene == "game")
    {
        Raylib.DrawText("Watch out for enemies!", 100, 150, 70, Color.BLACK);
        Raylib.DrawText("0", 480, 290, 150, Color.BLACK);
        // Raylib.DrawTexture(gameBackground, 0, 0, Color.WHITE);
        Raylib.DrawTexture(avatarImage, (int)character.x, (int)character.y, Color.WHITE);

        Raylib.DrawRectangleRec(enemyRect, Color.RED);
        Raylib.DrawRectangleRec(doorS1, Color.LIME);
    }
    else if (currentScene == "room1")
    {
        Raylib.DrawText("Yellow rectangles are 'keys'. By touching them, something will unlock", 80, 100, 25, Color.BLACK);
        Raylib.DrawText("1", 510, 290, 150, Color.BLACK);
        Raylib.DrawTexture(avatarImage, (int)character.x, (int)character.y, Color.WHITE);
        Raylib.DrawRectangleRec(door1S, Color.LIME);


    }
    else if (currentScene == "room2")
    {
        Raylib.DrawText("Purple rectangles reveal", 40, 50, 20, Color.BLACK);
        Raylib.DrawText("that which is hidden", 55, 75, 20, Color.BLACK);
        Raylib.DrawRectangleRec(door23, Color.LIME);
        Raylib.DrawRectangleRec(mazeButton, Color.PURPLE);
        Raylib.DrawRectangleRec(mazeButton2, Color.PURPLE);
        Raylib.DrawText("2", 510, 290, 150, Color.BLACK);
        Raylib.DrawTexture(avatarImage, (int)character.x, (int)character.y, Color.WHITE);
        Raylib.DrawRectangleRec(door21, Color.LIME);
    }
    else if (currentScene == "gameover2")
    {
        Raylib.ClearBackground(myColor);
        Raylib.DrawText("OOPS!", 310, 300, 64, Color.BLACK);
        Raylib.DrawText("Press R to restart the room", 310, 400, 32, Color.BLACK);
    }


    else if (currentScene == "room3")
    {
        Raylib.DrawText("3", 510, 290, 150, Color.BLACK);
        Raylib.DrawText("Sometimes you have to do mini-tasks to", 250, 150, 30, Color.BLACK);
        Raylib.DrawText("advance to the next stage", 360, 180, 30, Color.BLACK);
        Raylib.DrawTexture(avatarImage, (int)character.x, (int)character.y, Color.WHITE);
        Raylib.DrawRectangleRec(door32, Color.LIME);
        Raylib.DrawRectangleRec(door3a, Color.LIME);
        Raylib.DrawRectangleRec(door3b, Color.LIME);
    }
    else if (currentScene == "room3a")
    {
        Raylib.DrawText("Beware! Only one is the real one!", 300, 40, 32, Color.BLACK);
        Raylib.DrawRectangleRec(button3a, Color.PURPLE);
        Raylib.DrawTexture(avatarImage, (int)character.x, (int)character.y, Color.WHITE);
        Raylib.DrawRectangleRec(doora3, Color.LIME);

    }
    else if (currentScene == "room3b")
    {
        Raylib.DrawTexture(avatarImage, (int)character.x, (int)character.y, Color.WHITE);
        Raylib.DrawRectangleRec(doorb3, Color.LIME);
    }
    else if (currentScene == "start")
    {
        Raylib.DrawTexture(startBackground, 0, 0, Color.WHITE);
        Raylib.DrawText("TOPDOWN!", 225, 105, 112, Color.BLACK);
        Raylib.DrawText("TOPDOWN!", 220, 100, 112, Color.DARKPURPLE);
        Raylib.DrawText("Press ENTER to start", 315, 300, 32, Color.BLACK);

    }

    if (currentScene == "gameover")
    {
        Raylib.ClearBackground(myColor);
        Raylib.DrawText("GAME OVER", 315, 300, 64, Color.BLACK);
        Raylib.DrawText("Press R to restart", 315, 400, 32, Color.BLACK);
    }

    Raylib.EndDrawing();
}