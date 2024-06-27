using OpenModular.Module.UAP.DataSeeding;
using OpenModular.Persistence.DataSeeding.Builder;

var builder = new DataSeedingBuilder(Path.Combine(Directory.GetCurrentDirectory(), "../ApiHost"));

builder.Register<UAPDataSeedingDefinition>();

builder.Build();
