using System.Reflection.Emit;
using System.Xml.Linq;
using System;
using System.Collections.Generic;

namespace ConsoleApp_DungeonGame
{
    internal class Program
    {
        private static Character player;            // 플레이어 정보를 불러올 것
        private static ItemsArmor itemsArmor;                  // 아이템 정보를 불러올 것
        private static ItemsWeapon itemsWeapon;
        private static ItemsShield itemsSheild;
        private static ItemsShoes itemsShoes;
        private static string PlayerName;


        static List<string> itemList;       // 아이템 리스트
        static bool[] equippedItems;        // 아이템 착용 여부


        //static bool equippedArmor = false;
        //static bool equippedWeapon = false;
        //static bool equippedShield = false;
        //static bool equippedShoes = false;


        // main에 선택지와 정보를 불러올 것
        static void Main(string[] args)
        {

            SpartaDisplay();

            GameDataSetting();

            //DisplayGameIntro();
            while (true)
            {
                DisplayGameIntro();
            }
        }


        // 캐릭터의 이름을 받고싶다.
        static void SpartaDisplay()
        {
            // \ 역슬래쉬로 안바뀌는 이유?
            string spartaArt =
                @"
 _    _      _ _
| |  | |    | | |
| |__| | ___| | | ___
|  __  |/ _ \ | |/ _ \
| |  | |  __/ | | (_) |
|_|  |_|\___|_|_|\___/
                                 
";
            Console.WriteLine(spartaArt);

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.Write("캐릭터 명을 입력해 주세요. : ");
            PlayerName = Console.ReadLine();

            Console.WriteLine($"캐릭터 이름 : {PlayerName}");

            Console.WriteLine("다음 화면으로 넘어가시려면 아무 키나 눌러주세요.");
            Console.ReadKey();

        }



        // 게임 세팅
        static void GameDataSetting()
        {
            // 캐릭터 정보 세팅
            player = new Character(PlayerName, "전사", 1, 10, 5, 100, 1500);

            // 아이템 정보 세팅
            itemsArmor = new ItemsArmor(0.5f, 2, 3, 4, 5);
            itemsWeapon = new ItemsWeapon(0.7f, 1, 2);
            itemsSheild = new ItemsShield(1, 2);
            itemsShoes = new ItemsShoes(1, 2);

           

        }






        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");


            // 캐릭터 정보와 인벤토리 선택사항
            int input = CheckValidInput(1, 2);      // 최소,최대 정수값을 받음
            switch (input)
            {
                // 캐릭터 정보 열기
                case 1:
                    DisplayMyInfo();
                    break;

                // 인벤토리 열기
                case 2:
                    DisplayInventory();
                    break;
            }
        }


        // 상태 정보 창
        static void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("■■■■■■■");
            Console.WriteLine("■ 상태보기 ■");
            Console.WriteLine("■■■■■■■");

            Console.WriteLine();

            Console.WriteLine("-------- 캐릭터의 정보 --------");
            Console.WriteLine();
            Console.WriteLine($"    Lv.{player.Level}");            // 플레이어의 레벨
            Console.WriteLine($"    {player.Name} ({player.Job})");  // 플레이어의 이름과 직업
            Console.WriteLine($"    공격력 :{player.Atk}");         // 플레이어의 공격력
            Console.WriteLine($"    방어력 : {player.Def}");        // 플레이어의 방어력
            Console.WriteLine($"    체력 : {player.Hp}");           // 플레이어의 체력
            Console.WriteLine($"    Gold : {player.Gold} G");       // 플레이어의 재산
            Console.WriteLine();
            Console.WriteLine("--------------------------------");
            Console.WriteLine(" 0. 나가기");

            // 0 누르면 main 화면으로
            int input = CheckValidInput(0, 0);

            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }


        // 인벤토리 창
        static void DisplayInventory()
        {
            

            Console.Clear();

            Console.WriteLine("■■■■■■■");
            Console.WriteLine("■ 인벤토리 ■");
            Console.WriteLine("■■■■■■■");

            Console.WriteLine();

            Console.WriteLine("-------- 아이템 목록 --------");
            Console.WriteLine();


            itemList = new List<string>
            {
                $"비닐 조끼        < 방어력 +{itemsArmor.Vinil} > ",
                $"아이언 갑옷      < 방어력 +{itemsArmor.Iron} > ",
                $"목검             < 공격력 +{itemsWeapon.Wood} > ",
                $"돌맹이           < 공격력 +{itemsWeapon.Rock} > ",
                $"나무 방패        < 방어력 +{itemsSheild.Wood} > ",
                $"고무신           < 매력 +{itemsShoes.Rubber} > "
            };


            equippedItems = new bool[itemList.Count]; // equippedItems 배열 초기화


            if (itemList.Count == 0)
            {
                Console.WriteLine("아이템이 없습니다.");
                Console.WriteLine();
                Console.WriteLine("--------------------------------");
                Console.WriteLine(" 0. 나가기");


                // 0 누르면 main 화면으로
                int push = CheckValidInput(0, 0);

                switch (push)
                {
                    case 0:
                        DisplayGameIntro();
                        break;
                }
                return;
            }


            
            // 아이템 목록 번호 매기기
            for (int i = 0; i < itemList.Count; i++)
            {
                string itemName = itemList[i];
                string equippedStatus = equippedItems[i] ? "[착용] " : "[] ";
                
                Console.WriteLine($"{i + 1}. {equippedStatus}{itemName}");




            }



            /// 사용자에게 아이템 번호를 입력하도록 안내
            Console.WriteLine("--------------------------------");
            Console.WriteLine("아이템 번호를 입력하세요.");
            Console.WriteLine("--------------------------------");
            Console.WriteLine(" 0. 나가기");

            int selectedItemIndex = CheckValidInput(0, itemList.Count);

            if (selectedItemIndex == 0)  // 0을 누르면 인벤토리 창을 나가도록 처리
            {
                DisplayGameIntro();
                return;
            }


            // 선택된 아이템의 착용 상태 전환
            ToggleEquipItem(selectedItemIndex - 1);


            // 메인 인벤토리 메뉴로 돌아가기
            DisplayInventory();




        }


        // 아이템 착용 여부 확인
        static void ToggleEquipItem(int index)
        {
 
            equippedItems[index] = !equippedItems[index];


            if (equippedItems[index])
            {
                Console.WriteLine($"{itemList[index]} 아이템을 착용했습니다.");
            }
            else
            {
                Console.WriteLine($"{itemList[index]} 아이템을 해제했습니다.");
            }

            Console.WriteLine("아무 키나 눌러주세요...");
            Console.ReadKey();

            return;

        }

        static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);       // TryParse 숫자로 이루어져 있는지 판단
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }


    public class Character
    {
        // 캐릭터 항목
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
    }

    // 아이템 항목
    public class ItemsArmor
    {
        public float Vinil { get; }
        public int Iron { get; }

        public int Bronze { get; }
        public int Silver { get; }
        public int Gold { get; }

        public ItemsArmor(float vinil, int iron, int bronze, int silver, int gold)
        {
            Vinil = vinil;
            Iron = iron;
            Bronze = bronze;
            Silver = silver;
            Gold = gold;

        }


    }

    public class ItemsWeapon
    {
        public float Rock { get; }
        public int Wood { get; }

        public int Dagger { get; }


        public ItemsWeapon(float rock, int wood, int dagger)
        {
            Rock = rock;
            Wood = wood;
            Dagger = dagger;


        }


    }


    public class ItemsShield
    {
        public int Wood { get; }
        public int Bronze { get; }

        public ItemsShield(int wood, int bronze)
        {
            Wood = wood;
            Bronze = bronze;


        }


    }


    public class ItemsShoes
    {
        public int Rubber { get; }
        public int BasicBoots { get; }

        public ItemsShoes(int rubber, int basicBoots)
        {
            Rubber = rubber;
            BasicBoots = basicBoots;



        }


    }




}