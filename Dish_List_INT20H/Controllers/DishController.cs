using Dish_List_INT20H.Database;
using Dish_List_INT20H.Models;
using Dish_List_INT20H.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dish_List_INT20H.Controllers
{
    public static class DishController
    {
        public async static Task<GetItemsList<Dish>> GetDishesAsync(string? filterNarionality, int? filterComplaxity, int? filterMinTime, int? filterMaxTime, Guid? userId)
        {
            List<Dish> dishes = new List<Dish>();
            using (var db = new AppDbContext())
            {
                dishes = await db.Dishes
                    .Include(x => x.Products)
                    .ThenInclude(pt => pt.Product)
                    .ThenInclude(ct => ct.Category)
                    .Include(x => x.Nationality)
                    .ToListAsync();
            }

            dishes = filterNarionality != null ? dishes.Where(x => x.Nationality.Title == filterNarionality).ToList() : dishes;
            dishes = filterComplaxity != null ? dishes.Where(x => x.Complexity == filterComplaxity).ToList() : dishes;
            dishes = filterMinTime != null ? dishes.Where(x => x.CookingTime >= filterMinTime).ToList() : dishes;
            dishes = filterMaxTime != null ? dishes.Where(x => x.CookingTime <= filterMaxTime).ToList() : dishes;
            dishes = userId != null ? await GetDishesFromUserProducts(dishes, Guid.Parse(userId.ToString())) : dishes;

            GetItemsList<Dish> responseBody = new GetItemsList<Dish>(dishes);

            return responseBody;
        }

        public async static Task<List<Dish>> GetDishesFromUserProducts(List<Dish> dishesList, Guid userId)
        {
            var userProducts = await UserController.GetUsersProduct(userId);
            List<Dish> dishesWithOneProductsFromUser = new List<Dish>();
            foreach (var product in userProducts)
            {
                var dishOneProductSimililarity = dishesList.Where(x => x.Products.Any(y => y.Product.Id == product.Product.Id && y.Quantity <= product.Quantity)).ToList();
                dishesWithOneProductsFromUser.AddRange(dishOneProductSimililarity);
            }
            dishesWithOneProductsFromUser = dishesWithOneProductsFromUser.Distinct().ToList();
            dishesWithOneProductsFromUser = dishesWithOneProductsFromUser.Where(x => x.Products.Count <= userProducts.Count).ToList();


            List<Dish> dishesCanCook = new List<Dish>();
            foreach (var dish in dishesWithOneProductsFromUser)
            {
                var canCook = false;
                var dishProducts = dish.Products.ToList();
                for(int i = 0; i < dishProducts.Count; i++)
                {
                    canCook = userProducts.Any(x => x.Product.Id == dishProducts[i].Product.Id && x.Quantity >= dishProducts[i].Quantity);
                    if (canCook)
                    {
                        if(i == dishProducts.Count - 1)
                        {
                            dishesCanCook.Add(dish);
                        } 
                    }
                    else
                    {
                        break;
                    }
                }

            }

            return dishesCanCook;
        }

        public async static Task<Dish> GetDishByIdAsync(Guid id)
        {
            using (var db = new AppDbContext())
            {
                return await db.Dishes.Where(x => x.Id == id)
                    .Include(x => x.Products)
                    .ThenInclude(pt => pt.Product)
                    .ThenInclude(ct => ct.Category)
                    .Include(x => x.Nationality)
                    .FirstOrDefaultAsync();
            }
        }

        #region TECH
        public async static void GenerateInstatceDishesInDB()
        {
            var teriyakiSalmonWithHoneyTitles = new string[]
            { "Кунжут","Саке","Соєвий соус","Оливкова олія","Лосось"};

            var teriyakiSalmonWithHoneyQuantities = new int[]
           { 10,50,30,15,300};

            var fettuccineАlfredoTitles = new string[]
            {"Вершкове масло", "Чорний перець","Петрушка","Вершки","Сир пармезан","Спагетті"};
            var fettuccineАlfredoQuantities = new int[]
           {90, 5,5,100,75,450};

            var spaghettiBologneseTitles = new string[]
            {"Вустерширський соус",  "Орегано", "Бульйон з яловичини", "Томатна паста", "Сир пармезан", "Спагетті", "Фарш яловичини", "Помідори", "Гриби печериці", "Оливкова олія", "Зубчиків часнику",  "Цибуля"};
            var spaghettiBologneseQuantities = new int[]
            {30, 5, 300, 200, 100, 350, 500, 400, 90, 15, 2, 2};

            var salmonAvocadoSaladTitles = new string[]
            {"Лосось", "Лайм", "Оливкова олія", "Мед", "Авокадо", "Огірок", "Шпинат" };
            var salmonAvocadoSaladQuantities = new int[]
             {400, 1, 45, 10, 150, 50, 400};

            var mandaziTitles = new string[]
            {"Борошно", "Цукор", "Яйця", "Молоко"};
            var mandaziQuantities = new int[]
             {750, 90, 2, 250};
            var kumpirTitles = new string[]
            {"Сир пармезан", "Цибуля", "Вершкове масло", "Картопля", "Перець чилі", "Болгарський перець"};
            var kumpirQuantities = new int[]
            {150, 1, 80, 300, 10, 50};

            var carbonaraTitles = new string[]
            {"Спагетті", "Чорний перець","Сіль","Бекон","Сир пармезан","Яєчні жовтки"};
            var carbonaraQuantities = new int[]
            {30, 5, 5, 150, 50, 6};

            var test = new string[]
           {"Вершкове масло 85", "Цибуля 1", "Лосось 350", "оливкова Олія 30", "Болгарський перець"};


            var db = new AppDbContext();
            List<Dish> dishes = new List<Dish>();
            dishes.Add(new Dish()
            {
                Title = "Лосось теріякі з медом",
                Rate = 5,
                Recipe = "Змішайте всі інгредієнти в медовій глазурі теріякі.\r\nЗбийте, щоб добре перемішати.\r\nЗ’єднайте лосось і глазур.\r\nРозігрійте сковороду на середньо-повільному вогні.\r\nДодайте олію, обсмажте лосось з обох боків до повної готовності всередині та густоти глазурі.\r\nПрикрасьте кунжутом і відразу подавайте.\r\n",
                Descriprion = "Японська кухня вже давно і міцно увійшла до нашого гастрономічного життя, впровадивши в неї такі чудові соуси, як соєвий та теріяки. Якщо з першим все гранично ясно і зрозуміло (купив пляшечку - є соус, пошкодував грошенят - сиди і лопай майонез з кетчупом), то другий, теріяки, ми часом робимо, навіть не знаючи про це. Змішали куплений соєвий соус із цукром, додали імбир та часник, і ось вам полегшений варіант теріяку. До справжнього теріяку, зробленого за всіма правилами, входить ще й мирин – рисове вино, яке використовується тільки для кулінарних цілей. Ми можемо бути менш акуратними і замінити його саке або зовсім білим вином. А можна взагалі без алкоголю обійтися.",
                Complexity = 5,
                CookingTime = 40,
                Image = "teriyakiSalmonWithHoney.jpg",
                Nationality = FindOrCreateNationalities(array: db.DishNationalities, "Італійська"),
                Products = AddProductsByListTitles(db.Products, teriyakiSalmonWithHoneyTitles, teriyakiSalmonWithHoneyQuantities)
            });
            dishes.Add(new Dish()
            {
                Title = "Паста карбонара",
                Rate = 5,
                Recipe = "КРОК 1 Поставте велику каструлю з водою для кип’ятіння.\r\nКРОК 2 Дрібно наріжте 100 г панчетти, попередньо видаливши шкірку.\r\nДрібно натріть 50 г сиру пекоріно та 50 г пармезану та змішайте.\r\nКРОК 3 Збийте 3 великих яйця в середню миску та приправте невеликою кількістю свіжотертого чорного перцю.\r\nВідставте все в сторону.\r\nКРОК 4 Додайте 1 чайну ложку солі в киплячу воду, додайте 350 г спагетті і, коли вода знову закипить, варіть на повільному вогні під кришкою 10 хвилин або поки вони не стануть аль денте (щойно готові).\r\nКРОК 5 Роздушіть 2 очищені пухкі зубчики часнику лезом ножа, просто подрібніть їх.\r\nКРОК 6 Поки спагетті варяться, обсмажте панчетту з часником.\r\nНасипте 50 г несолоного масла у велику сковороду або вок і, щойно масло розтане, додайте панчетту та часник.\r\nКРОК 7 Залиште варитися на середньому вогні приблизно 5 хвилин, часто помішуючи, поки панчетта не стане золотистою та хрусткою.\r\nЧасник надав свій смак, тому вийміть його шумівкою та викиньте.\r\nКРОК 8 Знизьте вогонь під панчеттою.\r\nКоли паста буде готова, витягніть її з води за допомогою виделки або щипців і покладіть на сковороду разом з панчеттою.\r\nНе хвилюйтеся, якщо в каструлю також капне трохи води (ви хочете, щоб це сталося), і поки не викидайте воду для пасти.\r\nКРОК 9 Змішайте більшу частину сиру з яйцями, залишивши невелику жменю, щоб посипати її пізніше.\r\nКРОК 10 Зніміть сковороду зі спагетті та панчеттою з вогню.\r\nТепер швидко влийте яйця і сир.\r\nЗа допомогою щипців або довгої виделки підніміть спагетті, щоб вони легко змішалися з яєчною сумішшю, яка густіє, але не збивається, і все покривається.\r\nКРОК 11 Додайте додаткову кількість води для варіння макаронів, щоб вони залишалися пикантними (це буде достатньо кількох столових ложок).\r\nВи не хочете, щоб воно було мокрим, просто вологим.\r\nПриправити невеликою кількістю солі, якщо потрібно.\r\nКРОК 12 Використовуйте виделку з довгими зубцями, щоб накрутити пасту на тарілку або миску для подачі.\r\nПодавайте негайно, трохи посипавши сиром, що залишився, і тертим чорним перцем.\r\nЯкщо страва трохи підсохне перед подачею, додайте ще гарячої води для пасти, і глянцева соковитість відновиться.",
                Descriprion = "Паста алла карбонара — спагеті з дрібними шматочками гуанчале, змішані з соусом з яєць, сиру пармезан та пекоріно романо, солі і свіжезмеленого чорного перцю. Цей соус доходить до повної готовності від тепла щойно звареної пасти. Гуанчале нерідко замінюється панчетою. Страву винайдено в середині XX століття",
                Complexity = 5,
                CookingTime = 40,
                Image = "Carbonara.jpg",
                Nationality = FindOrCreateNationalities(array: db.DishNationalities, "Італійська"),
                Products = AddProductsByListTitles(db.Products, carbonaraTitles, carbonaraQuantities)
            });
            dishes.Add(new Dish()
            {
                Title = "Фетучіні Альфредо",
                Rate = 5,
                Recipe = "Відваріть макарони відповідно до інструкцій на упаковці у великій каструлі з киплячою водою та сіллю.\r\nДодайте жирні вершки та масло у велику сковороду на середньому вогні, поки вершки не запухають і масло не розтане.\r\nЗбийте пармезан і додайте приправи (сіль і чорний перець).\r\nДайте соусу трохи загуснути, а потім додайте макарони та перемішайте, поки вони не покриються соусом.\r\nПрикрашаємо петрушкою, і готово.",
                Descriprion = "Переклад з англійської-Феттучіні Альфредо або фетучіні аль ослик — це італійська паста зі свіжих фетучіні, помішаних маслом і сиром пармезан. Коли сир плавиться, він емульгує рідини, утворюючи гладкий і насичений сирний соус, який покриває макарони.",
                Complexity = 5,
                CookingTime = 40,
                Image = "FettuccineАlfredo.jpg",
                Nationality = FindOrCreateNationalities(array: db.DishNationalities, "Італійська"),
                Products = AddProductsByListTitles(db.Products, fettuccineАlfredoTitles, fettuccineАlfredoQuantities)
            });
            dishes.Add(new Dish()
            {
                Title = "Спагетті болоньєзе",
                Rate = 5,
                Recipe = "Відваріть макарони відповідно до інструкцій на упаковці у великій каструлі з киплячою водою та сіллю.\r\nДодайте жирні вершки та масло у велику сковороду на середньому вогні, поки вершки не запухають і масло не розтане.\r\nЗбийте пармезан і додайте приправи (сіль і чорний перець).\r\nДайте соусу трохи загуснути, а потім додайте макарони та перемішайте, поки вони не покриються соусом.\r\nПрикрашаємо петрушкою, і готово.",
                Descriprion = "Переклад з англійської-Феттучіні Альфредо або фетучіні аль ослик — це італійська паста зі свіжих фетучіні, помішаних маслом і сиром пармезан. Коли сир плавиться, він емульгує рідини, утворюючи гладкий і насичений сирний соус, який покриває макарони.",
                Complexity = 5,
                CookingTime = 40,
                Image = "pastaBoloniese.jpg",
                Nationality = FindOrCreateNationalities(array: db.DishNationalities, "Італійська"),
                Products = AddProductsByListTitles(db.Products, fettuccineАlfredoTitles, fettuccineАlfredoQuantities)
            });
            dishes.Add(new Dish()
            {
                Title = "Кумпір",
                Rate = 5,
                Recipe = "Якщо ви замовляєте кумпір в Туреччині, спочатку йде стандартна начинка, в картопляне пюре багато вершкового масла, а потім сир.\r\nПотім є ряд інших начинок, на які ви можете просто вказувати досхочу – солодка кукурудза, оливки, салямі, капустяний салат, російський салат, асорті – і ви йдете з надто фаршированою картоплею, тому що вас постійно захоплює вибір. на пропозицію.\r\nНатерти (приблизно – можна скільки завгодно) 150 г сиру.\r\nОдну цибулину і один солодкий червоний перець дрібно наріжте.\r\nПомістіть ці інгредієнти у велику миску, добре посипавши сіллю та перцем, пластівцями чилі (за бажанням).",
                Descriprion = "У Туреччині печена картопля з різними начинками називається Kumpir, і є найпопулярнішою стравою національної кухні. Її полюбляють за ситність, простоту приготування і, звичайно ж, за незабутній смак!",
                Complexity = 5,
                CookingTime = 40,
                Image = "Kumpir.jpg",
                Nationality = FindOrCreateNationalities(array: db.DishNationalities, "Турецька"),
                Products = AddProductsByListTitles(db.Products, kumpirTitles, kumpirQuantities)
            });
            dishes.Add(new Dish()
            {
                Title = "Лососево-авокадний салат",
                Rate = 5,
                Recipe = "Приправте сьомгу, потім натріть олією.\r\nЗмішайте інгредієнти для заправки.\r\nАвокадо розрізати навпіл, очистити від шкірки та нарізати скибочками.\r\nРозріжте огірок уздовж навпіл і на четвертинки, потім наріжте скибочками.\r\nРозподіліть салат, авокадо й огірок між чотирма тарілками, потім полийте половиною заправки.\r\nРозігрійте сковороду з антипригарним покриттям.\r\nДодайте лосось і смажте по 3-4 хвилини з кожного боку, поки він не стане хрустким, але все ще вологим усередині.\r\nПокладіть філе лосося на кожен салат і полийте заправкою, що залишилася.\r\nПодавати теплим.",
                Descriprion = "Цей салат із лососем і авокадо — рецепт здорового салату, який має багато поживних речовин і смаку. Вологий обсмажений лосось викладається поверх шпинату, авокадо, помідорів і червоної цибулі. Потім полити домашнім лимонним вінегретом. Він легкий, але ситний і досить простий, щоб його можна було приготувати менш ніж за 20 хвилин.",
                Complexity = 5,
                CookingTime = 40,
                Image = "SalmonAvocadoSalad.jpg",
                Nationality = FindOrCreateNationalities(array: db.DishNationalities, "Іспанська"),
                Products = AddProductsByListTitles(db.Products, salmonAvocadoSaladTitles, salmonAvocadoSaladQuantities)
            });
            dishes.Add(new Dish()
            {
                Title = "Мандазі домашнього приготування",
                Rate = 5,
                Recipe = "Це один рецепт, який просили багато людей, і я намагався зробити його максимально простим, і сподіваюся, що він вам підійде.\r\nПереконайтеся, що ви використовуєте правильне борошно, яке в основному містить розпушувачі.\r\nВідрегулюйте кількість цукру на свій смак і спробуйте використовувати різні смаки, щоб мати різноманітність, коли вони у вас є.\r\nВи можете використовувати кокосове молоко замість звичайного молока, ви також можете додати сушений кокос до сухого борошна або інших спецій, таких як подрібнена гвоздика або кориця.\r\nЩоб мандазі мали «здоровий вигляд», не розкачуйте тісто надто тонко перед смаженням і використовуйте процедуру, яку я вказав вище.\r\n1.\r\nЗмішайте борошно, корицю та цукор у відповідній мисці.\r\n2.\r\nВ окремій мисці збити яйце з молоком 3.\r\nЗробіть поглиблення в центрі борошна, додайте молоко та яєчну суміш і повільно перемішайте, щоб сформувати тісто.\r\n4.\r\nВимішуйте тісто 3-4 хвилини або доки воно не перестане прилипати до стінок миски й у вас не стане гладка поверхня.\r\n5.\r\nНакрийте тісто вологою серветкою і дайте відпочити 15 хвилин.\r\n6.\r\nРозкачайте тісто на злегка присипаній борошном поверхні в шматок товщиною 1 см.\r\n7.\r\nГострим невеликим ножем наріжте тісто потрібного розміру, відкладіть готове для смаження.\r\n8.\r\nРозігрійте олію у відповідній каструлі та обережно занурте шматочки мандазі, щоб вони смажилися до світло-коричневого кольору з першого боку, а потім переверніть, щоб смажити з другого боку.\r\n9.\r\nПодавайте їх теплими або холодними",
                Descriprion = "Солодке зверху посипається цукром або цукровою пудрою.А можна додати прованські трави, насіння льону, насіння соняшника, і вийде смачний солоний крекер, який потім можна використовувати в якості закуски.",
                Complexity = 5,
                CookingTime = 40,
                Image = "Home-madeMandazi.jpg",
                Nationality = FindOrCreateNationalities(array: db.DishNationalities, "Польська"),
                Products = AddProductsByListTitles(db.Products, mandaziTitles, mandaziQuantities)
            });
            for (int i = 0; i < dishes.Count; i++)
            {
                await db.Dishes.AddAsync(dishes[i]);
            }
            db.SaveChangesAsync();
        }

        public static DishNationalities FindOrCreateNationalities(DbSet<DishNationalities> array, string value)
        {
            var item = array.FirstOrDefault(x => x.Title == value);
            if (item == null)
            {
                return new DishNationalities() { Title = value };
            }
            else
            {
                return item;
            }
        }

        public static List<ProductQuantity> AddProductsByListTitles(DbSet<Product> array, string[] productTitles, int[] teriyakiSalmonWithHoneyQuantities)
        {
            var db = new AppDbContext();
            List<ProductQuantity> productQuantities = new List<ProductQuantity>();

            for (int i = 0; i < productTitles.Length; i++)
            {
                productQuantities.Add(new ProductQuantity()
                {
                    Product = array.FirstOrDefault(x => x.Title == productTitles[i]),
                    Quantity = teriyakiSalmonWithHoneyQuantities[i]
                });
            }

            return productQuantities;
        }

        #endregion
    }
}