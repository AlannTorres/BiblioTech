using BiblioTech.Domain.Common.Interfaces;

namespace BiblioTech.Domain.Common.Repositories;

public class Generators : IGenerators
{
    public string Generate() => Guid.NewGuid().ToString();
}
