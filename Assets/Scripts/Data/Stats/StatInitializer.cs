using System.ComponentModel;
using System.Threading.Tasks;
using UnityEngine;

public class StatInitializer : MonoBehaviour
{
    #region Buildings

    [Header("Buildings")]

    [SerializeField] private BuildingStatContainer _barracksStatContainer;
    [SerializeField] private BuildingStatContainer _churchStatContainer;
    [SerializeField] private BuildingStatContainer _gardenStatContainer;
    [SerializeField] private BuildingStatContainer _smelteryStatContainer;
    [SerializeField] private BuildingStatContainer _storageStatContainer;
    [SerializeField] private BuildingStatContainer _townHallStatContainer;
    [SerializeField] private BuildingStatContainer _watchTowerStatContainer;
    [SerializeField] private BuildingStatContainer _windmillStatContainer;
    [SerializeField] private BuildingStatContainer _workerHouseStatContainer;
    [SerializeField] private BuildingStatContainer _workshopStatContainer;

    #endregion

    #region Units

    [Header("Units")]

    [SerializeField] private UnitStatContainer _archerStatContainer;
    [SerializeField] private UnitStatContainer _archerTowerStatContainer;
    [SerializeField] private UnitStatContainer _builderStatContainer;
    [SerializeField] private UnitStatContainer _catapultStatContainer;
    [SerializeField] private UnitStatContainer _healerStatContainer;
    [SerializeField] private UnitStatContainer _heavyWarriorStatContainer;
    [SerializeField] private UnitStatContainer _spearmanStatContainer;

    #endregion
    public async void Awake()
    {
        CreateBuildingStats();
        CreateUnitStats();

        await Task.Delay(50);

        LoadBuildingStats();
        LoadUnitStats();
    }

    private void CreateBuildingStats()
    {
        BuildingStats barrackStats = new() { Durability = 100, BuildResources = new() {ResourceType.Stone, ResourceType.Metal } ,BuildCost = new() {2,2} , Job = "Trains units: Archers, Warriors, Heavy warriors" };
        DataParser.WriteJsonData("BarrackStats.json", barrackStats);

        BuildingStats workerHouseStats = new() { Durability = 100,BuildResources = new() {ResourceType.Wood, ResourceType.Stone} , BuildCost = new() { 2,2 }, Job = "Trains units: Worker" };
        DataParser.WriteJsonData("WorkerHouseStats.json", workerHouseStats);

        BuildingStats workshopStats = new() { Durability = 100, BuildResources = new() {ResourceType.Metal, ResourceType.Wood },BuildCost = new() { 2,2 }, Job = "Trains units: Catapult, Archer tower" };
        DataParser.WriteJsonData("WorkshopStats.json", workshopStats);

        BuildingStats churchStats = new() { Durability = 100,BuildResources = new() {ResourceType.Stone } ,BuildCost = new() { 2 }, Job = "Trains units: Healer" };
        DataParser.WriteJsonData("ChurchStats.json", churchStats);
        
        BuildingStats gardenStats = new() { Durability = 100,BuildResources = new() {ResourceType.Wood } ,BuildCost = new() { 2 }, Job = "Produces: Crops" };
        DataParser.WriteJsonData("GardenStats.json", gardenStats);

        BuildingStats windmillStats = new() { Durability = 100,BuildResources = new() {ResourceType.Wood } ,BuildCost = new() { 2 }, Job = "Produces: Food" };
        DataParser.WriteJsonData("WindmillStats.json", windmillStats);

        BuildingStats storageStats = new() { Durability = 100,  BuildResources = new() {ResourceType.Stone },BuildCost = new() { 2 }, Job = "Increases maximum storage" };
        DataParser.WriteJsonData("StorageStats.json", storageStats);

        BuildingStats smelteryStats = new() { Durability = 100, BuildResources = new() {ResourceType.Stone },BuildCost = new() { 2 }, Job = "Produces: Metal" };
        DataParser.WriteJsonData("SmelteryStats.json", smelteryStats);

        WatchTowerStats watchTowerStats = new() { Durability = 100,BuildResources = new() {ResourceType.Wood } ,BuildCost = new() { 5 }, DetectionRadius = 10, AttackDelay = 2, BuildZone = 15, Capacity = 5, MinRange = 0, MaxRange = 8, DamagePerArcher = 5 };
        DataParser.WriteJsonData("WatchTowerStats.json", watchTowerStats);

        BuildZoneBuildingStats townHallStats = new() { Durability = 150,BuildResources = new() {ResourceType.Wood} ,BuildCost = new() { 50 }, BuildZone = 15};
        DataParser.WriteJsonData("TownHallStats.json", townHallStats);

    }
    private void CreateUnitStats()
    {
        AttackingUnitStats archerStats = new() { MinAttackRadius = 0, MaxAttackRadius = 10, AttackDelay = 1, Damage = 4, DetectionRadius = 1, Health = 20, Speed = 8, Name = "Archer",ResourceAmountsNeeded = new() { 3 }, ResourceTypesNeeded = new() { ResourceType.Wood } };
        DataParser.WriteXMLData("ArcherStats.xml",archerStats);

        AttackingUnitStats catapultStats = new() { MinAttackRadius = 0, MaxAttackRadius = 10, AttackDelay = 2, Damage = 10, DetectionRadius = 1, Health = 40, Name = "Catapult",ResourceTypesNeeded = new() {ResourceType.Wood, ResourceType.Stone } ,ResourceAmountsNeeded = new() {3,3}, Speed = 4  };
        DataParser.WriteXMLData("CatapultStats.xml", catapultStats);
        
        AttackingUnitStats heavyWarriorStats = new() { MinAttackRadius = 0, MaxAttackRadius = 10, AttackDelay = 1, Damage = 6, DetectionRadius = 1, Health = 30, Name = "Heavy Warrior", Speed = 12,ResourceTypesNeeded = new() {ResourceType.Stone, ResourceType.Metal } , ResourceAmountsNeeded = new() {2,2 } };
        DataParser.WriteXMLData("HeavyWarriorStats.xml", heavyWarriorStats);
 
        AttackingUnitStats spearmanStats = new() { MinAttackRadius = 0, MaxAttackRadius = 10, AttackDelay = 1, Damage = 5, DetectionRadius = 1, Health = 25, Name = "Spearman", Speed = 13,ResourceTypesNeeded = new() {ResourceType.Stone }, ResourceAmountsNeeded = new() {1} };
        DataParser.WriteXMLData("SpearmanStats.xml", spearmanStats);

        BuilderStats builderStats = new() {DetectionRadius = 1, Health = 20, MineSpeed = 1, RepairSpeed = 2, RepairAmount = 5, Name = "Worker", Speed = 12, ResourceTypesNeeded = new() {ResourceType.Wood }, ResourceAmountsNeeded = new() {1}};
        DataParser.WriteXMLData("BuilderStats.xml", builderStats);

        HealerStats healerStats = new() { DetectionRadius = 1, Health = 20, HealDelay = 1, HealAmount = 2,  MinHealDistance = 0, MaxHealDistance = 5, Name = "Worker", Speed = 12,ResourceTypesNeeded = new() {ResourceType.Stone }, ResourceAmountsNeeded = new(){2} };
        DataParser.WriteXMLData("HealerStats.xml", healerStats);

        ArcherTowerStats archerTowerStats = new() { MinAttackRadius = 2, MaxAttackRadius = 8, AttackDelay = 2, DamagePerArcher = 5, Capacity = 10, Damage = 0, DetectionRadius = 1, Health = 30, Name = "Archer Tower", Speed = 6, ResourceTypesNeeded = new() {ResourceType.Metal},ResourceAmountsNeeded = new() {2 }};
        DataParser.WriteXMLData("ArcherTowerStats.xml", archerTowerStats);
    }

    private void LoadBuildingStats()
    {
        _barracksStatContainer.Stats = DataParser.ParseJsonData<BuildingStats>("BarrackStats.json");
        _workerHouseStatContainer.Stats = DataParser.ParseJsonData<BuildingStats>("WorkerHouseStats.json");
        _workshopStatContainer.Stats = DataParser.ParseJsonData<BuildingStats>("WorkshopStats.json");
        _churchStatContainer.Stats = DataParser.ParseJsonData<BuildingStats>("ChurchStats.json");
        _gardenStatContainer.Stats = DataParser.ParseJsonData<BuildingStats>("GardenStats.json");
        _windmillStatContainer.Stats = DataParser.ParseJsonData<BuildingStats>("WindmillStats.json");
        _storageStatContainer.Stats = DataParser.ParseJsonData<BuildingStats>("StorageStats.json");
        _smelteryStatContainer.Stats = DataParser.ParseJsonData<BuildingStats>("SmelteryStats.json");
        _watchTowerStatContainer.Stats = DataParser.ParseJsonData<WatchTowerStats>("WatchTowerStats.json");
        _townHallStatContainer.Stats = DataParser.ParseJsonData<BuildZoneBuildingStats>("TownHallStats.json");
    }

    private void LoadUnitStats()
    {
        _archerStatContainer.Stats = DataParser.ParseXMLData<AttackingUnitStats>("ArcherStats.xml");
        _catapultStatContainer.Stats = DataParser.ParseXMLData<AttackingUnitStats>("CatapultStats.xml");
        _heavyWarriorStatContainer.Stats = DataParser.ParseXMLData<AttackingUnitStats>("HeavyWarriorStats.xml");
        _spearmanStatContainer.Stats = DataParser.ParseXMLData<AttackingUnitStats>("SpearmanStats.xml");
        _builderStatContainer.Stats = DataParser.ParseXMLData<BuilderStats>("BuilderStats.xml");
        _healerStatContainer.Stats = DataParser.ParseXMLData<HealerStats>("HealerStats.xml");
        _archerTowerStatContainer.Stats = DataParser.ParseXMLData<ArcherTowerStats>("ArcherTowerStats.xml");
    }
}
