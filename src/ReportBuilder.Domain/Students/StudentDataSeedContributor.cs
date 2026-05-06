using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Uow;

namespace ReportBuilder.Students;

public class StudentDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IStudentRepository _studentRepository;
    private readonly IGuidGenerator _guidGenerator;

    public StudentDataSeedContributor(IStudentRepository studentRepository, IGuidGenerator guidGenerator)
    {
        _studentRepository = studentRepository;
        _guidGenerator = guidGenerator;
    }

    [UnitOfWork]
    public virtual async Task SeedAsync(DataSeedContext context)
    {
        if (await _studentRepository.GetCountAsync() > 0)
        {
            return;
        }

        var students = new[]
        {
            ("Ahmed Al-Rashidi",    "1001234567", StudentGender.Male,   new DateTime(2000, 3, 15), "Street 12, Block 5",  "Saudi Arabia", "Riyadh",      "0501234567", "ahmed.rashidi@example.com",  StudentStatus.Active,    "Grade 10", new DateTime(2018, 9, 1),  3.75m),
            ("Fatima Al-Zahraa",   "1001234568", StudentGender.Female, new DateTime(2001, 7, 22), "Villa 3, District 2", "Saudi Arabia", "Jeddah",      "0502234568", "fatima.zahraa@example.com",  StudentStatus.Active,    "Grade 11", new DateTime(2019, 9, 1),  3.90m),
            ("Mohammed Al-Ghamdi", "1001234569", StudentGender.Male,   new DateTime(1999, 1, 10), "Apt 7, Tower A",      "Saudi Arabia", "Dammam",      "0503234569", "m.ghamdi@example.com",       StudentStatus.Graduated, "Grade 12", new DateTime(2017, 9, 1),  3.50m),
            ("Sara Al-Otaibi",     "1001234570", StudentGender.Female, new DateTime(2002, 5, 30), "House 9, Lane 4",     "Saudi Arabia", "Makkah",      "0504234570", "sara.otaibi@example.com",    StudentStatus.Active,    "Grade 9",  new DateTime(2020, 9, 1),  3.20m),
            ("Khalid Al-Shehri",   "1001234571", StudentGender.Male,   new DateTime(2000, 11, 8), "Block 14, Flat 2",   "Saudi Arabia", "Madinah",     "0505234571", "k.shehri@example.com",       StudentStatus.Active,    "Grade 10", new DateTime(2018, 9, 1),  2.80m),
            ("Noura Al-Qahtani",   "1001234572", StudentGender.Female, new DateTime(2001, 4, 18), "Villa 22, District 7","Saudi Arabia","Riyadh",      "0506234572", "noura.qahtani@example.com",  StudentStatus.Active,    "Grade 11", new DateTime(2019, 9, 1),  3.60m),
            ("Omar Al-Harbi",      "1001234573", StudentGender.Male,   new DateTime(1998, 9, 25), "Street 5, Block 3",   "Saudi Arabia", "Taif",        "0507234573", "omar.harbi@example.com",     StudentStatus.Graduated, "Grade 12", new DateTime(2016, 9, 1),  3.10m),
            ("Reem Al-Dosari",     "1001234574", StudentGender.Female, new DateTime(2003, 2, 14), "Apt 4, Building 8",   "Saudi Arabia", "Jubail",      "0508234574", "reem.dosari@example.com",    StudentStatus.Active,    "Grade 8",  new DateTime(2021, 9, 1),  3.85m),
            ("Abdulaziz Al-Mutairi","1001234575",StudentGender.Male,   new DateTime(2000, 6, 3),  "House 16, Row 2",     "Saudi Arabia", "Khobar",      "0509234575", "a.mutairi@example.com",      StudentStatus.Inactive,  "Grade 10", new DateTime(2018, 9, 1),  2.50m),
            ("Hessa Al-Sulaiman",  "1001234576", StudentGender.Female, new DateTime(2001, 12, 20),"Villa 8, Street 11",  "Saudi Arabia", "Riyadh",      "0510234576", "hessa.sulaiman@example.com", StudentStatus.Active,    "Grade 11", new DateTime(2019, 9, 1),  3.95m),
            ("Tariq Al-Anzi",      "1001234577", StudentGender.Male,   new DateTime(1999, 8, 7),  "Block 6, Flat 12",    "Saudi Arabia", "Hail",        "0511234577", "tariq.anzi@example.com",     StudentStatus.Graduated, "Grade 12", new DateTime(2017, 9, 1),  2.90m),
            ("Dalal Al-Shamrani",  "1001234578", StudentGender.Female, new DateTime(2002, 3, 28), "Street 9, House 3",   "Saudi Arabia", "Abha",        "0512234578", "dalal.shamrani@example.com", StudentStatus.Active,    "Grade 9",  new DateTime(2020, 9, 1),  3.40m),
            ("Faisal Al-Zahrani",  "1001234579", StudentGender.Male,   new DateTime(2000, 10, 12),"Apt 19, Tower C",     "Saudi Arabia", "Jizan",       "0513234579", "faisal.zahrani@example.com", StudentStatus.Active,    "Grade 10", new DateTime(2018, 9, 1),  3.15m),
            ("Mona Al-Bishi",      "1001234580", StudentGender.Female, new DateTime(2001, 1, 5),  "Villa 5, District 4", "Saudi Arabia", "Najran",      "0514234580", "mona.bishi@example.com",     StudentStatus.Suspended, "Grade 11", new DateTime(2019, 9, 1),  1.80m),
            ("Nasser Al-Aqeel",    "1001234581", StudentGender.Male,   new DateTime(1998, 7, 19), "House 11, Lane 7",    "Saudi Arabia", "Tabuk",       "0515234581", "nasser.aqeel@example.com",   StudentStatus.Graduated, "Grade 12", new DateTime(2016, 9, 1),  3.70m),
            ("Lujain Al-Hamdan",   "1001234582", StudentGender.Female, new DateTime(2003, 5, 22), "Block 3, Flat 6",     "Saudi Arabia", "Riyadh",      "0516234582", "lujain.hamdan@example.com",  StudentStatus.Active,    "Grade 8",  new DateTime(2021, 9, 1),  3.80m),
            ("Bandar Al-Maliki",   "1001234583", StudentGender.Male,   new DateTime(2000, 4, 16), "Street 7, Block 9",   "Saudi Arabia", "Jeddah",      "0517234583", "bandar.maliki@example.com",  StudentStatus.Active,    "Grade 10", new DateTime(2018, 9, 1),  2.70m),
            ("Ghadeer Al-Farsi",   "1001234584", StudentGender.Female, new DateTime(2001, 9, 9),  "Villa 14, District 1","Saudi Arabia","Dammam",      "0518234584", "ghadeer.farsi@example.com",  StudentStatus.Active,    "Grade 11", new DateTime(2019, 9, 1),  3.55m),
            ("Saad Al-Enezi",      "1001234585", StudentGender.Male,   new DateTime(1999, 2, 27), "Apt 2, Building 5",   "Saudi Arabia", "Buraidah",    "0519234585", "saad.enezi@example.com",     StudentStatus.Graduated, "Grade 12", new DateTime(2017, 9, 1),  3.30m),
            ("Abeer Al-Juhani",    "1001234586", StudentGender.Female, new DateTime(2002, 11, 13),"House 20, Row 5",     "Saudi Arabia", "Khamis Mushait","0520234586","abeer.juhani@example.com",  StudentStatus.Active,    "Grade 9",  new DateTime(2020, 9, 1),  3.65m),
            ("Yazeed Al-Thaqafi",  "1001234587", StudentGender.Male,   new DateTime(2000, 8, 1),  "Block 8, Flat 3",     "Saudi Arabia", "Riyadh",      "0521234587", "yazeed.thaqafi@example.com", StudentStatus.Active,    "Grade 10", new DateTime(2018, 9, 1),  3.00m),
            ("Rawan Al-Essa",      "1001234588", StudentGender.Female, new DateTime(2001, 6, 4),  "Street 15, House 8",  "Saudi Arabia", "Jeddah",      "0522234588", "rawan.essa@example.com",     StudentStatus.Active,    "Grade 11", new DateTime(2019, 9, 1),  3.88m),
            ("Majed Al-Howsawi",   "1001234589", StudentGender.Male,   new DateTime(1998, 12, 30),"Villa 18, District 3","Saudi Arabia","Madinah",     "0523234589", "majed.howsawi@example.com",  StudentStatus.Graduated, "Grade 12", new DateTime(2016, 9, 1),  2.95m),
            ("Shahad Al-Gosaib",   "1001234590", StudentGender.Female, new DateTime(2003, 8, 17), "Apt 11, Tower B",     "Saudi Arabia", "Riyadh",      "0524234590", "shahad.gosaib@example.com",  StudentStatus.Active,    "Grade 8",  new DateTime(2021, 9, 1),  3.92m),
            ("Mansour Al-Zaid",    "1001234591", StudentGender.Male,   new DateTime(2000, 1, 24), "House 4, Lane 9",     "Saudi Arabia", "Makkah",      "0525234591", "mansour.zaid@example.com",   StudentStatus.Inactive,  "Grade 10", new DateTime(2018, 9, 1),  2.20m),
            ("Manal Al-Sayed",     "1001234592", StudentGender.Female, new DateTime(2001, 10, 11),"Block 12, Flat 9",    "Saudi Arabia", "Dammam",      "0526234592", "manal.sayed@example.com",    StudentStatus.Active,    "Grade 11", new DateTime(2019, 9, 1),  3.45m),
            ("Turki Al-Madani",    "1001234593", StudentGender.Male,   new DateTime(1999, 5, 6),  "Street 3, Block 11",  "Saudi Arabia", "Taif",        "0527234593", "turki.madani@example.com",   StudentStatus.Graduated, "Grade 12", new DateTime(2017, 9, 1),  3.60m),
            ("Ruba Al-Tamimi",     "1001234594", StudentGender.Female, new DateTime(2002, 7, 29), "Villa 7, District 6", "Saudi Arabia", "Khobar",      "0528234594", "ruba.tamimi@example.com",    StudentStatus.Active,    "Grade 9",  new DateTime(2020, 9, 1),  3.25m),
            ("Nawaf Al-Shammari",  "1001234595", StudentGender.Male,   new DateTime(2000, 9, 18), "Apt 14, Building 3",  "Saudi Arabia", "Riyadh",      "0529234595", "nawaf.shammari@example.com", StudentStatus.Active,    "Grade 10", new DateTime(2018, 9, 1),  3.10m),
            ("Eman Al-Harthi",     "1001234596", StudentGender.Female, new DateTime(2001, 3, 7),  "House 6, Row 1",      "Saudi Arabia", "Jubail",      "0530234596", "eman.harthi@example.com",    StudentStatus.Active,    "Grade 11", new DateTime(2019, 9, 1),  3.72m),
            ("Sami Al-Asiri",      "1001234597", StudentGender.Male,   new DateTime(1998, 11, 21),"Block 9, Flat 15",    "Saudi Arabia", "Abha",        "0531234597", "sami.asiri@example.com",     StudentStatus.Graduated, "Grade 12", new DateTime(2016, 9, 1),  3.35m),
            ("Arwa Al-Barakati",   "1001234598", StudentGender.Female, new DateTime(2003, 1, 14), "Street 21, House 5",  "Saudi Arabia", "Jeddah",      "0532234598", "arwa.barakati@example.com",  StudentStatus.Active,    "Grade 8",  new DateTime(2021, 9, 1),  3.78m),
            ("Hamad Al-Buainain",  "1001234599", StudentGender.Male,   new DateTime(2000, 6, 9),  "Villa 11, District 5","Saudi Arabia","Jizan",       "0533234599", "hamad.buainain@example.com", StudentStatus.Active,    "Grade 10", new DateTime(2018, 9, 1),  2.60m),
            ("Hala Al-Khalidi",    "1001234600", StudentGender.Female, new DateTime(2001, 8, 26), "Apt 8, Tower D",      "Saudi Arabia", "Najran",      "0534234600", "hala.khalidi@example.com",   StudentStatus.Suspended, "Grade 11", new DateTime(2019, 9, 1),  1.50m),
            ("Waleed Al-Subaii",   "1001234601", StudentGender.Male,   new DateTime(1999, 4, 3),  "House 13, Lane 6",    "Saudi Arabia", "Tabuk",       "0535234601", "waleed.subaii@example.com",  StudentStatus.Graduated, "Grade 12", new DateTime(2017, 9, 1),  3.20m),
            ("Reham Al-Mubarak",   "1001234602", StudentGender.Female, new DateTime(2002, 12, 18),"Block 7, Flat 4",     "Saudi Arabia", "Riyadh",      "0536234602", "reham.mubarak@example.com",  StudentStatus.Active,    "Grade 9",  new DateTime(2020, 9, 1),  3.50m),
            ("Saleh Al-Omairi",    "1001234603", StudentGender.Male,   new DateTime(2000, 2, 13), "Street 18, Block 2",  "Saudi Arabia", "Hail",        "0537234603", "saleh.omairi@example.com",   StudentStatus.Active,    "Grade 10", new DateTime(2018, 9, 1),  2.90m),
            ("Mariam Al-Balawi",   "1001234604", StudentGender.Female, new DateTime(2001, 5, 1),  "Villa 16, District 9","Saudi Arabia","Buraidah",    "0538234604", "mariam.balawi@example.com",  StudentStatus.Active,    "Grade 11", new DateTime(2019, 9, 1),  3.83m),
            ("Faris Al-Ruwaili",   "1001234605", StudentGender.Male,   new DateTime(1998, 10, 15),"Apt 6, Building 7",   "Saudi Arabia", "Riyadh",      "0539234605", "faris.ruwaili@example.com",  StudentStatus.Graduated, "Grade 12", new DateTime(2016, 9, 1),  3.45m),
            ("Hind Al-Subaie",     "1001234606", StudentGender.Female, new DateTime(2003, 4, 8),  "House 3, Row 8",      "Saudi Arabia", "Jeddah",      "0540234606", "hind.subaie@example.com",    StudentStatus.Active,    "Grade 8",  new DateTime(2021, 9, 1),  3.97m),
            ("Abdullah Al-Dehaim", "1001234607", StudentGender.Male,   new DateTime(2000, 7, 20), "Block 5, Flat 7",     "Saudi Arabia", "Dammam",      "0541234607", "a.dehaim@example.com",       StudentStatus.Active,    "Grade 10", new DateTime(2018, 9, 1),  3.05m),
            ("Ghalia Al-Fraih",    "1001234608", StudentGender.Female, new DateTime(2001, 11, 3), "Street 6, House 12",  "Saudi Arabia", "Khamis Mushait","0542234608","ghalia.fraih@example.com",  StudentStatus.Active,    "Grade 11", new DateTime(2019, 9, 1),  3.68m),
            ("Adel Al-Jahdhami",   "1001234609", StudentGender.Male,   new DateTime(1999, 3, 28), "Villa 9, District 8", "Saudi Arabia", "Taif",        "0543234609", "adel.jahdhami@example.com",  StudentStatus.Graduated, "Grade 12", new DateTime(2017, 9, 1),  2.75m),
            ("Shrouk Al-Barrak",   "1001234610", StudentGender.Female, new DateTime(2002, 9, 11), "Apt 17, Tower E",     "Saudi Arabia", "Makkah",      "0544234610", "shrouk.barrak@example.com",  StudentStatus.Active,    "Grade 9",  new DateTime(2020, 9, 1),  3.30m),
            ("Ibrahim Al-Marzouqi","1001234611", StudentGender.Male,   new DateTime(2000, 12, 5), "House 7, Lane 3",     "Saudi Arabia", "Riyadh",      "0545234611", "i.marzouqi@example.com",     StudentStatus.Inactive,  "Grade 10", new DateTime(2018, 9, 1),  2.45m),
            ("Wedad Al-Khuthila",  "1001234612", StudentGender.Female, new DateTime(2001, 2, 19), "Block 4, Flat 11",    "Saudi Arabia", "Madinah",     "0546234612", "wedad.khuthila@example.com", StudentStatus.Active,    "Grade 11", new DateTime(2019, 9, 1),  3.58m),
            ("Yasser Al-Mhana",    "1001234613", StudentGender.Male,   new DateTime(1998, 6, 14), "Street 24, Block 7",  "Saudi Arabia", "Khobar",      "0547234613", "yasser.mhana@example.com",   StudentStatus.Graduated, "Grade 12", new DateTime(2016, 9, 1),  3.15m),
            ("Aseel Al-Asmari",    "1001234614", StudentGender.Female, new DateTime(2003, 10, 27),"Villa 12, District 2","Saudi Arabia","Jubail",      "0548234614", "aseel.asmari@example.com",   StudentStatus.Active,    "Grade 8",  new DateTime(2021, 9, 1),  3.90m),
            ("Saud Al-Ghamdi",     "1001234615", StudentGender.Male,   new DateTime(2000, 5, 22), "Apt 9, Building 4",   "Saudi Arabia", "Abha",        "0549234615", "saud.ghamdi@example.com",    StudentStatus.Active,    "Grade 10", new DateTime(2018, 9, 1),  2.85m),
            ("Hanan Al-Qahtani",   "1001234616", StudentGender.Female, new DateTime(2001, 7, 6),  "House 18, Row 4",     "Saudi Arabia", "Riyadh",      "0550234616", "hanan.qahtani@example.com",  StudentStatus.Active,    "Grade 11", new DateTime(2019, 9, 1),  3.77m),
            ("Mishal Al-Ajmi",     "1001234617", StudentGender.Male,   new DateTime(1999, 1, 31), "Block 15, Flat 2",    "Saudi Arabia", "Jizan",       "0551234617", "mishal.ajmi@example.com",    StudentStatus.Graduated, "Grade 12", new DateTime(2017, 9, 1),  3.40m),
            ("Bayan Al-Rabiah",    "1001234618", StudentGender.Female, new DateTime(2002, 4, 24), "Street 10, House 7",  "Saudi Arabia", "Najran",      "0552234618", "bayan.rabiah@example.com",   StudentStatus.Active,    "Grade 9",  new DateTime(2020, 9, 1),  3.55m),
            ("Osama Al-Otibi",     "1001234619", StudentGender.Male,   new DateTime(2000, 8, 10), "Villa 6, District 7", "Saudi Arabia", "Tabuk",       "0553234619", "osama.otibi@example.com",    StudentStatus.Active,    "Grade 10", new DateTime(2018, 9, 1),  3.00m),
            ("Jumana Al-Zahrani",  "1001234620", StudentGender.Female, new DateTime(2001, 11, 15),"Apt 13, Tower F",     "Saudi Arabia", "Riyadh",      "0554234620", "jumana.zahrani@example.com", StudentStatus.Active,    "Grade 11", new DateTime(2019, 9, 1),  3.86m),
            ("Ziad Al-Harthi",     "1001234621", StudentGender.Male,   new DateTime(1998, 9, 4),  "House 22, Lane 1",    "Saudi Arabia", "Jeddah",      "0555234621", "ziad.harthi@example.com",    StudentStatus.Graduated, "Grade 12", new DateTime(2016, 9, 1),  2.80m),
            ("Raghad Al-Humaid",   "1001234622", StudentGender.Female, new DateTime(2003, 3, 19), "Block 11, Flat 8",    "Saudi Arabia", "Dammam",      "0556234622", "raghad.humaid@example.com",  StudentStatus.Active,    "Grade 8",  new DateTime(2021, 9, 1),  3.75m),
            ("Naif Al-Shehri",     "1001234623", StudentGender.Male,   new DateTime(2000, 6, 28), "Street 16, Block 4",  "Saudi Arabia", "Makkah",      "0557234623", "naif.shehri@example.com",    StudentStatus.Inactive,  "Grade 10", new DateTime(2018, 9, 1),  2.35m),
            ("Amira Al-Sehli",     "1001234624", StudentGender.Female, new DateTime(2001, 1, 12), "Villa 20, District 5","Saudi Arabia","Khobar",      "0558234624", "amira.sehli@example.com",    StudentStatus.Active,    "Grade 11", new DateTime(2019, 9, 1),  3.62m),
            ("Rayan Al-Haddad",    "1001234625", StudentGender.Male,   new DateTime(1999, 7, 23), "Apt 3, Building 9",   "Saudi Arabia", "Buraidah",    "0559234625", "rayan.haddad@example.com",   StudentStatus.Graduated, "Grade 12", new DateTime(2017, 9, 1),  3.25m),
            ("Sadeem Al-Muqbil",   "1001234626", StudentGender.Female, new DateTime(2002, 10, 6), "House 15, Row 7",     "Saudi Arabia", "Riyadh",      "0560234626", "sadeem.muqbil@example.com",  StudentStatus.Active,    "Grade 9",  new DateTime(2020, 9, 1),  3.48m),
            ("Moath Al-Mutlaq",    "1001234627", StudentGender.Male,   new DateTime(2000, 3, 17), "Block 2, Flat 14",    "Saudi Arabia", "Hail",        "0561234627", "moath.mutlaq@example.com",   StudentStatus.Active,    "Grade 10", new DateTime(2018, 9, 1),  2.95m),
            ("Lamya Al-Humud",     "1001234628", StudentGender.Female, new DateTime(2001, 5, 30), "Street 19, House 4",  "Saudi Arabia", "Taif",        "0562234628", "lamya.humud@example.com",    StudentStatus.Active,    "Grade 11", new DateTime(2019, 9, 1),  3.70m),
            ("Amjad Al-Qurashi",   "1001234629", StudentGender.Male,   new DateTime(1998, 4, 12), "Villa 4, District 4", "Saudi Arabia", "Jeddah",      "0563234629", "amjad.qurashi@example.com",  StudentStatus.Graduated, "Grade 12", new DateTime(2016, 9, 1),  3.55m),
            ("Hessa Al-Hamad",     "1001234630", StudentGender.Female, new DateTime(2003, 7, 5),  "Apt 16, Tower G",     "Saudi Arabia", "Riyadh",      "0564234630", "hessa.hamad@example.com",    StudentStatus.Active,    "Grade 8",  new DateTime(2021, 9, 1),  3.94m),
            ("Musaed Al-Dosari",   "1001234631", StudentGender.Male,   new DateTime(2000, 10, 19),"House 8, Lane 5",     "Saudi Arabia", "Khamis Mushait","0565234631","musaed.dosari@example.com",  StudentStatus.Active,    "Grade 10", new DateTime(2018, 9, 1),  3.20m),
            ("Samah Al-Tamimi",    "1001234632", StudentGender.Female, new DateTime(2001, 4, 23), "Block 6, Flat 1",     "Saudi Arabia", "Abha",        "0566234632", "samah.tamimi@example.com",   StudentStatus.Suspended, "Grade 11", new DateTime(2019, 9, 1),  1.70m),
            ("Badr Al-Sulami",     "1001234633", StudentGender.Male,   new DateTime(1999, 11, 8), "Street 8, Block 13",  "Saudi Arabia", "Madinah",     "0567234633", "badr.sulami@example.com",    StudentStatus.Graduated, "Grade 12", new DateTime(2017, 9, 1),  2.65m),
            ("Taghreed Al-Qahtani","1001234634", StudentGender.Female, new DateTime(2002, 2, 1),  "Villa 17, District 8","Saudi Arabia","Jizan",       "0568234634", "taghreed.qahtani@example.com",StudentStatus.Active,   "Grade 9",  new DateTime(2020, 9, 1),  3.38m),
            ("Khaled Al-Suwailem", "1001234635", StudentGender.Male,   new DateTime(2000, 8, 26), "Apt 1, Building 6",   "Saudi Arabia", "Najran",      "0569234635", "khaled.suwailem@example.com",StudentStatus.Active,    "Grade 10", new DateTime(2018, 9, 1),  2.75m),
            ("Haifa Al-Ghamdi",    "1001234636", StudentGender.Female, new DateTime(2001, 6, 14), "House 24, Row 3",     "Saudi Arabia", "Tabuk",       "0570234636", "haifa.ghamdi@example.com",   StudentStatus.Active,    "Grade 11", new DateTime(2019, 9, 1),  3.65m),
            ("Hatim Al-Enazi",     "1001234637", StudentGender.Male,   new DateTime(1998, 2, 20), "Block 10, Flat 5",    "Saudi Arabia", "Riyadh",      "0571234637", "hatim.enazi@example.com",    StudentStatus.Graduated, "Grade 12", new DateTime(2016, 9, 1),  3.10m),
            ("Nada Al-Fadhel",     "1001234638", StudentGender.Female, new DateTime(2003, 5, 13), "Street 25, House 9",  "Saudi Arabia", "Jeddah",      "0572234638", "nada.fadhel@example.com",    StudentStatus.Active,    "Grade 8",  new DateTime(2021, 9, 1),  3.88m),
            ("Hamdan Al-Ghabain",  "1001234639", StudentGender.Male,   new DateTime(2000, 1, 7),  "Villa 13, District 6","Saudi Arabia","Dammam",      "0573234639", "hamdan.ghabain@example.com", StudentStatus.Inactive,  "Grade 10", new DateTime(2018, 9, 1),  2.55m),
            ("Wijdan Al-Essa",     "1001234640", StudentGender.Female, new DateTime(2001, 9, 28), "Apt 5, Tower H",      "Saudi Arabia", "Makkah",      "0574234640", "wijdan.essa@example.com",    StudentStatus.Active,    "Grade 11", new DateTime(2019, 9, 1),  3.52m),
            ("Fahad Al-Yousif",    "1001234641", StudentGender.Male,   new DateTime(1999, 6, 17), "House 10, Lane 8",    "Saudi Arabia", "Khobar",      "0575234641", "fahad.yousif@example.com",   StudentStatus.Graduated, "Grade 12", new DateTime(2017, 9, 1),  3.80m),
            ("Ruba Al-Maliki",     "1001234642", StudentGender.Female, new DateTime(2002, 3, 4),  "Block 8, Flat 10",    "Saudi Arabia", "Jubail",      "0576234642", "ruba.maliki@example.com",    StudentStatus.Active,    "Grade 9",  new DateTime(2020, 9, 1),  3.42m),
            ("Sultan Al-Bader",    "1001234643", StudentGender.Male,   new DateTime(2000, 11, 30),"Street 11, Block 6",  "Saudi Arabia", "Buraidah",    "0577234643", "sultan.bader@example.com",   StudentStatus.Active,    "Grade 10", new DateTime(2018, 9, 1),  2.80m),
            ("Maysoon Al-Olayan",  "1001234644", StudentGender.Female, new DateTime(2001, 12, 17),"Villa 19, District 3","Saudi Arabia","Riyadh",      "0578234644", "maysoon.olayan@example.com", StudentStatus.Active,    "Grade 11", new DateTime(2019, 9, 1),  3.79m),
            ("Mazen Al-Jabri",     "1001234645", StudentGender.Male,   new DateTime(1998, 8, 11), "Apt 18, Building 2",  "Saudi Arabia", "Hail",        "0579234645", "mazen.jabri@example.com",    StudentStatus.Graduated, "Grade 12", new DateTime(2016, 9, 1),  2.70m),
            ("Sana Al-Rasheed",    "1001234646", StudentGender.Female, new DateTime(2003, 6, 24), "House 21, Row 6",     "Saudi Arabia", "Taif",        "0580234646", "sana.rasheed@example.com",   StudentStatus.Active,    "Grade 8",  new DateTime(2021, 9, 1),  3.96m),
            ("Rami Al-Obaidi",     "1001234647", StudentGender.Male,   new DateTime(2000, 4, 3),  "Block 13, Flat 6",    "Saudi Arabia", "Abha",        "0581234647", "rami.obaidi@example.com",    StudentStatus.Active,    "Grade 10", new DateTime(2018, 9, 1),  3.15m),
            ("Atheer Al-Zubaidi",  "1001234648", StudentGender.Female, new DateTime(2001, 7, 16), "Street 13, House 6",  "Saudi Arabia", "Jizan",       "0582234648", "atheer.zubaidi@example.com", StudentStatus.Active,    "Grade 11", new DateTime(2019, 9, 1),  3.58m),
            ("Nayef Al-Shammari",  "1001234649", StudentGender.Male,   new DateTime(1999, 3, 9),  "Villa 15, District 9","Saudi Arabia","Najran",      "0583234649", "nayef.shammari@example.com", StudentStatus.Graduated, "Grade 12", new DateTime(2017, 9, 1),  3.50m),
            ("Fatimah Al-Batti",   "1001234650", StudentGender.Female, new DateTime(2002, 1, 20), "Apt 7, Tower I",      "Saudi Arabia", "Tabuk",       "0584234650", "fatimah.batti@example.com",  StudentStatus.Active,    "Grade 9",  new DateTime(2020, 9, 1),  3.35m),
            ("Walid Al-Otaibi",    "1001234651", StudentGender.Male,   new DateTime(2000, 7, 8),  "House 5, Lane 2",     "Saudi Arabia", "Riyadh",      "0585234651", "walid.otaibi@example.com",   StudentStatus.Inactive,  "Grade 10", new DateTime(2018, 9, 1),  2.40m),
            ("Dina Al-Harthy",     "1001234652", StudentGender.Female, new DateTime(2001, 10, 25),"Block 1, Flat 13",    "Saudi Arabia", "Jeddah",      "0586234652", "dina.harthy@example.com",    StudentStatus.Active,    "Grade 11", new DateTime(2019, 9, 1),  3.74m),
            ("Essam Al-Rashed",    "1001234653", StudentGender.Male,   new DateTime(1998, 5, 14), "Street 22, Block 8",  "Saudi Arabia", "Dammam",      "0587234653", "essam.rashed@example.com",   StudentStatus.Graduated, "Grade 12", new DateTime(2016, 9, 1),  3.00m),
            ("Nourah Al-Khaldi",   "1001234654", StudentGender.Female, new DateTime(2003, 9, 7),  "Villa 1, District 1", "Saudi Arabia", "Khobar",      "0588234654", "nourah.khaldi@example.com",  StudentStatus.Active,    "Grade 8",  new DateTime(2021, 9, 1),  3.82m),
            ("Fawaz Al-Harbi",     "1001234655", StudentGender.Male,   new DateTime(2000, 2, 21), "Apt 12, Building 8",  "Saudi Arabia", "Makkah",      "0589234655", "fawaz.harbi@example.com",    StudentStatus.Active,    "Grade 10", new DateTime(2018, 9, 1),  2.65m),
            ("Areej Al-Qayyim",    "1001234656", StudentGender.Female, new DateTime(2001, 8, 4),  "House 17, Row 2",     "Saudi Arabia", "Madinah",     "0590234656", "areej.qayyim@example.com",   StudentStatus.Active,    "Grade 11", new DateTime(2019, 9, 1),  3.66m),
            ("Bader Al-Mubarak",   "1001234657", StudentGender.Male,   new DateTime(1999, 12, 18),"Block 16, Flat 4",    "Saudi Arabia", "Jubail",      "0591234657", "bader.mubarak@example.com",  StudentStatus.Graduated, "Grade 12", new DateTime(2017, 9, 1),  2.85m),
            ("Layla Al-Anazi",     "1001234658", StudentGender.Female, new DateTime(2002, 6, 9),  "Street 27, House 2",  "Saudi Arabia", "Buraidah",    "0592234658", "layla.anazi@example.com",    StudentStatus.Active,    "Grade 9",  new DateTime(2020, 9, 1),  3.46m),
            ("Muhannad Al-Khalaf", "1001234659", StudentGender.Male,   new DateTime(2000, 9, 27), "Villa 23, District 2","Saudi Arabia","Riyadh",      "0593234659", "muhannad.khalaf@example.com",StudentStatus.Active,    "Grade 10", new DateTime(2018, 9, 1),  3.05m),
            ("Rana Al-Subhi",      "1001234660", StudentGender.Female, new DateTime(2001, 3, 15), "Apt 20, Tower J",     "Saudi Arabia", "Hail",        "0594234660", "rana.subhi@example.com",     StudentStatus.Active,    "Grade 11", new DateTime(2019, 9, 1),  3.81m),
            ("Rashid Al-Kulaib",   "1001234661", StudentGender.Male,   new DateTime(1998, 7, 3),  "House 12, Lane 4",    "Saudi Arabia", "Taif",        "0595234661", "rashid.kulaib@example.com",  StudentStatus.Graduated, "Grade 12", new DateTime(2016, 9, 1),  3.30m),
            ("Basma Al-Fayez",     "1001234662", StudentGender.Female, new DateTime(2002, 8, 22), "Villa 25, District 7","Saudi Arabia","Riyadh",      "0596234662", "basma.fayez@example.com",    StudentStatus.Active,    "Grade 9",  new DateTime(2020, 9, 1),  3.60m),
            ("Zuhair Al-Saeed",    "1001234663", StudentGender.Male,   new DateTime(2000, 11, 14),"Apt 22, Building 5",  "Saudi Arabia", "Jeddah",      "0597234663", "zuhair.saeed@example.com",   StudentStatus.Active,    "Grade 10", new DateTime(2018, 9, 1),  2.95m),
            ("Rima Al-Brikan",     "1001234664", StudentGender.Female, new DateTime(2001, 4, 7),  "Block 17, Flat 9",    "Saudi Arabia", "Dammam",      "0598234664", "rima.brikan@example.com",    StudentStatus.Active,    "Grade 11", new DateTime(2019, 9, 1),  3.84m),
            ("Nawaf Al-Mansouri",  "1001234665", StudentGender.Male,   new DateTime(1999, 9, 29), "Street 30, House 1",  "Saudi Arabia", "Khobar",      "0599234665", "nawaf.mansouri@example.com", StudentStatus.Graduated, "Grade 12", new DateTime(2017, 9, 1),  3.05m),
            ("Hessa Al-Zahrani",   "1001234666", StudentGender.Female, new DateTime(2003, 12, 11),"Villa 27, District 4","Saudi Arabia","Madinah",     "0600234666", "hessa.zahrani@example.com",  StudentStatus.Active,    "Grade 8",  new DateTime(2021, 9, 1),  3.93m),
        };

        foreach (var (name, nationalId, gender, dob, address, country, region, mobile, email, status, grade, enrollment, gpa) in students)
        {
            await _studentRepository.InsertAsync(new Student(
                _guidGenerator.Create(),
                name, nationalId, gender, dob,
                address, country, region, mobile, email,
                status, grade, enrollment, gpa));
        }
    }
}
