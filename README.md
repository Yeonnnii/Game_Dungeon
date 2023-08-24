# Game_Dungeon
> C# 콘솔창 RPG 게임
> 
>  (visual studio)


---


### 개발기간
> 23.08.21 ~ 23.08.23


---


### 구현하고 싶은 것
1. 이름과 직업을 고른 뒤 메인 창으로 이동
2. 메인 창에서 정보창, 인벤토리 창 이동
3. 정보창에 지정한 이름과 직업 정보 불러오기
4.  인벤토리 창에서 아이템 장착, 해제
5.  아이템 장착시, 정보창에 정보 업데이트
    
---

### 코드

##### - 선언 이름

불러올 항목 선언


참/거짓 하나씩 생각하다가, 메서드로 묶음

```

 internal class Program
    {
        private static Character player;            // 플레이어 정보를 불러올 것
        private static ItemsArmor itemsArmor;                  // 아이템 정보를 불러올 것
        private static ItemsWeapon itemsWeapon;
        private static ItemsShield itemsSheild;
        private static ItemsShoes itemsShoes;
        private static string PlayerName;            // 플레이어 이름


        static List<string> itemList;       // 아이템 리스트
        static bool[] equippedItems;        // 아이템 착용 여부 (하기의 선언을 압축)

        //static bool equippedArmor = false;
        //static bool equippedWeapon = false;
        //static bool equippedShield = false;
        //static bool equippedShoes = false;

```

##### - main 화면 흐름 구성

정보창에 새로운 방식으로 Data를 불러오려다 실패

while문 빼도 상관없

```
 // main에 선택지와 정보를 불러올 것
        static void Main(string[] args)
        {

            SpartaDisplay();        // 처음 시작화면

            GameDataSetting();      // 화면에 나타낼 데이터

            //DisplayGameIntro();
            while (true)
            {
                DisplayGameIntro();    // 게임 보기 창
            }
        }

```

##### - 시작화면

꾸며보고 싶어서, Chat GPT에게 Hello 그림을 부탁했다.

역슬러시를 외부 C# 코드를 입력할 때 되지만,

visual에서 먹히지 않는다. 역슬래쉬로 안바뀌는 이유?

환경 문제?


```
 static void SpartaDisplay()
        {
              // 문자로 간단하게 그릴 수 있는 것을 깨달음 띄워쓰기 중요

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
            // 캐릭터 이름을 직접 입력받아 저장

            Console.WriteLine($"캐릭터 이름 : {PlayerName}");

            Console.WriteLine("다음 화면으로 넘어가시려면 아무 키나 눌러주세요.");
            Console.ReadKey();        // 키 누르면 프로그램 중지

        }
```

##### - 게임 세팅

이름부터 저장 받고, 직업은 후에 생각
기본 정보 값 저장
추가되는 순서대로 능력치 저장 ( 추가 위치 기억 중요)

```

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

```

##### - 게임 보기 화면

숫자를 입력받아 switch 연결

```
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

```


##### - 상태 보기 화면

저장된 플러에이어 정보 값

```

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



```


##### - 아이템 화면

제일 공들인 메서드..

아이템 리스트에 자동으로 번호 매기기

번호 입력을 잘 받는 것이 확인이 되나,

입력값이 참을 인지 못함 ( 해결 할 문제1 )


```

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
```

##### - 아이템 착용

아이템 리스트 번호를 입력 받을 때, 리스트 착용 문구

아이템 리스트 번호 외에 입력 받았을 때, 문구

  
```

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

```

##### - 저장 데이터 목록

캐릭터 정보

아이템 항목들

```
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

```
