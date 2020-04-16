using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Hacker : MonoBehaviour
{
    // Game configuration data
    const string menuHint = "Type menu to return to menu";
    string[] LibraryPasswords = { "books", "aisle", "shelf", "password", "font", "borrow"  };
    string[] PolicePasswords = { "prisoner", "handcuffs", "holster", "uniform", "arrest" };
    string[] NASAPasswords = { "spaceship", "rocket", "telescope", "astronomy", "universe" };

    // Game state
    enum Screen { Menu, Password, Win };
    enum Level { Library = 1, Police = 2, NASA = 3 };
    Screen screen;
    Level level;
    string password;

    // Start is called before the first frame update
    void Start()
    {
        DisplayMainScreen();
    }

    // Handle user input
    void OnUserInput(string input)
    {
        if (input.ToLower() == "menu")
        {
            DisplayMainScreen();
        }
        else if (screen == Screen.Menu)
        {
            NavigateToLevel(input);
        }
        else if (screen == Screen.Password)
        {
            CheckPassword(input);
        }
    }

    // Display the main menu
    void DisplayMainScreen()
    {
        screen = Screen.Menu;

        Terminal.ClearScreen();
        Terminal.WriteLine
        (
            "What would you like to hack into?\n\n" +
            "Press 1 for the local library\n" +
            "Press 2 for the police station\n" +
            "Press 3 for NASA\n\n" +
            "Enter your selection:"
        );
    }

    // Handle navigation to level
    void NavigateToLevel(string input)
    {
        if (input == "1" || input == "2" || input == "3")
        {
            level = (Level)int.Parse(input);
            AskForPassword();
        }
        else 
        {
            Terminal.WriteLine("Please enter a valid selection:");
            Terminal.WriteLine(menuHint);
        }
    }

    // Ask the user to enter a password
    void AskForPassword()
    {
        screen = Screen.Password;
        
        SetRandomPassword();
        Terminal.ClearScreen();
        Terminal.WriteLine("Enter your password\nHint: " + password.Anagram());
    }


    // Verify if password is correct or not
    void CheckPassword(string input)
    {
        if (input == password)
        {
            DisplayWinScreen();
        }
        else
        {
            AskForPassword();
        }
    }

    // Set the password to a random predefined word
    void SetRandomPassword()
    {
        switch (level)
        {
            case Level.Library:
                password = LibraryPasswords[Random.Range(0, LibraryPasswords.Length)];
                break;
            case Level.Police:
                password = PolicePasswords[Random.Range(0, PolicePasswords.Length)];
                break;
            case Level.NASA:
                password = NASAPasswords[Random.Range(0, NASAPasswords.Length)];
                break;
            default:
                Debug.LogError("Invalid level: " + level);
                break;
        }
    }

    // Display the win screen
    void DisplayWinScreen()
    {
        screen = Screen.Win;

        Terminal.ClearScreen();
        GiveReward();
        Terminal.WriteLine(menuHint);
    }

    // Display the reward to the player
    void GiveReward()
    {
        switch (level)
        {
            case Level.Library:
                Terminal.WriteLine("Have a book...");
                Terminal.WriteLine(@"
    _______
   /      /,
  /      //
 /______//
(______(/
                ");
                break;
            case Level.Police:
                Terminal.WriteLine("You got the prison key!");
                Terminal.WriteLine
                (@"
 __
/o \_____
\__/-='='`
                ");
                break;
            case Level.NASA:
                Terminal.WriteLine("Here's our best telescope...");
                Terminal.WriteLine
                (@"
    _____________
==c(___(o(____(_()
    \=\
     )=\
    //|\\
   //|| \\
  // ||  \\
                ");
                break;
            default:
                Debug.LogError("Invalid level: " + level);
                break;
        }
    }
}
