using UnityEngine;

public class Hacker : MonoBehaviour {
    //Game configration data
    string[] lev1Passwords = {"teacher", "classroom", "book", "lecture", "student"};
    string[] lev2Passwords = {"engineering", "architecture", "journalism", "history", "archeology" };
    string[] lev3Passwords = {"microbiology", "beaker", "combustion", "volatile", "thermometer" };

    //Game state
    int level;
    enum Screen {MainMenu,Password, Win};
    Screen currentScreen;
    string password;

	// Use this for initialization
	void Start ()
    {
        ShownMainMenu();
    }
	
    void ShownMainMenu()
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("Hacker System 3000\n");
        Terminal.WriteLine("What would you like to hack into today?\n");
        Terminal.WriteLine("1 - Your local high school");
        Terminal.WriteLine("2 - Your University");
        Terminal.WriteLine("3 - A Science Research Lab\n");
        Terminal.WriteLine("Type menu at any time to go back");
        Terminal.WriteLine("Enter your choice: ");
    }

    void OnUserInput(string input)
    {

        if (input == "menu") //We can always go to main menu
        {
            ShownMainMenu();
        }
        else if (currentScreen==Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassward(input);
        }

    }

    void RunMainMenu(string input)
    {
        bool isValidLevel = (input == "1" || input == "2" || input == "3");
        if (isValidLevel)
        {
            level = int.Parse(input);
            AskForPassword();
        }

        else if (input == "I"|| input == "i") //easter egg
        {
            Terminal.WriteLine("Hello Mr Incredible, choose a level you want to hack into");
        }
        else
        {
            Terminal.WriteLine("Please choose a valid level");
        }
    }

    void AskForPassword()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        SetRandomPassword();
        Terminal.WriteLine("Please enter your passward");
        Terminal.WriteLine("Hint: " + password.Anagram());
    }

    void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                password = lev1Passwords[Random.Range(0, lev1Passwords.Length)];
                break;
            case 2:
                password = lev2Passwords[Random.Range(0, lev2Passwords.Length)];
                break;
            case 3:
                password = lev3Passwords[Random.Range(0, lev3Passwords.Length)];
                break;
            default:
                Debug.LogError("Invalid level");
                break;
        }
    }

    void CheckPassward(string input)
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

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
    }

    void ShowLevelReward()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Have a book...");
                Terminal.WriteLine(@"
      ______ ______
    _/      Y      \_
   // ~~ ~~ | ~~ ~  \\
  // ~ ~ ~~ | ~~~ ~~ \\      
 //________.|.________\\    
`----------`-'----------'
"
                                  );
                break;
            case 2:
                Terminal.WriteLine("You have got your diploma...");
                Terminal.WriteLine(@"
   _.-'`'-._        ________
.-'    _    '-.   (`\        `\
`-._   `\_.-'      `-\ DIPLOMA \
  | ``-``\|           \   (@)   \
  `-.....-A           _\   |\    \
          #          ( _)_________)
          #           `----------`
"
                                      );
                break;
            case 3:
                Terminal.WriteLine("Have some lab equipment...");
                Terminal.WriteLine(@"
       .---.              _
      _\___/_           _| |
       )\_/(          .'_) |
      /     \        /_/ |_|
     /~~~~~~~\      (_)  |_|
    /         \      \ \______
   (           )     |________|
    `---------´
"
                                      );
                break;
            default:
                Debug.LogError("Invalid level reached");
                break;
        }
    }
}
