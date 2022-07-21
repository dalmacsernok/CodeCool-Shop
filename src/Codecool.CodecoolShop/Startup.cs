using EmailService;
using Codecool.BookDb.Manager;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codecool.CodecoolShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();

            services.AddSingleton(emailConfig);

            services.AddScoped<IEmailSender, EmailSender>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Product/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=Index}/{filter?}/{id?}");
            });

            SetupInMemoryDatabases();
        }

        private void SetupInMemoryDatabases()
        {
            IProductDao productDataStore = ProductDaoMemory.GetInstance(new BookDbManager().ConnectionString, new BookDbManager().Mode);
            IGenreDao genreDataStore = GenreDaoMemory.GetInstance(new BookDbManager().ConnectionString, new BookDbManager().Mode);
            IPublisherDao publisherDataStore = PublisherDaoMemory.GetInstance(new BookDbManager().ConnectionString, new BookDbManager().Mode);
            IOrderDao orderDataStore = OrderDaoMemory.GetInstance(new BookDbManager().ConnectionString, new BookDbManager().Mode);

            Genre Horror = new Genre
            {
                Name = "HorrorMemory"
            };

            Genre Fantasy = new Genre
            {
                Name = "Fantasy"
            };

            Genre ScienceFiction = new Genre
            {
                Name = "Science Fiction"
            };

            Genre Adventure = new Genre
            {
                Name = "Adventure"
            };

            Genre Biography = new Genre
            {
                Name = "Biography"
            };

            Genre Literature = new Genre
            {
                Name = "Literature"
            };

            genreDataStore.Add(Horror);
            genreDataStore.Add(Fantasy);
            genreDataStore.Add(ScienceFiction);
            genreDataStore.Add(Adventure);
            genreDataStore.Add(Biography);
            genreDataStore.Add(Literature);

            Publisher Helikon = new Publisher
            {
                Name = "Helikon"
            };

            Publisher Könyvmolyképző = new Publisher
            {
                Name = "Könyvmolyképző"
            };

            Publisher Cartaphilus = new Publisher
            {
                Name = "Cartaphilus"
            };

            Publisher Geopen = new Publisher
            {
                Name = "Geopen"
            };

            Publisher Kozmosz = new Publisher
            {
                Name = "Kozmosz"
            };

            Publisher Agave = new Publisher
            {
                Name = "Agave"
            };

            Publisher MetropolisMedia = new Publisher
            {
                Name = "Metropolis Media"
            };

            Publisher Holnap = new Publisher
            {
                Name = "Holnap"
            };

            Publisher Európa = new Publisher
            {
                Name = "Európa"
            };

            Publisher Fumax = new Publisher
            {
                Name = "Fumax"
            };

            Publisher Maxim = new Publisher
            {
                Name = "Maxim"
            };

            Publisher Gabo = new Publisher
            {
                Name = "GABO"
            };

            Publisher Hermész = new Publisher
            {
                Name = "Hermész"
            };

            Publisher Phoenix = new Publisher
            {
                Name = "Phoenix"
            };

            publisherDataStore.Add(Helikon);
            publisherDataStore.Add(Könyvmolyképző);
            publisherDataStore.Add(Cartaphilus);
            publisherDataStore.Add(Geopen);
            publisherDataStore.Add(Kozmosz);
            publisherDataStore.Add(Agave);
            publisherDataStore.Add(MetropolisMedia);
            publisherDataStore.Add(Holnap);
            publisherDataStore.Add(Európa);
            publisherDataStore.Add(Fumax);
            publisherDataStore.Add(Maxim);
            publisherDataStore.Add(Gabo);
            publisherDataStore.Add(Hermész);
            publisherDataStore.Add(Phoenix);

            productDataStore.Add(new Product { Author = "Frank Herbert", Name = "Dűne", DefaultPrice = 5299.0m, Currency = "HUF", Description = "Az ​univerzum legfontosabb terméke a fűszer, amely meghosszabbítja az életet, lehetővé teszi az űrutazást, és élő számítógépet csinál az emberből. Az emberlakta világokat uraló Impériumban azé a hatalom, aki a fűszert birtokolja. A Padisah Császár, a bolygókat uraló Nagy Házak, az Űrliga és a titokzatos rend, a Bene Gesserit kényes hatalmi egyensúlyának, a civilizáció egészének záloga, hogy a fűszerből nem lehet hiány.", Genre = ScienceFiction, Publisher = Kozmosz, ReleaseDate = 1987 });

            productDataStore.Add(new Product { Author = "Philip K. Dick", Name = "A halál útvesztője", DefaultPrice = 2788.0m, Currency = "HUF", Description = "A LEGKEGYETLENEBB KÉRDÉS: ÉLNI VAGY ÉLNI HAGYNI? A Delmak–O különös telep egy különös bolygón, ahol furcsa társaság verődik össze, eltérő képzettségű és látásmódú emberek, akik nagyrészt elöljáróik utasítására jöttek ide, bár akad olyan is, aki az ima erejével. Amikor a csoport tagjai megpróbálják kideríteni a telep voltaképpeni célját, mégpedig a felettük keringő műhold segítségével, megoldhatatlan technikai problémába ütköznek. Csapdába esnek...", Genre = ScienceFiction, Publisher = Agave, ReleaseDate = 2021 });

            productDataStore.Add(new Product { Author = "Arthur C. Clarke", Name = "2001 - Űrodisszeia", DefaultPrice = 2340.0m, Currency = "HUF", Description = "Képtelenség-e, hogy a mi helyi világmindenségünk, a Tejút csillagait is benépesítik velünk egyenlő vagy nálunk különb lények? Képtelenség-e, hogy egy napon a csillagok közt összetalálkozunk velük? Ezt a kérdést teszi fel Clarke könyve előszavában, amely lebilincselően él egy ugyancsak általa megfogalmazott gondolattal: „A lehetséges határait csak egyetlen módon fedezhetjük fel, ha megkockáztatjuk, hogy kevéssel túl is haladunk rajta, a lehetetlenbe.", Genre = ScienceFiction, Publisher = MetropolisMedia, ReleaseDate = 2015 });

            productDataStore.Add(new Product { Author = "Jules Verne", Name = "Nemo Kapitány", DefaultPrice = 2465.0m, Currency = "HUF", Description = "Ki volt Nemo kapitány? Az óceánok titokzatos ura, aki már évszázaddal ezelőtt rendelkezett modern korunk technikai vívmányaival. Zseniális tudós, aki az emberiség üdvét szolgáló fölfedezéseivel bujdokolt a világ elől. Jules Verne halhatatlan fantasztikus regényei közül is kiemelkedik Nemo kapitány víz alatti birodalmának históriája, a Nautilus tengeralattjáró rejtelmes, izgalmas története.", Genre = Adventure, Publisher = Holnap, ReleaseDate = 1987 });

            productDataStore.Add(new Product { Author = "Sir Arthur Conan Doyle", Name = "Az elveszett világ", DefaultPrice = 490.0m, Currency = "HUF", Description = "Challenger professzor, a meglehetősen nehéz természetű, különc tudós elmélete szerint léteznie kell Amazóniában egy \"Elveszett világnak\", ahol terület elzártsága miatt megállt az élővilág evolúciója, s a vidéket ősi, máshol már millió évek óta kihalt lények népesítik be. Az elmélet bizonyítására útnak indul egy kicsi, ám elszánt csapat. Útjuk során azonban nem csupán a vad, ősi természet lényei jelentenek veszélyt...", Genre = Adventure, Publisher = Hermész, ReleaseDate = 2015 });

            productDataStore.Add(new Product { Author = "James Fenimore Cooper", Name = "Az utolsó mohikán", DefaultPrice = 450.0m, Currency = "HUF", Description = "Klasszikus történet egy férfiről, aki hátat fordít az embertelen társadalomnak, s a határvidék vadonjait választja. Natty Bummppo, más néven Sólyomszem bátor és becsületes felderítő önkéntes száműzetésbe vonul, elzárkózik azoktól a normáktól, amelyeket már nem képes elfogadni. Élete kockáztatásával elkíséri Cora és Alice Munró, az angol parancsnok lányait, akik az indiánok megszállta ellenséges területen keresztül apjukhoz igyekeznek.", Genre = Adventure, Publisher = Phoenix, ReleaseDate = 1992 });


            productDataStore.Add(new Product { Author = "Sylvia Plath", Name = "Az üvegbúra", DefaultPrice = 1190.0m, Currency = "HUF", Description = "A tragikusan fiatalon, harmincévesen elhunyt Sylvia Plath (1932-1963) Az üvegbúra című, javarészt önéletrajzi ihletésű, de fiktív részleteket is tartalmazó regényében egy rendkívül tehetséges, ám súlyos pszichés problémákkal küszködő amerikai egyetemista lány – Esther Greenwood – portréját festi meg. Bár első pillantásra Esther a sors kegyeltjének tűnik (nemcsak hogy remek iskolába járhat, de lehetőséget kap arra is, hogy egy hónapig gyakornokként dolgozhasson egy híres New York-i divatlapnál), valójában egyre csak vergődik, fuldoklik az őt körülvevő világ „üvegbúrája” alatt. A depressziója és öngyilkossági kísérlete miatt egy ideig elmegyógyintézetben kezelt (egyebek között elektrosokk-terápiának is alávetett) fiatal lány végül elindul a gyógyulás útján – nem úgy, mint az Esther történetét megíró Sylvia Plath, aki Az üvegbúra megjelenésének évében lett öngyilkos.", Genre = Biography, Publisher = Helikon, ReleaseDate = 2020 });

            productDataStore.Add(new Product { Author = "Charles R. Cross", Name = "A ​mennyeknél súlyosabb", DefaultPrice = 3821.0m, Currency = "HUF", Description = "Kurt Cobain 1994. április 5-én lett öngyilkos. Akarata szerint zárta le rövid, dühödt, fájdalmakkal teli, ám termékeny életét. A mennyeknél súlyosabb ezt az életet kíséri végig a szem- és fültanú hitelességével. Szerzője, Charles R. Cross az egykori Rocket zenei magazin szerkesztője belülről ismeri a seattle-i zenei életet, és a kezdetektől közvetlen közelről követhette Kurt Cobain és a Nirvana pályafutását is. A könyv megírását 4 év kutatómunka és több mint 400 interjú előzte meg. A szerző kivételes hozzáférést kapott Cobain publikálatlan naplóihoz, dalszövegeihez, családi fotóihoz és rengeteg dokumentumhoz, amelyek alapján páratlan részletességgel rekonstruálta az elmúlt évtizedek egyik legnagyobb zenei ikonjának életét az aberdeeni gyerekkortól a híressé váláson át egészen az öngyilkosságig.", Genre = Biography, Publisher = Cartaphilus, ReleaseDate = 2003 });

            productDataStore.Add(new Product { Author = "Kari Hotakainen", Name = "Az ​ismeretlen Kimi Räikkönen", DefaultPrice = 4299.0m, Currency = "HUF", Description = "„Csodálatos ​lenne inkognitóban vezetni a Forma-1-ben” – mondja Kimi. De persze ő is tudja, hogy ilyen világ nem létezik, és nem létezhet. Lehet fűnyírót vezetni inkognitóban, de nem egy hétmillió eurós versenyautót. Kimi Räikkönen, a Jégember a Forma-1 világának egyik legismertebb és legnépszerűbb alakja, és mégis jórészt ismeretlen: szűkszavú megnyilatkozásait imádják a rajongói, akik nyilván szeretnének többet is megtudni erről a fantasztikus sportolóról. Az egyik legnagyszerűbb – és legnépszerűbb – mai finn prózaíró, Kari Hotakainen erre vállalkozott: hogy megismerje az embert a csillogó, lélegzetelállító díszletek mögött.", Genre = Biography, Publisher = Helikon, ReleaseDate = 2018 });

            productDataStore.Add(new Product { Author = "John Steinbeck", Name = "Egerek és emberek", DefaultPrice = 2121.0m, Currency = "HUF", Description = "A kaliforniai Soledad közelében furcsa párt tesz le az autóbusz: George alacsony és fürge észjárású, társa, Lennie lomha, nehézfejű óriás. Egy közeli tanyára tartanak, munkát keresnek. Bár a tanya lakói és munkásai korántsem barátságosak, nincs más választásuk: itt kell maradniuk, ha meg akarják valósítani közös álmukat, hogy vegyenek egy saját tanyát, ahol majd önállóan gazdálkodhatnak. Lennie-t ártatlan együgyűsége folyton bajba sodorja, és George-nak igencsak észnél kell lennie, ha mindkettőjüket meg akarja óvni a következményektől. Barátságuk ritka kincs az őket körülvevő sivár, brutális világban, amely könyörtelenül megsemmisülésre ítél mindent, ami emberi.", Genre = Literature, Publisher = Könyvmolyképző, ReleaseDate = 2016 });

            productDataStore.Add(new Product { Author = "Harper Lee", Name = "Ne ​bántsátok a feketerigót!", DefaultPrice = 3647.0m, Currency = "HUF", Description = "A ​Ne bántsátok a feketerigót! írónőjét feltételezhetően nem kis mértékben ihlették személyes élményei regénye megírásakor. Ezt nemcsak abból a tényből következtethetjük, hogy a regény cselekménye Alabama államban játszódik le, ahol a szerző maga is született, hanem más életrajzi adatokból is. Mint az eseményeket elbeszélő regénybeli leánykának, az írónőnek is ügyvéd volt az édesapja. Harper Lee gyermek- és ifjúkorát szülőföldjén töltötte, ahol valóban realitás mindaz, amit regényében ábrázol, és ez a valóság vérlázító. A regénybeli alabamai kisvárosban ugyanis feltűnésszámba megy az olyan megnyilvánulás, amely a négerek legelemibb jogainak elismerését célozza, különcnek számít az olyan ember, aki síkra mer szállni a színesbőrűek érdekei mellett.", Genre = Literature, Publisher = Geopen, ReleaseDate = 2020 });

            productDataStore.Add(new Product { Author = "J. D. Salinger", Name = "Zabhegyező", DefaultPrice = 1699.0m, Currency = "HUF", Description = "A regény főhőse Holden Caulfield 17 éves amerikai gimnazista, akit éppen a negyedik iskolából rúgtak ki. A cselekmény egy meg nem értett, a társadalmi konvenciókat befogadni és gyakorolni képtelen s ezért mindenünnen kitaszított kamasz fiú háromnapos kálváriája. Holden első személyben mondja el a kicsapása utáni három napjának történetét, melyet New Yorkban éjszakai mulatóhelyeken, kétes hírű szállodában s az utcán tölt el. Közben mindent megpróbál, hogy a világgal, az emberekkel normális kapcsolatot alakítson ki, de sikertelenül. Menekül az emberek elől, de mindenütt hazug embereket talál. Az egyetlen élőlény, akivel őszintén beszélhet, s aki talán meg is érti valamennyire, titokban felkeresett tízéves kishúga. De ő sem tud segíteni: Holden a történteket egy ideggyógyintézet lakójaként meséli el.", Genre = Literature, Publisher = Helikon, ReleaseDate = 2021 });

            productDataStore.Add(new Product { Author = "Stephen King", Name = "Végítélet", DefaultPrice = 6999.0m, Currency = "HUF", Description = "A ​történelem nem szűkölködött tragikus eseményekben: mindent elsöprő árvizek, pusztító tűzvészek, határokat nem ismerő járványok tizedelték az emberiséget. Ám minden elemi csapásnál nagyobb fenyegetést jelent önmaga számára az ember. A tudomány és a technika fejlődésével már-már tökélyre fejlesztette a pusztítás eszközeit, s szinte törvényszerű, hogy előbb-utóbb kicsúszik a kezéből az ellenőrzés. Visszaélt tudásával és hatalmával, megérett a bűnhődésre...", Genre = Horror, Publisher = Európa, ReleaseDate = 2021 });

            productDataStore.Add(new Product { Author = "Paul Tremblay", Name = "Szellemek a fejben", DefaultPrice = 3280.0m, Currency = "HUF", Description = "A ​new england-i Barrett család élete romokba dől, amikor a tizennégy éves Marjorie akut skizofrénia jeleit kezdi mutatni. Az orvosok sehogy sem tudják megakadályozni a lány süllyedését az őrületbe, így Barrették tehetetlenségükben a hitbe menekülnek: a helyi katolikus paphoz fordulnak segítségért. Wanderly atya némi vizsgálódást követően ördögűzést javasol, mert úgy hiszi, hogy Marjorie nem beteg, hanem a gonosz szállta meg. Felveszi a kapcsolatot egy helyi műsorgyártó céggel, akik médiaszenzációt sejtve felajánlják, hogy valóságshow-t készítenek Barrették megpróbáltatásaiból. A család végül a pénz miatt kénytelen belemenni a dologba, és így veszi kezdetét A megszállottság című show. A műsor hatalmas népszerűségre tesz szert, de kisvártatva leállítják, mert a családi házban rémisztő tragédia történik.", Genre = Horror, Publisher = Agave, ReleaseDate = 2016 });

            productDataStore.Add(new Product { Author = "Jack Ketchum", Name = "A szomszéd lány", DefaultPrice = 3280.0m, Currency = "HUF", Description = "Külvárosi környék az 1950-es években. Árnyékos, fák szegélyezte utcák, gondosan ápolt pázsit, kényelmes kis otthonok. Kellemes, békés környezet ahhoz, hogy itt nőjön fel az ember. Kivéve a tinédzser Megnek és mozgássérült húgának, Susannek. Valahol egy zsákutcában, a Chandler család sötét, nedves pincéjében a két lány saját nagynénjük áldozatává válik, kiszolgáltatva a teljes őrületbe merülő távoli rokon kegyetlen rigolyáinak és dührohamainak. Olyan őrület ez, amely megfertőzi a nő három fiát is – végül pedig az egész környéket. Egyedül a szomszédban élő, tizenkét éves David ütközik csak meg a történteken, tétován ingadozva a két lány és azok könyörtelen, vad kínzóinak tettei között. Ennek a fiúnak végül egy határozottan felnőtt döntést kell meghoznia.", Genre = Horror, Publisher = Agave, ReleaseDate = 2021 });

            productDataStore.Add(new Product { Author = "Mark Lawrence", Name = "A Széthullott Birodalom", DefaultPrice = 7495.0m, Currency = "HUF", Description = "A ​teljes Széthullott Birodalom-trilógia (Tövisek Hercege, Tövisek Királya, Tövisek Császára) egyetlen, omnibusz-kötetben! Óvakodj a Tövisek Hercegétől… Kilenc évesen végignézte, ahogy anyját és öccsét meggyilkolják. Tizenhárom évesen már egy vérszomjas rablóbanda vezére. Tizenöt évesen király akar lenni… És ez még csak a kezdet. De bármennyire megingathatatlan is az akaratereje, legyőzheti-e egyetlen fiatalember az elképzelhetetlen hatalommal bíró ellenséget?", Genre = Fantasy, Publisher = Fumax, ReleaseDate = 2017 });

            productDataStore.Add(new Product { Author = "Laini Taylor", Name = "A különös álmodozó", DefaultPrice = 3499.0m, Currency = "HUF", Description = "Az ​álom választja az álmodót, nem fordítva – és az ifjú Lazlo Strange, a háborúban elárvult segédkönyvtáros mindig is attól tartott, hogy az ő álma bizony rosszul választott. Ötéves kora óta megszállottja egy rég elveszett, rejtélyes városnak, de a fiatalember nem elég vakmerő ahhoz, hogy nekiinduljon felkutatni a legendás helyet, átszelve a fél világot. Ám amikor döbbenetes lehetőséget kínál fel számára az Istenölő nevű hős és legendás harcosokból álló csapata, Lazlo úgy dönt, meg kell ragadnia ezt az esélyt, különben örökre elveszíti az álmát. Mi történt a mitikus városban kétszáz évvel ezelőtt, ami elvágta a világtól? És ki lehet a kék bőrű istennő, aki újra meg újra felbukkan Lazlo álmaiban? A válaszokat az ősi város rejti…", Genre = Fantasy, Publisher = Maxim, ReleaseDate = 2018 });

            productDataStore.Add(new Product { Author = "Brian Staveley", Name = "A császár pengéi", DefaultPrice = 3990.0m, Currency = "HUF", Description = "A császárt meggyilkolták, és az Annur Birodalom rendje felborult. A halott uralkodó három gyermekének azonban a gyász mellett egy mélyre hatoló összeesküvéssel is meg kell birkóznia. A császár lánya, Adare a fővárosban hajszolja apja gyilkosát, akit azonban a császári család istennője látszik pártolni, és Adare hamarosan magára marad az udvar intrikái közt. Öccsét, Valynt, aki a birodalom legveszélyesebb alakulatánál hadapród, egy óceán választja el a palotától, azonban több gyanús baleset után ráeszmél, hogy az ő élete is veszélyben van – akárcsak Kadené, a trónörökösé, aki egy messzi kolostorban tanulmányozza az Üres Isten szerzeteseinek ősi tudását. Ám mielőtt Valyn Kaden segítségére siethetne, még ki kell állnia az elit hadsereg titokzatos és halálos próbáját.", Genre = Fantasy, Publisher = Gabo, ReleaseDate = 2015 });

            Order order = new Order();
            orderDataStore.Add(order);


        }
    }
}
