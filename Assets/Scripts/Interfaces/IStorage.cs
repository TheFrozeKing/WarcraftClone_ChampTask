using System.Collections.Generic;

public interface IStorage
{
    public void Store(Dictionary<ResourceType, int> resources);
}