# 3 Praktinė Užduotis
Sukurti RSA algoritmo šifravimo/dešifravimo sistemą. Sistemos lange įvedamas tekstas ir užšifruojamas RSA algoritmu. Kitame laukelyje užšifruotas turi būti dešifruojamas. Pradiniai duomenys: pradinis tekstas x. Rezultatas: užšifruotas tekstas y.  

**5 taškai** - realizuoti aplikaciją, kurios funkcionalumas paminėtas pradinėje sąlygoje.  
**4 taškai** už algoritmo realizavimą be bibliotekos pagalbos.  
**2 taškai** - **rezultatas** ir **viešasis raktas** saugojami duomenų bazėje arba failų sistemoje. Nuskaityti duomenų bazės įrašus arba failų turinį norint dešifruoti tekstą.  
**3 taškai** - sistemos lange įvedamas tekstas, viešojo rakto sistemai reikalingi parametrai ir pasirenkama sistemos funkcija šifravimas arba dešifravimas. Pradiniai duomenys: du pirminiai skaičiai **p** ir **q**, pradinis tekstas **x**. 
**1 taškas** - - aplikacijos kodo patalpinimas į GitHub/Gitlab su tarpiniais komentarais (commit).  
  
Užduotį atsiskaityti iki balandžio 9 d. Savaite pavėlavus atsiskaityti praktinį darbą įvertinimas sumažės 30 procentų. Praktinio darbo Github'o nuorodą įkelti į Moodle sistemą.  
  
Programėlės veikimo principas: Įvedamas tekstas, kuris konvertuojamas į ASCII koduotės simbolius (galite naudoti kitas koduotes). Įvesti du pirminiai skaičiai leidžia apskaičiuoti n, o turint n randamas Φ(n). Pasinaudojant Φ(n) apskaičiuojama viešosios eksponentės e reikšmė. Jau turimas viešasis raktas (n,e), kuris bus saugojamas duomenų bazėje. Galima vykdyti šifravimo metodą. Paimama pirmosios raidės ASCII reikšmė ir jai pritaikomas šifravimo metodas ir taip kartojama visam tekstui. Apskaičiuojamas privatusis raktas. Dešifravimo metodui reikalingas papildomas metodas, kuris turėdamas n turi surasti du pirminius skaičius, kurie reikalingi ieškant Φ(n) reikšmės. Turint Φ(n) ir e apskaičiuojamas privatusis raktas. Paimamas dešifruotas tekstas ir kiekvienam ASCII simboliui pritaikomas dešifravimo metodas.
