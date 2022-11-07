using System;

namespace nummerplade_api.Classes
{
    public class dmr_data
    {
        // Init
        public BasicInformation? basic { get; set; }
        public DebtData? debtData { get; set; }
        public Extended? extended { get; set; }
        public InspectionData? inspectionData { get; set; }

    }

    // Sub classes
    public class BasicInformation
    {
        public string? regNr { get; set; }
        public string? stelNr { get; set; }
        public string? status { get; set; }
        public DateTime? statusDato { get; set; }
        public DateTime? foersteRegistreringDato { get; set; }
        public string? koeretoejArtNavn { get; set; }
        public string? koeretoejAnvendelseNavn { get; set; }
        public int? totalVaegt { get; set; }
        public int? egenVaegt { get; set; }
        public int? vogntogVaegt { get; set; }
        public object? koereklarVaegtMinimum { get; set; }
        public int? tekniskTotalVaegt { get; set; }
        public object? paahaengVognTotalVaegt { get; set; }
        public int? akselAntal { get; set; }
        public object? koeretoejstand { get; set; }
        public string? modelAar { get; set; }
        public double? motorSlagVolumen { get; set; }
        public double? motorStoerrelse { get; set; }
        public object? motorStoersteEffekt { get; set; }
        public object? motorHestekraefter { get; set; }
        public double? motorKmPerLiter { get; set; }
        public int? motorKilometerstand { get; set; }
        public string? drivkraftTypeNavn { get; set; }
        public object? maksimumHastighed { get; set; }
        public int? motorCylinderAntal { get; set; }
        public object? faelgDaek { get; set; }
        public string? stelNummerAnbringelse { get; set; }
        public long? koeretoejId { get; set; }
    }

    public class DebtData
    {
        public string? carId { get; set; }
        public List<object>? laaneDokumenter { get; set; }
        public object? konkurs { get; set; }
    }

    public class Extended
    {
        public string? carId { get; set; }
        public DateTime? date { get; set; }
        public Taxes? taxes { get; set; }
        public Insurance? insurance { get; set; }
        public General? general { get; set; }
        public Techical? techical { get; set; }
        public Inspection? inspection { get; set; }
    }

    public class Fejl
    {
        public string? number { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
    }

    public class General
    {
        public string? typeGodkendelsesNummer { get; set; }
        public string? stelNummer { get; set; }
        public string? maerke { get; set; }
        public string? model { get; set; }
        public string? variant { get; set; }
        public string? koeretoejArt { get; set; }
        public string? regNr { get; set; }
        public DateTime? firstRegDate { get; set; }
        public string? koeretoejAnvendelse { get; set; }
        public string? status { get; set; }
        public DateTime? statusDato { get; set; }
        public long? kid { get; set; }
        public string? farve { get; set; }
        public object? standEfterImport { get; set; }
        public object? modelAar { get; set; }
        public bool? automatgear { get; set; }
        public bool? hoejrestyring { get; set; }
        public bool? turbo { get; set; }
        public int? antalAirbags { get; set; }
        public int? koeretoejMotorKilometerstand { get; set; }
        public string? sekundaerStatus { get; set; }
        public DateTime? sekundaerStatusDato { get; set; }
        public bool? blockedStatus { get; set; }
        public string? blockedDescription { get; set; }
        public string? kmDisplay { get; set; }
    }

    public class Historik
    {
        public string? selskab { get; set; }
        public string? status { get; set; }
        public string? oprettet { get; set; }
        public List<Historik>? historik { get; set; }
    }

    public class Inspection
    {
        public DateTime? naesteSyn { get; set; }
        public DateTime? sidsteSyn { get; set; }
        public string? sidsteSynResultat { get; set; }
        public string? sidsteSynType { get; set; }
    }

    public class InspectionData
    {
        public string? carId { get; set; }
        public List<Rapporter>? rapporter { get; set; }
        public bool? mistaenkeligtKmStand { get; set; }
        public string? senesteRegNr { get; set; }
    }

    public class Insurance
    {
        public string? selskab { get; set; }
        public string? status { get; set; }
        public string? oprettet { get; set; }
        public List<Historik>? historik { get; set; }
    }

    public class Rapporter
    {
        public string? firma { get; set; }
        public string? cvr { get; set; }
        public string? adresse { get; set; }
        public int? postNr { get; set; }
        public string? synstype { get; set; }
        public string? regNr { get; set; }
        public string? synsresultat { get; set; }
        public string? synsdato { get; set; }
        public string? slutTime { get; set; }
        public string? slutMinut { get; set; }
        public string? kategori { get; set; }
        public int? kmstand { get; set; }
        public List<Fejl>? fejl { get; set; }
        public List<double>? coordinates { get; set; }
    }

    public class Taxes
    {
        public List<object>? opkraevedeAfgifter { get; set; }
        public List<object>? afgiftDetaljer { get; set; }
    }

    public class Techical
    {
        public int? totalVaegt { get; set; }
        public int? egenVaegt { get; set; }
        public object? koereklarVaegtMinimum { get; set; }
        public int? tekniskTotalVaegt { get; set; }
        public object? vogntogVaegt { get; set; }
        public object? paahaengVognTotalVaegt { get; set; }
        public int? akselAntal { get; set; }
        public object? siddepladserMinimum { get; set; }
        public bool? tilkoblingsmulighed { get; set; }
        public int? tilkoblingsvaegtUdenBremser { get; set; }
        public int? tilkoblingsvaegtMedBremser { get; set; }
        public object? karrosseriTypeNavn { get; set; }
        public double? motorSlagVolumen { get; set; }
        public object? motorStoersteEffekt { get; set; }
        public double? motorKmPerLiter { get; set; }
        public object? motorElektriskForbrug { get; set; }
        public object? motorElektriskForbrugMaalt { get; set; }
        public object? motorBraendstofForbrugMaalt { get; set; }
        public object? batteriKapacitet { get; set; }
        public object? elektriskRaekkevidde { get; set; }
        public object? maalenorm { get; set; }
        public string? drivkraftTypeNavn { get; set; }
        public bool? pluginHybrid { get; set; }
        public object? motorMaerkning { get; set; }
        public object? maksimumHastighed { get; set; }
        public int? motorCylinderAntal { get; set; }
        public int? antalDoere { get; set; }
        public object? faelgDaek { get; set; }
        public object? sporviddenForrest { get; set; }
        public object? sporviddenBagest { get; set; }
        public object? stelNummerAnbringelse { get; set; }
        public object? passagerAntal { get; set; }
        public object? traekkendeAksler { get; set; }
        public object? akselAfstand { get; set; }
        public object? stoersteAkselTryk { get; set; }
        public object? miljoeOplysningCO2Udslip { get; set; }
        public object? miljoeOplysningEmissionCO { get; set; }
        public object? miljoeOplysningEmissionHCPlusNOX { get; set; }
        public object? miljoeOplysningEmissionNOX { get; set; }
        public bool? miljoeOplysningPartikelFilter { get; set; }
        public object? miljoeOplysningRoegtaethed { get; set; }
        public object? miljoeOplysningRoegtaethedOmdrejningstal { get; set; }
        public object? miljoeOplysningPartikler { get; set; }
        public object? motorKoerselStoej { get; set; }
        public object? motorStandStoej { get; set; }
        public object? motorStandStoejOmdrejningstal { get; set; }
        public string? normTypeNavn { get; set; }
        public bool? miljoeEftermonteretPartikelFilter { get; set; }
        public object? effektDisplay { get; set; }
    }


}
