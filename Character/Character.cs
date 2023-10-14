using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Character
{
    internal class Character
    {
        private readonly string? name; // Имя персонажа
        private int x, y; // Координата по горизонтали/вертикали
        private bool camp; // Лагерь
        private int health; // Здоровье
        private readonly int max_health; // Максимальное здоровье
        private int dmg; // Урон
        private bool inFight; // Сражается ли персонаж

        public Character(string? name, int x, int y, bool camp, int health)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.camp = camp;
            this.health = health;
            max_health = health;
            dmg = new Random().Next(7, 20);
            inFight = false;
        }

        /// <summary>
        /// Вывод информации по персонажу
        /// </summary>
        public void Out()
        {
            Console.WriteLine($"Имя персонажа: {name}\n" +
            $"Текущая позиция: [{x} ; {y}]\n" +
            $"Здоровье: {health}\n" +
            $"Наносимый урон: {dmg}");

            if ( camp ) Console.WriteLine($"Принадлежность лагерю: друг\n");
            else Console.WriteLine($"Принадлежность лагерю: враг\n");

        }

        /// <summary>
        /// Передвижение персонажа по горизонтали 
        /// <param name="dx"></param>
        /// </summary>
        public void MoveX(int dx) => x = dx;

        /// <summary>
        /// Передвижение персонажа по вертикали
        /// </summary>
        /// <param name="dy"></param>
        public void MoveY(int dy) => y = dy;

        /// <summary>
        /// Удаление персонажа | Фатальная смерть
        /// </summary>
        public void Delete() => health = 0;

        /// <summary>
        /// Уменьшение здоровье персонажа
        /// </summary>
        /// <param name="character"></param>
        public void Damage(Character character)
        {
            character.health -= dmg;
            if (character.health < 0) character.health = 0;
        }

        /// <summary>
        /// Восстанавливает здоровье персонажа на заданное значение
        /// </summary>
        /// <param name="hp"></param>
        public void Heal(int hp)
        {
            if (health + hp > max_health)
                health = max_health;
            else health = hp;
        }

        /// <summary>
        /// Восстанавливает здоровье персонажа полностью
        /// </summary>
        public void FullHeal()
        {
            if (health > 0)
                health = max_health;
        }

        // Побочные методы

        public void ChangeCamp()
        {
            if (health > 0)
                camp = !camp;
        }

        public bool IsFriend() => camp;
        
        public int[] GetCoords() => new int[] {x, y};

        public int GetDamage() => dmg;

        public bool IsAlive() => health > 0;

        public bool InFight() => inFight;

        public void ChangeFight(bool fight) => inFight = fight;

        [Conditional("DEBUG")]
        static public void SetDamage(Character character, int dmg) => character.dmg = dmg;
        
        [Conditional("DEBUG")]
        static public void SetHp(Character character, int hp) => character.health = hp;


    }
}
