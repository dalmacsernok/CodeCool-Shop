DROP TABLE IF EXISTS book;
DROP TABLE IF EXISTS genre;
DROP TABLE IF EXISTS publisher;
DROP TABLE IF EXISTS order_data;


CREATE TABLE publisher (
    id int IDENTITY(1,1) PRIMARY KEY,
    publisher_name NVARCHAR(30) NOT NULL
);


CREATE TABLE genre (
    id int IDENTITY(1,1) PRIMARY KEY,
    genre_name VARCHAR(30) NOT NULL
);

CREATE TABLE book (
    id int IDENTITY(1,1) PRIMARY KEY,
    title NVARCHAR(40),
    book_description NVARCHAR(1000),
	author NVARCHAR(40),
    genre_id int,
    publisher_id int,
    default_price decimal(6,1),
    release_date smallint,
    img NVARCHAR(100),
    FOREIGN KEY (genre_id) REFERENCES genre(id),
    FOREIGN KEY (publisher_id) REFERENCES publisher(id)
);

CREATE TABLE order_data (
    id int IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(40),
    email NVARCHAR(80),
    phone_number NVARCHAR(40),
    billing_address NVARCHAR(200),
    shipping_address NVARCHAR(200)
)


INSERT INTO genre (genre_name) VALUES
    ('Horror'),
    ('Fantasy'),
    ('Science-Fiction'),
    ('Adventure'),
    ('Biography'),
    ('Literature'),
    (N'Illés')
;

INSERT INTO publisher (publisher_name) VALUES
    ('Helikon'),
    (N'Könyvmolyképző'),
    ('Cartaphilus'),
    ('Geopen'),
    ('Kozmosz'),
    ('Agave'),
    ('Metropolis Media'),
    ('Holnap'),
    (N'Európa'),
    ('Fumax'),
    ('Maxim'),
    ('Gabo'),
    (N'Hermész'),
    ('Phoenix')
;

INSERT INTO book (title, book_description, author, genre_id, publisher_id, default_price, release_date, img) VALUES

    (N'Dűne', N'Az ​univerzum legfontosabb terméke a fűszer, amely meghosszabbítja az életet, lehetővé teszi az űrutazást, és élő számítógépet csinál az emberből. Az emberlakta világokat uraló Impériumban azé a hatalom, aki a fűszert birtokolja. A Padisah Császár, a bolygókat uraló Nagy Házak, az Űrliga és a titokzatos rend, a Bene Gesserit kényes hatalmi egyensúlyának, a civilizáció egészének záloga, hogy a fűszerből nem lehet hiány.', N'Frank Herbert', (SELECT id FROM genre WHERE genre_name = N'Science-Fiction'), (SELECT id FROM publisher WHERE publisher_name = N'Kozmosz'), 5299.0, 1987, N'./wwwroot/img/Dűne.jpg')
    ;

INSERT INTO book (title, book_description, author, genre_id, publisher_id, default_price, release_date, img) VALUES

    (N'A halál útvesztője', N'A LEGKEGYETLENEBB KÉRDÉS: ÉLNI VAGY ÉLNI HAGYNI? A Delmak–O különös telep egy különös bolygón, ahol furcsa társaság verődik össze, eltérő képzettségű és látásmódú emberek, akik nagyrészt elöljáróik utasítására jöttek ide, bár akad olyan is, aki az ima erejével. Amikor a csoport tagjai megpróbálják kideríteni a telep voltaképpeni célját, mégpedig a felettük keringő műhold segítségével, megoldhatatlan technikai problémába ütköznek. Csapdába esnek...', N'Philip K. Dick', (SELECT id FROM genre WHERE genre_name = N'Science-Fiction'), (SELECT id FROM publisher WHERE publisher_name = N'Agave'), 2788.0, 2021, N'./wwwroot/img/A halál útvesztője.jpg')
    ;


INSERT INTO book (title, book_description, author, genre_id, publisher_id, default_price, release_date, img) VALUES

    (N'2001 - Űrodisszeia', N'Képtelenség-e, hogy a mi helyi világmindenségünk, a Tejút csillagait is benépesítik velünk egyenlő vagy nálunk különb lények? Képtelenség-e, hogy egy napon a csillagok közt összetalálkozunk velük? Ezt a kérdést teszi fel Clarke könyve előszavában, amely lebilincselően él egy ugyancsak általa megfogalmazott gondolattal: „A lehetséges határait csak egyetlen módon fedezhetjük fel, ha megkockáztatjuk, hogy kevéssel túl is haladunk rajta, a lehetetlenbe.', N'Arthur C. Clarke', (SELECT id FROM genre WHERE genre_name = N'Science-Fiction'), (SELECT id FROM publisher WHERE publisher_name = N'Metropolis Media'), 2340.0, 2015, N'./wwwroot/img/2001 - Űrodisszeia.jpg')
    ;

INSERT INTO book (title, book_description, author, genre_id, publisher_id, default_price, release_date, img) VALUES

    (N'Nemo Kapitány', N'Ki volt Nemo kapitány? Az óceánok titokzatos ura, aki már évszázaddal ezelőtt rendelkezett modern korunk technikai vívmányaival. Zseniális tudós, aki az emberiség üdvét szolgáló fölfedezéseivel bujdokolt a világ elől. Jules Verne halhatatlan fantasztikus regényei közül is kiemelkedik Nemo kapitány víz alatti birodalmának históriája, a Nautilus tengeralattjáró rejtelmes, izgalmas története.', N'Jules Verne', (SELECT id FROM genre WHERE genre_name = N'Adventure'), (SELECT id FROM publisher WHERE publisher_name = N'Holnap'), 2465.0, 1987, N'./wwwroot/img/Nemo Kapitány.jpg')
    ;

INSERT INTO book (title, book_description, author, genre_id, publisher_id, default_price, release_date, img) VALUES

    (N'Az elveszett világ', N'Challenger professzor, a meglehetősen nehéz természetű, különc tudós elmélete szerint léteznie kell Amazóniában egy "Elveszett világnak", ahol terület elzártsága miatt megállt az élővilág evolúciója, s a vidéket ősi, máshol már millió évek óta kihalt lények népesítik be. Az elmélet bizonyítására útnak indul egy kicsi, ám elszánt csapat. Útjuk során azonban nem csupán a vad, ősi természet lényei jelentenek veszélyt...', N'Sir Arthur Conan Doyle', (SELECT id FROM genre WHERE genre_name = N'Adventure'), (SELECT id FROM publisher WHERE publisher_name = N'Hermész'), 490.0, 2015, N'./wwwroot/img/Az elveszett világ.jpg')
    ;

INSERT INTO book (title, book_description, author, genre_id, publisher_id, default_price, release_date, img) VALUES

    (N'Az utolsó mohikán', N'Klasszikus történet egy férfiről, aki hátat fordít az embertelen társadalomnak, s a határvidék vadonjait választja. Natty Bummppo, más néven Sólyomszem bátor és becsületes felderítő önkéntes száműzetésbe vonul, elzárkózik azoktól a normáktól, amelyeket már nem képes elfogadni. Élete kockáztatásával elkíséri Cora és Alice Munró, az angol parancsnok lányait, akik az indiánok megszállta ellenséges területen keresztül apjukhoz igyekeznek.', N'James Fenimore Cooper', (SELECT id FROM genre WHERE genre_name = N'Adventure'), (SELECT id FROM publisher WHERE publisher_name = N'Phoenix'), 450.0, 1992, N'./wwwroot/img/Az utolsó mohikán.jpg')
    ;

INSERT INTO book (title, book_description, author, genre_id, publisher_id, default_price, release_date, img) VALUES
    (N'Végítélet', N'A ​történelem nem szűkölködött tragikus eseményekben: mindent elsöprő árvizek, pusztító tűzvészek, határokat nem ismerő járványok tizedelték az emberiséget. Ám minden elemi csapásnál nagyobb fenyegetést jelent önmaga számára az ember. A tudomány és a technika fejlődésével már-már tökélyre fejlesztette a pusztítás eszközeit, s szinte törvényszerű, hogy előbb-utóbb kicsúszik a kezéből az ellenőrzés. Visszaélt tudásával és hatalmával, megérett a bűnhődésre...', N'Stephen King', (SELECT id FROM genre WHERE genre_name = N'Horror'), (SELECT id FROM publisher WHERE publisher_name = N'Európa'), 6999.0, 2021, N'./wwwroot/img/Végítélet.jpg')
    ;

INSERT INTO book (title, book_description, author, genre_id, publisher_id, default_price, release_date, img) VALUES
    (N'Szellemek a fejben', N'A ​new england-i Barrett család élete romokba dől, amikor a tizennégy éves Marjorie akut skizofrénia jeleit kezdi mutatni. Az orvosok sehogy sem tudják megakadályozni a lány süllyedését az őrületbe, így Barrették tehetetlenségükben a hitbe menekülnek: a helyi katolikus paphoz fordulnak segítségért. Wanderly atya némi vizsgálódást követően ördögűzést javasol, mert úgy hiszi, hogy Marjorie nem beteg, hanem a gonosz szállta meg. Felveszi a kapcsolatot egy helyi műsorgyártó céggel, akik médiaszenzációt sejtve felajánlják, hogy valóságshow-t készítenek Barrették megpróbáltatásaiból. A család végül a pénz miatt kénytelen belemenni a dologba, és így veszi kezdetét A megszállottság című show. A műsor hatalmas népszerűségre tesz szert, de kisvártatva leállítják, mert a családi házban rémisztő tragédia történik...', N'Paul Tremblay', (SELECT id FROM genre WHERE genre_name = N'Horror'), (SELECT id FROM publisher WHERE publisher_name = N'Agave'), 3280.0, 2016, N'./wwwroot/img/Szellemek a fejben.jpg')
    ;

INSERT INTO book (title, book_description, author, genre_id, publisher_id, default_price, release_date, img) VALUES
    (N'A szomszéd lány', N'Külvárosi környék az 1950-es években. Árnyékos, fák szegélyezte utcák, gondosan ápolt pázsit, kényelmes kis otthonok. Kellemes, békés környezet ahhoz, hogy itt nőjön fel az ember. Kivéve a tinédzser Megnek és mozgássérült húgának, Susannek. Valahol egy zsákutcában, a Chandler család sötét, nedves pincéjében a két lány saját nagynénjük áldozatává válik, kiszolgáltatva a teljes őrületbe merülő távoli rokon kegyetlen rigolyáinak és dührohamainak. Olyan őrület ez, amely megfertőzi a nő három fiát is – végül pedig az egész környéket. Egyedül a szomszédban élő, tizenkét éves David ütközik csak meg a történteken, tétován ingadozva a két lány és azok könyörtelen, vad kínzóinak tettei között. Ennek a fiúnak végül egy határozottan felnőtt döntést kell meghoznia.', N'Jack Ketchum', (SELECT id FROM genre WHERE genre_name = N'Horror'), (SELECT id FROM publisher WHERE publisher_name = N'Agave'), 3280.0, 2021, N'./wwwroot/img/A szomszéd lány.jpg')
    ;

INSERT INTO book (title, book_description, author, genre_id, publisher_id, default_price, release_date, img) VALUES
    (N'A Széthullott Birodalom', N'A ​teljes Széthullott Birodalom-trilógia (Tövisek Hercege, Tövisek Királya, Tövisek Császára) egyetlen, omnibusz-kötetben! Óvakodj a Tövisek Hercegétől… Kilenc évesen végignézte, ahogy anyját és öccsét meggyilkolják. Tizenhárom évesen már egy vérszomjas rablóbanda vezére. Tizenöt évesen király akar lenni… És ez még csak a kezdet. De bármennyire megingathatatlan is az akaratereje, legyőzheti-e egyetlen fiatalember az elképzelhetetlen hatalommal bíró ellenséget?', N'Mark Lawrence', (SELECT id FROM genre WHERE genre_name = N'Fantasy'), (SELECT id FROM publisher WHERE publisher_name = N'Fumax'), 7495.0, 2017, N'./wwwroot/img/A Széthullott Birodalom.jpg')
    ;

INSERT INTO book (title, book_description, author, genre_id, publisher_id, default_price, release_date, img) VALUES
    (N'A különös álmodozó', N'Az ​álom választja az álmodót, nem fordítva – és az ifjú Lazlo Strange, a háborúban elárvult segédkönyvtáros mindig is attól tartott, hogy az ő álma bizony rosszul választott. Ötéves kora óta megszállottja egy rég elveszett, rejtélyes városnak, de a fiatalember nem elég vakmerő ahhoz, hogy nekiinduljon felkutatni a legendás helyet, átszelve a fél világot. Ám amikor döbbenetes lehetőséget kínál fel számára az Istenölő nevű hős és legendás harcosokból álló csapata, Lazlo úgy dönt, meg kell ragadnia ezt az esélyt, különben örökre elveszíti az álmát. Mi történt a mitikus városban kétszáz évvel ezelőtt, ami elvágta a világtól? És ki lehet a kék bőrű istennő, aki újra meg újra felbukkan Lazlo álmaiban? A válaszokat az ősi város rejti...', N'Laini Taylor', (SELECT id FROM genre WHERE genre_name = N'Fantasy'), (SELECT id FROM publisher WHERE publisher_name = 'Maxim'), 3499.0, 2018, N'./wwwroot/img/A különös álmodozó.jpg')
    ;

INSERT INTO book (title, book_description, author, genre_id, publisher_id, default_price, release_date, img) VALUES
    (N'A császár pengéi', N'A császárt meggyilkolták, és az Annur Birodalom rendje felborult. A halott uralkodó három gyermekének azonban a gyász mellett egy mélyre hatoló összeesküvéssel is meg kell birkóznia. A császár lánya, Adare a fővárosban hajszolja apja gyilkosát, akit azonban a császári család istennője látszik pártolni, és Adare hamarosan magára marad az udvar intrikái közt. Öccsét, Valynt, aki a birodalom legveszélyesebb alakulatánál hadapród, egy óceán választja el a palotától, azonban több gyanús baleset után ráeszmél, hogy az ő élete is veszélyben van – akárcsak Kadené, a trónörökösé, aki egy messzi kolostorban tanulmányozza az Üres Isten szerzeteseinek ősi tudását. Ám mielőtt Valyn Kaden segítségére siethetne, még ki kell állnia az elit hadsereg titokzatos és halálos próbáját.', N'Brian Staveley', (SELECT id FROM genre WHERE genre_name = N'Fantasy'), (SELECT id FROM publisher WHERE publisher_name = 'Gabo'), 3990.0, 2015, N'./wwwroot/img/A császár pengéi.jpg')
    ;
INSERT INTO book (title, book_description, author, genre_id, publisher_id, default_price, release_date, img) VALUES
(N'Az üvegbúra', N'A tragikusan fiatalon, harmincévesen elhunyt Sylvia Plath (1932-1963) Az üvegbúra című, javarészt önéletrajzi ihletésű, de fiktív részleteket is tartalmazó regényében egy rendkívül tehetséges, ám súlyos pszichés problémákkal küszködő amerikai egyetemista lány – Esther Greenwood – portréját festi meg. Bár első pillantásra Esther a sors kegyeltjének tűnik (nemcsak hogy remek iskolába járhat, de lehetőséget kap arra is, hogy egy hónapig gyakornokként dolgozhasson egy híres New York-i divatlapnál), valójában egyre csak vergődik, fuldoklik az őt körülvevő világ „üvegbúrája” alatt. A depressziója és öngyilkossági kísérlete miatt egy ideig elmegyógyintézetben kezelt (egyebek között elektrosokk-terápiának is alávetett) fiatal lány végül elindul a gyógyulás útján – nem úgy, mint az Esther történetét megíró Sylvia Plath, aki Az üvegbúra megjelenésének évében lett öngyilkos.', N'Sylvia Plath',(SELECT id FROM genre WHERE genre_name = N'Biography'), (SELECT id FROM publisher WHERE publisher_name = N'Helikon'), 1990.0, 2020, N'./wwwroot/img/Az üvegbúra.jpg');

INSERT INTO book (title, book_description, author, genre_id, publisher_id, default_price, release_date, img) VALUES
     (N'A ​mennyeknél súlyosabb', N'Kurt Cobain 1994. április 5-én lett öngyilkos. Akarata szerint zárta le rövid, dühödt, fájdalmakkal teli, ám termékeny életét. A mennyeknél súlyosabb ezt az életet kíséri végig a szem- és fültanú hitelességével. Szerzője, Charles R. Cross az egykori Rocket zenei magazin szerkesztője belülről ismeri a seattle-i zenei életet, és a kezdetektől közvetlen közelről követhette Kurt Cobain és a Nirvana pályafutását is. A könyv megírását 4 év kutatómunka és több mint 400 interjú előzte meg. A szerző kivételes hozzáférést kapott Cobain publikálatlan naplóihoz, dalszövegeihez, családi fotóihoz és rengeteg dokumentumhoz, amelyek alapján páratlan részletességgel rekonstruálta az elmúlt évtizedek egyik legnagyobb zenei ikonjának életét az aberdeeni gyerekkortól a híressé váláson át egészen az öngyilkosságig.', 'Charles R. Cross',(SELECT id FROM genre WHERE genre_name = N'Biography'), (SELECT id FROM publisher WHERE publisher_name = N'Cartaphilus'), 3821.0, 2003, N'./wwwroot/img/A ​mennyeknél súlyosabb.jpg' );

     INSERT INTO book (title, book_description, author, genre_id, publisher_id, default_price, release_date, img) VALUES
       (N'Az ​ismeretlen Kimi Räikkönen', '„Csodálatos ​lenne inkognitóban vezetni a Forma-1-ben” – mondja Kimi. De persze ő is tudja, hogy ilyen világ nem létezik, és nem létezhet. Lehet fűnyírót vezetni inkognitóban, de nem egy hétmillió eurós versenyautót. Kimi Räikkönen, a Jégember a Forma-1 világának egyik legismertebb és legnépszerűbb alakja, és mégis jórészt ismeretlen: szűkszavú megnyilatkozásait imádják a rajongói, akik nyilván szeretnének többet is megtudni erről a fantasztikus sportolóról. Az egyik legnagyszerűbb – és legnépszerűbb – mai finn prózaíró, Kari Hotakainen erre vállalkozott: hogy megismerje az embert a csillogó, lélegzetelállító díszletek mögött.','Kari Hotakainen',(SELECT id FROM genre WHERE genre_name = N'Biography'), (SELECT id FROM publisher WHERE publisher_name =N'Helikon'), 4299.0,2018, N'./wwwroot/img/Az ​ismeretlen Kimi Räikkönen.jpg' );


       INSERT INTO book (title, book_description, author, genre_id, publisher_id, default_price, release_date, img) VALUES
        (N'Egerek és emberek',N'A kaliforniai Soledad közelében furcsa párt tesz le az autóbusz: George alacsony és fürge észjárású, társa, Lennie lomha, nehézfejű óriás. Egy közeli tanyára tartanak, munkát keresnek. Bár a tanya lakói és munkásai korántsem barátságosak, nincs más választásuk: itt kell maradniuk, ha meg akarják valósítani közös álmukat, hogy vegyenek egy saját tanyát, ahol majd önállóan gazdálkodhatnak. Lennie-t ártatlan együgyűsége folyton bajba sodorja, és George-nak igencsak észnél kell lennie, ha mindkettőjüket meg akarja óvni a következményektől. Barátságuk ritka kincs az őket körülvevő sivár, brutális világban, amely könyörtelenül megsemmisülésre ítél mindent, ami emberi.',N'John Steinbeck',(SELECT id FROM genre WHERE genre_name = N'Literature'), (SELECT id FROM publisher WHERE publisher_name =N'Könyvmolyképző'), 2121.0, 2016, N'./wwwroot/img/Egerek és emberek.jpg');


        INSERT INTO book (title, book_description, author, genre_id, publisher_id, default_price, release_date, img) VALUES
           (N'Ne ​bántsátok a feketerigót!', N'A ​Ne bántsátok a feketerigót! írónőjét feltételezhetően nem kis mértékben ihlették személyes élményei regénye megírásakor. Ezt nemcsak abból a tényből következtethetjük, hogy a regény cselekménye Alabama államban játszódik le, ahol a szerző maga is született, hanem más életrajzi adatokból is. Mint az eseményeket elbeszélő regénybeli leánykának, az írónőnek is ügyvéd volt az édesapja. Harper Lee gyermek- és ifjúkorát szülőföldjén töltötte, ahol valóban realitás mindaz, amit regényében ábrázol, és ez a valóság vérlázító. A regénybeli alabamai kisvárosban ugyanis feltűnésszámba megy az olyan megnyilvánulás, amely a négerek legelemibb jogainak elismerését célozza, különcnek számít az olyan ember, aki síkra mer szállni a színesbőrűek érdekei mellett.',N'Harper Lee', (SELECT id FROM genre WHERE genre_name = N'Literature'), (SELECT id FROM publisher WHERE publisher_name =N'Geopen'), 3647.0, 2020, N'./wwwroot/img/Ne ​bántsátok a feketerigót!.jpg');

           INSERT INTO book (title, book_description, author, genre_id, publisher_id, default_price, release_date, img) VALUES

           ( N'Zabhegyező', N'A regény főhőse Holden Caulfield 17 éves amerikai gimnazista, akit éppen a negyedik iskolából rúgtak ki. A cselekmény egy meg nem értett, a társadalmi konvenciókat befogadni és gyakorolni képtelen s ezért mindenünnen kitaszított kamasz fiú háromnapos kálváriája. Holden első személyben mondja el a kicsapása utáni három napjának történetét, melyet New Yorkban éjszakai mulatóhelyeken, kétes hírű szállodában s az utcán tölt el. Közben mindent megpróbál, hogy a világgal, az emberekkel normális kapcsolatot alakítson ki, de sikertelenül. Menekül az emberek elől, de mindenütt hazug embereket talál. Az egyetlen élőlény, akivel őszintén beszélhet, s aki talán meg is érti valamennyire, titokban felkeresett tízéves kishúga. De ő sem tud segíteni: Holden a történteket egy ideggyógyintézet lakójaként meséli el.',N'J. D. Salinger', (SELECT id FROM genre WHERE genre_name = N'Literature'), (SELECT id FROM publisher WHERE publisher_name =N'Helikon'),1699.0, 2021,N'./wwwroot/img/Zabhegyező.jpg' );