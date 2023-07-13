using Minesweeper.Console;
using Minesweeper.Core;
using Minesweeper.Core.Enums;
using Minesweeper.Core.Models;

// For each difficultyLevel can be implemented different test cases that's why we can create various test scenarios
DifficultyLevel difficultyLevel = Printer.ChooseDifficultyLevel();
// The difficulty level of game is selected in here
GameSettings settings = DifficultyManager.GetGameSettingsByDifficultylevel(difficultyLevel);
// This method returns 2D array which is named map
var field = FieldGenerator.GetRandomField(settings.Width, settings.Height, settings.Mines);
// This variable is main class for the game
var gameProcessor = new GameProcessor(field);
// This method gets initialize values of maps first of the game
var currentField = gameProcessor.GetCurrentField();
// This method prints map on the console
Printer.PrintField(currentField);

// The part which will be tested in this loop
while (gameProcessor.GameState == GameState.Active)
{
    System.Drawing.Point coordinates = Printer.GetCoordinates();
    // This method is to test with positive test scenarions
    gameProcessor.Open(coordinates.X, coordinates.Y);
    // This method returns the field which is updated after each step 
    currentField = gameProcessor.GetCurrentField();
    // This method print the map of game 
    Printer.PrintField(currentField);
}
// Print result
Printer.PrintGameResult(gameProcessor.GameState);


// Test Scenarios for the open method
/*
// We test boundary level analysis for game to lower bound value ==> 0,0
// We test boundary level analysis for game to upper bound value ==> (81,81) , (256,256)
// We test boundary level analysis for game to under lower bound value ==> (-1, -1)
// We test boundary level analysis for game to over upper bound value ==> (82,82) , (257,257)
// We test boundary level analysis for game to random values.
*/

// Test Scenarios for the GetCurrentField method
/*
//We test total number of cells whether is opened or not.
//We test the mines which is printed on the screen whether equals total mines number or not.
//We test , When the same value was triggered two times , if the console changed or not
//We test neighbor values whether is printed on the console or not

*/