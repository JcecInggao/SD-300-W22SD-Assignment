﻿Game newGame = new Game();

Console.WriteLine("What is your name?");
Hero hero = new Hero(Console.ReadLine(), newGame);
newGame.Player = hero;

// Weapons
WeaponList.AddWeapon(new Weapon("Wooden Club", 5));
WeaponList.AddWeapon(new Weapon("Dagger", 10));
WeaponList.AddWeapon(new Weapon("Mace", 15));
WeaponList.AddWeapon(new Weapon("Greatsword", 20));

// Armour
ArmourList.AddArmour(new Armour("No Armour", 1));
ArmourList.AddArmour(new Armour("Leather Armour", 5));
ArmourList.AddArmour(new Armour("Chainmail Armour", 10));
ArmourList.AddArmour(new Armour("Steel Armour", 15));

// Monsters (name, att, def, hp, isDead)
newGame.Monsters.Add(new Monster("MonStart", 3, 2, 20, false));
newGame.Monsters.Add(new Monster("Munstir", 6, 3, 50, false));
newGame.Monsters.Add(new Monster("Munchstur", 9, 4, 70, false));
newGame.Monsters.Add(new Monster("Munstar", 12, 5, 100, false));
newGame.Monsters.Add(new Monster("MonEND", 15, 6, 150, false));

newGame.Start();

class Game
{
    public Hero Player { get; set; }
    public Fight Combat { get; set; } = new Fight();
    public List<Monster> Monsters { get; set; } = new List<Monster>();

    // This method calls upon the main menu
    public void Start()
    {
        MainMenu();
    }
    public int ReadInput()
    {
        string playerInput = Console.ReadLine();
        int inputCode;
        if (int.TryParse(playerInput, out inputCode))
        {
            inputCode = int.Parse(playerInput);
        }
        return inputCode;
    }

    public void MainMenu()
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine($"What would you like to do {Player.Name}?");
        Console.WriteLine();
        Console.WriteLine("[1] Display Statictics");
        Console.WriteLine("[2] Display Inventory");
        Console.WriteLine("[3] Fight");
        Console.WriteLine();
        Console.WriteLine("========================================");

        switch (ReadInput())
        {
            case 1:
                Player.ShowStats();
                break;
            case 2:
                Player.ShowInventory();
                break;


            case 3:
                //create new list for alive monsters
                List<Monster> availableMonsters = new List<Monster>();
                foreach (Monster monster in Monsters)
                {
                    // checks if the monster list for alive monsters 
                    if (!monster.IsDead)
                    {
                        // adds available Monsters to a new list
                        availableMonsters.Add(monster);
                    }
                }

                //select random monster
                Random rng = new Random();
                int monsterIndex = rng.Next(availableMonsters.Count);
                Monster currentEnemy = availableMonsters[monsterIndex];
                Console.WriteLine("Initiating battle, Press enter to continue");
                Console.ReadKey();
                //MainMenu();
                Combat.StartFight(Player, currentEnemy);
                break;

            default:
                Console.WriteLine("Invalid input, please try again: ");
                Console.ReadKey();
                MainMenu();
                break;
        }
    }
}

class Hero
{
    // basic stats
    public string Name { get; set; }
    public int BaseStrength { get; set; } = 50;
    public int BaseDefence { get; set; } = 5;
    public int OriginalHealth { get; set; } = 100;
    public int CurrentHealth { get; set; }
    // inventory
    public int WeaponEquppied { get; set; } = 5;
    public int ArmourEquppied { get; set; } = 1;
    // statistics page
    public int GamesPlayed { get; set; } = 1;
    public int FightsWon { get; set; } = 0;
    public int FightsLost { get; set; } = 0;

    public Game Game = new Game();

    public Hero(string name, Game game)
    {
        Name = name;
        CurrentHealth = OriginalHealth;
        Game = game;
    }


    public void ShowStats()
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine($"{Name} Stats:");
        Console.WriteLine("");
        Console.WriteLine($"Games Played: {GamesPlayed}");
        Console.WriteLine($"Fights Won: {FightsWon}");
        Console.WriteLine($"Fights Lost: {FightsLost}");
        Console.WriteLine("");
        Console.WriteLine($"Health: {CurrentHealth}HP/{OriginalHealth}HP ");
        Console.WriteLine($"Base Strength: {BaseStrength} | Weapon: {WeaponEquppied} | Total: {BaseStrength + WeaponEquppied}");
        Console.WriteLine($"Base Defence: {BaseDefence}  | Armour: {ArmourEquppied} | Total: {BaseDefence + ArmourEquppied}");
        Console.WriteLine("");
        Console.WriteLine("Press Enter/Return to go back");
        Console.WriteLine("========================================");
        Console.ReadKey();
        Game.MainMenu();
    }

    public void ShowInventory()
    {
        // returns what items the Hero is Equipped with
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine($"{Name} Inventory:");
        Console.WriteLine("");
        Console.WriteLine("Equipped:");
        switch (WeaponEquppied)
        {
            case 5:
                Console.WriteLine("Wooden Club");
                break;
            case 10:
                Console.WriteLine("Dagger");
                break;
            case 15:
                Console.WriteLine("Mace");
                break;
            case 20:
                Console.WriteLine("Greatsword");
                break;
            default:
                // should never occur
                Console.WriteLine("Invalid Weapon Equipped, please change current Weapon");
                break;
        }
        switch (ArmourEquppied)
        {
            case 1:
                Console.WriteLine("No Armour");
                break;
            case 5:
                Console.WriteLine("Leather Armour");
                break;
            case 10:
                Console.WriteLine("Chainmail Armour");
                break;
            case 15:
                Console.WriteLine("Steel Armour");
                break;
            default:
                // should never occur
                Console.WriteLine("Invalid Armour Equipped, please change current Armour");
                break;
        }
        Console.WriteLine("");
        Console.WriteLine("Weapons:");
        WeaponList.ShowWeapons();
        Console.WriteLine("");
        Console.WriteLine("Armours:");
        ArmourList.ShowArmours();
        Console.WriteLine("");
        Console.WriteLine("[1] Change Weapon ");
        Console.WriteLine("[2] Change Armour");
        Console.WriteLine("Press Enter/Return to go back");
        Console.WriteLine("========================================");
        switch (Game.ReadInput())
        {
            case 1:
                EquipWeapon();
                break;
            case 2:
                EquipArmour();
                break;
            default:
                Game.MainMenu();
                break;
        }
    }
    public void EquipWeapon()
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine($"Which Weapon would you like to equip?");
        Console.WriteLine("");
        Console.WriteLine("Weapons:");
        Console.WriteLine("[1] Wooden Club --------- Attack: 5");
        Console.WriteLine("[2] Dagger -------------- Attack: 10");
        Console.WriteLine("[3] Mace ---------------- Attack: 15");
        Console.WriteLine("[4] Greatsword ---------- Attack: 20");
        Console.WriteLine("[5] Cancel");
        Console.WriteLine("");
        Console.WriteLine("========================================");
        switch (Game.ReadInput())
        {
            case 1:
                Console.WriteLine("Equipped: Wooden Club");
                WeaponEquppied = 5;
                Console.ReadKey();
                ShowInventory();
                break;
            case 2:
                Console.WriteLine("Equipped: Dagger");
                WeaponEquppied = 10;
                Console.ReadKey();
                ShowInventory();
                break;
            case 3:
                Console.WriteLine("Equipped: Mace");
                Console.ReadKey();
                WeaponEquppied = 15;
                ShowInventory();
                break;
            case 4:
                Console.WriteLine("Equipped: Greatsword");
                Console.ReadKey();
                WeaponEquppied = 20;
                ShowInventory();
                break;
            case 5:
                ShowInventory();
                break;
            default:
                EquipWeapon();
                break;
        }
    }
    public void EquipArmour()
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine($"Which Armour would you like to equip?");
        Console.WriteLine("");
        Console.WriteLine("Armour:");
        Console.WriteLine("[1] No Armour------------ Defence: 1");
        Console.WriteLine("[2] Leather Armour ------ Defence: 5");
        Console.WriteLine("[3] Chainmail Armour----- Defence: 10");
        Console.WriteLine("[4] Steel Armour -------- Defence: 15");
        Console.WriteLine("[5] Cancel");
        Console.WriteLine("");
        Console.WriteLine("========================================");
        switch (Game.ReadInput())
        {
            case 1:
                Console.WriteLine("Equipped: No Armour");
                ArmourEquppied = 1;
                Console.ReadKey();
                ShowInventory();
                break;
            case 2:
                Console.WriteLine("Equipped: Leather Armour");
                ArmourEquppied = 5;
                Console.ReadKey();
                ShowInventory();
                break;
            case 3:
                Console.WriteLine("Equipped: Chainmail Armour");
                ArmourEquppied = 10;
                Console.ReadKey();
                ShowInventory();
                break;
            case 4:
                Console.WriteLine("Equipped: Steel Armour");
                ArmourEquppied = 15;
                Console.ReadKey();
                ShowInventory();
                break;
            case 5:
                ShowInventory();
                break;
            default:
                Console.WriteLine("Invalid input, please try again: ");
                Console.ReadKey();
                EquipArmour();
                break;
        }
    }
}

class Monster
{
    public string Name { get; set; }
    public int Strength { get; set; }
    public int Defense { get; set; }
    public int OriginalHealth { get; set; }
    public int CurrentHealth { get; set; }
    public bool IsDead { get; set; }
    public Monster(string name, int strength, int defense, int originalHealth, bool isDead)
    {
        Name = name;
        Strength = strength;
        Defense = defense;
        OriginalHealth = originalHealth;
        CurrentHealth = originalHealth;
        IsDead = isDead;
    }
}

class Weapon
{
    public string Name { get; set; }
    public int Power { get; set; }

    public Weapon(string name, int power)
    {
        Name = name;
        Power = power;
    }
}

class Armour
{
    public string Name { get; set; }
    public int Power { get; set; }
    public Armour(string name, int power)
    {
        Name = name;
        Power = power;
    }
}

static class WeaponList
{
    public static List<Weapon> Weapons { get; set; } = new List<Weapon>();
    
    public static void AddWeapon(Weapon weapon)
    {
        Weapons.Add(weapon);
    }

    // called in the ShowInventory method to display weapons
    // displays all weapons even if new weapons are created or added 
    public static void ShowWeapons()
    {
        foreach (Weapon weapons in Weapons)
        {
            // adds consistiency in the spacing in the inventory screen (within 20 characters)
            string dashLength = "";
            for (int i = weapons.Name.Length; i < 20; i++)
            {
                dashLength += "-";
            }
            Console.WriteLine($"{weapons.Name} {dashLength} Power: {weapons.Power}");
        }
    }
}
static class ArmourList
{
    public static List<Armour> Armours { get; set; } = new List<Armour>();
    public static void AddArmour(Armour armour)
    {
        Armours.Add(armour);
    }

    // called in the ShowInventory method to display armours
    // displays all weapons even if new armours are created or added 
    public static void ShowArmours()
    {
        foreach (Armour armours in Armours)
        {
            // adds consistiency in the spacing in the inventory screen (within 20 characters)
            string dashLength = "";
            for (int i = armours.Name.Length; i < 20; i++)
            {
                dashLength += "-";
            }
            Console.WriteLine($"{armours.Name} {dashLength} Power: {armours.Power}");
        }
    }
}


class Fight
{
    private int _monstersDefeated = 0;

    public Hero Player { get; set; }
    public Monster Enemy { get; set; }
    public Game Game { get; set; }
    public void StartFight(Hero hero, Monster monster)
    {
        Player = hero;
        Enemy = monster;
        Game = hero.Game;
        while (Player.CurrentHealth > 0 && Enemy.CurrentHealth > 0)
        {
            Console.Clear();
            HeroTurn();
            if (Enemy.CurrentHealth < 1)
            {
                Win();
                break;
            }
            MonsterTurn();
            if (Player.CurrentHealth < 1)
            {
                Lose();
                break;
            }
        }
    }

    public void HeroTurn()
    {
        // The “damage” of that attack is calculated based on the Hero’s Base Strength + Equipped Weapon Power. Damage subtracts from the Current Health of the Monster.
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("");
        Console.WriteLine("Attack: Deal damage based on your strength");
        Console.WriteLine("Heal: Heal a random amount of health based on your defense");
        Console.WriteLine("Run: End Fight");
        Console.WriteLine("");
        Console.WriteLine($"{Player.Name}: {Player.CurrentHealth}HP/{Player.OriginalHealth}HP");
        Console.WriteLine($"{Enemy.Name}: {Enemy.CurrentHealth}HP/{Enemy.OriginalHealth}HP");
        Console.WriteLine("");
        Console.WriteLine("--------------");
        Console.WriteLine("[1] Attack");
        Console.WriteLine("[2] Heal");
        Console.WriteLine("[3] Run");
        Console.WriteLine("--------------");
        Console.WriteLine("========================================");
        Console.WriteLine("");

        // CheckInput
        string playerInput = Console.ReadLine();
        int inputCode;
        if (int.TryParse(playerInput, out inputCode))
        {
            inputCode = int.Parse(playerInput);
        }

        switch (inputCode)
        {
            // attack
            case 1:
                int dealtDamage = DamageCalculator(Player.BaseStrength, Player.WeaponEquppied);
                Enemy.CurrentHealth = Enemy.CurrentHealth - dealtDamage;
                Console.WriteLine($"You strike {Enemy.Name}, dealing {dealtDamage} damage!");
                Console.ReadKey();
                break;
            case 2:
                int damageHeal = DamageCalculator(Player.BaseDefence, Player.ArmourEquppied);
                Player.CurrentHealth = Player.CurrentHealth + damageHeal;
                if (Player.CurrentHealth > Player.OriginalHealth)
                {
                    Console.WriteLine($"You drink a potion, restoring {damageHeal}HP");
                    damageHeal = Player.CurrentHealth - Player.OriginalHealth;
                    Console.WriteLine($"You've overhealed, only {damageHeal}HP was restored");
                    Player.CurrentHealth = Player.OriginalHealth;
                } else
                {
                    Console.WriteLine($"You drink a potion, restoring {damageHeal} HP");
                }
                Console.WriteLine($"{Player.Name}: {Player.CurrentHealth}HP/{Player.OriginalHealth}HP");
                Console.ReadKey();
                break;
            case 3:
                Console.WriteLine($"You flee from {Enemy.Name}");
                Player.FightsLost++;
                Console.ReadKey();
                Game.MainMenu();
                break;
            default:
                Console.WriteLine("Invalid input, please try again: ");
                Console.ReadKey();
                HeroTurn();
                break;
        }
    }

    public void MonsterTurn()
    {
        // The “damage” of that attack is calculated by subtracting the Hero’s Base Defence, and Equipped Armour’s Power, from the Monster’s Strength. The result is subtracted from the Hero’s Current Health.
        int dealtDamage = DamageCalculator(Enemy.Strength);
        Player.CurrentHealth = Player.CurrentHealth - dealtDamage;
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine($"{Enemy.Name} is attacking");
        Console.WriteLine($"{Enemy.Name} did {dealtDamage} damage!");
        Console.WriteLine("");
        Console.WriteLine($"{Player.Name}: {Player.CurrentHealth}/{Player.OriginalHealth}");
        Console.WriteLine($"{Enemy.Name}: {Enemy.CurrentHealth}/{Enemy.OriginalHealth}");
        Console.WriteLine("");
        Console.WriteLine("Press enter to continue");
        Console.WriteLine("========================================");
        Console.ReadKey();

    }

    // Monster attack
    public int DamageCalculator(int strength)
    {
        // possibly higher damage
        Random rand = new Random();
        int critChance = rand.Next(1, 100);
        int playerBaseDamage = strength;
        // adds random chance to do extra damage
        int randomMultiplier = rand.Next(-5, 5);
        int totalDamage = playerBaseDamage + randomMultiplier;
        // 2% to crit
        if (critChance < 2)
        {
            // does extra 1.5 damage
            totalDamage = totalDamage + totalDamage / 2;
        }

        return totalDamage;
    }

    // Player attack
    public int DamageCalculator(int strength, int weaponStrength)
    {
        // higher chance to crit
        Random rand = new Random();
        int critChance = rand.Next(1, 100);
        int BaseDamage = strength + weaponStrength;
        // adds random chance to do extra damage
        int randomMultiplier = rand.Next(-3, 3);
        int totalDamage = BaseDamage + randomMultiplier;
        // 10% to crit
        if (critChance < 100)
        {
            // does extra 1.5 damage
            totalDamage = totalDamage + totalDamage / 2;
        }

        return totalDamage;
    }

    public int Heal(int defense)
    {
        // possibly higher damage
        Random rand = new Random();
        int critChance = rand.Next(1, 100);
        int baseHeal = defense;
        // adds random chance to do extra healing
        int randomMultiplier = rand.Next(-3, 10);
        int totalHeal = baseHeal + randomMultiplier;
        // % to crit
        if (critChance < 10)
        {
            // does extra 1.5 damage
            totalHeal = totalHeal + totalHeal / 2;
        }

        return totalHeal;
    }
    public void Win()
    {
        Player.FightsWon++;
        _monstersDefeated++;
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("");
        Console.WriteLine("Congratulations");
        Console.WriteLine($"You Beat {Enemy.Name}");
        if (_monstersDefeated == 5)
        {
            Console.WriteLine("You have defeated all the monsters!");
            Console.WriteLine("Thank you for playing!");
        }
        Console.WriteLine("");
        Console.WriteLine("========================================");
        Console.ReadKey();
        Game.MainMenu();
    }

    public void Lose()
    {
        Player.FightsLost++;
        _monstersDefeated = 0;
        // revives monsters
        foreach (Monster monster in Game.Monsters)
        {
            if (monster.IsDead)
            {
                monster.IsDead = false;
            }
        }
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("");
        Console.WriteLine("You Died");
        Console.WriteLine("Returning user to main menu");
        Console.WriteLine("");
        Console.WriteLine("========================================");
        Console.ReadKey();
        Game.MainMenu();
    }
}

