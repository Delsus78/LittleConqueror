namespace LittleConqueror.AppService.Domain.Models.TechResearches;

public enum TechResearchType
{
    // Miltaire
    Armement_I = 0,
    Armement_II = 1,
    Armement_III = 2,
    Poudre_A_Canon = 3,
    Armement_IV = 4,
    Arme_Atomique = 5,
    // GeoPolitique
    Classement = 6,
    Politique = 7,
    Alliance = 8,
    Guerre = 9,
    Espionnage = 10,
    Echanges = 11,
    Traite_De_Paix = 12,
    // Theorie
    Gribouillis = 13,
    Roue = 14,
    Ecriture = 15,
    Apprentissage = 16,
    Ecoles = 17,
    Mathematiques = 18,
    Monnaie = 19,
    Physiques = 20,
    Explosion = 21,
    Traitement_Des_Fluides = 22,
    Forage = 23,
    Projet_Manhattan = 24,
    Financement_Mondiale_Future_I = 25,
    Financement_Mondiale_Future_II = 26,
    Financement_Mondiale_Future_III = 27,
    Financement_Mondiale_Future_IV = 28,
    Skills_Sellers = 29,
    // Ingenierie
    Pierre = 30,
    Rendement_Minier_I = 31,
    Rendement_Agricole_I = 32,
    Fer = 33,
    Rendement_Minier_II = 34,
    Rendement_Agricole_II = 35,
    Routes = 36,
    Chemins_De_Fer = 37,
    Or = 38,
    Electricite = 39,
    Diamant = 40,
    Rendement_Minier_III = 41,
    Rendement_Agricole_III = 42,
    Petrole = 43,
    Carburant = 44,
    Fission_Nucleaire = 45,
    Fusion_Nucleaire = 46,
    Tesseract_Energetique = 47,
    Rendement_Scientifique_I = 48,
    Rendement_Scientifique_II = 49,
    Rendement_Scientifique_III = 50,
    Rendement_Scientifique_IV = 51,
    Technologies_Futures = 52,
    Mode_Creatif = 53
}

public enum TechResearchCategory
{
    TheoryResearch, // servent de base à d'autre recherche
    MilitaryResearch, // améliore les armées, les défenses, leurs vitesse de déplacement, etc.
    EngineeringResearch, // améliore les bâtiments, les routes, les ponts, etc.
    GeopoliticResearch, // les relations diplomatiques, les alliances, les traités, etc.
}

public enum TechResearchStatus
{
    Undiscovered,
    Researching,
    Researched
}

public static class TechResearchesDataDictionariesDeprecated
{
    public static Dictionary<TechResearchType, (int cost, TimeSpan researchTime, TechResearchCategory category, List<TechResearchType> preReqs, string name, string description)> Values { get; } = new()
    {
        // Miltaire
        { TechResearchType.Armement_I, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.MilitaryResearch,
                preReqs: new List<TechResearchType> { TechResearchType.Pierre }, 
                name: "Armement I", 
                description: "Vos armées sont désormais équipées d'armes rudimentaires, leurs forces est multiplier par 1.2") },
        
        { TechResearchType.Armement_II, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.MilitaryResearch,
                preReqs: new List<TechResearchType> { TechResearchType.Armement_I, TechResearchType.Fer }, 
                name: "Armement II", 
                description: "Vos armées sont désormais équipées d'armes plus avancées, leurs forces est multiplier par 1.5") },
        
        { TechResearchType.Poudre_A_Canon, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.MilitaryResearch,
                preReqs: new List<TechResearchType> { TechResearchType.Explosion }, 
                name: "Poudre à canon",
                description: "Avec les armes à feu, vos armées ont désormais un avantage supplémentaire, deux dés seront lancés par groupe, le plus grand sera gardé") },
        
        { TechResearchType.Armement_III, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.MilitaryResearch,
                preReqs: new List<TechResearchType> { TechResearchType.Armement_II, TechResearchType.Poudre_A_Canon },
                name: "Armement III",
                description: "Vos armées sont désormais équipées d'armes de guerre, leurs forces est multiplier par 2") },
        
        { TechResearchType.Armement_IV, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.MilitaryResearch,
                preReqs: new List<TechResearchType> { TechResearchType.Armement_III, TechResearchType.Electricite, TechResearchType.Carburant },
                name: "Armement IV",
                description: "Vos armées sont désormais équipées d'armes de destruction massive, leurs forces est multiplier par 3") },
        
        { TechResearchType.Arme_Atomique, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.MilitaryResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Armement_IV, TechResearchType.Fission_Nucleaire },
            name: "Arme Atomique",
            description: "Vous possédez désormais la puissance de détruire des villes entières, il est possible de lancer une bombe atomique sur une ville ennemie via une de vos ville adéquate") },
        
        // GeoPolitique
        { TechResearchType.Classement,
            (cost: 0, researchTime: TimeSpan.FromMinutes(1), category: TechResearchCategory.GeopoliticResearch,
            preReqs: new List<TechResearchType> {},
            name:"Classement",
            description:"Vous avez désormais une idée de votre position par rapport aux autres dans le menu principal.") },
            
        { TechResearchType.Politique, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.GeopoliticResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Classement, TechResearchType.Ecoles },
            name: "Politique",
            description: "Vous avez désormais accès à des options politiques, vous pouvez désormais choisir une direction politique dans le menu principale.") },
        
        { TechResearchType.Alliance, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.GeopoliticResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Politique },
            name: "Alliance",
            description: "Vous avez désormais la possibilité de former des alliances avec d'autres joueurs via le classement.") },
        
        { TechResearchType.Guerre, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.GeopoliticResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Politique },
            name: "Guerre",
            description: "Vous avez désormais la possibilité de déclarer la guerre à d'autres joueurs via le classement.") },
        
        { TechResearchType.Espionnage, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.GeopoliticResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Guerre },
            name: "Espionnage",
            description: "Vous avez désormais la possibilité de créer et envoyer des espions chez d'autres joueurs.") },
        
        { TechResearchType.Echanges, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.GeopoliticResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Alliance, TechResearchType.Monnaie, TechResearchType.Routes },
            name: "Echanges",
            description: "Vous avez désormais la possibilité d'ouvrir des routes commerciales avec d'autres joueurs.") },
        
        { TechResearchType.Traite_De_Paix, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.GeopoliticResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Echanges, TechResearchType.Arme_Atomique },
            name: "Traité de paix",
            description: "En signant un traité de paix, vous vous engagez à n'utiliser la bombe atomique que par dissuasion. Cela vous permet de commencer à contribuer pour les financements mondiaux futurs.") },
        
        // Theorie
        { TechResearchType.Gribouillis,
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.TheoryResearch,
            preReqs: new List<TechResearchType> {},
            name:"Gribouillis",
            description:"Vous avez découvert comment faire des petits gribouillis ! Dans des cavernes... par exemple...") },
        
        { TechResearchType.Roue, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.TheoryResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Gribouillis },
            name:"Roue",
            description: "Vous avez découvert comment faire des roues, c'est super ! Pour rouler, mais nan jure tsé le mec qui s'embrouille dans une description") },
        
        { TechResearchType.Ecriture, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.TheoryResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Gribouillis },
            name:"Ecriture",
            description: "Vous avez découvert l'écriture ! Mieux que les gribouillis, vous pouvez maintenant écrire des mots ! La dingz") },
        
        { TechResearchType.Apprentissage, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.TheoryResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Ecriture },
            name:"Apprentissage",
            description: "Vous avez découvert comment apprendre aux autres, parfait ! Ca va peut-etre enfin arreter de jouer avec son caca chez vous.") },
        
        { TechResearchType.Ecoles, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.TheoryResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Apprentissage },
            name:"Ecoles",
            description: "Vous avez découvert que construire des écoles c'est pas mal, ça permet d'apprendre plus vite et mieux !") },
        
        { TechResearchType.Mathematiques, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.TheoryResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Ecoles },
            name:"Mathématiques",
            description:"Vous avez découvert les mathématiques, c'est super ! Je suis sur que ces calcules vont être de fou utiles.") },
        
        { TechResearchType.Monnaie, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.TheoryResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Mathematiques, TechResearchType.Or },
            name:"Monnaie",
            description: "Vous avez découvert la monnaie, Wouhou !") },
        
        { TechResearchType.Physiques, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.TheoryResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Mathematiques },
            name:"Physiques",
            description: "Vous avez découvert la physique, Ah ! Je savais bien que les calcules ca allaient servir à quelque chose !") },
        
        { TechResearchType.Explosion, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.TheoryResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Physiques },
            name:"Explosion",
            description: "Vous avez découvert comment faire des explosions, Boom ! Attention c'est dangereux comme même...") },
        
        { TechResearchType.Traitement_Des_Fluides, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.TheoryResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Physiques },
            name:"Traitement Des Fluides",
            description: "Vous avez découvert comment traiter les fluides, Nice !") },
        
        { TechResearchType.Forage, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.TheoryResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Explosion },
            name:"Forage",
            description: "Vous avez découvert comment forer, cool ! Ok c'est une bonne utilisation des explosions.") },
        
        { TechResearchType.Projet_Manhattan, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.TheoryResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Traitement_Des_Fluides, TechResearchType.Explosion },
            name:"Projet Manhattan",
            description: "Vous avez terminé le projet Manhattan, oh. Ca c'est pas bon signe...") },
        
        { TechResearchType.Financement_Mondiale_Future_I, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.TheoryResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Traite_De_Paix },
            name:"Financement Mondiale Future I",
            description: "Ensemble pour un monde meilleur. Vous avez commencé à contribuer pour les financements mondiaux futurs.") },
        
        { TechResearchType.Financement_Mondiale_Future_II, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.TheoryResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Financement_Mondiale_Future_I },
            name:"Financement Mondiale Future II",
            description: "Ensemble pour un monde meilleur. Vous avez continué à contribuer pour les financements mondiaux futurs.") },
        
        { TechResearchType.Financement_Mondiale_Future_III, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.TheoryResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Financement_Mondiale_Future_II },
            name:"Financement Mondiale Future III",
            description: "Ensemble pour un monde meilleur. Vous avez continué à contribuer pour les financements mondiaux futurs.") },
        
        { TechResearchType.Financement_Mondiale_Future_IV, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.TheoryResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Financement_Mondiale_Future_III },
            name:"Financement Mondiale Future IV",
            description: "Ensemble pour un monde meilleur. Vous avez finalisé votre contribution pour les financements mondiaux futurs.") },
        
        { TechResearchType.Skills_Sellers, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.TheoryResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Mode_Creatif },
            name:"Skills Sellers",
            description: "????" ) },
        
        // Ingenierie
        { TechResearchType.Pierre, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.EngineeringResearch,
            preReqs: new List<TechResearchType> {},
            name:"Pierre",
            description: "Vous avez découvert comment tailler la pierre !") },
        
        { TechResearchType.Rendement_Minier_I, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.EngineeringResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Pierre },
            name:"Rendement Minier I",
            description: "Cette amélioration permet d'augmenter la production de ressources minières.") },
        
        { TechResearchType.Rendement_Agricole_I, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.EngineeringResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Pierre },
            name: "Rendement Agricole I",
            description: "Cette amélioration permet d'augmenter la production de ressources agricoles.") },
        
        { TechResearchType.Fer, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.EngineeringResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Pierre },
            name:"Fer",
            description: "Vous avez découvert comment travailler le fer !") },
        
        { TechResearchType.Rendement_Minier_II, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.EngineeringResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Rendement_Minier_I, TechResearchType.Fer },
            name:"Rendement Minier II",
            description: "Cette amélioration permet d'augmenter la production de ressources minières.") },
        
        { TechResearchType.Rendement_Agricole_II, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.EngineeringResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Rendement_Agricole_I, TechResearchType.Fer },
            name:"Rendement Agricole II",
            description: "Cette amélioration permet d'augmenter la production de ressources agricoles.") },
        
        { TechResearchType.Routes, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.EngineeringResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Pierre, TechResearchType.Roue },
            name:"Routes",
            description: "Vous avez découvert comment construire des routes ! Vos unités se déplacent plus rapidement.") },
        
        { TechResearchType.Chemins_De_Fer, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.EngineeringResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Routes, TechResearchType.Fer },
            name:"Chemins De Fer",
            description: "Vous avez découvert comment construire des chemins de fer ! Vos unités se déplacent encore plus rapidement.") },
        
        { TechResearchType.Or, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.EngineeringResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Fer },
            name:"Or",
            description: "Vous avez découvert comment travailler l'or !") },
        
        { TechResearchType.Electricite, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.EngineeringResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Or },
            name:"Electricité",
            description: "Vous avez découvert comment produire de l'électricité ! Un confort s'installe dans vos villes.") },
        
        { TechResearchType.Diamant, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.EngineeringResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Or },
            name:"Diamant",
            description: "Vous avez découvert comment travailler le diamant !") },
        
        { TechResearchType.Rendement_Minier_III, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.EngineeringResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Rendement_Minier_II, TechResearchType.Electricite },
            name:"Rendement Minier III",
            description: "Cette amélioration permet d'augmenter la production de ressources minières.") },
        
        { TechResearchType.Rendement_Agricole_III, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.EngineeringResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Rendement_Agricole_II, TechResearchType.Electricite },
            name:"Rendement Agricole III",
            description: "Cette amélioration permet d'augmenter la production de ressources agricoles.") },
        
        { TechResearchType.Petrole, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.EngineeringResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Diamant },
            name:"Pétrole",
            description: "Vous avez découvert comment extraire le pétrole !") },
        
        { TechResearchType.Carburant, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.EngineeringResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Petrole },
            name:"Carburant",
            description: "Vous avez découvert comment produire du carburant ! Vos unités se déplacent encore plus rapidement.") },
        
        { TechResearchType.Fission_Nucleaire, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.EngineeringResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Carburant, TechResearchType.Electricite, TechResearchType.Projet_Manhattan },
            name:"Fission Nucléaire",
            description: "Vous avez découvert comment produire de l'énergie nucléaire !") },
        
        { TechResearchType.Fusion_Nucleaire, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.EngineeringResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Fission_Nucleaire },
            name:"Fusion Nucléaire",
            description: "Un exploit sans précédent, Vous voici à un pas d'obtenir une énergie infini...") },
        
        { TechResearchType.Tesseract_Energetique, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.EngineeringResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Fusion_Nucleaire },
            name:"Tesseract Energetique",
            description: "La source d'énergie la plus puissante de l'univers, vous avez découvert comment l'exploiter !") },
        
        { TechResearchType.Rendement_Scientifique_I, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.EngineeringResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Rendement_Agricole_III, TechResearchType.Rendement_Minier_III, TechResearchType.Tesseract_Energetique },
            name:"Rendement Scientifique I",
            description: "Cette amélioration permet d'augmenter la production de ressources scientifiques.") },
        
        { TechResearchType.Rendement_Scientifique_II, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.EngineeringResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Rendement_Scientifique_I },
            name:"Rendement Scientifique II",
            description: "Cette amélioration permet d'augmenter la production de ressources scientifiques.") },
        
        { TechResearchType.Rendement_Scientifique_III, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.EngineeringResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Rendement_Scientifique_II },
            name:"Rendement Scientifique III",
            description: "Cette amélioration permet d'augmenter la production de ressources scientifiques.") },
        
        { TechResearchType.Rendement_Scientifique_IV, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.EngineeringResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Rendement_Scientifique_III },
            name:"Rendement Scientifique IV",
            description: "Cette amélioration permet d'augmenter la production de ressources scientifiques.") },
        
        { TechResearchType.Technologies_Futures, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.EngineeringResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Financement_Mondiale_Future_IV },
            name:"Technologies Futures",
            description: "Vous avez découvert des technologies du futur, c'est la folie ! Mais ... est-il possible d'aller plus loin ?") },
        
        { TechResearchType.Mode_Creatif, 
            (cost: 0, researchTime: TimeSpan.FromDays(1), category: TechResearchCategory.EngineeringResearch,
            preReqs: new List<TechResearchType> { TechResearchType.Technologies_Futures },
            name:"Mode Créatif",
            description: "Cette energie infinie, cette puissance, cette liberté... Plus aucune restrictions, vous êtes un dieu, capable de tout créer, tout générez, tout détruire...") },
    };
}