namespace Character
{
    internal class Program
    {

        static void WriteCharacters(Character[] persons)
        {
            for (int i = 0; i < persons.Length; i++)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(i + 1);
                Console.BackgroundColor = ConsoleColor.Black;
                persons[i].Out();
            }
        }

        static void Main(string[] args)
        {
            Console.Write("Введите количество персонажей: ");
            uint amount = Convert.ToUInt32( Console.ReadLine() );
            Character[] persons = new Character[amount];

            for (int i = 0; i < amount; i++)
            {
                Console.Write($"Введите имя {i + 1} персонажа: ");
                string? name = Console.ReadLine();
                Console.Write("Введите принадлежность к лагерю (друг/враг): ");
                bool camp = Console.ReadLine().ToLower() == "друг";
                Console.Write("Введите здоровье: ");
                int health = Convert.ToInt32( Console.ReadLine() );
                Console.Write("Введите координату X: ");
                int x = Convert.ToInt32( Console.ReadLine() );
                Console.Write("Введите координату Y: ");
                int y = Convert.ToInt32( Console.ReadLine() );

                persons[i] = new Character(name, x, y, camp, health);
                Console.Clear();
            }

            WriteCharacters(persons);

            Console.Write("Выберите персонажа: ");
            int idx_chr = Convert.ToInt32( Console.ReadLine() )-1;

            Console.Clear();
            
            while (true)
            {

                Console.Clear();

                Console.WriteLine("i - Информация по персонажу\n" +
                    "c - Сменить персонажа\n" +
                    "g - Лечение\n" +
                    "f - Полное восстановление\n" +
                    "m - Стать перебежчиком\n" +
                    "d - Передвинуться на x координату\n" +
                    "w - Передвинуться на y координату\n" +
                    "a - Атака");
#if DEBUG
                Console.WriteLine("\n!!! DEBUG !!!\n" +
                    "1 - Изменить урон какому-либо персонажу\n" +
                    "2 - Изменить здоровье какому-либо персонажу (возродить/убить)");
#endif

                ConsoleKey key = Console.ReadKey().Key;
                
                switch (key)
                {
                    case ConsoleKey.I: // Информация
                        Console.Clear();
                        persons[idx_chr].Out();
                        Console.WriteLine("\nНажмите ENTER чтобы продолжить");
                        Console.ReadLine();
                        break;

                    case ConsoleKey.C: // Смена персонажа
                        Console.Clear();
                        WriteCharacters(persons);
                        Console.Write("Выберите персонажа: ");
                        int temp_idx_chr = Convert.ToInt32(Console.ReadLine()) - 1;

                        if (!persons[temp_idx_chr].IsAlive())
                        {
                            Console.Write("\n\nВыбраный персонаж мертв\nНажмите ENTER чтобы продолжить");
                            Console.ReadLine();
                            break;
                        }

                        idx_chr = temp_idx_chr;
                        break;

                    case ConsoleKey.G: // Лечение по значению
                        Console.Clear();
                        int regen = new Random().Next(5, 70);
                        if (persons[idx_chr].IsAlive())
                        {
                            persons[idx_chr].Heal(regen);
                            Console.Write($"Вы полечились на {regen} хп.\nНажмите ENTER чтобы продолжить");
                        }
                        else
                        {
                            Console.Write("Мертвые не могут полечиться\nНажмите ENTER чтобы продолжить");
                        }
                        Console.ReadLine();
                        break;

                    case ConsoleKey.F: // Полное лечение
                        if (!persons[idx_chr].InFight())
                            persons[idx_chr].FullHeal();
                        else
                        {
                            Console.Clear();
                            Console.Write("Во время битвы нельзя восстановить здоровье полностью\nНажмите ENTER чтобы продолжить");
                            Console.ReadLine();
                        }
                        break;

                    case ConsoleKey.M: // Смена лагеря
                        Console.Clear();
                        if (!persons[idx_chr].InFight() && persons[idx_chr].IsAlive())
                            persons[idx_chr].ChangeCamp();
                        else if (persons[idx_chr].InFight())
                        {
                            Console.Write("Во время битвы нельзя менять лагерь\nНажмите ENTER чтобы продолжить");
                        }
                        else
                        {
                            Console.Write("Мертвым нельзя менять лагерь\nНажмите ENTER чтобы продолжить");
                        }
                        Console.ReadLine();
                        break;

                    case ConsoleKey.D: // Передвижение по x
                        Console.Clear();
                        if (!persons[idx_chr].InFight() && persons[idx_chr].IsAlive())
                        {
                            Console.Write("Введите на какую координату x переместиться: ");
                            int x = Convert.ToInt32( Console.ReadLine() );
                            persons[idx_chr].MoveX(x);
                        }
                        else if (persons[idx_chr].InFight())
                        {
                            Console.Write("Во время битвы нельзя перемещаться\nНажмите ENTER чтобы продолжить");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.Write("Мертвым нельзя двигаться\nНажмите ENTER чтобы продолжить");
                            Console.ReadLine();
                        }
                        break;

                    case ConsoleKey.W: // Передвижение по y
                        Console.Clear();
                        if (!persons[idx_chr].InFight() && persons[idx_chr].IsAlive())
                        {
                            Console.Write("Введите на какую координату y переместиться: ");
                            int y = Convert.ToInt32(Console.ReadLine());
                            persons[idx_chr].MoveY(y);
                        }
                        else if (persons[idx_chr].InFight())
                        {
                            Console.Write("Во время битвы нельзя перемещаться\nНажмите ENTER чтобы продолжить");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.Write("Мертвым нельзя двигаться\nНажмите ENTER чтобы продолжить");
                            Console.ReadLine();
                        }
                        break;
                    case ConsoleKey.A: // Атака
                        Console.Clear();
                        // Проверка на мертвого игрового персонажа
                        if (!persons[idx_chr].IsAlive())
                        {
                            Console.Write("Вы мертвы\nНажмите ENTER чтобы продолжить");
                            Console.ReadLine();
                            break;
                        }
                        WriteCharacters(persons);
                        Console.Write("Выберите персонажа которого хотите атаковать: ");
                        int idx_chr_attack = Convert.ToInt32(Console.ReadLine()) - 1;

                        Console.Clear();

                        // Проверка на неправильный выбор атакуемого персонажа
                        if (idx_chr_attack < 0 || idx_chr_attack >= persons.Length)
                        {
                            Console.Write("Такого персонажа не существует\nНажмите ENTER чтобы продолжить");
                            Console.ReadLine();
                            break;
                        }

                        // Проверка на атаку себя
                        else if (idx_chr == idx_chr_attack)
                        {
                            Console.Write("Вы не можете атаковать самого себя\nНажмите ENTER чтобы продолжить");
                            Console.ReadLine();
                            break;
                        }

                        // Проверка на позиции персонажей
                        else if (persons[idx_chr].GetCoords()[0] != persons[idx_chr_attack].GetCoords()[0] ||
                            persons[idx_chr].GetCoords()[1] != persons[idx_chr_attack].GetCoords()[1])
                        {
                            Console.Write("Персонажи находятся в разных местах\nНажмите ENTER чтобы продолжить");
                            Console.ReadLine();
                            break;
                        }

                        // Проверка на мертвого атакуемого персонажа
                        else if (!persons[idx_chr_attack].IsAlive())
                        {
                            Console.Write("Враг уже мертв\nНажмите ENTER чтобы продолжить");
                            Console.ReadLine();
                            break;
                        }

                        // Проверка на союзника
                        else if (persons[idx_chr].IsFriend() == persons[idx_chr_attack].IsFriend() )
                        {
                            Console.Write("Вы не можете атаковать союзника\nНажмите ENTER чтобы продолжить");
                            Console.ReadLine();
                            break;
                        }

                        // Переход персонажей в состояние сражения
                        persons[idx_chr].ChangeFight(true);
                        persons[idx_chr_attack].ChangeFight(true);

                        // Нанесение урона врагу
                        persons[idx_chr].Damage(persons[idx_chr_attack]);
                        Console.WriteLine($"Вы нанесли {persons[idx_chr].GetDamage()} ед. урона\n");

                        // Проверка на убитого врага, если жив - то атакует в ответ
                        if (persons[idx_chr_attack].IsAlive())
                        {
                            // Враг наносит урон игровому персонажу
                            persons[idx_chr_attack].Damage(persons[idx_chr]);
                            Console.WriteLine($"Враг нанес вам {persons[idx_chr_attack].GetDamage()} ед. урона");
                        }
                        else
                        {
                            Console.WriteLine("Враг был повержен");
                            persons[idx_chr].ChangeFight(false);
                            persons[idx_chr_attack].ChangeFight(false);

                        }

                        // Проверка на убитого игрового персонажа
                        if (!persons[idx_chr].IsAlive())
                        {
                            Console.WriteLine("Вы были повержены");
                            persons[idx_chr].ChangeFight(false);
                            persons[idx_chr_attack].ChangeFight(false);

                        }

                        Console.Write("\nНажмите ENTER чтобы продолжить");
                        Console.ReadLine();

                        break;
#if DEBUG
                    /// DEBUG
                    case ConsoleKey.D1:
                        Console.Clear();
                        WriteCharacters(persons);
                        Console.Write("Выберите персонажа: ");
                        int idx_chr_dmg = Convert.ToInt32(Console.ReadLine()) - 1;
                        Console.Write("Введите количество урона: ");
                        int dmg = Convert.ToInt32(Console.ReadLine());
                        Character.SetDamage(persons[idx_chr_dmg], dmg);
                        break;

                    case ConsoleKey.D2:
                        Console.Clear();
                        WriteCharacters(persons);
                        Console.Write("Выберите персонажа: ");
                        int idx_chr_hp = Convert.ToInt32(Console.ReadLine()) - 1;
                        Console.Write("Введите здоровье: ");
                        int hp = Convert.ToInt32(Console.ReadLine());
                        Character.SetHp(persons[idx_chr_hp], hp);
                        break;
#endif

                    default:
                        break;
                }


            }

        }
    }
}